using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using YoYoProject.Models;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMProjectParentModel : ModelBase
    {
        [DataMember]
        public string projectPath { get; set; }

        [DataMember]
        public List<Guid> hiddenResources { get; set; }

        [DataMember]
        public SortedDictionary<Guid, GMResourceInfoModel> alteredResources { get; set; }

        public GMProjectParentModel()
            : base("GMProjectParent", "1.0")
        {

        }
    }
}