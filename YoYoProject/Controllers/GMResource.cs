using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace YoYoProject.Controllers
{
    public abstract class GMResource : ControllerBase
    {
        public string Name { get; set; }

        public bool Dirty { get; private set; }

        protected internal abstract string ResourcePath { get; }
        
        internal GMProject Project { get; set; }

        protected GMResource()
        {
            Dirty = false;
        }

        protected T GetProperty<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (Project == null)
                return value;

            return Project.Configs.GetProperty(Id, propertyName, value);
        }

        protected void SetProperty<T>(T value, ref T storage, [CallerMemberName] string propertyName = null)
        {
            if (Project == null)
            {
                storage = value;
                return;
            }

            if (!Project.Configs.SetProperty(Id, propertyName, value))
                storage = value;

            Dirty = true;
        }
        
        internal GMResourceInfoModel SerializeResourceInfo()
        {
            var configDeltas = Project.Configs.GetConfigDeltasForResource(Id);
            return new GMResourceInfoModel
            {
                id = Guid.NewGuid(), // TODO Keep this persistent!
                resourcePath = ResourcePath,
                resourceType = GetType().Name,

                // TODO Implement
                resourceCreationConfigs = null,
                configDeltas = configDeltas.Count == 0 ? null : configDeltas.Select(x => x.Name).ToList(),
                configDeltaFiles = null
            };
        }
    }
}