using System.Runtime.Serialization;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMAudioGroupModel : GMBaseGroupModel
    {
        public GMAudioGroupModel()
            : base("GMAudioGroup", "1.0")
        {

        }
    }
}