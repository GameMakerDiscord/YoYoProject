using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMtvOSOptionsModel : GMOptionsBaseModel
    {
        [DataMember]
        public bool option_tvos_apple_sign_in { get; set; }

        [DataMember]
        public string option_tvos_bundle_name { get; set; }

        [DataMember]
        public bool option_tvos_display_cursor { get; set; }

        [DataMember]
        public string option_tvos_display_name { get; set; }

        [DataMember]
        public string option_tvos_icon_1280 { get; set; }

        [DataMember]
        public string option_tvos_icon_400 { get; set; }

        [DataMember]
        public string option_tvos_icon_400_2x { get; set; }

        [DataMember]
        public bool option_tvos_interpolate_pixels { get; set; }

        [DataMember]
        public string option_tvos_output_dir { get; set; }

        [DataMember]
        public bool option_tvos_push_notifications { get; set; }

        [DataMember]
        public Scale option_tvos_scale { get; set; }

        [DataMember]
        public int option_tvos_splash_time { get; set; }

        [DataMember]
        public string option_tvos_splashscreen { get; set; }

        [DataMember]
        public string option_tvos_splashscreen_2x { get; set; }

        [DataMember]
        public string option_tvos_team_id { get; set; }

        [DataMember]
        public string option_tvos_texture_page { get; set; }

        [DataMember]
        public string option_tvos_topshelf { get; set; }

        [DataMember]
        public string option_tvos_topshelf_2x { get; set; }

        [DataMember]
        public string option_tvos_topshelf_wide { get; set; }

        [DataMember]
        public string option_tvos_topshelf_wide_2x { get; set; }

        [DataMember]
        public BuildVersion option_tvos_version { get; set; }

        public GMtvOSOptionsModel()
            : base("GMtvOSOptionsModel", "1.0")
        {

        }
    }
}