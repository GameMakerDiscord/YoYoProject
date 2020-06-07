using System.Runtime.Serialization;

namespace YoYoProject
{
    [DataContract]
    internal class GMOptionsBaseModel : GMResourceModel
    {
        public enum Scale : int
        {
            KeepAspectRatio,
            FullScale
        }

        public enum SaveLocation : int
        {
            LocalAppData,
            AppData
        }

        public enum WebGLSetting : int
        {
            Disabled,
            Required,
            AutoDetect
        }

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