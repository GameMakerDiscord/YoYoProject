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

        public int TileCount
        {
            get
            {
                if (Sprite == null)
                    return 0;

                return (Sprite.Width / TileWidth) * (Sprite.Height / TileHeight);
            }
        }
        
        protected internal override string ResourcePath => $@"tilesets\{Name}\{Name}.yy";

        public GMTileSet()
        {
            AutoTileSets = new List<GMAutoTileSet>();
            Animation = new GMTileSetAnimation(this);
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

        internal int GetTileIndex(int x, int y)
        {
            if (Sprite == null)
                return 0;

            int stride = Sprite.Width / TileWidth;
            x = Math.Min(Math.Max(x, 0), Sprite.Width / TileWidth);
            y = Math.Min(Math.Max(y, 0), Sprite.Height / TileHeight);

            return y * stride + x;
        }

        protected internal override ModelBase Serialize()
        {
            int outColumns = 1;
            int tileCount = 0;

            if (Sprite != null)
            {
                outColumns = Sprite.Width / (OutTileBorderHorizontal * 2 + TileWidth);
                tileCount = TileCount;
            }

            return new GMTileSetModel
            {
                id = Id,
                name = Name,
                out_columns = outColumns,
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
                tile_count = tileCount,
                auto_tile_set = AutoTileSets.Select(x => (GMAutoTileSetModel)x.Serialize()).ToList(),
                tile_animation_frames = Animation.Animations
                                                 .Select(x => (GMTileAnimationFrameModel)x.Serialize())
                                                 .ToList(),
                tile_animation_speed = Animation.Speed,
                tile_animation = (GMTileAnimationModel)Animation.Serialize(),
                macroPageTiles = null, // TODO Implement
            };
        }
    }

    // TODO Implement some API to make this actually usable...
    public sealed class GMAutoTileSet : ControllerBase
    {
        private string name;
        public string Name
        {
            get { return GetProperty(name); }
            set { SetProperty(value, ref name); }
        }
        
        private List<uint> tiles;
        public List<uint> Tiles
        {
            get { return GetProperty(tiles); }
            set { SetProperty(value, ref tiles); }
        }
        
        private bool closedEdge;
        public bool ClosedEdge
        {
            get { return GetProperty(closedEdge); }
            set { SetProperty(value, ref closedEdge); }
        }

        internal GMAutoTileSet()
        {
            Tiles = new List<uint>();
        }

        protected internal override ModelBase Serialize()
        {
            return new GMAutoTileSetModel
            {
                id = Id,
                name = Name,
                closed_edge = ClosedEdge
            };
        }
    }

    public sealed class GMTileSetAnimation : ControllerBase
    {
        public List<Animation> Animations { get; }

        public float Speed { get; set; }

        private readonly GMTileSet tileSet;

        internal GMTileSetAnimation(GMTileSet tileSet)
        {
            if (tileSet == null)
                throw new ArgumentNullException(nameof(tileSet));

            Animations = new List<Animation>();

            this.tileSet = tileSet;
        }

        protected internal override ModelBase Serialize()
        {
            int frameCount = Animations.Max(x => x.Frames.Count);
            uint[] frameData = new uint[tileSet.TileCount * frameCount];
            for (int i = 0; i < tileSet.TileCount; ++i)
            {
                for (int j = 0; j < frameCount; ++j)
                    frameData[i * frameCount + j] = (uint)i; // TODO Unless overrides are found in Animation.Frames
            }

            return new GMTileAnimationModel
            {
                AnimationCreationOrder = null, // TODO What is this?
                FrameData = frameData,
                SerialiseFrameCount = frameCount
            };
        }

        public sealed class Animation : ControllerBase
        {
            private string name;
            public string Name
            {
                get { return GetProperty(name); }
                set { SetProperty(value, ref name); }
            }
            
            // TODO Make this usable...
            private List<uint> frames;
            public List<uint> Frames
            {
                get { return GetProperty(frames); }
                set { SetProperty(value, ref frames); }
            }

            protected internal override ModelBase Serialize()
            {
                return new GMTileAnimationFrameModel
                {
                    id = Id,
                    name = Name,
                    frames = Frames
                };
            }
        }
    }
}
