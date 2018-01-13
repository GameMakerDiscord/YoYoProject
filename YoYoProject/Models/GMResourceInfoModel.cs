using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YoYoProject
{
    // NOTE This class does not inherit ModelBase because of limitations of Microsoft's
    //      DataContractSerializer not allowing us to hide inherited properties.
    //      That, mixed with GMS2 not loading projects if they specify those propertys
    //      in this object, forces us to break best practice.
    [DataContract]
    internal sealed class GMResourceInfoModel
    {
        [DataMember]
        public Guid id { get; set; }

        [DataMember]
        public string resourcePath { get; set; }

        [DataMember]
        public string resourceType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<string> resourceCreationConfigs { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<string> configDeltas { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<string> configDeltaFiles { get; set; }
    }
}