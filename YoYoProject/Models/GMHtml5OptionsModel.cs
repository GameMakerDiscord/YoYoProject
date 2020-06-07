using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMHtml5OptionsModel : GMOptionsBaseModel
    {
        [DataMember]
        public bool option_html5_allow_fullscreen { get; set; }

        [DataMember]
        public string option_html5_browser_title { get; set; }

        [DataMember]
        public bool option_html5_centregame { get; set; }

        [DataMember]
        public bool option_html5_display_cursor { get; set; }

        [DataMember]
        public string option_html5_facebook_app_display_name { get; set; }

        [DataMember]
        public string option_html5_facebook_id { get; set; }

        [DataMember]
        public bool option_html5_flurry_enable { get; set; }

        [DataMember]
        public string option_html5_flurry_id { get; set; }

        [DataMember]
        public string option_html5_foldername { get; set; }

        [DataMember]
        public bool option_html5_google_analytics_enable { get; set; }

        [DataMember]
        public string option_html5_google_tracking_id { get; set; }

        [DataMember]
        public string option_html5_icon { get; set; }

        [DataMember]
        public string option_html5_index { get; set; }

        [DataMember]
        public bool option_html5_interpolate_pixels { get; set; }

        [DataMember]
        public string option_html5_jsprepend { get; set; }

        [DataMember]
        public string option_html5_loadingbar { get; set; }

        [DataMember]
        public bool option_html5_localrunalert { get; set; }

        [DataMember]
        public bool option_html5_outputdebugtoconsole { get; set; }

        [DataMember]
        public string option_html5_outputname { get; set; }

        [DataMember]
        public Scale option_html5_scale { get; set; }

        [DataMember]
        public string option_html5_splash_png { get; set; }

        [DataMember]
        public string option_html5_texture_page { get; set; }

        [DataMember]
        public bool option_html5_use_facebook { get; set; }

        [DataMember]
        public bool option_html5_usebuiltinfont { get; set; }

        [DataMember]
        public bool option_html5_usebuiltinparticles { get; set; }

        [DataMember]
        public bool option_html5_usesplash { get; set; }

        [DataMember]
        public BuildVersion option_html5_version { get; set; }

        [DataMember]
        public WebGLSetting option_html5_webgl { get; set; }

        public GMHtml5OptionsModel()
            : base("GMHtml5OptionsModel", "1.0")
        {

        }
    }
}