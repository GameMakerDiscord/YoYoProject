using System;
using System.Collections.Generic;
using System.Linq;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMTileSet : GMResource
    {
        private GMSprite sprite;
        public GMSprite Sprite
        {
            get { return GetProperty(sprite); }
            set { SetProperty(value, ref sprite); }
        }

        private int tileWidth;
        public int TileWidth
        {
            get { return GetProperty(tileWidth); }
            set { SetProperty(value, ref tileWidth); }
        }

        private int tileHeight;
        public int TileHeight
        {
            get { return GetProperty(tileHeight); }
            set { SetProperty(value, ref tileHeight); }
        }

        private int tileOffsetX;
        public int TileOffsetX
        {
            get { return GetProperty(tileOffsetX); }
            set { SetProperty(value, ref tileOffsetX); }
        }

        private int tileOffsetY;
        public int TileOffsetY
        {
            get { return GetProperty(tileOffsetY); }
            set { SetProperty(value, ref tileOffsetY); }
        }

        private int tileSeparationHorizontal;
        public int TileSeparationHorizontal
        {
            get { return GetProperty(tileSeparationHorizontal); }
            set { SetProperty(value, ref tileSeparationHorizontal); }
        }

        private int tileSeparationVertical;
        public int TileSeparationVertical
        {
            get { return GetProperty(tileSeparationVertical); }
            set { SetProperty(value, ref tileSeparationVertical); }
        }
        
        private bool noExport;
        public bool NoExport
        {
            get { return GetProperty(noExport); }
            set { SetProperty(value, ref noExport); }
        }

        private GMTextureGroup textureGroup;
        public GMTextureGroup TextureGroup
        {
            get { return GetProperty(textureGroup); }
            set { SetProperty(value, ref textureGroup); }
        }

        private int outTileBorderHorizontal;
        public int OutTileBorderHorizontal
        {
            get { return GetProperty(outTileBorderHorizontal); }
            set { SetProperty(value, ref outTileBorderHorizontal); }
        }

        private int outTileBorderVertical;
        public int OutTileBorderVertical
        {
            get { return GetProperty(outTileBorderVertical); }
            set { SetProperty(value, ref outTileBorderVertical); }
        }

        private List<GMAutoTileSet> autoTileSets;
        public List<GMAutoTileSet> AutoTileSets
        {
            get { return GetProperty(autoTileSets); }
            set { SetProperty(value, ref autoTileSets); }
        }

        private GMTileSetAnimation animation;
        public GMTileSetAnimation Animation
        {
            get { return GetProperty(animation); }
            set { SetProperty(value, ref animation); }
        }
        
        protected internal override string ResourcePath => $@"tilesets\{Name}\{Name}.yy";

        public GMTileSet()
        {
            AutoTileSets = new List<GMAutoTileSet>();
            Animation = new GMTileSetAnimation();
        }

        protected internal override void Create()
        {
            Sprite = null;
            TileWidth = 16;
            TileHeight = 16;
            TileOffsetX = 0;
            TileOffsetY = 0;
            TileSeparationHorizontal = 0;
            TileSeparationVertical = 0;
            NoExport = false;
            TextureGroup = Project.Resources.Get<GMMainOptions>().Graphics.DefaultTextureGroup;
            OutTileBorderHorizontal = 2;
            OutTileBorderVertical = 2;
        }

        protected internal override ModelBase Serialize()
        {
            return new GMTileSetModel
            {
                id = Id,
                name = Name,
                out_columns = 0, // TODO IMPLEMENT
                spriteId = Sprite?.Id ?? Guid.Empty,
                tilewidth = TileWidth,
                tileheight = TileHeight,
                tilexoff = TileOffsetX,
                tileyoff = TileOffsetY,
                tilehsep = TileSeparationHorizontal,
                tilevsep = TileSeparationVertical,
                sprite_no_export = NoExport,
                textureGroupId = TextureGroup?.Id ?? Guid.Empty,
                out_tilehborder = OutTileBorderHorizontal,
                out_tilevborder = OutTileBorderVertical,
                tile_count = 0, // TODO IMPLEMENT
                auto_tile_set = AutoTileSets.Select(x => (GMAutoTileSetModel)x.Serialize()).ToList(),
                tile_animation_frames = Animation.Frames
                                                 .Select(x => (GMTileAnimationFrameModel)x.Serialize())
                                                 .ToList(),
                tile_animation_speed = Animation.Speed,
                tile_animation = (GMTileAnimationModel)Animation.Serialize(),
                macroPageTiles = null, // TODO Implement
            };
        }
    }

    public sealed class GMAutoTileSet : ControllerBase
    {
        protected internal override ModelBase Serialize()
        {
            throw new NotImplementedException();
        }
    }

    public sealed class GMTileSetAnimation : ControllerBase
    {
        public List<Frame> Frames { get; }

        public float Speed { get; set; }

        internal GMTileSetAnimation()
        {
            Frames = new List<Frame>();
        }

        protected internal override ModelBase Serialize()
        {
            throw new NotImplementedException();
        }

        public sealed class Frame : ControllerBase
        {
            public string Name { get; set; }

            public List<uint> frames { get; set; }

            protected internal override ModelBase Serialize()
            {
                throw new NotImplementedException();
            }
        }
    }
}
