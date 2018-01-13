using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMSpriteModel : GMResourceModel
    {
        [DataMember]
        public GMSpriteBboxMode bboxmode { get; set; }

        [DataMember]
        public GMSpriteColKind colkind{ get; set; }

        [DataMember]
        public bool sepmasks { get; set; }

        [DataMember]
        public GMSpriteType type { get; set; }

        [DataMember]
        public bool premultiplyAlpha { get; set; }

        [DataMember]
        public bool edgeFiltering { get; set; }

        [DataMember]
        public int xorig { get; set; }

        [DataMember]
        public int yorig { get; set; }

        [DataMember]
        public uint coltolerance { get; set; }

        [DataMember]
        public float swfPrecision { get; set; }

        [DataMember]
        public int bbox_left { get; set; }

        [DataMember]
        public int bbox_right { get; set; }

        [DataMember]
        public int bbox_top { get; set; }

        [DataMember]
        public int bbox_bottom { get; set; }

        [DataMember]
        public bool HTile { get; set; }

        [DataMember]
        public bool VTile { get; set; }

        [DataMember]
        public bool For3D { get; set; }

        [DataMember]
        public bool originLocked { get; set; }

        [DataMember]
        public int width { get; set; }

        [DataMember]
        public int height { get; set; }

        [DataMember]
        public Guid textureGroupId { get; set; }

        [DataMember]
        public List<uint> swatchColours { get; set; }

        [DataMember]
        public int gridX { get; set; }

        [DataMember]
        public int gridY { get; set; }

        [DataMember]
        public List<GMSpriteFrameModel> frames { get; set; }

        [DataMember]
        public List<GMSpriteImageLayerModel> layers { get; set; }

        [DataMember]
        public float playbackSpeed { get; set; }

        [DataMember]
        public GMSpritePlaybackSpeedType playbackSpeedType { get; set; }

        public GMSpriteModel()
            : base("GMSprite", "1.12")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMSpriteFrameModel : ModelBase
    {
        [DataMember]
        public GMSpriteImageModel compositeImage { get; set; }

        [DataMember]
        public List<GMSpriteImageModel> images { get; set; }

        [DataMember]
        public Guid SpriteId { get; set; }

        public GMSpriteFrameModel()
            : base("GMSpriteFrame", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMSpriteImageModel : ModelBase
    {
        [DataMember]
        public Guid LayerId { get; set; }

        [DataMember]
        public Guid FrameId { get; set; }

        public GMSpriteImageModel()
            : base("GMSpriteImage", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMSpriteImageLayerModel : ModelBase
    {
        [DataMember]
        public Guid SpriteId { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public bool visible { get; set; }

        [DataMember]
        public bool isLocked { get; set; }

        [DataMember]
        public GMSpriteImageLayerBlendMode blendMode { get; set; }

        [DataMember]
        public float opacity { get; set; }

        public GMSpriteImageLayerModel()
            : base("GMImageLayer", "1.0")
        {
            
        }
    }

    public enum GMSpriteBboxMode
    {
        Automatic,
        FullImage,
        Manual
    }

    public enum GMSpriteColKind
    {
        Precise,
        Rectangle,
        Ellipse,
        Diamond,
        PrecisePerFrame
    }
    
    public enum GMSpriteType
    {
        Bitmap,
        SWF,
        Spine
    }

    public enum GMSpriteOrigin
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        Custom
    }

    public enum GMSpritePlaybackSpeedType
    {
        FramesPerSecond,
        FramesPerGameFrame
    }

    public enum GMSpriteImageLayerBlendMode
    {
        Normal,
        Add,
        Subtract,
        Multiply
    }
}
