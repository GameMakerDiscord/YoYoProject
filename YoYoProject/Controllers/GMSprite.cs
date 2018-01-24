using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using YoYoProject.Models;
using YoYoProject.Utility;
using Color = YoYoProject.Common.Color;

namespace YoYoProject.Controllers
{
    public sealed class GMSprite : GMResource
    {
        private GMSpriteBboxMode bboxMode;
        public GMSpriteBboxMode BboxMode
        {
            get { return GetProperty(bboxMode); }
            set { SetProperty(value, ref bboxMode); }
        }

        private GMSpriteColKind collisionKind;
        public GMSpriteColKind CollisionKind
        {
            get { return GetProperty(collisionKind); }
            set { SetProperty(value, ref collisionKind); }
        }

        private bool separateMasks;
        public bool SeparateMasks
        {
            get { return GetProperty(separateMasks); }
            set { SetProperty(value, ref separateMasks); }
        }

        private GMSpriteType type;
        public GMSpriteType Type
        {
            get { return GetProperty(type); }
            set { SetProperty(value, ref type); }
        }

        private bool premultiplyAlpha;
        public bool PremultiplyAlpha
        {
            get { return GetProperty(premultiplyAlpha); }
            set { SetProperty(value, ref premultiplyAlpha); }
        }

        private bool edgeFiltering;
        public bool EdgeFiltering
        {
            get { return GetProperty(edgeFiltering); }
            set { SetProperty(value, ref edgeFiltering); }
        }

        private int xOrigin;
        public int OriginX
        {
            get { return GetProperty(xOrigin); }
            set { SetProperty(value, ref xOrigin); }
        }

        private int yOrigin;
        public int OriginY
        {
            get { return GetProperty(yOrigin); }
            set { SetProperty(value, ref yOrigin); }
        }

        private uint collisonTolerance;
        public uint CollisonTolerance
        {
            get { return GetProperty(collisonTolerance); }
            set { SetProperty(value, ref collisonTolerance); }
        }

        private float swfPrecision;
        public float SwfPrecision
        {
            get { return GetProperty(swfPrecision); }
            set { SetProperty(value, ref swfPrecision); }
        }

        private int bboxLeft;
        public int BboxLeft
        {
            get { return GetProperty(bboxLeft); }
            set { SetProperty(value, ref bboxLeft); }
        }

        private int bboxRight;
        public int BboxRight
        {
            get { return GetProperty(bboxRight); }
            set { SetProperty(value, ref bboxRight); }
        }

        private int bboxTop;
        public int BboxTop
        {
            get { return GetProperty(bboxTop); }
            set { SetProperty(value, ref bboxTop); }
        }

        private int bboxBottom;
        public int BboxBottom
        {
            get { return GetProperty(bboxBottom); }
            set { SetProperty(value, ref bboxBottom); }
        }

        private bool horizontalTile;
        public bool HorizontalTile
        {
            get { return GetProperty(horizontalTile); }
            set { SetProperty(value, ref horizontalTile); }
        }

        private bool verticalTile;
        public bool VerticalTile
        {
            get { return GetProperty(verticalTile); }
            set { SetProperty(value, ref verticalTile); }
        }

        private bool for3D;
        public bool For3D
        {
            get { return GetProperty(for3D); }
            set { SetProperty(value, ref for3D); }
        }

        private bool originLocked;
        public bool OriginLocked
        {
            get { return GetProperty(originLocked); }
            set { SetProperty(value, ref originLocked); }
        }

        private GMTextureGroup textureGroup;
        public GMTextureGroup TextureGroup
        {
            get { return GetProperty(textureGroup); }
            set { SetProperty(value, ref textureGroup); }
        }

        private int width;
        public int Width
        {
            get { return GetProperty(width); }
            set { SetProperty(value, ref width); }
        }

        private int height;
        public int Height
        {
            get { return GetProperty(height); }
            set { SetProperty(value, ref height); }
        }

        private int gridX;
        public int GridX
        {
            get { return GetProperty(gridX); }
            set { SetProperty(value, ref gridX); }
        }

        private int gridY;
        public int GridY
        {
            get { return GetProperty(gridY); }
            set { SetProperty(value, ref gridY); }
        }
        
