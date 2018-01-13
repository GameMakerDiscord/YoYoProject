using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMAudioOptionsModel : GMOptionsBaseModel
    {
        [DataMember]
        public List<GMAudioGroupModel> audioGroups { get; set; }

        public GMAudioOptionsModel()
            : base("GMAudioOptions", "1.0")
        {

        }
    }
}