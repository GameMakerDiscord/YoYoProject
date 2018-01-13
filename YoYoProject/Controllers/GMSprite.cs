using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // TODO Make this a manager or a IReadOnlyList
        private List<GMSpriteFrame> frames;
        public List<GMSpriteFrame> Frames
        {
            get { return GetProperty(frames); }
            set { SetProperty(value, ref frames); }
        }

        // TODO Make this a manager or a IReadOnlyList
        private List<GMSpriteImageLayer> layers;
        public List<GMSpriteImageLayer> Layers
        {
            get { return GetProperty(layers); }
            set { SetProperty(value, ref layers); }
        }

        private float playbackSpeed;
        public float PlaybackSpeed
        {
            get { return GetProperty(playbackSpeed); }
            set { SetProperty(value, ref playbackSpeed); }
        }

        private GMSpritePlaybackSpeedType playbackSpeedType;
        public GMSpritePlaybackSpeedType PlaybackSpeedType
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

        public GMSprite()
        {
            Name = "sprite0"; // TODO Generate non-conflicting name
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
            TextureGroup = null; // TODO Resolve default TextureGroup
            Width = 64;
            Height = 64;
            GridX = 0;
            GridY = 0;
            Frames = new List<GMSpriteFrame>();
            Layers = new List<GMSpriteImageLayer>();
            PlaybackSpeed = 15;
            PlaybackSpeedType = GMSpritePlaybackSpeedType.FramesPerSecond;
            SwatchColors = new List<Color>();

            CreateLayer();
        }

        public GMSpriteImageLayer CreateLayer()
        {
            var layer = new GMSpriteImageLayer(this)
            {
                Id = Guid.NewGuid(),
                Name = Layers.Count == 0 ? "default" : $"Layer {Layers.Count}"
            };

            Layers.Add(layer);

            return layer;
        }

        public GMSpriteFrame CreateFrame()
        {
            var frame = new GMSpriteFrame(this)
            {
                Id = Guid.NewGuid()
            };

            Frames.Add(frame);

            return frame;
        }

        protected internal override ModelBase Serialize()
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
    }

    public sealed class GMSpriteFrame : ControllerBase
    {
        public GMSprite Sprite { get; }

        public GMSpriteImage CompositeImage { get; }

        public List<GMSpriteImage> Images { get; }

        internal GMSpriteFrame(GMSprite sprite)
        {
            if (sprite == null)
                throw new ArgumentNullException(nameof(sprite));

            Sprite = sprite;
            CompositeImage = new GMSpriteImage(this, null)
            {
                Id = Guid.NewGuid()
            };

            Images = new List<GMSpriteImage>();

            foreach (var layer in sprite.Layers)
            {
                Images.Add(new GMSpriteImage(this, layer)
                {
                    Id = Guid.NewGuid()
                });
            }
        }

        public void SetImage(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            // TODO Handle resizing images correctly

            var file = Image.FromFile(path);
            Sprite.Width = file.Width;
            Sprite.Height = file.Height;

            CompositeImage.SetImage(file);

            // TODO What do when multiple layers?
            foreach (var image in Images)
                image.SetImage(file);
        }

        protected internal override ModelBase Serialize()
        {
            return new GMSpriteFrameModel
            {
                id = Id,
                SpriteId = Sprite.Id,
                compositeImage = (GMSpriteImageModel)CompositeImage.Serialize(),
                images = Images.Select(x => (GMSpriteImageModel)x.Serialize()).ToList()
            };
        }
    }

    public sealed class GMSpriteImage : ControllerBase
    {
        public GMSpriteFrame Frame { get; }

        public GMSpriteImageLayer Layer { get; }

        private string ImagePath =>
            Path.Combine(Frame.Sprite.Project.RootDirectory, Layer == null
                ? $@"sprites\{Frame.Sprite.Name}\{Frame.Id}.png"
                : $@"sprites\{Frame.Sprite.Name}\layers\{Frame.Id}\{Layer.Id}.png"
            );

        private Image image;

        internal GMSpriteImage(GMSpriteFrame frame, GMSpriteImageLayer layer)
        {
            if (frame == null)
                throw new ArgumentNullException(nameof(frame));

            Frame = frame;
            Layer = layer;
            image = null;

            //if (!File.Exists(ImagePath))
                image = new Bitmap(Frame.Sprite.Width, Frame.Sprite.Height);
        }

        public void SetImage(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            image = Image.FromFile(path);
        }

        public void SetImage(Image img)
        {
            if (img == null)
                throw new ArgumentNullException(nameof(img));

            image = img;
        }
        
        // TODO Image manip methods

        protected internal override ModelBase Serialize()
        {
            string imageDirectory = Path.GetDirectoryName(ImagePath);
            FileSystem.EnsureDirectory(imageDirectory);

            image?.Save(ImagePath, ImageFormat.Png);

            return new GMSpriteImageModel
            {
                id = Id,
                LayerId = Layer?.Id ?? Guid.Empty,
                FrameId = Frame.Id
            };
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
            set { SetProperty(value, ref opacity); }
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

        protected internal override ModelBase Serialize()
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
}