        public FrameManager Frames { get; }

        public LayerManager Layers { get; }

        private float playbackSpeed;
        public float PlaybackSpeed
        {
            get { return GetProperty(playbackSpeed); }
            set { SetProperty(value, ref playbackSpeed); }
        }

        private GMAnimationSpeedType playbackSpeedType;
        public GMAnimationSpeedType PlaybackSpeedType
        {
            get { return GetProperty(playbackSpeedType); }
            set { SetProperty(value, ref playbackSpeedType); }
        }

        private List<Color> swatchColors;
        public List<Color> SwatchColors
        {
            get { return GetProperty(swatchColors); }
            set { SetProperty(value, ref swatchColors); }
        }

        protected internal override string ResourcePath => $@"sprites\{Name}\{Name}.yy";

        protected internal override void Create()
        {
            Name = Project.Resources.GenerateValidName("sprite");
            BboxMode = GMSpriteBboxMode.Automatic;
            CollisionKind = GMSpriteColKind.Rectangle;
            SeparateMasks = false;
            Type = GMSpriteType.Bitmap;
            PremultiplyAlpha = false;
            EdgeFiltering = false;
            OriginX = 0;
            OriginY = 0;
            CollisonTolerance = 0;
            SwfPrecision = 2.525f;
            BboxLeft = 0;
            BboxRight = 0;
            BboxTop = 0;
            BboxBottom = 0;
            HorizontalTile = false;
            VerticalTile = false;
            For3D = false;
            OriginLocked = false;
            TextureGroup = Project.Resources.Get<GMMainOptions>().Graphics.DefaultTextureGroup;
            Width = 64;
            Height = 64;
            GridX = 0;
            GridY = 0;
            PlaybackSpeed = 15;
            PlaybackSpeedType = GMAnimationSpeedType.FramesPerSecond;
            SwatchColors = new List<Color>();

            Layers.Create();
        }

        public GMSprite()
        {
            Frames = new FrameManager(this);
            Layers = new LayerManager(this);
        }

        public void Resize(int w, int h)
        {
            if (w < 0)
                throw new ArgumentOutOfRangeException(nameof(w), w, "Cannot be less than 0");

            if (h < 0)
                throw new ArgumentOutOfRangeException(nameof(h), h, "Cannot be less than 0");

            if (w == Width && h == Height)
                return;

            Width = w;
            Height = h;

            foreach (var frame in Frames)
                frame.Resize(w, h);
        }
        
        internal override ModelBase Serialize()
        {
            return new GMSpriteModel
            {
                id = Id,
                name = Name,
                bboxmode = BboxMode,
                colkind = CollisionKind,
                sepmasks = SeparateMasks,
                type = Type,
                premultiplyAlpha = PremultiplyAlpha,
                edgeFiltering = EdgeFiltering,
                xorig = OriginX,
                yorig = OriginY,
                coltolerance = CollisonTolerance,
                swfPrecision = SwfPrecision,
                bbox_left = BboxLeft,
                bbox_right = BboxRight,
                bbox_top = BboxTop,
                bbox_bottom = BboxBottom,
                HTile = HorizontalTile,
                VTile = VerticalTile,
                For3D = For3D,
                originLocked = OriginLocked,
                textureGroupId = TextureGroup.Id,
                width = Width,
                height = Height,
                gridX = GridX,
                gridY = GridY,
                frames = Frames.Select(x => (GMSpriteFrameModel)x.Serialize()).ToList(),
                layers = Layers.Select(x => (GMSpriteImageLayerModel)x.Serialize()).ToList(),
                playbackSpeed = PlaybackSpeed,
                playbackSpeedType = PlaybackSpeedType,
                swatchColours = SwatchColors.Select(x => x.Value).ToList()
            };
        }

        public sealed class FrameManager : IReadOnlyList<GMSpriteFrame>
        {
            public int Count => frames.Count;

            public GMSpriteFrame this[int index] => frames[index];

            private readonly List<GMSpriteFrame> frames;
            private readonly GMSprite sprite;

            internal FrameManager(GMSprite sprite)
            {
                if (sprite == null)
                    throw new ArgumentNullException(nameof(sprite));

                frames = new List<GMSpriteFrame>();
                this.sprite = sprite;
            }

