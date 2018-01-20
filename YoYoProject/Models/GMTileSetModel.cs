using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMTileSetModel : GMResourceModel
    {
        [DataMember]
        public int out_columns { get; set; }

        [DataMember]
        public Guid spriteId { get; set; }

        [DataMember]
        public int tilewidth { get; set; }

        [DataMember]
        public int tileheight { get; set; }

        [DataMember]
        public int tilexoff { get; set; }

        [DataMember]
        public int tileyoff { get; set; }

        [DataMember]
        public int tilehsep { get; set; }

        [DataMember]
        public int tilevsep { get; set; }

        [DataMember]
        public bool sprite_no_export { get; set; }

        [DataMember]
        public Guid textureGroupId { get; set; }

        [DataMember]
        public int out_tilehborder { get; set; }

        [DataMember]
        public int out_tilevborder { get; set; }

        [DataMember]
        public int tile_count { get; set; }

        [DataMember]
        public List<GMAutoTileSetModel> auto_tile_sets { get; set; }

        [DataMember]
        public List<GMTileAnimationFrameModel> tile_animation_frames { get; set; }

        [DataMember]
        public float tile_animation_speed { get; set; }

        [DataMember]
        public GMTileAnimationModel tile_animation { get; set; }

        [DataMember]
        public GMTileMapModel macroPageTiles { get; set; }
        
        public GMTileSetModel()
            : base("GMTileSet", "1.11")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMAutoTileSetModel : ModelBase
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public List<uint> tiles { get; set; }

        [DataMember]
        public bool closed_edge { get; set; }

        public GMAutoTileSetModel()
            : base("GMAutoTileSet", "1.0")
        {
            
        }
    }

    [DataContract]
    public sealed class GMTileAnimationFrameModel : ModelBase
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public List<uint> frames { get; set; }

        // NOTE Intentionally GMTileAnimation
        public GMTileAnimationFrameModel()
            : base("GMTileAnimation", "1.0")
        {
            
        }
    }

    [DataContract]
    public sealed class GMTileAnimationModel : ModelBase
    {
        [DataMember]
        public List<List<uint>> AnimationCreationOrder { get; set; }

        [DataMember]
        public uint[] FrameData { get; set; }

        [DataMember]
        public int SerialiseFrameCount { get; set; }
    }

    [DataContract]
    public sealed class GMTileMapModel : ModelBase
    {
        [DataMember]
        public int SerialiseWidth { get; set; }

        [DataMember]
        public int SerialiseHeight { get; set; }

        [DataMember]
        public uint[] SerialiseData { get; set; }

        [DataMember]
        public uint[] TileSerialiseData { get; set; }
    }
}
