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

        private int originX;
        public int OriginX
        {
            get { return GetProperty(originX); }
            set { SetProperty(value, ref originX); }
        }

        private int originY;
        public int OriginY
        {
            get { return GetProperty(originY); }
            set { SetProperty(value, ref originY); }
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

        internal override string ResourcePath => $@"sprites\{Name}\{Name}.yy";

        public GMSprite()
        {
            Frames = new FrameManager(this);
            Layers = new LayerManager(this);
            SwatchColors = new List<Color>();
        }

        internal override void Create(string name)
        {
            Name = Project.Resources.GenerateValidName(name ?? "sprite");
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

            AddResourceToFolder("GMSprite");
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

        internal override void Deserialize(ModelBase model)
        {
            var sprite = (GMSpriteModel)model;

            Name = sprite.name;
            BboxMode = sprite.bboxmode;
            CollisionKind = sprite.colkind;
            SeparateMasks = sprite.sepmasks;
            Type = sprite.type;
            PremultiplyAlpha = sprite.premultiplyAlpha;
            EdgeFiltering = sprite.edgeFiltering;
            OriginX = sprite.xorig;
            OriginY = sprite.yorig;
            CollisonTolerance = sprite.coltolerance;
            SwfPrecision = sprite.swfPrecision;
            BboxLeft = sprite.bbox_left;
            BboxRight = sprite.bbox_right;
            BboxTop = sprite.bbox_top;
            BboxBottom = sprite.bbox_bottom;
            HorizontalTile = sprite.HTile;
            VerticalTile = sprite.VTile;
            For3D = sprite.For3D;
            OriginLocked = sprite.originLocked;
            TextureGroup = null;
            Width = sprite.width;
            Height = sprite.height;
            GridX = sprite.gridX;
            GridY = sprite.gridY;
            Layers.Deserialize(sprite.layers);
            Frames.Deserialize(sprite.frames);
            PlaybackSpeed = sprite.playbackSpeed;
            PlaybackSpeedType = sprite.playbackSpeedType;
            SwatchColors = sprite.swatchColours?.Select(x => new Color(x)).ToList() ?? new List<Color>();
        }

        internal override void FinalizeDeserialization(ModelBase model)
        {
            var spriteModel = (GMSpriteModel)model;
            var tgGuid = spriteModel.textureGroupId;
            if (tgGuid == Guid.Empty)
                TextureGroup = Project.Resources.Get<GMMainOptions>().Graphics.DefaultTextureGroup;
            else
                TextureGroup = Project.Resources.Get<GMMainOptions>().Graphics.TextureGroups.Find(g => g.Id == tgGuid);
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

            public GMSpriteFrame Get(Guid id)
            {
                return frames.FirstOrDefault(x => x.Id == id);
            }

            public void Delete(GMSpriteFrame frame)
            {
                frames.Remove(frame);
                string framepath = Path.Combine(frame.Project.RootDirectory, "sprites", frame.Sprite.Name, frame.Id.ToString(), ".png");
                try
                {
                    File.Delete(framepath);
                } catch { }
                foreach (var lr in frame.Layers)
                {
                    string layerpath = Path.Combine(frame.Project.RootDirectory, "sprites", frame.Sprite.Name, "layers", frame.Id.ToString(), lr.Layer.ToString(), ".png");
                    try
                    {
                        File.Delete(layerpath);
                    } catch { }
                }
                // TODO Delete files on disk
                // Still unfinished...
            }

            public void Clear()
            {
                frames.Clear();
            }

            internal void Deserialize(List<GMSpriteFrameModel> model)
            {
                foreach (var modelFrame in model)
                {
                    var frame = new GMSpriteFrame(sprite)
                    {
                        Id = modelFrame.id
                    };

                    frame.Deserialize(modelFrame);
                    frames.Add(frame);
                }
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

            public GMSpriteImageLayer Get(Guid id)
            {
                return layers.FirstOrDefault(x => x.Id == id);
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

            internal void Deserialize(List<GMSpriteImageLayerModel> modelLayers)
            {
                foreach (var modelLayer in modelLayers)
                {
                    var layer = new GMSpriteImageLayer(sprite);
                    layer.Deserialize(modelLayer);

                    layers.Add(layer);
                }
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

        public GMSpriteImage CompositeImage { get; private set; }

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

        public void SetImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            if (layers.Count > 0)
                layers[0].SetImage(image);

            Sprite.Resize(image.Width, image.Height);
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

        internal override void Deserialize(ModelBase model)
        {
            var frame = (GMSpriteFrameModel)model;

            Id = frame.id;
            CompositeImage = GMSpriteImage.FromExisting(
                frame.compositeImage.id,
                this,
                Sprite.Layers.Get(frame.compositeImage.LayerId)
            );

            foreach (var imageModel in frame.images)
            {
                var image = GMSpriteImage.FromExisting(
                    imageModel.id,
                    this,
                    Sprite.Layers.Get(imageModel.LayerId)
                );

                layers.Add(image);
            }
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

        internal override void Deserialize(ModelBase model)
        {
            var layer = (GMSpriteImageLayerModel)model;

            Id = layer.id;
            Name = layer.name;
            Visible = layer.visible;
            IsLocked = layer.isLocked;
            BlendMode = layer.blendMode;
            Opacity = layer.opacity;
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

        internal static GMSpriteImage FromExisting(Guid id, GMSpriteFrame frame, GMSpriteImageLayer layer)
        {
            return new GMSpriteImage(frame, layer)
            {
                Id = id
            };
        }
    }
}
