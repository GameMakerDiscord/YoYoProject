using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMWindowsOptionsModel : GMResourceModel
    {
        [DataMember]
        public string option_windows_display_name { get; set; }

        [DataMember]
        public string option_windows_executable_name { get; set; }

        [DataMember]
        public BuildVersion option_windows_version { get; set; }

        [DataMember]
        public string option_windows_company_info { get; set; }

        [DataMember]
        public string option_windows_product_info { get; set; }

        [DataMember]
        public string option_windows_copyright_info { get; set; }

        [DataMember]
        public string option_windows_description_info { get; set; }

        [DataMember]
        public bool option_windows_display_cursor { get; set; }

        [DataMember]
        public string option_windows_icon { get; set; }

        [DataMember]
        public SaveLocation option_windows_save_location { get; set; }

        [DataMember]
        public string option_windows_splash_screen { get; set; }

        [DataMember]
        public bool option_windows_use_splash { get; set; }

        [DataMember]
        public bool option_windows_start_fullscreen { get; set; }

        [DataMember]
        public bool option_windows_allow_fullscreen_switching { get; set; }

        [DataMember]
        public bool option_windows_interpolate_pixels { get; set; }

        [DataMember]
        public bool option_windows_vsync { get; set; }

        [DataMember]
        public bool option_windows_resize_window { get; set; }

        [DataMember]
        public bool option_windows_borderless { get; set; }

        [DataMember]
        public Scale option_windows_scale { get; set; }

        [DataMember]
        public int option_windows_sleep_margin { get; set; }

        [DataMember]
        public string option_windows_texture_page { get; set; }

        [DataMember]
        public string option_windows_installer_finished { get; set; }

        [DataMember]
        public string option_windows_installer_header { get; set; }

        [DataMember]
        public string option_windows_license { get; set; }

        [DataMember]
        public string option_windows_nsis_file { get; set; }

        [DataMember]
        public bool option_windows_enable_steam { get; set; }

        [DataMember]
        public bool option_windows_disable_sandbox { get; set; }

        public GMWindowsOptionsModel()
            : base("GMWindowsOptions", "1.0")
        {

        }
    }
}