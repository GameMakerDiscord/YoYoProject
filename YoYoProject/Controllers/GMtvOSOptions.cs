using YoYoProject.Common;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMtvOSOptions : GMResource
    {
        private bool appleSignIn;
        public bool AppleSignIn
        {
            get { return GetProperty(appleSignIn); }
            set { SetProperty(value, ref appleSignIn); }
        }

        private string bundleName;
        public string BundleName
        {
            get { return GetProperty(bundleName); }
            set { SetProperty(value, ref bundleName); }
        }

        private bool displayCursor;
        public bool DisplayCursor
        {
            get { return GetProperty(displayCursor); }
            set { SetProperty(value, ref displayCursor); }
        }

        private string displayName;
        public string DisplayName
        {
            get { return GetProperty(displayName); }
            set { SetProperty(value, ref displayName); }
        }

        private string icon1280;
        public string Icon1280
        {
            get { return GetProperty(icon1280); }
            set { SetProperty(value, ref icon1280); }
        }

        private string icon400;
        public string Icon400
        {
            get { return GetProperty(icon400); }
            set { SetProperty(value, ref icon400); }
        }

        private string icon4002x;
        public string Icon4002x
        {
            get { return GetProperty(icon4002x); }
            set { SetProperty(value, ref icon4002x); }
        }

        private bool interpolatePixels;
        public bool InterpolatePixels
        {
            get { return GetProperty(interpolatePixels); }
            set { SetProperty(value, ref interpolatePixels); }
        }

        private string outputDir;
        public string OutputDir
        {
            get { return GetProperty(outputDir); }
            set { SetProperty(value, ref outputDir); }
        }

        private bool pushNotifications;
        public bool PushNotifications
        {
            get { return GetProperty(pushNotifications); }
            set { SetProperty(value, ref pushNotifications); }
        }

        private Scale scale;
        public Scale Scale
        {
            get { return GetProperty(scale); }
            set { SetProperty(value, ref scale); }
        }

        private int splashTime;
        public int SplashTime
        {
            get { return GetProperty(splashTime); }
            set { SetProperty(value, ref splashTime); }
        }

        private string splashScreen;
        public string SplashScreen
        {
            get { return GetProperty(splashScreen); }
            set { SetProperty(value, ref splashScreen); }
        }

        private string splashScreen2x;
        public string SplashScreen2x
        {
            get { return GetProperty(splashScreen2x); }
            set { SetProperty(value, ref splashScreen2x); }
        }

        private string teamId;
        public string TeamId
        {
            get { return GetProperty(teamId); }
            set { SetProperty(value, ref teamId); }
        }

        private TexturePageSize texturePageSize;
        public TexturePageSize TexturePageSize
        {
            get { return GetProperty(texturePageSize); }
            set { SetProperty(value, ref texturePageSize); }
        }

        private string topShelf;
        public string TopShelf
        {
            get { return GetProperty(topShelf); }
            set { SetProperty(value, ref topShelf); }
        }

        private string topShelf2x;
        public string TopShelf2x
        {
            get { return GetProperty(topShelf2x); }
            set { SetProperty(value, ref topShelf2x); }
        }

        private string topShelfWide;
        public string TopShelfWide
        {
            get { return GetProperty(topShelfWide); }
            set { SetProperty(value, ref topShelfWide); }
        }

        private string topShelfWide2x;
        public string TopShelfWide2x
        {
            get { return GetProperty(topShelfWide2x); }
            set { SetProperty(value, ref topShelfWide2x); }
        }

        private BuildVersion buildVersion;
        public BuildVersion BuildVer
        {
            get { return GetProperty(buildVersion); }
            set { SetProperty(value, ref buildVersion); }
        }

        internal override string ResourcePath => @"options\tvos\options_tvos.yy";

        internal override void Create(string name)
        {
            AppleSignIn = false;
            BundleName = "com.company.game";
            DisplayCursor = false;
            DisplayName = "Made in GameMaker Studio 2";
            Icon1280 = @"${base_options_dir}\tvos\icons\1280.png";
            Icon400 = @"${base_options_dir}\tvos\icons\400.png";
            Icon4002x = @"${base_options_dir}\tvos\icons\400_2x.png";
            InterpolatePixels = true;
            OutputDir = @"~/GameMakerStudio2/tvOS";
            PushNotifications = false;
            Scale = Scale.KeepAspectRatio;
            SplashTime = 10;
            SplashScreen = @"${base_options_dir}\tvos\splash\splash.png";
            SplashScreen2x = @"${base_options_dir}\tvos\splash\splash_2x.png";
            TeamId = string.Empty;
            TexturePageSize = new TexturePageSize(2048, 2048);
            TopShelf = @"${base_options_dir}\tvos\topshelf\topshelf.png";
            TopShelf2x = @"${base_options_dir}\tvos\topshelf\topshelf_2x.png";
            TopShelfWide = @"${base_options_dir}\tvos\topshelf\topshelf_wide.png";
            TopShelfWide2x = @"${base_options_dir}\tvos\topshelf\topshelf_wide_2x.png";
            BuildVer = new BuildVersion(1, 0, 0, 0);
        }

        internal override ModelBase Serialize()
        {
            return new GMtvOSOptionsModel
            {
                id = Id,
                name = "tvOS",
                option_tvos_apple_sign_in = AppleSignIn,
                option_tvos_bundle_name = BundleName,
                option_tvos_display_cursor = DisplayCursor,
                option_tvos_display_name = DisplayName,
                option_tvos_icon_1280 = Icon1280,
                option_tvos_icon_400 = Icon400,
                option_tvos_icon_400_2x = Icon4002x,
                option_tvos_interpolate_pixels = InterpolatePixels,
                option_tvos_output_dir = OutputDir,
                option_tvos_push_notifications = PushNotifications,
                option_tvos_scale = Scale,
                option_tvos_splashscreen = SplashScreen,
                option_tvos_splashscreen_2x = SplashScreen2x,
                option_tvos_splash_time = SplashTime,
                option_tvos_team_id = TeamId,
                option_tvos_texture_page = TexturePageSize.ToString(),
                option_tvos_topshelf = TopShelf,
                option_tvos_topshelf_2x = TopShelf2x,
                option_tvos_topshelf_wide = TopShelfWide,
                option_tvos_topshelf_wide_2x = TopShelfWide2x,
                option_tvos_version = BuildVer
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            var tvOSOptionsModel = (GMtvOSOptionsModel)model;

            AppleSignIn = tvOSOptionsModel.option_tvos_apple_sign_in;
            BundleName = tvOSOptionsModel.option_tvos_bundle_name;
            DisplayCursor = tvOSOptionsModel.option_tvos_display_cursor;
            DisplayName = tvOSOptionsModel.option_tvos_display_name;
            Icon1280 = tvOSOptionsModel.option_tvos_icon_1280;
            Icon400 = tvOSOptionsModel.option_tvos_icon_400;
            Icon4002x = tvOSOptionsModel.option_tvos_icon_400_2x;
            InterpolatePixels = tvOSOptionsModel.option_tvos_interpolate_pixels;
            OutputDir = tvOSOptionsModel.option_tvos_output_dir;
            PushNotifications = tvOSOptionsModel.option_tvos_push_notifications;
            Scale = tvOSOptionsModel.option_tvos_scale;
            SplashScreen = tvOSOptionsModel.option_tvos_splashscreen;
            SplashScreen2x = tvOSOptionsModel.option_tvos_splashscreen_2x;
            SplashTime = tvOSOptionsModel.option_tvos_splash_time;
            TeamId = tvOSOptionsModel.option_tvos_team_id;
            TexturePageSize = new TexturePageSize(tvOSOptionsModel.option_tvos_texture_page);
            TopShelf = tvOSOptionsModel.option_tvos_topshelf;
            TopShelf2x = tvOSOptionsModel.option_tvos_topshelf_2x;
            TopShelfWide = tvOSOptionsModel.option_tvos_topshelf_wide;
            TopShelfWide2x = tvOSOptionsModel.option_tvos_topshelf_wide_2x;
            BuildVer = tvOSOptionsModel.option_tvos_version;
        }
    }
}
