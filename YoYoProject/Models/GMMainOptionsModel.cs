using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject
{
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

        [DataMember]
        public bool option_spine_licence { get; set; }

        public GMMainOptionsModel()
            : base("GMMainOptions", "1.0")
        {

        }
    }
}