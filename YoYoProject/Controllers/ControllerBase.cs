using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YoYoProject.Models;
using YoYoProject.Utility;

namespace YoYoProject.Controllers
{
    public abstract class ControllerBase
    {
        public Guid Id { get; set; }

        public bool Dirty { get; internal set; } // TODO Icky, still want this to be protected

        internal GMProject Project { get; set; }

        protected ControllerBase()
        {
            Dirty = false;
        }

        // TODO Really, Serialize needs to be able to take a ConfigTree.Node and return what the model
        //      would look like at that config without any side-effects
        internal abstract ModelBase Serialize();

        internal virtual void Deserialize(ModelBase model)
        {
            // TODO Eventually become abstract
            throw new NotImplementedException();
        }

        internal virtual void FinalizeDeserialization(ModelBase model)
        {
            // NOTE Nothing to do normally
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

        public string ToJson()
        {
            return Json.Serialize(Serialize());
        }
    }
}
