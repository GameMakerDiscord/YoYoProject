using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMLinuxOptionsModel : GMOptionsBaseModel
    {
        [DataMember]
        public string option_linux_display_name { get; set; }

        [DataMember]
        public BuildVersion option_linux_version { get; set; }

        [DataMember]
        public string option_linux_maintainer_email { get; set; }

        [DataMember]
        public string option_linux_homepage { get; set; }

        [DataMember]
        public string option_linux_short_desc { get; set; }

        [DataMember]
        public string option_linux_long_desc { get; set; }

        [DataMember]
        public string option_linux_splash_screen { get; set; }

        [DataMember]
        public bool option_linux_display_splash { get; set; }

        [DataMember]
        public string option_linux_icon { get; set; }

        [DataMember]
        public bool option_linux_start_fullscreen { get; set; }

        [DataMember]
        public bool option_linux_allow_fullscreen { get; set; }

        [DataMember]
        public bool option_linux_interpolate_pixels { get; set; }

        [DataMember]
        public bool option_linux_display_cursor { get; set; }

        [DataMember]
        public bool option_linux_sync { get; set; }

        [DataMember]
        public bool option_linux_resize_window { get; set; }

        [DataMember]
        public int option_linux_scale { get; set; }

        [DataMember]
        public string option_linux_texture_page { get; set; }

        [DataMember]
        public bool option_linux_enable_steam { get; set; }

        public GMLinuxOptionsModel()
            : base("GMLinuxOptionsModel", "1.0")
        {

        }
    }
}