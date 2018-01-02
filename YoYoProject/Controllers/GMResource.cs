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
        
        internal IReadOnlyList<ConfigDelta> Configs => new List<ConfigDelta>(configDeltas.Values);

        private readonly Dictionary<string, ConfigDelta> configDeltas;
        private readonly Stack<ConfigDelta> configStack;

        protected GMResource()
        {
            Dirty = false;

            configDeltas = new Dictionary<string, ConfigDelta>();
            configStack = new Stack<ConfigDelta>();
        }

        protected T GetProperty<T>(T value, [CallerMemberName] string propertyName = null)
        {
            foreach (var config in configStack)
            {
                object deltaValue;
                if (config.TryGetProperty(propertyName, out deltaValue))
                    return (T)deltaValue;
            }

            return value;
        }

        protected void SetProperty<T>(T value, out T storage, [CallerMemberName] string propertyName = null)
        {
            if (configStack.Count > 0)
            {
                var config = configStack.Peek();
                config.SetProperty(propertyName, value);
            }

            storage = value;
        }

        internal void PushConfig(ConfigTree.Node config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            string name = config.Name;

            ConfigDelta configDelta;
            if (!configDeltas.TryGetValue(name, out configDelta))
            {
                configDelta = new ConfigDelta(name, this);
                configDeltas.Add(name, configDelta);
            }

            configStack.Push(configDelta);
        }

        internal void PopConfig()
        {
            configStack.Pop();
        }
        
        protected internal GMResourceInfoModel SerializeResourceInfo()
        {
            return new GMResourceInfoModel
            {
                ResourcePath = ResourcePath,
                ResourceType = GetType().Name,

                // TODO Implement
                ResourceCreationConfigs = null,
                ConfigDeltas = configDeltas.Count == 0 ? null : configDeltas.Values.Select(x => x.Name).ToList(),
                ConfigDeltaFiles = null
            };
        }
    }
}