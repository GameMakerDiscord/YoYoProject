using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using YoYoProject.Models;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMProjectModel : ModelBase
    {
        [DataMember]
        public GMProjectParentModel parentProject { get; set; }

        [DataMember]
        public SortedDictionary<Guid, GMResourceInfoModel> resources { get; set; }

        [DataMember]
        public List<string> configs { get; set; }

        [DataMember]
        public List<Guid> script_order { get; set; }

        [DataMember]
        public bool IsDnDProject { get; set; }

        [DataMember]
        public bool option_ecma { get; set; }

        [DataMember]
        public string tutorial { get; set; }

        public GMProjectModel()
            : base("GMProject", "1.0")
        {
            
        }
    }
}
