using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal class GMPointModel : ModelBase
    {
        [DataMember]
        public float x { get; set; }

        [DataMember]
        public float y { get; set; }

        public GMPointModel()
            : base("GMPoint", "1.0")
        {
            
        }
    }
}