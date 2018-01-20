using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject.Models
{
    internal sealed class GMRoomModel : GMResourceModel
    {
        public bool IsDnD { get; set; }

        public Guid parentId { get; set; }

        public List<GMRViewModel> views { get; set; }

        public List<GMRLayerModel> layers { get; set; }

        public bool inheritLayers { get; set; }

        public string creationCodeFile { get; set; }

        public List<Guid> instanceCreationOrderIDs { get; set; }

        public bool inheritCode { get; set; }

        public bool inheritCreationOrder { get; set; }

        public GMRoomSettingsModel roomSettings { get; set; }

        public GMRoomViewSettingsModel viewSettings { get; set; }

        public GMRoomPhysicsSettingsModel physicsSettings { get; set; }
        
        public GMRoomModel()
            : base("GMRoom", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMRViewModel : ModelBase
    {
        [DataMember]
        public bool inherit { get; set; }

        [DataMember]
        public bool visible { get; set; }

        [DataMember]
        public int xview { get; set; }

        [DataMember]
        public int yview { get; set; }

        [DataMember]
        public int wview { get; set; }

        [DataMember]
        public int hview { get; set; }

        [DataMember]
        public int xport { get; set; }

        [DataMember]
        public int yport { get; set; }

        [DataMember]
        public int wport { get; set; }

        [DataMember]
        public int hport { get; set; }

        [DataMember]
        public int hborder { get; set; }

        [DataMember]
        public int vborder { get; set; }

        [DataMember]
        public int hspeed { get; set; }

        [DataMember]
        public int vspeed { get; set; }

        [DataMember]
        public Guid objId { get; set; }

        public GMRViewModel()
            : base("GMRView", "1.0")
        {

        }
    }

    internal sealed class GMRoomSettingsModel : ModelBase
    {
        public bool inheritRoomSettings { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool persistent { get; set; }

        public GMRoomSettingsModel()
            : base("GMRoomSettings", "1.0")
        {
            
        }
    }

    internal sealed class GMRoomViewSettingsModel : ModelBase
    {
        public bool inheritViewSettings { get; set; }

        public bool enableViews { get; set; }

        public bool clearViewBackground { get; set; }

        public bool clearDisplayBuffer { get; set; }

        public GMRoomViewSettingsModel()
            : base("GMRoomViewSettings", "1.0")
        {
            
        }
    }

    public sealed class GMRoomPhysicsSettingsModel : ModelBase
    {
        public bool inheritPhysicsSettings { get; set; }

        public bool PhysicsWorld { get; set; }

        public float PhysicsWorldGravityX { get; set; }

        public float PhysicsWorldGravityY { get; set; }

        public float PhysicsWorldPixToMeters { get; set; }
        
        public GMRoomPhysicsSettingsModel()
            : base("GMRoomPhysicsSettings", "1.0")
        {
            
        }
    }
    
    internal sealed class GMRBackgroundLayerModel : GMRLayerModel
    {
        public Guid spriteId { get; set; }

        public Color colour { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public bool htiled { get; set; }

        public bool vtiled { get; set; }

        public float hspeed { get; set; }

        public float vspeed { get; set; }

        public bool stretch { get; set; }

        public float animationFPS { get; set; }

        public GMAnimationSpeedType animationSpeedType { get; set; }

        public bool userdefined_animFPS { get; set; }

        public GMRBackgroundLayerModel()
            : base("GMRBackgroundLayer", "1.0")
        {
            
        }
    }

    internal sealed class GMRInstanceLayerModel : GMRLayerModel
    {
        public List<GMRInstanceModel> instances { get; set; }

        public GMRInstanceLayerModel()
            : base("GMRInstanceLayer", "1.0")
        {
            
        }
    }

    internal sealed class GMRInstanceModel : GMRLayerItemModelBase
    {
        public string name_with_no_file_rename { get; set; }

        public List<GMOverriddenPropertyModel> properties { get; set; }

        public bool IsDnD { get; set; }

        public bool inheritCode { get; set; }

        public Guid objId { get; set; }

        public string creationCodeFile { get; set; }

        public string creationCodeType { get; set; }

        public Color colour { get; set; }

        public float rotation { get; set; }

        public float scaleX { get; set; }

        public float scaleY { get; set; }

        public GMRInstanceModel()
            : base("GMRInstance", "1.0")
        {
            
        }
    }

    internal sealed class GMRTileLayerModel : GMRLayerModel
    {
        public Guid tilesetId { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public GMTileMapModel tiles { get; set; }

        public int prev_tilewidth { get; set; }

        public int prev_tileheight { get; set; }
        
        public GMRTileLayerModel()
            : base("GMRTileLayer", "1.0")
        {

        }
    }

    internal sealed class GMRPathLayerModel : GMRLayerModel
    {
        public Guid pathId { get; set; }

        public Color colour { get; set; }
        
        public GMRPathLayerModel()
            : base("GMRPathLayer", "1.0")
        {

        }
    }

    internal sealed class GMRAssetLayerModel : GMRLayerModel
    {
        public List<GMRLayerItemModelBase> assets { get; set; }

        public GMRAssetLayerModel()
            : base("GMRAssetLayer", "1.0")
        {

        }
    }

    internal class GMRLayerModel : ModelBase
    {
        public bool hierarchyFrozen { get; set; }

        public bool visible { get; set; }

        public bool inheritVisibility { get; set; }

        public bool hierarchyVisible { get; set; }

        public int depth { get; set; }

        public int userdefined_depth { get; set; }

        public bool inheritLayerDepth { get; set; }

        public bool inheritLayerSettings { get; set; }

        public int grid_x { get; set; }

        public int grid_y { get; set; }

        public string name { get; set; }

        public List<GMRLayerModel> layers { get; set; }

        public bool inheritSubLayers { get; set; }

        protected GMRLayerModel(string modelName, string mvc)
            : base(modelName, mvc)
        {

        }

        public GMRLayerModel()
            : base("GMRLayer", "1.0")
        {
            
        }
    }

    internal abstract class GMRLayerItemModelBase : ModelBase
    {
        public Guid m_originalParentID { get; set; }

        public bool m_serialiseFrozen { get; set; }

        public bool ignore { get; set; }

        public string name { get; set; }

        public bool inheritItemSettings { get; set; }

        public float x { get; set; }

        public float y { get; set; }

        protected GMRLayerItemModelBase(string modelName, string mvc)
            : base(modelName, mvc)
        {

        }
    }
}
