using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using YoYoProject.Controllers;
using YoYoProject.Models;

namespace YoYoProject
{
    [DebuggerStepThrough]
    public sealed class GMResourceManager : IReadOnlyDictionary<Guid, GMResource>, IEnumerable<GMResource>
    {
        public int Count => resources.Count;

        public IEnumerable<Guid> Keys => resources.Keys;

        public IEnumerable<GMResource> Values => resources.Values;

        public GMResource this[Guid key] => Get(key);

        private readonly SortedDictionary<Guid, GMResource> resources;
        private readonly GMProject project;

        public GMResourceManager(GMProject project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));
            
            resources = new SortedDictionary<Guid, GMResource>();
            this.project = project;
        }

        public TResource Create<TResource>(string name = null)
            where TResource : GMResource, new()
        {
            var resource = new TResource
            {
                Project = project,
                ResourceInfoId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                Dirty = true
            };

            resource.Create(name);
            resources.Add(resource.Id, resource);

            return resource;
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResource Get<TResource>()
            where TResource : GMResource, new()
        {
            var result = (TResource)resources.Values.SingleOrDefault(x => x is TResource);
            if (result != null)
                return result;
            
            return project.Parent.Reference?.Resources.Get<TResource>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<TResource> GetAllOfType<TResource>()
            where TResource : GMResource, new()
        {
            var result = resources.Values.OfType<TResource>().ToList();
            if (project.Parent.Reference != null)
                result.AddRange(project.Parent.Reference.Resources.GetAllOfType<TResource>());

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<GMResource> GetAllOfType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            
            var result = resources.Values.Where(x => x.GetType() == type).ToList();
            if (project.Parent.Reference != null)
                result.AddRange(project.Parent.Reference.Resources.GetAllOfType(type));

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResource Get<TResource>(Guid id)
            where TResource : GMResource, new()
        {
            return (TResource)Get(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GMResource Get(Guid id)
        {
            GMResource resource;
            if (resources.TryGetValue(id, out resource))
                return resource;

            return project.Parent.Reference?.Resources.Get(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResource Get<TResource>(string name)
            where TResource : GMResource, new()
        {
            return (TResource)Get(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GMResource Get(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            GMResource result = resources.Values.SingleOrDefault(x => x.Name == name);
            if (result != null)
                return result;

            return project.Parent.Reference?.Resources.Get(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(Guid key)
        {
            return resources.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(Guid key, out GMResource value)
        {
            return resources.TryGetValue(key, out value);
        }

        public string GenerateValidName(string prefix)
        {
            if (prefix == null)
                prefix = "resource";

            string name = prefix;
            for (int i = 1; Get(name) != null; ++i)
                name = prefix + i.ToString("G");
            
            return name;
        }

        internal SortedDictionary<Guid, GMResourceInfoModel> Serialize()
        {
            var resourceInfos = new SortedDictionary<Guid, GMResourceInfoModel>();
            foreach (var resource in resources.Values)
            {
                var configDeltas = project.Configs.GetForResource(resource.Id);
                resourceInfos.Add(resource.Id, new GMResourceInfoModel
                {
                    id = resource.ResourceInfoId,
                    resourcePath = resource.ResourcePath,
                    resourceType = resource.GetType().Name,

                    resourceCreationConfigs = null,
                    configDeltas = configDeltas.Count == 0 ? null : configDeltas.Select(x => x.Name).ToList(),
                    configDeltaFiles = null
                });
            }

            return resourceInfos;
        }

        internal void Deserialize(SortedDictionary<Guid, GMResourceInfoModel> projectResources)
        {
            if (projectResources == null)
                throw new ArgumentNullException(nameof(projectResources));

            // TODO Please find a more memory effecient way to do this
            var models = new List<ModelBase>(projectResources.Count);

            foreach (var kvp in projectResources)
            {
                var id = kvp.Key;
                var info = kvp.Value;

                Tuple<Type, Type> modelType;
                if (!ModelTypes.TryGetValue(info.resourceType, out modelType))
                    throw new InvalidOperationException($"Cannot deserialize unknown resource model '{info.resourceType}'");

                // TODO Optimize
                var resource = (GMResource)Activator.CreateInstance(modelType.Item1);
                resource.Project = project;
                resource.ResourceInfoId = info.id;
                resource.Id = id;

                var fullPath = Path.Combine(project.RootDirectory, info.resourcePath);
                var model = (ModelBase)Utility.Json.Deserialize(modelType.Item2, fullPath);
                resource.Deserialize(model);

                resources.Add(resource.Id, resource);
                models.Add(model);
            }

            foreach (var model in models)
                resources[model.id].FinalizeDeserialization(model);
        }

        public IEnumerator<GMResource> GetEnumerator()
        {
            return resources.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // NOTE Hidden GetEnumerator() for IReadOnlyDictionary because it's less useful than one for
        //      IEnumerable<GMResource>
        IEnumerator<KeyValuePair<Guid, GMResource>> IEnumerable<KeyValuePair<Guid, GMResource>>.GetEnumerator()
        {
            return resources.GetEnumerator();
        }

        private readonly static Dictionary<string, Tuple<Type, Type>> ModelTypes = new Dictionary<string, Tuple<Type, Type>>
        {
            ["GMFolder"] = Tuple.Create(typeof(GMFolder), typeof(GMFolderModel)),
            ["GMSprite"] = Tuple.Create(typeof(GMSprite), typeof(GMSpriteModel)),
            ["GMTileSet"] = Tuple.Create(typeof(GMTileSet), typeof(GMTileSetModel)),
            ["GMSound"] = Tuple.Create(typeof(GMSound), typeof(GMSoundModel)),
            ["GMPath"] = Tuple.Create(typeof(GMPath), typeof(GMPathModel)),
            ["GMScript"] = Tuple.Create(typeof(GMScript), typeof(GMScriptModel)),
            ["GMShader"] = Tuple.Create(typeof(GMShader), typeof(GMShaderModel)),
            ["GMFont"] = Tuple.Create(typeof(GMFont), typeof(GMFontModel)),
            ["GMTimeline"] = Tuple.Create(typeof(GMTimeline), typeof(GMTimelineModel)),
            ["GMObject"] = Tuple.Create(typeof(GMObject), typeof(GMObjectModel)),
            ["GMRoom"] = Tuple.Create(typeof(GMRoom), typeof(GMRoomModel)),
            ["GMNotes"] = Tuple.Create(typeof(GMNotes), typeof(GMNotesModel)),
            ["GMIncludedFile"] = Tuple.Create(typeof(GMIncludedFile), typeof(GMIncludedFileModel)),
            ["GMExtension"] = Tuple.Create(typeof(GMExtension), typeof(GMExtensionModel)),
            ["GMMainOptions"] = Tuple.Create(typeof(GMMainOptions), typeof(GMMainOptionsModel)),
            ["GMWindowsOptions"] = Tuple.Create(typeof(GMWindowsOptions), typeof(GMWindowsOptionsModel)),
            ["GMMacOptions"] = Tuple.Create(typeof(GMMacOptions), typeof(GMMacOptionsModel)),
            ["GMLinuxOptions"] = Tuple.Create(typeof(GMLinuxOptions), typeof(GMLinuxOptionsModel))
        };
    }
}