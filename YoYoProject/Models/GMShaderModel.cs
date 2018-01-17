using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMShaderModel : GMResourceModel
    {
        [DataMember]
        public GMShaderType type { get; set; }

        public GMShaderModel()
            : base("GMShader", "1.0")
        {
            
        }
    }

    public enum GMShaderType
    {
        None = 0,
        GLSLES = 1,
        GLSL = 2,
        HLSL11 = 4,
        PSSL = 5
    }
}
