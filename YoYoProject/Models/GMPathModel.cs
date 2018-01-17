using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMPathModel : GMResourceModel
    {
        [DataMember]
        public GMPathKind kind { get; set; }

        [DataMember]
        public bool closed { get; set; }

        [DataMember]
        public int precision { get; set; }

        [DataMember]
        public int hsnap { get; set; }

        [DataMember]
        public int vsnap { get; set; }

        [DataMember]
        public List<GMPathPointModel> points { get; set; }
        
        public GMPathModel()
            : base("GMPath", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMPathPointModel : ModelBase
    {
        [DataMember]
        public float speed { get; set; }

        [DataMember]
        public float x { get; set; }

        [DataMember]
        public float y { get; set; }

        public GMPathPointModel()
            : base("GMPathPoint", "1.0")
        {
            
        }
    }

    public enum GMPathKind
    {
        StraightLines,
        SmoothCurves
    }
}
