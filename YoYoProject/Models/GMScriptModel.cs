using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMScriptModel : GMResourceModel
    {
        [DataMember]
        public bool IsCompatibility { get; set; }

        [DataMember]
        public bool IsDnD { get; set; }

        public GMScriptModel()
            : base("GMScript", "1.0")
        {
            
        }
    }
}
