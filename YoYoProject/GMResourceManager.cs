using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using YoYoProject.Controllers;

namespace YoYoProject
{
    public sealed class GMResourceManager : IReadOnlyDictionary<Guid, GMResource>
    {
        public int Count => resources.Count;

        public IEnumerable<Guid> Keys => resources.Keys;

        public IEnumerable<GMResource> Values => resources.Values;

        public GMResource this[Guid key] => Get(key);

        private readonly SortedDictionary<Guid, GMResource> resources;

        public GMResourceManager()
        {
            resources = new SortedDictionary<Guid, GMResource>();
        }

        public TResource Create<TResource>()
            where TResource : GMResource, new()
        {
            var resource = new TResource
            {
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

        public bool ContainsKey(Guid key)
        {
            return resources.ContainsKey(key);
        }

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
        
        public IEnumerator<KeyValuePair<Guid, GMResource>> GetEnumerator()
        {
            return resources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}