using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMFolderModel : GMResourceModel
    {
        [DataMember] public string folderName;

        [DataMember] public string localisedFolderName;

        [DataMember] public string filterType;

        [DataMember] public bool isDefaultView;

        [DataMember] public List<Guid> children;

        public GMFolderModel()
            : base("GMFolder", "1.1")
        {

        }
    }
}