            public GMSpriteFrame Create()
            {
                var frame = new GMSpriteFrame(sprite)
                {
                    Id = Guid.NewGuid()
                };

                frames.Add(frame);

                return frame;
            }

            public void Delete(GMSpriteFrame frame)
            {
                frames.Remove(frame);
                // TODO Delete files on disk
            }

            public void Clear()
            {
                frames.Clear();
            }

            public IEnumerator<GMSpriteFrame> GetEnumerator()
            {
                return frames.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public sealed class LayerManager : IReadOnlyList<GMSpriteImageLayer>
        {
            public int Count => layers.Count;

            public GMSpriteImageLayer this[int index] => layers[index];

            private readonly List<GMSpriteImageLayer> layers;
            private readonly GMSprite sprite;

            public LayerManager(GMSprite sprite)
            {
                if (sprite == null)
                    throw new ArgumentNullException(nameof(sprite));

                layers = new List<GMSpriteImageLayer>();
                this.sprite = sprite;
            }

            public GMSpriteImageLayer Create()
            {
                var layer = new GMSpriteImageLayer(sprite)
                {
                    Id = Guid.NewGuid(),
                    Name = layers.Count == 0 ? "default" : $"Layer {layers.Count}"
                };

                layers.Insert(0, layer);

                foreach (var frame in sprite.Frames)
                    frame.layers.Insert(0, GMSpriteImage.Create(frame, layer));

                return layer;
            }

            public void Delete(GMSpriteImageLayer layer)
            {
                if (layer == null)
                    throw new ArgumentNullException(nameof(layer));

                layers.Remove(layer);

                foreach (var frame in sprite.Frames)
                    frame.layers.RemoveAll(x => x.Layer == layer);

                // TODO Delete files on disk
            }

            public int DepthOf(GMSpriteImageLayer layer)
            {
                return layers.IndexOf(layer);
            }

            public IEnumerator<GMSpriteImageLayer> GetEnumerator()
            {
                return layers.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }

    public sealed class GMSpriteFrame : ControllerBase
    {
        public GMSprite Sprite { get; }

        public GMSpriteImage CompositeImage { get; }

        // NOTE It's important that the order of this list be maintained by the LayerManager
        public IReadOnlyList<GMSpriteImage> Layers => layers;

        // ReSharper disable once InconsistentNaming
        internal readonly List<GMSpriteImage> layers;

        internal GMSpriteFrame(GMSprite sprite)
        {
            if (sprite == null)
                throw new ArgumentNullException(nameof(sprite));

            Sprite = sprite;
            CompositeImage = GMSpriteImage.Create(this, null);

            layers = new List<GMSpriteImage>();
            foreach (var layer in sprite.Layers)
                layers.Add(GMSpriteImage.Create(this, layer));
        }

        public void SetImage(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            
            var file = Image.FromFile(path);

            if (layers.Count > 0)
                layers[0].SetImage(file);

            Sprite.Resize(file.Width, file.Height);
        }

        internal void Resize(int width, int height)
        {
            CompositeImage.Resize(width, height);

            foreach (var image in layers)
                image.Resize(width, height);
        }

        internal override ModelBase Serialize()
        {
            // TODO Only do this if dirty
            CompositeImage.SetImage(GenerateCompositeImage());

            return new GMSpriteFrameModel
            {
                id = Id,
                SpriteId = Sprite.Id,
                compositeImage = (GMSpriteImageModel)CompositeImage.Serialize(),
                images = layers.Select(x => (GMSpriteImageModel)x.Serialize())
                               .ToList()
            };
        }

        private Image GenerateCompositeImage()
        {
            var compositeImage = new Bitmap(Sprite.Width, Sprite.Height);

            using (var graphics = Graphics.FromImage(compositeImage))
            {
                for (var i = layers.Count - 1; i >= 0; --i)
                {
                    var image = layers[i];
                    graphics.DrawImage(image.GetImage(), Point.Empty);
                }
            }

            return compositeImage;
        }
    }
    
    public sealed class GMSpriteImageLayer : ControllerBase
    {
        public GMSprite Sprite { get; }

        private string name;
        public string Name
        {
            get { return GetProperty(name); }
            set { SetProperty(value, ref name); }
        }

        private bool visible;
        public bool Visible
        {
            get { return GetProperty(visible); }
            set { SetProperty(value, ref visible); }
        }

        private bool isLocked;
        public bool IsLocked
        {
            get { return GetProperty(isLocked); }
            set { SetProperty(value, ref isLocked); }
        }

        private GMSpriteImageLayerBlendMode blendMode;
        public GMSpriteImageLayerBlendMode BlendMode
        {
            get { return GetProperty(blendMode); }
            set { SetProperty(value, ref blendMode); }
        }

        private float opacity;
        public float Opacity
        {
            get { return GetProperty(opacity); }
            set
            {
                if (value < 0)
                    SetProperty(0, ref opacity);
                else if (value > 100)
                    SetProperty(100, ref opacity);
                else
                    SetProperty(value, ref opacity);
            }
        }

        internal GMSpriteImageLayer(GMSprite sprite)
        {
            if (sprite == null)
                throw new ArgumentNullException(nameof(sprite));

            Sprite = sprite;
            Id = Guid.NewGuid();
            Name = "";
            Visible = true;
            IsLocked = false;
            BlendMode = GMSpriteImageLayerBlendMode.Normal;
            Opacity = 100f;
        }

        internal override ModelBase Serialize()
        {
            return new GMSpriteImageLayerModel
            {
                id = Id,
                SpriteId = Sprite.Id,
                name = Name,
                visible = Visible,
                isLocked = IsLocked,
                blendMode = BlendMode,
                opacity = Opacity
            };
        }
    }

    public sealed class GMSpriteImage : ControllerBase
    {
        public GMSpriteFrame Frame { get; }

        public GMSpriteImageLayer Layer { get; }

        private GMSprite Sprite => Frame.Sprite;

        private string ImagePath =>
            Path.Combine(Frame.Sprite.Project.RootDirectory, Layer == null
                ? $@"sprites\{Frame.Sprite.Name}\{Frame.Id}.png"
                : $@"sprites\{Frame.Sprite.Name}\layers\{Frame.Id}\{Layer.Id}.png"
            );

        // NOTE We want to try and keep access to this restricted to internal because we need to manage
        //      the caching of image data, plus restrict what kind of operations can be taken (ex: resizing)
        private Image image;

        private GMSpriteImage(GMSpriteFrame frame, GMSpriteImageLayer layer)
        {
            if (frame == null)
                throw new ArgumentNullException(nameof(frame));

            Frame = frame;
            Layer = layer;
            image = null;
        }

        // TODO Need to be able to SetImage for specific layers

        public void SetImage(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            SetImage(Image.FromFile(path));
        }

        public void SetImage(Image img)
        {
            if (img == null)
                throw new ArgumentNullException(nameof(img));

            image = img;
            Sprite.Resize(image.Width, image.Height);
        }

        internal Image GetImage()
        {
            EnsureImageLoaded();
            return image;
        }

        // TODO Image manip methods

        // https://stackoverflow.com/a/24199315
        internal void Resize(int width, int height)
        {
            EnsureImageLoaded();

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            image = destImage;
        }

        internal override ModelBase Serialize()
        {
            string imageDirectory = Path.GetDirectoryName(ImagePath);
            FileSystem.EnsureDirectory(imageDirectory);

            // TODO Unload OnSaveComplete
            image?.Save(ImagePath, ImageFormat.Png);

            return new GMSpriteImageModel
            {
                id = Id,
                LayerId = Layer?.Id ?? Guid.Empty,
                FrameId = Frame.Id
            };
        }

        private void EnsureImageLoaded()
        {
            if (image != null)
                return;

            SetImage(Path.Combine(Project.RootDirectory, ImagePath));
        }

        internal void Unload()
        {
            image = null;
        }

        internal static GMSpriteImage Create(GMSpriteFrame frame, GMSpriteImageLayer layer)
        {
            return new GMSpriteImage(frame, layer)
            {
                Id = Guid.NewGuid(),
                image = new Bitmap(frame.Sprite.Width, frame.Sprite.Height)
            };
        }
    }
}
