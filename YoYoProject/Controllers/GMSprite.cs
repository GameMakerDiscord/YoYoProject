using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoYoProject.Common;
using YoYoProject.Models;

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
        public int XOrigin
        {
            get { return GetProperty(xOrigin); }
            set { SetProperty(value, ref xOrigin); }
        }

        private int yOrigin;
        public int YOrigin
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

        private List<GMSpriteFrame> frames;
        public List<GMSpriteFrame> Frames
        {
            get { return GetProperty(frames); }
            set { SetProperty(value, ref frames); }
        }

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
            XOrigin = 0;
            YOrigin = 0;
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
            Width = 0;
            Height = 0;
            GridX = 0;
            GridY = 0;
            Frames = new List<GMSpriteFrame>();
            Layers = new List<GMSpriteImageLayer>();
            PlaybackSpeed = 15;
            PlaybackSpeedType = GMSpritePlaybackSpeedType.FramesPerSecond;
            SwatchColors = new List<Color>();
        }

        public GMSpriteImageLayer CreateLayer()
        {
            throw new NotImplementedException();
        }

        public GMSpriteFrame CreateFrame()
        {
            throw new NotImplementedException();
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
                xorig = XOrigin,
                yorig = YOrigin,
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
        public GMSprite Sprite { get; private set; }

        public GMSpriteImage CompositeImage { get; set; }

        public List<GMSpriteImage> Images { get; set; }

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
        public GMSpriteImageLayer Layer { get; private set; }

        public GMSpriteFrame Frame { get; private set; }

        // TODO Image manip here

        protected internal override ModelBase Serialize()
        {
            return new GMSpriteImageModel
            {
                id = Id,
                LayerId = Layer.Id,
                FrameId = Frame.Id
            };
        }
    }

    public sealed class GMSpriteImageLayer : ControllerBase
    {
        public GMSprite Sprite { get; private set; }

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
