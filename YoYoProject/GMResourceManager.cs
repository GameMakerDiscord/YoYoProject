using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using YoYoProject.Controllers;
using YoYoProject.Models;
using YoYoProject.Utility;

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
                Id = Guid.NewGuid()
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
            return (TResource)resources.Values.Single(x => x is TResource);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<TResource> GetAllOfType<TResource>()
            where TResource : GMResource, new()
        {
            return resources.Values.OfType<TResource>().ToList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<GMResource> GetAllOfType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return resources.Values.Where(x => x.GetType() == type).ToList();
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
            return resources[id];
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

            return resources.Values.SingleOrDefault(x => x.Name == name);
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

            foreach (var kvp in projectResources)
            {
                var id = kvp.Key;
                var info = kvp.Value;

                Tuple<Type, Type> modelType;
                if (!ModelTypes.TryGetValue(info.resourceType, out modelType)) // TODO Throw exception
                    continue;

                // TODO Optimize
                var resource = (GMResource)Activator.CreateInstance(modelType.Item1);
                resource.Project = project;
                resource.ResourceInfoId = info.id;
                resource.Id = id;

                var fullPath = Path.Combine(project.RootDirectory, info.resourcePath);
                var model = (ModelBase)Json.Deserialize(modelType.Item2, fullPath);
                resource.Deserialize(model);

                resources.Add(resource.Id, resource);
            }
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
            ["GMSprite"] = Tuple.Create(typeof(GMSprite), typeof(GMSpriteModel))
        };
    }
}