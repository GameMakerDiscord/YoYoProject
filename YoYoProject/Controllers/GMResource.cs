using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YoYoProject.Controllers
{
    public abstract class GMResource : ControllerBase
    {
        public string Name { get; set; }

        public bool Dirty { get; private set; }

        protected internal abstract string ResourcePath { get; }
        
        internal GMProject Project { get; set; }

        internal Guid ResourceInfoId { get; set; }

        protected GMResource()
        {
            Dirty = false;
        }

        // ReSharper disable AssignNullToNotNullAttribute
        protected T GetProperty<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (Project == null || Project.Configs.Active.IsDefault)
                return value;
            
            for (var config = Project.Configs.Active; config != null; config = config.Parent)
            {
                Dictionary<string, object> deltas;
                if (!config.Deltas.TryGetValue(Id, out deltas))
                    continue;

                object deltaValue;
                if (deltas.TryGetValue(propertyName, out deltaValue))
                    return (T)deltaValue;
            }

            return value;
        }

        protected void SetProperty<T>(T value, ref T storage, [CallerMemberName] string propertyName = null)
        {
            if (Project == null || Project.Configs.Active.IsDefault)
            {
                storage = value;
                return;
            }

            var config = Project.Configs.Active;

            Dictionary<string, object> deltas;
            if (!config.Deltas.TryGetValue(Id, out deltas))
            {
                deltas = new Dictionary<string, object>();
                config.Deltas.Add(Id, deltas);
            }

            deltas[propertyName] = value;

            Dirty = true;
        }
        // ReSharper restore AssignNullToNotNullAttribute
    }
}