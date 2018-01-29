using System;
using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal abstract class ModelBase
    {
        [DataMember]
        public Guid id { get; set; }
        
        [DataMember]
        public string mvc { get; private set; }
        
        [DataMember]
        public string modelName { get; private set; }

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
