using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMTimelineModel : GMResourceModel
    {
        [DataMember]
        public List<GMTimelineMomentModel> momentList { get; set; }

        public GMTimelineModel()
            : base("GMTimeline", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMTimelineMomentModel : ModelBase
    {
        [DataMember]
        public int moment { get; set; }

        [DataMember]
        public GMEventModel evnt { get; set; }
        
        public GMTimelineMomentModel()
            : base("GMMoment", "1.0")
        {
            
        }
    }
}
