using System;
using System.Runtime.Serialization;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMTextureGroupModel : GMBaseGroupModel
    {
        [DataMember]
        public bool scaled { get; set; }

        [DataMember]
        public bool autocrop { get; set; }

        [DataMember]
        public int border { get; set; }

        [DataMember]
        public Guid groupParent { get; set; }

        [DataMember]
        public int mipsToGenerate { get; set; }

        public GMTextureGroupModel()
            : base("GMTextureGroup", "1.0")
        {
            
        }
    }
}