using System;

namespace YoYoProject.Common
{
    [Flags]
    public enum TargetPlatforms : long
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
}