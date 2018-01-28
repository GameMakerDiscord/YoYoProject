using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public bool Export
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

        public AutoTileSetManager AutoTileSets { get; }

        public AnimationsManager Animations { get; }

        public TileMap Brushes { get; }

        public int TileCount
        {
            get
            {
                if (Sprite == null)
                    return 0;

                return (Sprite.Width / TileWidth) * (Sprite.Height / TileHeight);
            }
        }
        
        internal override string ResourcePath => $@"tilesets\{Name}\{Name}.yy";

        public GMTileSet()
        {
            AutoTileSets = new AutoTileSetManager(this);
            Animations = new AnimationsManager(this);
            Brushes = new TileMap();
        }

        internal override void Create(string name)
        {
            Name = Project.Resources.GenerateValidName(name ?? "tileset");
            Sprite = null;
            TileWidth = 16;
            TileHeight = 16;
            TileOffsetX = 0;
            TileOffsetY = 0;
            TileSeparationHorizontal = 0;
            TileSeparationVertical = 0;
            Export = true;
            TextureGroup = Project.Resources.Get<GMMainOptions>().Graphics.DefaultTextureGroup;
            OutTileBorderHorizontal = 2;
            OutTileBorderVertical = 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal uint GetTileIndex(Tile tile)
        {
            return GetTileIndex(tile.X, tile.Y);
        }

        internal uint GetTileIndex(int x, int y)
        {
            if (Sprite == null)
                return 0;

            int stride = Sprite.Width / TileWidth;
            return (uint)(y * stride + x);
        }

        internal override ModelBase Serialize()
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
                sprite_no_export = Export,
                textureGroupId = TextureGroup?.Id ?? Guid.Empty,
                out_tilehborder = OutTileBorderHorizontal,
                out_tilevborder = OutTileBorderVertical,
                tile_count = tileCount,
                auto_tile_sets = AutoTileSets.Serialize(),
                tile_animation_frames = Animations.SerializeFrames(),
                tile_animation_speed = Animations.Speed,
                tile_animation = (GMTileAnimationModel)Animations.Serialize(),
                macroPageTiles = Brushes.Serialize()
            };
        }

        public sealed class AutoTileSetManager : IReadOnlyList<GMAutoTileSet>
        {
            public int Count => autoTileSets.Count;

            public GMAutoTileSet this[int index] => autoTileSets[index];

            private readonly List<GMAutoTileSet> autoTileSets;
            private readonly GMTileSet tileSet;

            internal AutoTileSetManager(GMTileSet tileSet)
            {
                if (tileSet == null)
                    throw new ArgumentNullException(nameof(tileSet));

                autoTileSets = new List<GMAutoTileSet>();

                this.tileSet = tileSet;
            }

            public GMAutoTileSet Create(GMAutoTileSetType type)
            {
                var autoTileSet = new GMAutoTileSet(type, tileSet)
                {
                    Id = Guid.NewGuid(),
                    Name = $"autotile_{autoTileSets.Count + 1}"
                };

                autoTileSets.Add(autoTileSet);

                return autoTileSet;
            }

            public void Delete(GMAutoTileSet autoTileSet)
            {
                autoTileSets.Remove(autoTileSet);
            }

            internal List<GMAutoTileSetModel> Serialize()
            {
                return autoTileSets.Select(x => (GMAutoTileSetModel)x.Serialize()).ToList();
            }

            public IEnumerator<GMAutoTileSet> GetEnumerator()
            {
                return autoTileSets.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public sealed class AnimationsManager : ControllerBase, IReadOnlyList<GMTileSetAnimation>
        {
            private float speed;
            public float Speed
            {
                get { return GetProperty(speed); }
                set { SetProperty(value, ref speed); }
            }

            public int Count => animations.Count;

            public GMTileSetAnimation this[int index] => animations[index];

            private readonly List<GMTileSetAnimation> animations;
            private readonly GMTileSet tileSet;

            internal AnimationsManager(GMTileSet tileSet)
            {
                if (tileSet == null)
                    throw new ArgumentNullException(nameof(tileSet));

                animations = new List<GMTileSetAnimation>();

                this.tileSet = tileSet;
            }

            public GMTileSetAnimation Create(int frameCount)
            {
                var animation = new GMTileSetAnimation(frameCount, tileSet)
                {
                    Id = Guid.NewGuid(),
                    Name = $"animation_{animations.Count + 1}"
                };

                animations.Add(animation);

                return animation;
            }

            public void Delete(GMTileSetAnimation animation)
            {
                animations.Remove(animation);
            }

            internal override ModelBase Serialize()
            {
                int frameCount = 0;
                foreach (var animation in animations)
                    frameCount = Math.Max(frameCount, animation.Frames.Length);

                uint[] frameData = new uint[tileSet.TileCount * frameCount];
                if (frameCount > 0)
                {
                    for (int i = 0; i < tileSet.TileCount; ++i)
                    {
                        for (int j = 0; j < frameCount; ++j)
                            frameData[i * frameCount + j] = (uint)i;
                    }

                    // TODO Handle collisions
                    // NOTE More research is required but it looks like GMS will discard subsequent animations if
                    //      the first frame collides with the first frame of any previous animation. Furthermore,
                    //      it also appears that it will discard individual frames of any animations that collide
                    //      with any frames of a previous animation as well.
                    foreach (var animation in animations)
                    {
                        uint key = tileSet.GetTileIndex(animation.Frames[0]);
                        for (int i = 0; i < frameCount; ++i)
                        {
                            int j = i % animation.Frames.Length;
                            frameData[key + j] = tileSet.GetTileIndex(animation.Frames[j]);
                        }
                    }
                }

                return new GMTileAnimationModel
                {
                    AnimationCreationOrder = null,          // NOTE Deprecated
                    FrameData = frameData,
                    SerialiseFrameCount = frameCount
                };
            }

            internal List<GMTileAnimationFrameModel> SerializeFrames()
            {
                return animations.Select(x => (GMTileAnimationFrameModel)x.Serialize()).ToList();
            }

            public IEnumerator<GMTileSetAnimation> GetEnumerator()
            {
                return animations.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }

    public sealed class GMAutoTileSet : ControllerBase
    {
        private string name;
        public string Name
        {
            get { return GetProperty(name); }
            set { SetProperty(value, ref name); }
        }
        
        // TODO Provide constants to reference each tile's purpose (TopLeft, Top, etc...)
        private Tile[] tiles;
        public Tile[] Tiles
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

        private readonly GMTileSet tileSet;

        internal GMAutoTileSet(GMAutoTileSetType type, GMTileSet tileSet)
        {
            if (tileSet == null)
                throw new ArgumentNullException(nameof(tileSet));

            int length = type == GMAutoTileSetType.Full ? 47 : 16;
            Tiles = new Tile[length];

            this.tileSet = tileSet;
        }

        internal override ModelBase Serialize()
        {
            return new GMAutoTileSetModel
            {
                id = Id,
                name = Name,
                closed_edge = ClosedEdge,
                tiles = Tiles.Select(x => tileSet.GetTileIndex(x)).ToList()
            };
        }
    }

    public sealed class GMTileSetAnimation : ControllerBase
    {
        private string name;
        public string Name
        {
            get { return GetProperty(name); }
            set { SetProperty(value, ref name); }
        }
        
        private Tile[] frames;
        public Tile[] Frames
        {
            get { return GetProperty(frames); }
            set { SetProperty(value, ref frames); }
        }

        private readonly GMTileSet tileSet;

        internal GMTileSetAnimation(int frameCount, GMTileSet tileSet)
        {
            if (tileSet == null)
                throw new ArgumentNullException(nameof(tileSet));

            // TODO Validate 2, 4, 8, 16, 32, 64, 128, 256?
            if (frameCount < 0)
                throw new ArgumentOutOfRangeException(nameof(frameCount), frameCount, "frameCount cannot be less than 0");

            Frames = new Tile[frameCount];

            this.tileSet = tileSet;
        }

        internal override ModelBase Serialize()
        {
            return new GMTileAnimationFrameModel
            {
                id = Id,
                name = Name,
                frames = Frames.Select(x => tileSet.GetTileIndex(x)).ToList()
            };
        }
    }
    
    public struct Tile
    {
        public int X { get; set; }

        public int Y { get; set; }

        public void Set(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public enum GMAutoTileSetType
    {
        Transitional, // 16
        Full // 47
    }
}
