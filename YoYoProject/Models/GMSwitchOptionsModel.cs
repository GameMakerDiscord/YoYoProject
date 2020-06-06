using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMSwitchOptionsModel : GMOptionsBaseModel
    {
        [DataMember]
        public bool option_switch_check_nsp_publish_errors { get; set; }

        [DataMember]
        public bool option_switch_enable_fileaccess_checking { get; set; }

        [DataMember]
        public bool option_switch_enable_nex_libraries { get; set; }

        [DataMember]
        public bool option_switch_interpolate_pixels { get; set; }

        [DataMember]
        public Scale option_switch_scale { get; set; }

        [DataMember]
        public string option_switch_project_nmeta { get; set; }

        [DataMember]
        public string option_switch_texture_page { get; set; }

        public GMSwitchOptionsModel()
            : base("GMSwitchOptionsModel", "1.0")
        {

        }
    }
}