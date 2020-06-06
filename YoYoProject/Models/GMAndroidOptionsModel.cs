using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject
{
    namespace Common
    {
        public enum DeviceSupport
        {
            OnlyWithGPU, // Only install on devices with a GPU.
            AllDevices // Install on any supported device.
        }

        public enum InstallLocation
        {
            Automatic,
            PreferExternal
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

        // TODO: finish icons!

        [DataMember]
        public InstallLocation option_android_install_location { get; set; }

        [DataMember]
        public bool option_android_interpolate_pixels { get; set; }

        [DataMember]
        public bool option_android_lint { get; set; }

        public GMAndroidOptionsModel()
            : base("GMAndroidOptionsModel", "1.0")
        {

        }
    }
}