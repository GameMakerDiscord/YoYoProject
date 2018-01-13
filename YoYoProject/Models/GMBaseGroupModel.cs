using System.Runtime.Serialization;
using YoYoProject.Common;
using YoYoProject.Models;

namespace YoYoProject
{
    [DataContract]
    internal class GMBaseGroupModel : ModelBase
    {
        [DataMember]
        public string groupName { get; set; }

        [DataMember]
        public TargetPlatforms targets { get; set; }

        public GMBaseGroupModel()
            : base("GMBaseGroup", "1.0")
        {

        }

        public GMBaseGroupModel(string modelName, string mvc)
            : base(modelName, mvc)
        {

        }
    }
}