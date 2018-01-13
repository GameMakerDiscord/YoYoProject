using System.Runtime.Serialization;

namespace YoYoProject
{
    [DataContract]
    internal class GMOptionsBaseModel : GMResourceModel
    {
        public GMOptionsBaseModel()
            : base("GMOptionsBase", "1.0")
        {

        }

        public GMOptionsBaseModel(string modelName, string mvc)
            : base(modelName, mvc)
        {

        }
    }
}