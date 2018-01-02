using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using YoYoProject.Models;

namespace YoYoProject
{
    [DataContract]
    internal sealed class GMProjectModel : ModelBase
    {
        [DataMember]
        public GMProjectParentModel parentProject { get; set; }

        [DataMember]
        public SortedDictionary<Guid, GMResourceInfoModel> resources { get; set; }

        [DataMember]
        public List<string> configs { get; set; }

        [DataMember]
        public List<Guid> script_order { get; set; }

        [DataMember]
        public bool IsDnDProject { get; set; }

        [DataMember]
        public bool option_ecma { get; set; }

        [DataMember]
        public string tutorial { get; set; }

        public GMProjectModel()
            : base("GMProject", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMProjectParentModel : ModelBase
    {
        [DataMember]
        public string projectPath { get; set; }

        [DataMember]
        public List<Guid> hiddenResources { get; set; }

        [DataMember]
        public Dictionary<Guid, GMResourceInfoModel> alteredResources { get; set; }

        public GMProjectParentModel()
            : base("GMProjectParent", "1.0")
        {

        }
    }

    [DataContract]
    internal sealed class GMResourceInfoModel : GMResourceModel
    {
        [DataMember(Name = "resourcePath")]
        public string ResourcePath { get; set; }

        [DataMember(Name = "resourceType")]
        public string ResourceType { get; set; }

        [DataMember(Name = "resourceCreationConfigs")]
        public List<string> ResourceCreationConfigs { get; set; }

        [DataMember(Name = "configDeltas")]
        public List<string> ConfigDeltas { get; set; }

        [DataMember(Name = "configDeltaFiles")]
        public List<string> ConfigDeltaFiles { get; set; }

        public GMResourceInfoModel()
            : base("GMResourceInfo", "1.0")
        {

        }
    }

    [DataContract]
    internal sealed class GMFolderModel : GMResourceModel
    {
        [DataMember] public string folderName;

        [DataMember] public string localisedFolderName;

        [DataMember] public string filterType;

        [DataMember] public bool isDefaultView;

        [DataMember] public List<Guid> children;

        public GMFolderModel()
            : base("GMFolder", "1.1")
        {

        }
    }

    [DataContract]
    internal sealed class GMWindowsOptionsModel : GMResourceModel
    {
        [DataMember] public string option_windows_display_name;

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
        public int option_windows_save_location { get; set; }

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
        public int option_windows_scale { get; set; }

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

        public GMWindowsOptionsModel()
            : base("GMWindowsOptions", "1.0")
        {

        }
    }

    [DataContract]
    internal sealed class BuildVersion
    {
        [DataMember]
        public int major { get; set; }

        [DataMember]
        public int minor { get; set; }

        [DataMember]
        public int build { get; set; }

        [DataMember]
        public int revision { get; set; }
    }

    [DataContract]
    internal sealed class GMRViewModel : ModelBase
    {
        [DataMember]
        public bool inherit { get; set; }

        [DataMember]
        public bool visible { get; set; }

        [DataMember]
        public int xview { get; set; }

        [DataMember]
        public int yview { get; set; }

        [DataMember]
        public int wview { get; set; }

        [DataMember]
        public int hview { get; set; }

        [DataMember]
        public int xport { get; set; }

        [DataMember]
        public int yport { get; set; }

        [DataMember]
        public int wport { get; set; }

        [DataMember]
        public int hport { get; set; }

        [DataMember]
        public int hborder { get; set; }

        [DataMember]
        public int vborder { get; set; }

        [DataMember]
        public int hspeed { get; set; }

        [DataMember]
        public int vspeed { get; set; }

        [DataMember]
        public Guid objId { get; set; }

        public GMRViewModel()
            : base("GMRView", "1.0")
        {

        }
    }

    [DataContract]
    internal class GMResourceModel : ModelBase
    {
        [DataMember]
        public string name { get; set; }

        public GMResourceModel()
            : base("GMResource", "1.0")
        {

        }

        public GMResourceModel(string modelName, string mvc)
            : base(modelName, mvc)
        {
            
        }
    }

    [DataContract]
    internal class GMOptionsBaseModel : GMResourceModel
    {
        public GMOptionsBaseModel()
            : base("GMOptionsBase", "1.0")
        {

        }

        public GMOptionsBaseModel(string modelName, string mvc)
            : base(modelName, mvc)
        {

        }
    }

    [DataContract]
    internal sealed class GMMainOptionsModel : GMOptionsBaseModel
    {
        [DataMember]
        public string option_gameguid { get; set; }

        [DataMember]
        public int option_game_speed { get; set; }

        [DataMember]
        public bool option_mips_for_3d_textures { get; set; }

        [DataMember]
        public Color option_draw_colour { get; set; }

        [DataMember]
        public string option_steam_app_id { get; set; }

        [DataMember]
        public bool option_allow_game_statistics { get; set; }

        [DataMember]
        public bool option_sci_usesci { get; set; }

        [DataMember]
        public string option_author { get; set; }

        [DataMember]
        public string option_lastchanged { get; set; }

        [DataMember]
        public GMGraphicsOptionsModel graphics_options { get; set; }

        [DataMember]
        public GMAudioOptionsModel audio_options { get; set; }

        public GMMainOptionsModel()
            : base("GMMainOptions", "1.0")
        {

        }
    }

    [DataContract]
    internal sealed class GMGraphicsOptionsModel : GMOptionsBaseModel
    {
        [DataMember]
        public List<GMTextureGroupModel> textureGroups { get; set; }

        public GMGraphicsOptionsModel()
            : base("GMGraphicsOptions", "1.0")
        {

        }
    }

    [DataContract]
    internal class GMBaseGroupModel : ModelBase
    {
        [DataMember]
        public string groupName { get; set; }

        [DataMember]
        public TargetPlatforms targets { get; set; }

        public GMBaseGroupModel()
            : base("GMBaseGroup", "1.0")
        {

        }

        public GMBaseGroupModel(string modelName, string mvc)
            : base(modelName, mvc)
        {

        }
    }

    [Flags]
    internal enum TargetPlatforms : long
    {
        MacOsX = 2,
        iOS = 4,
        Android = 8,
        Html5 = 32,
        Windows = 64,
        Ubuntu = 128,
        WindowsPhone8 = 4096,
        SteamWorkshop = 16384,
        Windows8Javascript = 32768,
        TizenJavascript = 65536,
        Windows_YYC = 1048576,
        Android_YYC = 2097152,
        Windows8 = 4194304,
        TizenNative = 8388608,
        Tizen_YYC = 16777216,
        iOS_YYC = 33554432,
        MacOsX_YYC = 67108864,
        Ubuntu_YYC = 134217728,
        WindowsPhone8_YYC = 268435456,
        Windows8_YYC = 536870912,
        PSVita = 2147483648,
        PS4 = 4294967296,
        XboxOne = 34359738368,
        PSVita_YYC = 68719476736,
        PS4_YYC = 137438953472,
        XboxOne_YYC = 1099511627776,
        PS3 = 2199023255552,
        PS3_YYC = 4398046511104,
        GameMakerPlayer = 17592186044416,
        MicrosoftUAP = 35184372088832,
        MicrosoftUAP_YYC = 70368744177664,
        AndroidTV = 140737488355328,
        AndroidTV_YYC = 281474976710656,
        AmazonFireTV = 562949953421312,
        AmazonFireTV_YYC = 1125899906842624,
        tvOS = 9007199254740992,
        tvOS_YYC = 18014398509481984,
        AllPlatforms = tvOS_YYC | tvOS | AmazonFireTV_YYC | AmazonFireTV | AndroidTV_YYC | AndroidTV | MicrosoftUAP_YYC | MicrosoftUAP | GameMakerPlayer | PS3_YYC | PS3 | XboxOne_YYC | PS4_YYC | PSVita_YYC | XboxOne | PS4 | PSVita | Windows8_YYC | WindowsPhone8_YYC | Ubuntu_YYC | MacOsX_YYC | iOS_YYC | Tizen_YYC | TizenNative | Windows8 | Android_YYC | Windows_YYC | TizenJavascript | Windows8Javascript | SteamWorkshop | WindowsPhone8 | Ubuntu | Windows | Html5 | Android | iOS | MacOsX
    }

    [DataContract]
    internal sealed class GMTextureGroupModel : GMBaseGroupModel
    {
        [DataMember]
        public bool scaled { get; set; }

        [DataMember]
        public bool autocrop { get; set; }

        [DataMember]
        public int border { get; set; }

        [DataMember]
        public Guid groupParent { get; set; }

        [DataMember]
        public int mipsToGenerate { get; set; }

        public GMTextureGroupModel()
            : base("GMTextureGroup", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMAudioOptionsModel : GMOptionsBaseModel
    {
        [DataMember]
        public List<GMAudioGroupModel> audioGroups { get; set; }

        public GMAudioOptionsModel()
            : base("GMAudioOptions", "1.0")
        {

        }
    }

    internal sealed class GMAudioGroupModel : GMBaseGroupModel
    {
        public GMAudioGroupModel()
            : base("GMAudioGroup", "1.0")
        {

        }
    }

    [DataContract]
    internal sealed class Color
    {
        [DataMember]
        public uint Value { get; set; }
    }

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

    [DataContract]
    internal sealed class GMMacOptionsModel : GMOptionsBaseModel
    {
        [DataMember] public string option_mac_display_name;

        [DataMember]
        public string option_mac_app_id { get; set; }

        [DataMember]
        public BuildVersion option_mac_version { get; set; }

        [DataMember]
        public string option_mac_output_dir { get; set; }

        [DataMember]
        public string option_mac_team_id { get; set; }

        [DataMember]
        public string option_mac_signing_identity { get; set; }

        [DataMember]
        public string option_mac_copyright { get; set; }

        [DataMember]
        public string option_mac_splash_png { get; set; }

        [DataMember]
        public string option_mac_icon_png { get; set; }

        [DataMember]
        public bool option_mac_menu_dock { get; set; }

        [DataMember]
        public bool option_mac_display_cursor { get; set; }

        [DataMember]
        public bool option_mac_start_fullscreen { get; set; }

        [DataMember]
        public bool option_mac_allow_fullscreen { get; set; }

        [DataMember]
        public bool option_mac_interpolate_pixels { get; set; }

        [DataMember]
        public bool option_mac_vsync { get; set; }

        [DataMember]
        public bool option_mac_resize_window { get; set; }

        [DataMember]
        public int option_mac_scale { get; set; }

        [DataMember]
        public string option_mac_texture_page { get; set; }

        [DataMember]
        public bool option_mac_build_app_store { get; set; }

        [DataMember]
        public bool option_mac_allow_incoming_network { get; set; }

        [DataMember]
        public bool option_mac_allow_outgoing_network { get; set; }

        [DataMember]
        public string option_mac_app_category { get; set; }

        [DataMember]
        public bool option_mac_enable_steam { get; set; }

        public GMMacOptionsModel()
            : base("GMMacOptions", "1.0")
        {

        }
    }
}
