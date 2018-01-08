using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using YoYoProject.Controllers;

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

        public TResource Create<TResource>()
            where TResource : GMResource, new()
        {
            var resource = new TResource
            {
                Project = project,
                Id = Guid.NewGuid()
            };

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

            return resources.Values.Single(x => x.Name == name);
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

        internal SortedDictionary<Guid, GMResourceInfoModel> SerializeResourceInfo()
        {
            var resourceInfos = new SortedDictionary<Guid, GMResourceInfoModel>();
            foreach (var kvp in resources)
            {
                resourceInfos.Add(kvp.Key, kvp.Value.SerializeResourceInfo());
            }

            return resourceInfos;
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
    }
}