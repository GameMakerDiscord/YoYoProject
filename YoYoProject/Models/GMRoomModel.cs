using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using YoYoProject.Common;
using YoYoProject.Models;
using YoYoStudio.MVCFormat;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMRoomModel : GMResourceModel
    {
        [DataMember]
        public bool IsDnD { get; set; }

        [DataMember]
        public Guid parentId { get; set; }

        [DataMember]
        public List<GMRViewModel> views { get; set; }

        [DataMember]
        public List<GMRLayerModel> layers { get; set; }

        [DataMember]
        public bool inheritLayers { get; set; }

        [DataMember]
        public string creationCodeFile { get; set; }

        [DataMember]
        public List<Guid> instanceCreationOrderIDs { get; set; }

        [DataMember]
        public bool inheritCode { get; set; }

        [DataMember]
        public bool inheritCreationOrder { get; set; }

        [DataMember]
        public GMRoomSettingsModel roomSettings { get; set; }

        [DataMember]
        public GMRoomViewSettingsModel viewSettings { get; set; }

        [DataMember]
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

    [DataContract]
    internal sealed class GMRoomSettingsModel : ModelBase
    {
        [DataMember]
        public bool inheritRoomSettings { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public bool persistent { get; set; }

        public GMRoomSettingsModel()
            : base("GMRoomSettings", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMRoomViewSettingsModel : ModelBase
    {
        [DataMember]
        public bool inheritViewSettings { get; set; }

        [DataMember]
        public bool enableViews { get; set; }

        [DataMember]
        public bool clearViewBackground { get; set; }

        [DataMember]
        public bool clearDisplayBuffer { get; set; }

        public GMRoomViewSettingsModel()
            : base("GMRoomViewSettings", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMRoomPhysicsSettingsModel : ModelBase
    {
        [DataMember]
        public bool inheritPhysicsSettings { get; set; }

        [DataMember]
        public bool PhysicsWorld { get; set; }

        [DataMember]
        public float PhysicsWorldGravityX { get; set; }

        [DataMember]
        public float PhysicsWorldGravityY { get; set; }

        [DataMember]
        public float PhysicsWorldPixToMeters { get; set; }
        
        public GMRoomPhysicsSettingsModel()
            : base("GMRoomPhysicsSettings", "1.0")
        {
            
        }
    }
    
    [DataContract]
    internal sealed class GMRInstanceModel : GMRLayerItemModelBase
    {
        [DataMember]
        public string name_with_no_file_rename { get; set; }

        [DataMember]
        public List<GMOverriddenPropertyModel> properties { get; set; }

        [DataMember]
        public bool IsDnD { get; set; }

        [DataMember]
        public bool inheritCode { get; set; }

        [DataMember]
        public Guid objId { get; set; }

        [DataMember]
        public string creationCodeFile { get; set; }

        [DataMember]
        public string creationCodeType { get; set; }

        [DataMember]
        public Color colour { get; set; }

        [DataMember]
        public float rotation { get; set; }

        [DataMember]
        public float scaleX { get; set; }

        [DataMember]
        public float scaleY { get; set; }

        public GMRInstanceModel()
            : base("GMRInstance", "1.0")
        {
            
        }
    }
    
    [DataContract]
    internal abstract class GMRLayerItemModelBase : ModelBase
    {
        [DataMember]
        public Guid m_originalParentID { get; set; }

        [DataMember]
        public bool m_serialiseFrozen { get; set; }

        [DataMember]
        public bool ignore { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public bool inheritItemSettings { get; set; }

        [DataMember]
        public float x { get; set; }

        [DataMember]
        public float y { get; set; }

        protected GMRLayerItemModelBase(string modelName, string mvc)
            : base(modelName, mvc)
        {

        }
    }
}

// NOTE In order to make the JSON Serializer behave itself we need to use this namespace and
//      naming convention. Super super frustrating but I'm not gonna spend any more time trying
//      to make an inflexible framework play nicely
namespace YoYoStudio.MVCFormat
{
    [DataContract]
    internal sealed class GMRBackgroundLayer_Model : GMRLayerModel
    {
        [DataMember]
        public Guid spriteId { get; set; }

        [DataMember]
        public Color colour { get; set; }

        [DataMember]
        public int x { get; set; }

        [DataMember]
        public int y { get; set; }

        [DataMember]
        public bool htiled { get; set; }

        [DataMember]
        public bool vtiled { get; set; }

        [DataMember]
        public float hspeed { get; set; }

        [DataMember]
        public float vspeed { get; set; }

        [DataMember]
        public bool stretch { get; set; }

        [DataMember]
        public float animationFPS { get; set; }

        [DataMember]
        public string animationSpeedType { get; set; }

        [DataMember]
        public bool userdefined_animFPS { get; set; }

        public GMRBackgroundLayer_Model()
            : base("GMRBackgroundLayer", "1.0")
        {

        }
    }

    [DataContract]
    internal sealed class GMRInstanceLayer_Model : GMRLayerModel
    {
        [DataMember]
        public List<GMRInstanceModel> instances { get; set; }

        public GMRInstanceLayer_Model()
            : base("GMRInstanceLayer", "1.0")
        {

        }
    }

    [DataContract]
    internal sealed class GMRTileLayer_Model : GMRLayerModel
    {
        [DataMember]
        public Guid tilesetId { get; set; }

        [DataMember]
        public int x { get; set; }

        [DataMember]
        public int y { get; set; }

        [DataMember]
        public GMTileMapModel tiles { get; set; }

        [DataMember]
        public int prev_tilewidth { get; set; }

        [DataMember]
        public int prev_tileheight { get; set; }

        public GMRTileLayer_Model()
            : base("GMRTileLayer", "1.0")
        {

        }
    }

    [DataContract]
    internal sealed class GMRPathLayer_Model : GMRLayerModel
    {
        [DataMember]
        public Guid pathId { get; set; }

        [DataMember]
        public Color colour { get; set; }

        public GMRPathLayer_Model()
            : base("GMRPathLayer", "1.0")
        {

        }
    }

    [DataContract]
    internal sealed class GMRAssetLayer_Model : GMRLayerModel
    {
        [DataMember]
        public List<GMRLayerItemModelBase> assets { get; set; }

        public GMRAssetLayer_Model()
            : base("GMRAssetLayer", "1.0")
        {

        }
    }

    [DataContract]
    [KnownType("SerializationTypes")]
    internal class GMRLayerModel : ModelBase
    {
        [DataMember]
        public bool m_serialiseFrozen { get; set; }

        [DataMember]
        public Guid m_parentID { get; set; }

        [DataMember]
        public bool hierarchyFrozen { get; set; }

        [DataMember]
        public bool visible { get; set; }

        [DataMember]
        public bool inheritVisibility { get; set; }

        [DataMember]
        public bool hierarchyVisible { get; set; }

        [DataMember]
        public int depth { get; set; }

        [DataMember]
        public bool userdefined_depth { get; set; }

        [DataMember]
        public bool inheritLayerDepth { get; set; }

        [DataMember]
        public bool inheritLayerSettings { get; set; }

        [DataMember]
        public int grid_x { get; set; }

        [DataMember]
        public int grid_y { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public List<GMRLayerModel> layers { get; set; }

        [DataMember]
        public bool inheritSubLayers { get; set; }

        public GMRLayerModel()
            : base("GMRLayer", "1.0")
        {

        }

        public GMRLayerModel(string modelName, string mvc)
            : base(modelName, mvc)
        {

        }

        public static IEnumerable<Type> SerializationTypes()
        {
            return new[]
            {
                typeof(GMRTileLayer_Model),
                typeof(GMRBackgroundLayer_Model),
                typeof(GMRInstanceLayer_Model),
                typeof(GMRAssetLayer_Model),
                typeof(GMRPathLayer_Model)
            };
        }
    }
}