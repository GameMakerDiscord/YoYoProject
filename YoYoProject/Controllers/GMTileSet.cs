using System;
using System.Collections;
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

        public AutoTileSetManager AutoTileSets { get; }

        public AnimationsManager Animations { get; }

        public BrushManager Brushes { get; }

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
            AutoTileSets = new AutoTileSetManager(this);
            Animations = new AnimationsManager(this);
            Brushes = new BrushManager(this);
        }

        protected internal override void Create()
        {
            Name = Project.Resources.GenerateValidName("tileset");
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
                tile_animation_frames = Animations.Select(x => (GMTileAnimationFrameModel)x.Serialize())
                                                  .ToList(),
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

            public GMAutoTileSet Create()
            {
                var autoTileSet = new GMAutoTileSet
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

            public GMTileSetAnimation Create()
            {
                var animation = new GMTileSetAnimation
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

            protected internal override ModelBase Serialize()
            {
                int frameCount = 0;
                foreach (var animation in animations)
                    frameCount = Math.Max(frameCount, animation.Frames.Count);

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
                        uint key = animation.Frames[0];
                        for (int i = 0; i < frameCount; ++i)
                        {
                            int j = i % animation.Frames.Count;
                            frameData[key + j] = animation.Frames[j];
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

            public IEnumerator<GMTileSetAnimation> GetEnumerator()
            {
                return animations.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        // TODO Implement me
        // TODO Not a Manager lol
        public sealed class BrushManager
        {
            private uint[,] tiles;

            private readonly GMTileSet tileSet;

            internal BrushManager(GMTileSet tileSet)
            {
                if (tileSet == null)
                    throw new ArgumentNullException(nameof(tileSet));

                tiles = new uint[0, 0];
                this.tileSet = tileSet;
            }

            internal GMMacroPageTilesModel Serialize()
            {
                int width = tiles.GetLength(0);
                int height = tiles.GetLength(1);
                uint[] tileData = new uint[width * height];
                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                        tileData[y * width + x] = tiles[x, y];
                }

                return new GMMacroPageTilesModel
                {
                    SerialiseWidth = width,
                    SerialiseHeight = height,
                    TileSerialiseData = tileData
                };
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

        // TODO Make this usable...
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
