using YoYoProject.Common;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMLinuxOptions : GMResource
    {
        private string displayName;
        public string DisplayName
        {
            get { return GetProperty(displayName); }
            set { SetProperty(value, ref displayName); }
        }
        
        private BuildVersion version;
        public BuildVersion Version
        {
            get { return GetProperty(version); }
            set { SetProperty(value, ref version); }
        }
        
        private string maintainerEmail;
        public string MaintainerEmail
        {
            get { return GetProperty(maintainerEmail); }
            set { SetProperty(value, ref maintainerEmail); }
        }
        
        private string homepage;
        public string Homepage
        {
            get { return GetProperty(homepage); }
            set { SetProperty(value, ref homepage); }
        }
        
        private string shortDescription;
        public string ShortDescription
        {
            get { return GetProperty(shortDescription); }
            set { SetProperty(value, ref shortDescription); }
        }
        
        private string longDescription;
        public string LongDescription
        {
            get { return GetProperty(longDescription); }
            set { SetProperty(value, ref longDescription); }
        }
        
        private string splashScreen;
        public string SplashScreen
        {
            get { return GetProperty(splashScreen); }
            set { SetProperty(value, ref splashScreen); }
        }

        private bool displaySplash;
        public bool DisplaySplash
        {
            get { return GetProperty(displaySplash); }
            set { SetProperty(value, ref displaySplash); }
        }

        private string icon;
        public string Icon
        {
            get { return GetProperty(icon); }
            set { SetProperty(value, ref icon); }
        }
        
        private bool startFullscreen;
        public bool StartFullscreen
        {
            get { return GetProperty(startFullscreen); }
            set { SetProperty(value, ref startFullscreen); }
        }
        
        private bool allowFullscreen;
        public bool AllowFullscreen
        {
            get { return GetProperty(allowFullscreen); }
            set { SetProperty(value, ref allowFullscreen); }
        }
        
        private bool interpolatePixels;
        public bool InterpolatePixels
        {
            get { return GetProperty(interpolatePixels); }
            set { SetProperty(value, ref interpolatePixels); }
        }
        
        private bool displayCursor;
        public bool DisplayCursor
        {
            get { return GetProperty(displayCursor); }
            set { SetProperty(value, ref displayCursor); }
        }
        
        private bool vsync;
        public bool Vsync
        {
            get { return GetProperty(vsync); }
            set { SetProperty(value, ref vsync); }
        }
        
        private bool resizeWindow;
        public bool ResizeWindow
        {
            get { return GetProperty(resizeWindow); }
            set { SetProperty(value, ref resizeWindow); }
        }
        
        private Scale scale;
        public Scale Scale
        {
            get { return GetProperty(scale); }
            set { SetProperty(value, ref scale); }
        }
        
        private TexturePageSize texturePage;
        public TexturePageSize TexturePage
        {
            get { return GetProperty(texturePage); }
            set { SetProperty(value, ref texturePage); }
        }
        
        private bool enableSteam;
        public bool EnableSteam
        {
            get { return GetProperty(enableSteam); }
            set { SetProperty(value, ref enableSteam); }
        }

        private bool disableSandbox;
        public bool DisableSandbox
        {
            get { return GetProperty(disableSandbox); }
            set { SetProperty(value, ref disableSandbox); }
        }
        
        internal override string ResourcePath => @"options\linux\options_linux.yy";

        internal override void Create(string name)
        {
            DisplayName = "Made in GameMaker Studio 2";
            Version = new BuildVersion(1, 0, 0, 0);
            MaintainerEmail = "";
            Homepage = "http://www.yoyogames.com";
            ShortDescription = "";
            LongDescription = "";
            SplashScreen = @"${base_options_dir}/linux/splash/splash.png"; // TODO Copy in default
            DisplaySplash = false;
            Icon = @"${base_options_dir}/linux/icons/64.png"; // TODO Copy in default
            StartFullscreen = false;
            AllowFullscreen = false;
            InterpolatePixels = false;
            DisplayCursor = true;
            Vsync = false;
            ResizeWindow = false;
            Scale = Scale.KeepAspectRatio;
            TexturePage = new TexturePageSize(2048, 2048);
            EnableSteam = false;
            DisableSandbox = false;
        }

        internal override ModelBase Serialize()
        {
            return new GMLinuxOptionsModel
            {
                id = Id,
                name = "Linux",
                option_linux_display_name = DisplayName,
                option_linux_version = Version,
                option_linux_maintainer_email = MaintainerEmail,
                option_linux_homepage = Homepage,
                option_linux_short_desc = ShortDescription,
                option_linux_long_desc = LongDescription,
                option_linux_splash_screen = SplashScreen,
                option_linux_display_splash = DisplaySplash,
                option_linux_icon = Icon,
                option_linux_start_fullscreen = StartFullscreen,
                option_linux_allow_fullscreen = AllowFullscreen,
                option_linux_interpolate_pixels = InterpolatePixels,
                option_linux_display_cursor = DisplayCursor,
                option_linux_sync = Vsync,
                option_linux_resize_window = ResizeWindow,
                option_linux_scale = Scale,
                option_linux_texture_page = TexturePage.ToString(),
                option_linux_enable_steam = EnableSteam,
                option_linux_disable_sandbox = DisableSandbox
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            var linuxOptionsModel = (GMLinuxOptionsModel)model;

            Id = linuxOptionsModel.id;
            Name = linuxOptionsModel.name;
            DisplayName = linuxOptionsModel.option_linux_display_name;
            Version = linuxOptionsModel.option_linux_version;
            MaintainerEmail = linuxOptionsModel.option_linux_maintainer_email;
            Homepage = linuxOptionsModel.option_linux_homepage;
            ShortDescription = linuxOptionsModel.option_linux_short_desc;
            LongDescription = linuxOptionsModel.option_linux_long_desc;
            SplashScreen = linuxOptionsModel.option_linux_splash_screen;
            DisplaySplash = linuxOptionsModel.option_linux_display_splash;
            Icon = linuxOptionsModel.option_linux_icon;
            StartFullscreen = linuxOptionsModel.option_linux_start_fullscreen;
            AllowFullscreen = linuxOptionsModel.option_linux_allow_fullscreen;
            InterpolatePixels = linuxOptionsModel.option_linux_interpolate_pixels;
            DisplayCursor = linuxOptionsModel.option_linux_display_cursor;
            Vsync = linuxOptionsModel.option_linux_sync;
            ResizeWindow = linuxOptionsModel.option_linux_resize_window;
            Scale = linuxOptionsModel.option_linux_scale;
            TexturePage = new TexturePageSize(linuxOptionsModel.option_linux_texture_page);
            EnableSteam = linuxOptionsModel.option_linux_enable_steam;
            DisableSandbox = linuxOptionsModel.option_linux_disable_sandbox;
        }
    }
}
