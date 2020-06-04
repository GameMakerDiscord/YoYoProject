using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMMacOptionsModel : GMOptionsBaseModel
    {
        [DataMember]
        public string option_mac_display_name { get; set; }

        [DataMember]
        public string option_mac_app_id { get; set; }

        [DataMember]
        public BuildVersion option_mac_version { get; set; }

        [DataMember]
        public string option_mac_output_dir { get; set; }

        [DataMember]
        public string option_mac_team_id { get; set; }

        [DataMember]
        public string option_mac_signing_identity { get; set; }

        [DataMember]
        public string option_mac_copyright { get; set; }

        [DataMember]
        public string option_mac_splash_png { get; set; }

        [DataMember]
        public string option_mac_icon_png { get; set; }

        [DataMember]
        public bool option_mac_menu_dock { get; set; }

        [DataMember]
        public bool option_mac_display_cursor { get; set; }

        [DataMember]
        public bool option_mac_start_fullscreen { get; set; }

        [DataMember]
        public bool option_mac_allow_fullscreen { get; set; }

        [DataMember]
        public bool option_mac_interpolate_pixels { get; set; }

        [DataMember]
        public bool option_mac_vsync { get; set; }

        [DataMember]
        public bool option_mac_resize_window { get; set; }

        [DataMember]
        public int option_mac_scale { get; set; }

        [DataMember]
        public string option_mac_texture_page { get; set; }

        [DataMember]
        public bool option_mac_build_app_store { get; set; }

        [DataMember]
        public bool option_mac_allow_incoming_network { get; set; }

        [DataMember]
        public bool option_mac_allow_outgoing_network { get; set; }

        [DataMember]
        public string option_mac_app_category { get; set; }

        [DataMember]
        public bool option_mac_enable_steam { get; set; }

        [DataMember]
        public bool option_mac_disable_sandbox { get; set; }

        public GMMacOptionsModel()
            : base("GMMacOptions", "1.0")
        {

        }
    }
}