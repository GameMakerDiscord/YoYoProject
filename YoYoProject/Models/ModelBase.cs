using System;
using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    // TODO Make this internal
    [DataContract]
    public abstract class ModelBase
    {
        [DataMember]
        public Guid id { get; set; }

        // TODO Implement MvcVersion class
        [DataMember]
        public string mvc { get; set; }

        // TODO Resolve from type name
        [DataMember]
        public string modelName { get; set; }

        protected ModelBase()
        {
            // NOTE Empty ctor for deserialization
        }

        protected ModelBase(string modelName, string mvc)
        {
            if (modelName == null)
                throw new ArgumentNullException(nameof(modelName));
            
            if (mvc == null)
                throw new ArgumentNullException(nameof(mvc));

            this.id = Guid.NewGuid();
            this.mvc = mvc;
            this.modelName = modelName;
        }
    }
}
