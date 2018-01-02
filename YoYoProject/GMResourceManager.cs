using System;
using System.Collections;
using System.Collections.Generic;

namespace YoYoProject
{
    // TODO IReadOnlyDictionary?
    public sealed class GMResourceManager : IEnumerable<GMResource>
    {
        public int Count
        {
            get { return resources.Count; }
        }

        public ControllerBase this[Guid key]
        {
            get { return resources[key]; }
        }

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

        public SortedDictionary<Guid, GMResourceInfoModel> SerializeResourceInfo()
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
    }
}