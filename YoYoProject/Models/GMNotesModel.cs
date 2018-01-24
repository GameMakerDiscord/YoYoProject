using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMNotesModel : GMResourceModel
    {
        public GMNotesModel()
            : base("GMNotes", "1.0")
        {
            
        }
    }
}
