using YoYoProject.Common;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMHtml5Options : GMResource
    {
        private bool allowFullscreen;
        public bool AllowFullscreen
        {
            get { return GetProperty(allowFullscreen); }
            set { SetProperty(value, ref allowFullscreen); }
        }

        private string browserTitle;
        public string BrowserTitle
        {
            get { return GetProperty(browserTitle); }
            set { SetProperty(value, ref browserTitle); }
        }

        private bool centreGame;
        public bool CentreGame
        {
            get { return GetProperty(centreGame); }
            set { SetProperty(value, ref centreGame); }
        }

        private bool displayCursor;
        public bool DisplayCursor
        {
            get { return GetProperty(displayCursor); }
            set { SetProperty(value, ref displayCursor); }
        }

        private string facebookAppDisplayName;
        public string FacebookAppDisplayName
        {
            get { return GetProperty(facebookAppDisplayName); }
            set { SetProperty(value, ref facebookAppDisplayName); }
        }

        private string facebookId;
        public string FacebookId
        {
            get { return GetProperty(facebookId); }
            set { SetProperty(value, ref facebookId); }
        }

        private bool flurryEnable;
        public bool FlurryEnable
        {
            get { return GetProperty(flurryEnable); }
            set { SetProperty(value, ref flurryEnable); }
        }

        private string flurryId;
        public string FlurryId
        {
            get { return GetProperty(flurryId); }
            set { SetProperty(value, ref flurryId); }
        }

        private string folderName;
        public string FolderName
        {
            get { return GetProperty(folderName); }
            set { SetProperty(value, ref folderName); }
        }

        private bool googleAnalyticsEnable;
        public bool GoogleAnalyticsEnable
        {
            get { return GetProperty(googleAnalyticsEnable); }
            set { SetProperty(value, ref googleAnalyticsEnable); }
        }

        private string googleTrackingId;
        public string GoogleTrackingId
        {
            get { return GetProperty(googleTrackingId); }
            set { SetProperty(value, ref googleTrackingId); }
        }

        private string icon;
        public string Icon
        {
            get { return GetProperty(icon); }
            set { SetProperty(value, ref icon); }
        }

        private string index;
        public string Index
        {
            get { return GetProperty(index); }
            set { SetProperty(value, ref index); }
        }

        private bool interpolatePixels;
        public bool InterpolatePixels
        {
            get { return GetProperty(interpolatePixels); }
            set { SetProperty(value, ref interpolatePixels); }
        }

        private string jsPrepend;
        public string JsPrepend
        {
            get { return GetProperty(jsPrepend); }
            set { SetProperty(value, ref jsPrepend); }
        }

        private string loadingBar;
        public string LoadingBar
        {
            get { return GetProperty(loadingBar); }
            set { SetProperty(value, ref loadingBar); }
        }

        private bool localRunAlert;
        public bool LocalRunAlert
        {
            get { return GetProperty(localRunAlert); }
            set { SetProperty(value, ref localRunAlert); }
        }

        private bool outDebugToConsole;
        public bool OutDebugToConsole
        {
            get { return GetProperty(outDebugToConsole); }
            set { SetProperty(value, ref outDebugToConsole); }
        }

        private string outputName;
        public string OutputName
        {
            get { return GetProperty(outputName); }
            set { SetProperty(value, ref outputName); }
        }

        private Scale scale;
        public Scale Scale
        {
            get { return GetProperty(scale); }
            set { SetProperty(value, ref scale); }
        }

        private string splashPng;
        public string SplashPng
        {
            get { return GetProperty(splashPng); }
            set { SetProperty(value, ref splashPng); }
        }

        private TexturePageSize texturePage;
        public TexturePageSize TexturePage
        {
            get { return GetProperty(texturePage); }
            set { SetProperty(value, ref texturePage); }
        }

        private bool useFacebook;
        public bool UseFacebook
        {
            get { return GetProperty(useFacebook); }
            set { SetProperty(value, ref useFacebook); }
        }

        private bool useBuiltinFont;
        public bool UseBuiltinFont
        {
            get { return GetProperty(useBuiltinFont); }
            set { SetProperty(value, ref useBuiltinFont); }
        }

        private bool useBuiltinParticles;
        public bool UseBuiltinParticles
        {
            get { return GetProperty(useBuiltinParticles); }
            set { SetProperty(value, ref useBuiltinParticles); }
        }

        private bool useSplash;
        public bool UseSplash
        {
            get { return GetProperty(useSplash); }
            set { SetProperty(value, ref useSplash); }
        }

        private BuildVersion version;
        public BuildVersion Version
        {
            get { return GetProperty(version); }
            set { SetProperty(value, ref version); }
        }

        private WebGLSetting webGl;
        public WebGLSetting WebGl
        {
            get { return GetProperty(webGl); }
            set { SetProperty(value, ref webGl); }
        }

        internal override string ResourcePath => @"options\html5\options_html5.yy";

        internal override void Create(string name)
        {
            AllowFullscreen = true;
            BrowserTitle = "Made in GameMaker Studio 2";
            CentreGame = false;
            DisplayCursor = true;
            FacebookAppDisplayName = string.Empty;
            FacebookId = string.Empty;
            FlurryEnable = false;
            FlurryId = string.Empty;
            FolderName = "html5game";
            GoogleAnalyticsEnable = false;
            GoogleTrackingId = string.Empty;
            Icon = @"${base_options_dir}/html5/fav.ico";
            Index = string.Empty;
            InterpolatePixels = false;
            JsPrepend = string.Empty;
            LoadingBar = string.Empty;
            LocalRunAlert = true;
            OutDebugToConsole = true;
            OutputName = "index.html";
            Scale = Scale.KeepAspectRatio;
            SplashPng = @"${base_options_dir}/html5/splash.png";
            TexturePage = new TexturePageSize(2048, 2048);
            UseFacebook = false;
            UseBuiltinFont = true;
            UseBuiltinParticles = true;
            UseSplash = false;
            Version = new BuildVersion(1, 0, 0, 0);
            WebGl = WebGLSetting.AutoDetect;
        }

        internal override ModelBase Serialize()
        {
            return new GMHtml5OptionsModel
            {
                id = Id,
                name = "HTML5",
                option_html5_allow_fullscreen = AllowFullscreen,
                option_html5_browser_title = BrowserTitle,
                option_html5_centregame = CentreGame,
                option_html5_display_cursor = DisplayCursor,
                option_html5_facebook_app_display_name = FacebookAppDisplayName,
                option_html5_facebook_id = FacebookId,
                option_html5_flurry_enable = FlurryEnable,
                option_html5_flurry_id = FlurryId,
                option_html5_foldername = FolderName,
                option_html5_google_analytics_enable = GoogleAnalyticsEnable,
                option_html5_google_tracking_id = GoogleTrackingId,
                option_html5_icon = Icon,
                option_html5_index = Index,
                option_html5_interpolate_pixels = InterpolatePixels,
                option_html5_jsprepend = JsPrepend,
                option_html5_loadingbar = LoadingBar,
                option_html5_localrunalert = LocalRunAlert,
                option_html5_outputdebugtoconsole = OutDebugToConsole,
                option_html5_outputname = OutputName,
                option_html5_scale = Scale,
                option_html5_splash_png = SplashPng,
                option_html5_texture_page = TexturePage.ToString(),
                option_html5_usebuiltinfont = UseBuiltinFont,
                option_html5_usebuiltinparticles = UseBuiltinParticles,
                option_html5_usesplash = UseSplash,
                option_html5_use_facebook = UseFacebook,
                option_html5_version = Version,
                option_html5_webgl = WebGl
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            var Html5OptionsModel = (GMHtml5OptionsModel)model;

            Id = Html5OptionsModel.id;
            Name = Html5OptionsModel.name;
            AllowFullscreen = Html5OptionsModel.option_html5_allow_fullscreen;
            BrowserTitle = Html5OptionsModel.option_html5_browser_title;
            CentreGame = Html5OptionsModel.option_html5_centregame;
            DisplayCursor = Html5OptionsModel.option_html5_display_cursor;
            FacebookAppDisplayName = Html5OptionsModel.option_html5_facebook_app_display_name;
            FacebookId = Html5OptionsModel.option_html5_facebook_id;
            FlurryEnable = Html5OptionsModel.option_html5_flurry_enable;
            FlurryId = Html5OptionsModel.option_html5_flurry_id;
            FolderName = Html5OptionsModel.option_html5_foldername;
            GoogleAnalyticsEnable = Html5OptionsModel.option_html5_google_analytics_enable;
            GoogleTrackingId = Html5OptionsModel.option_html5_google_tracking_id;
            Icon = Html5OptionsModel.option_html5_icon;
            Index = Html5OptionsModel.option_html5_index;
            InterpolatePixels = Html5OptionsModel.option_html5_interpolate_pixels;
            JsPrepend = Html5OptionsModel.option_html5_jsprepend;
            LoadingBar = Html5OptionsModel.option_html5_loadingbar;
            LocalRunAlert = Html5OptionsModel.option_html5_localrunalert;
            OutDebugToConsole = Html5OptionsModel.option_html5_outputdebugtoconsole;
            OutputName = Html5OptionsModel.option_html5_outputname;
            Scale = Html5OptionsModel.option_html5_scale;
            SplashPng = Html5OptionsModel.option_html5_splash_png;
            TexturePage = new TexturePageSize(Html5OptionsModel.option_html5_texture_page);
            UseBuiltinFont = Html5OptionsModel.option_html5_usebuiltinfont;
            UseBuiltinParticles = Html5OptionsModel.option_html5_usebuiltinparticles;
            UseSplash = Html5OptionsModel.option_html5_usesplash;
            UseFacebook = Html5OptionsModel.option_html5_use_facebook;
            Version = Html5OptionsModel.option_html5_version;
            WebGl = Html5OptionsModel.option_html5_webgl;
        }
    }
}
