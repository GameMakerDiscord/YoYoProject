using System.Runtime.Serialization;
using YoYoProject.Models;

namespace YoYoProject
{
    [DataContract]
    internal class GMResourceModel : ModelBase
    {
        [DataMember]
        public string name { get; set; }

        public GMResourceModel()
            : base("GMResource", "1.0")
        {

        }

        public GMResourceModel(string modelName, string mvc)
            : base(modelName, mvc)
        {
            
        }
    }
}