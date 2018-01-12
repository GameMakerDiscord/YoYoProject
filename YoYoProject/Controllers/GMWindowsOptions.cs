using System;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMWindowsOptions : GMResource
    {
        private string displayName;
        public string DisplayName
        {
            get { return GetProperty(displayName); }
            set { SetProperty(value, ref displayName); }
        }
        
        private string executableName;
        public string ExecutableName
        {
            get { return GetProperty(executableName); }
            set { SetProperty(value, ref executableName); }
        }
        
        private BuildVersion version;
        public BuildVersion Version
        {
            get { return GetProperty(version); }
            set { SetProperty(value, ref version); }
        }
        
        private string companyInfo;
        public string CompanyInfo
        {
            get { return GetProperty(companyInfo); }
            set { SetProperty(value, ref companyInfo); }
        }
        
        private string productInfo;
        public string ProductInfo
        {
            get { return GetProperty(productInfo); }
            set { SetProperty(value, ref productInfo); }
        }
        
        private string copyrightInfo;
        public string CopyrightInfo
        {
            get { return GetProperty(copyrightInfo); }
            set { SetProperty(value, ref copyrightInfo); }
        }
        
        private string descriptionInfo;
        public string DescriptionInfo
        {
            get { return GetProperty(descriptionInfo); }
            set { SetProperty(value, ref descriptionInfo); }
        }
        
        private bool displayCursor;
        public bool DisplayCursor
        {
            get { return GetProperty(displayCursor); }
            set { SetProperty(value, ref displayCursor); }
        }
        
        private string icon;
        public string Icon
        {
            get { return GetProperty(icon); }
            set { SetProperty(value, ref icon); }
        }
        
        private int saveLocation;
        public int SaveLocation
        {
            get { return GetProperty(saveLocation); }
            set { SetProperty(value, ref saveLocation); }
        }
        
        private string splashScreen;
        public string SplashScreen
        {
            get { return GetProperty(splashScreen); }
            set { SetProperty(value, ref splashScreen); }
        }
        
        private bool useSplash;
        public bool UseSplash
        {
            get { return GetProperty(useSplash); }
            set { SetProperty(value, ref useSplash); }
        }
        
        private bool startFullscreen;
        public bool StartFullscreen
        {
            get { return GetProperty(startFullscreen); }
            set { SetProperty(value, ref startFullscreen); }
        }
        
        private bool allowFullscreenSwitching;
        public bool AllowFullscreenSwitching
        {
            get { return GetProperty(allowFullscreenSwitching); }
            set { SetProperty(value, ref allowFullscreenSwitching); }
        }
        
        private bool interpolatePixels;
        public bool InterpolatePixels
        {
            get { return GetProperty(interpolatePixels); }
            set { SetProperty(value, ref interpolatePixels); }
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
        
        private bool borderless;
        public bool Borderless
        {
            get { return GetProperty(borderless); }
            set { SetProperty(value, ref borderless); }
        }
        
        private int scale;
        public int Scale
        {
            get { return GetProperty(scale); }
            set { SetProperty(value, ref scale); }
        }
        
        private int sleepMargin;
        public int SleepMargin
        {
            get { return GetProperty(sleepMargin); }
            set { SetProperty(value, ref sleepMargin); }
        }
        
        private string texturePage;
        public string TexturePage
        {
            get { return GetProperty(texturePage); }
            set { SetProperty(value, ref texturePage); }
        }
        
        private string installerFinisher;
        public string InstallerFinisher
        {
            get { return GetProperty(installerFinisher); }
            set { SetProperty(value, ref installerFinisher); }
        }
        
        private string installerHeader;
        public string InstallerHeader
        {
            get { return GetProperty(installerHeader); }
            set { SetProperty(value, ref installerHeader); }
        }
        
        private string license;
        public string License
        {
            get { return GetProperty(license); }
            set { SetProperty(value, ref license); }
        }
        
        private string nsisFile;
        public string NsisFile
        {
            get { return GetProperty(nsisFile); }
            set { SetProperty(value, ref nsisFile); }
        }
        
        private bool enableSteam;
        public bool EnableSteam
        {
            get { return GetProperty(enableSteam); }
            set { SetProperty(value, ref enableSteam); }
        }
        
        protected internal override string ResourcePath => @"options\windows\options_windows.yy";

        public GMWindowsOptions()
        {
            DisplayName = "Made in GameMaker Studio 2";
            ExecutableName = "${project_name}";
            Version = new BuildVersion(1, 0, 0, 0);
            CompanyInfo = "YoYo Games Ltd";
            ProductInfo = "Made in GameMaker Studio 2";
            CopyrightInfo = $"(c) {DateTime.UtcNow.Year} CompanyName";
            DescriptionInfo = "A GameMaker Studio 2 Game";
            DisplayCursor = true;
            Icon = @"${base_options_dir}\\windows\\icons\\icon.ico"; // TODO Copy in default
            SaveLocation = 0; // TODO ???
            SplashScreen = @"${base_options_dir}\\windows\\splash\\splash.png"; // TODO Copy in default
            UseSplash = false;
            StartFullscreen = false;
            AllowFullscreenSwitching = false;
            InterpolatePixels = false;
            Vsync = false;
            ResizeWindow = false;
            Borderless = false;
            Scale = 0; // TODO ???
            SleepMargin = 10;
            TexturePage = "2048x2048"; // TODO Reference object?
            InstallerFinisher = @"${base_options_dir}\\windows\\installer\\finished.bmp"; // TODO Copy in default
            InstallerHeader = @"${base_options_dir}\\windows\\installer\\header.bmp"; // TODO Copy in default
            License = @"${base_options_dir}\\windows\\installer\\license.txt"; // TODO Copy in default
            NsisFile = @"${base_options_dir}\\windows\\installer\\nsis_script.nsi"; // TODO Copy in default
            EnableSteam = false;
        }

        protected internal override ModelBase Serialize()
        {
            return new GMWindowsOptionsModel
            {
                id = Id,
                name = "Windows",
                option_windows_display_name = DisplayName,
                option_windows_executable_name = ExecutableName,
                option_windows_version = Version,
                option_windows_company_info = CompanyInfo,
                option_windows_product_info = ProductInfo,
                option_windows_copyright_info = CopyrightInfo,
                option_windows_description_info = DescriptionInfo,
                option_windows_display_cursor = DisplayCursor,
                option_windows_icon = Icon,
                option_windows_save_location = SaveLocation,
                option_windows_splash_screen = SplashScreen,
                option_windows_use_splash = UseSplash,
                option_windows_start_fullscreen = StartFullscreen,
                option_windows_allow_fullscreen_switching = AllowFullscreenSwitching,
                option_windows_interpolate_pixels = InterpolatePixels,
                option_windows_vsync = Vsync,
                option_windows_resize_window = ResizeWindow,
                option_windows_borderless = Borderless,
                option_windows_scale = Scale,
                option_windows_sleep_margin = SleepMargin,
                option_windows_texture_page = TexturePage,
                option_windows_installer_finished = InstallerFinisher,
                option_windows_installer_header = InstallerHeader,
                option_windows_license = License,
                option_windows_nsis_file = NsisFile,
                option_windows_enable_steam = EnableSteam
            };
        }
    }
}