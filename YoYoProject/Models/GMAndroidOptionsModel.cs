using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject
{
    namespace Common
    {
        public enum DeviceSupport : int
        {
            OnlyWithGPU, // Only install on devices with a GPU.
            AllDevices // Install on any supported device.
        }

        public enum InstallLocation : int
        {
            Automatic,
            PreferExternal
        }

        public enum ScreenDepth : int
        {
            SixteenBit, // yes, as words, because 16Bit is illegal.
            TwentyFourBit
        }
    }
    [DataContract]
    internal sealed class GMAndroidOptionsModel : GMOptionsBaseModel
    {
        [DataMember]
        public string option_android_application_tag_inject { get; set; }

        [DataMember]
        public bool option_android_arch_arm64 { get; set; }

        [DataMember]
        public bool option_android_arch_armv7 { get; set; }

        [DataMember]
        public bool option_android_arch_x86 { get; set; }

        [DataMember]
        public bool option_android_arch_x86_64 { get; set; }

        [DataMember]
        public string option_android_build_tools { get; set; }

        [DataMember]
        public string option_android_compile_sdk { get; set; }

        [DataMember]
        public DeviceSupport option_android_device_support { get; set; }

        [DataMember]
        public string option_android_display_name { get; set; }

        [DataMember]
        public string option_android_facebook_app_display_name { get; set; }

        [DataMember]
        public string option_android_facebook_id { get; set; }

        [DataMember]
        public bool option_android_gamepad_support { get; set; }

        [DataMember]
        public bool option_android_google_apk_expansion { get; set; }

        [DataMember]
        public bool option_android_google_cloud_saving { get; set; }

        [DataMember]
        public string option_android_google_licensing_public_key { get; set; }

        [DataMember]
        public string option_android_google_services_app_id { get; set; }

        [DataMember]
        public bool option_android_icon_adaptive_generate { get; set; }

        [DataMember]
        public string option_android_icon_adaptive_hdpi { get; set; }

        [DataMember]
        public string option_android_icon_adaptive_ldpi { get; set; }

        [DataMember]
        public string option_android_icon_hdpi { get; set; }

        // TODO: finish icons!

        [DataMember]
        public InstallLocation option_android_install_location { get; set; }

        [DataMember]
        public bool option_android_interpolate_pixels { get; set; }

        [DataMember]
        public bool option_android_lint { get; set; }

        [DataMember]
        public string option_android_minimum_sdk { get; set; } // I wonder why it's a string, if it's just a number like "26".

        [DataMember]
        public bool option_android_orient_landscape { get; set; }

        [DataMember]
        public bool option_android_orient_landscape_flipped { get; set; }

        [DataMember]
        public bool option_android_orient_portrait { get; set; }

        [DataMember]
        public bool option_android_orient_portrait_flipped { get; set; }

        [DataMember]
        public string option_android_package_company { get; set; }

        [DataMember]
        public string option_android_package_domain { get; set; }

        [DataMember]
        public string option_android_package_product { get; set; }

        [DataMember]
        public bool option_android_permission_bluetooth { get; set; }

        [DataMember]
        public bool option_android_permission_internet { get; set; }

        [DataMember]
        public bool option_android_permission_network_state { get; set; }

        [DataMember]
        public bool option_android_permission_read_phone_state { get; set; }

        [DataMember]
        public bool option_android_permission_record_audio { get; set; }

        [DataMember]
        public bool option_android_permission_write_external_storage { get; set; }

        [DataMember]
        public Scale option_android_scale { get; set; }

        [DataMember]
        public ScreenDepth option_android_screen_depth { get; set; }

        [DataMember]
        public int option_android_sleep_margin { get; set; }

        [DataMember]
        public string option_android_splash_screens_landscape { get; set; }

        [DataMember]
        public string option_android_splash_screens_portrait { get; set; }

        [DataMember]
        public int option_android_splash_time { get; set; }

        [DataMember]
        public string option_android_support_lib { get; set; }

        [DataMember]
        public bool option_android_sync_amazon { get; set; } // This bool is weird, does it just say that settings in Amazon should be equal to these?

        [DataMember]
        public string option_android_target_sdk { get; set; }

        [DataMember]
        public string option_android_texture_page { get; set; }

        [DataMember]
        public bool option_android_tools_from_version { get; set; } // Second weird boolean.

        [DataMember]
        public string option_android_tv_banner { get; set; }

        [DataMember]
        public bool option_android_tv_isgame { get; set; }

        [DataMember]
        public bool option_android_use_facebook { get; set; }

        [DataMember]
        public BuildVersion option_android_version { get; set; }

        public GMAndroidOptionsModel()
            : base("GMAndroidOptionsModel", "1.0")
        {

        }
    }
}