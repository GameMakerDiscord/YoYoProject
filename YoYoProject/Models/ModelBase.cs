using System;
using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal abstract class ModelBase
    {
        // NOTE We need to emulate "EmitTypeInformation" in the context of the IDE, so we're
        //      manually emitting our own type info when needed. This property must be first
        //      in order to interop with the IDE correctly
        [DataMember(EmitDefaultValue = false)]
        private string __type { get; set; }

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

        protected ModelBase(string modelName, string mvc, string mvcType)
            : this(modelName, mvc)
        {
            __type = mvcType;
        }
    }
}
