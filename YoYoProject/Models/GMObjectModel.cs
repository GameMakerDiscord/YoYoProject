using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMObjectModel : GMResourceModel
    {
        [DataMember]
        public Guid spriteId { get; set; }

        [DataMember]
        public Guid maskSpriteId { get; set; }

        [DataMember]
        public Guid parentObjectId { get; set; }

        [DataMember]
        public bool solid { get; set; }

        [DataMember]
        public bool visible { get; set; }

        [DataMember]
        public bool persistent { get; set; }

        [DataMember]
        public bool physicsObject { get; set; }

        [DataMember]
        public bool physicsSensor { get; set; }

        [DataMember]
        public GMObjectPhysicsShapeType physicsShape { get; set; }

        // NOTE Deprecated
        [DataMember]
        public int physicsGroup { get; set; }

        [DataMember]
        public float physicsDensity { get; set; }

        [DataMember]
        public float physicsRestitution { get; set; }

        [DataMember]
        public float physicsLinearDamping { get; set; }

        [DataMember]
        public float physicsAngularDamping { get; set; }

        [DataMember]
        public float physicsFriction { get; set; }

        [DataMember]
        public bool physicsStartAwake { get; set; }

        [DataMember]
        public bool physicsKinematic { get; set; }

        [DataMember]
        public List<GMPointModel> physicsShapePoints { get; set; }

        [DataMember]
        public List<GMEventModel> eventList { get; set; }

        [DataMember]
        public List<GMObjectPropertyModel> properties { get; set; }

        [DataMember]
        public List<GMOverriddenPropertyModel> overriddenProperties { get; set; }

        public GMObjectModel()
            : base("GMObject", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMEventModel : ModelBase
    {
        [DataMember]
        public Guid m_owner { get; set; }

        [DataMember]
        public Guid collisionObjectId { get; set; }

        [DataMember]
        public bool IsDnD { get; set; }

        [DataMember]
        public int enumb { get; set; }

        [DataMember]
        public int eventtype { get; set; }
        
        public GMEventModel()
            : base("GMEvent", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMObjectPropertyModel : ModelBase
    {
        [DataMember]
        public string varName { get; set; }

        [DataMember]
        public GMObjectPropertyType varType { get; set; }

        [DataMember]
        public string value { get; set; }

        [DataMember]
        public bool rangeEnabled { get; set; }

        [DataMember]
        public float rangeMin { get; set; }

        [DataMember]
        public float rangeMax { get; set; }

        [DataMember]
        public List<string> listItems { get; set; }

        [DataMember]
        public bool multiselect { get; set; }

        [DataMember]
        public GMResourceType resourceFilter { get; set; }
        
        public GMObjectPropertyModel()
            : base("GMObjectProperty", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMOverriddenPropertyModel : ModelBase
    {
        [DataMember]
        public Guid propertyId { get; set; }

        [DataMember]
        public Guid objectId { get; set; }

        [DataMember]
        public string value { get; set; }

        public GMOverriddenPropertyModel()
            : base("GMOverriddenProperty", "1.0")
        {

        }
    }

    public enum GMObjectPhysicsShapeType
    {
        Circle,
        Box,
        Convex
    }

    public enum GMObjectPropertyType
    {
        Real,
        Integer,
        String,
        Boolean,
        Expression,
        Resource,
        List,
        Color
    }

    [Flags]
    public enum GMResourceType
    {
        None = 0,
        TileSet = 1,
        Sprite = 2,
        Sound = 4,
        Path = 8,
        Script = 16,
        Shader = 32,
        Font = 64,
        TimeLine = 128,
        Object = 256,
        Room = 512,
        DataFile = 1024,
        Extension = 2048,
        Macros = 4096,
        Notes = 8192,
        Config = 16384,
        Options = 32768,
        Folder = 65536
    }
}
