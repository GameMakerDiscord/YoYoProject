using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMGraphicsOptionsModel : GMOptionsBaseModel
    {
        [DataMember]
        public List<GMTextureGroupModel> textureGroups { get; set; }

        public GMGraphicsOptionsModel()
            : base("GMGraphicsOptions", "1.0")
        {

        }
    }
}