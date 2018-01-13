using System;
using System.Runtime.Serialization;
using YoYoProject.Models;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMRViewModel : ModelBase
    {
        [DataMember]
        public bool inherit { get; set; }

        [DataMember]
        public bool visible { get; set; }

        [DataMember]
        public int xview { get; set; }

        [DataMember]
        public int yview { get; set; }

        [DataMember]
        public int wview { get; set; }

        [DataMember]
        public int hview { get; set; }

        [DataMember]
        public int xport { get; set; }

        [DataMember]
        public int yport { get; set; }

        [DataMember]
        public int wport { get; set; }

        [DataMember]
        public int hport { get; set; }

        [DataMember]
        public int hborder { get; set; }

        [DataMember]
        public int vborder { get; set; }

        [DataMember]
        public int hspeed { get; set; }

        [DataMember]
        public int vspeed { get; set; }

        [DataMember]
        public Guid objId { get; set; }

        public GMRViewModel()
            : base("GMRView", "1.0")
        {

        }
    }
}