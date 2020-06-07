using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMObject : GMResource
    {
        private GMSprite sprite;
        public GMSprite Sprite
        {
            get { return GetProperty(sprite); }
            set { SetProperty(value, ref sprite); }
        }
        
        private GMSprite mask;
        public GMSprite Mask
        {
            get { return GetProperty(mask); }
            set { SetProperty(value, ref mask); }
        }
        
        private GMObject parent;
        public GMObject Parent
        {
            get { return GetProperty(parent); }
            set { SetProperty(value, ref parent); }
        }
        
        private bool solid;
        public bool Solid
        {
            get { return GetProperty(solid); }
            set { SetProperty(value, ref solid); }
        }
        
        private bool visible;
        public bool Visible
        {
            get { return GetProperty(visible); }
            set { SetProperty(value, ref visible); }
        }
        
        private bool persistent;
        public bool Persistent
        {
            get { return GetProperty(persistent); }
            set { SetProperty(value, ref persistent); }
        }
        
        public GMObjectPhysics Physics { get; }

        public EventManager Events { get; }
        
        public PropertyManager Properties { get; }

        internal override string ResourcePath => $@"objects\{Name}\{Name}.yy";

        public GMObject()
        {
            Physics = new GMObjectPhysics(this);
            Events = new EventManager(this);
            Properties = new PropertyManager(this);
        }

        internal override void Create(string name)
        {
            Name = Project.Resources.GenerateValidName(name ?? "object");
            Sprite = null;
            Mask = null;
            Parent = null;
            Solid = false;
            Visible = true;
            Persistent = false;
            Physics.Enabled = false;
            Physics.Sensor = false;
            Physics.Restitution = 0.1f;
            Physics.LinearDamping = 0.1f;
            Physics.Friction = 0.2f;
            Physics.StartAwake = true;
            Physics.Kinematic = false;
            Physics.Shape.Type = GMObjectPhysicsShapeType.Box;
            AddResourceToFolder("GMObject");
        }

        internal override ModelBase Serialize()
        {
            return new GMObjectModel
            {
                id = Id,
                name = Name,
                spriteId = Sprite?.Id ?? Guid.Empty,
                maskSpriteId = Mask?.Id ?? Guid.Empty,
                parentObjectId = Parent?.Id ?? Guid.Empty,
                solid = Solid,
                visible = Visible,
                persistent = Persistent,
                physicsObject = Physics.Enabled,
                physicsSensor = Physics.Sensor,
                physicsShape = Physics.Shape.Type,
                physicsDensity = Physics.Density,
                physicsRestitution = Physics.Restitution,
                physicsLinearDamping = Physics.LinearDamping,
                physicsAngularDamping = Physics.AngularDamping,
                physicsFriction = Physics.Friction,
                physicsStartAwake = Physics.StartAwake,
                physicsKinematic = Physics.Kinematic,
                physicsShapePoints = Physics.Shape.SerializePoints(),
                eventList = Events.Serialize(),
                properties = Properties.Serialize(),
                overriddenProperties = Properties.SerializeOverrides()
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            var objectModel = (GMObjectModel)model;

            Id = objectModel.id;
            Name = objectModel.name;
            Sprite = null;
            Mask = null;
            Parent = null;
            Solid = objectModel.solid;
            Visible = objectModel.visible;
            Persistent = objectModel.persistent;
            Physics.Enabled = objectModel.physicsObject;
            Physics.Sensor = objectModel.physicsSensor;
            Physics.Shape.Type = objectModel.physicsShape;
            Physics.Density = objectModel.physicsDensity;
            Physics.Restitution = objectModel.physicsRestitution;
            Physics.LinearDamping = objectModel.physicsLinearDamping;
            Physics.AngularDamping = objectModel.physicsAngularDamping;
            Physics.Friction = objectModel.physicsFriction;
            Physics.StartAwake = objectModel.physicsStartAwake;
            Physics.Kinematic = objectModel.physicsKinematic;
            if (objectModel.physicsShapePoints != null)
                Physics.Shape.DeserializePoints(objectModel.physicsShapePoints);
            Events.Deserialize(objectModel.eventList);
            if (objectModel.properties != null)
                Properties.Deserialize(objectModel.properties);
        }

        internal override void FinalizeDeserialization(ModelBase model)
        {
            var objectModel = (GMObjectModel)model;

            Sprite = objectModel.spriteId == Guid.Empty
                ? null : Project.Resources.Get<GMSprite>(objectModel.spriteId);
            Mask = objectModel.maskSpriteId == Guid.Empty
                ? null : Project.Resources.Get<GMSprite>(objectModel.maskSpriteId);
            Parent = objectModel.parentObjectId == Guid.Empty
                ? null : Project.Resources.Get<GMObject>(objectModel.parentObjectId);
        }

        public sealed class GMObjectPhysics : ControllerBase
        {
            private bool enabled;
            public bool Enabled
            {
                get { return GetProperty(enabled); }
                set { SetProperty(value, ref enabled); }
            }
            
            private bool sensor;
            public bool Sensor
            {
                get { return GetProperty(sensor); }
                set { SetProperty(value, ref sensor); }
            }

            private float density;
            public float Density
            {
                get { return GetProperty(density); }
                set { SetProperty(value, ref density); }
            }
            
            private float restitution;
            public float Restitution
            {
                get { return GetProperty(restitution); }
                set { SetProperty(value, ref restitution); }
            }
            
            private float linearDamping;
            public float LinearDamping
            {
                get { return GetProperty(linearDamping); }
                set { SetProperty(value, ref linearDamping); }
            }
            
            private float angularDamping;
            public float AngularDamping
            {
                get { return GetProperty(angularDamping); }
                set { SetProperty(value, ref angularDamping); }
            }
            
            private float friction;
            public float Friction
            {
                get { return GetProperty(friction); }
                set { SetProperty(value, ref friction); }
            }
            
            private bool startAwake;
            public bool StartAwake
            {
                get { return GetProperty(startAwake); }
                set { SetProperty(value, ref startAwake); }
            }
            
            private bool kinematic;
            public bool Kinematic
            {
                get { return GetProperty(kinematic); }
                set { SetProperty(value, ref kinematic); }
            }

            public ShapeManager Shape { get; }

            internal GMObjectPhysics(GMObject gmObject)
            {
                if (gmObject == null)
                    throw new ArgumentNullException(nameof(gmObject));

                Shape = new ShapeManager(gmObject);
            }

            internal override ModelBase Serialize()
            {
                throw new InvalidOperationException();
            }

            public sealed class ShapeManager : ControllerBase, IReadOnlyList<GMPoint>
            {
                private GMObjectPhysicsShapeType type;
                public GMObjectPhysicsShapeType Type
                {
                    get { return GetProperty(type); }
                    set
                    {
                        if (type == value)
                            return;

                        SetProperty(value, ref type);
                        UpdatePointsForShapeType(value);
                    }
                }

                public int Count => points.Count;

                public GMPoint this[int index] => points[index];

                private readonly List<GMPoint> points;
                private readonly GMObject gmObject;

                internal ShapeManager(GMObject gmObject)
                {
                    if (gmObject == null)
                        throw new ArgumentNullException(nameof(gmObject));

                    points = new List<GMPoint>();

                    this.gmObject = gmObject;
                }

                public GMPoint Create(float x, float y)
                {
                    var point = new GMPoint(x, y)
                    {
                        Id = Guid.NewGuid()
                    };

                    points.Add(point);

                    return point;
                }

                public void Delete(GMPoint point)
                {
                    points.Remove(point);
                }

                internal override ModelBase Serialize()
                {
                    throw new InvalidOperationException();
                }

                internal override void Deserialize(ModelBase model)
                {
                    throw new InvalidOperationException();
                }

                // HACK List<> does not inherit from ModelBase
                internal List<GMPointModel> SerializePoints()
                {
                    return points.Select(x => (GMPointModel)x.Serialize()).ToList();
                }

                // HACK List<> does not inherit from ModelBase
                internal void DeserializePoints(List<GMPointModel> pointsModel)
                {
                    foreach (var pointModel in pointsModel)
                    {
                        var point = new GMPoint(pointModel.x, pointModel.y)
                        {
                            Id = pointModel.id
                        };

                        points.Add(point);
                    }
                }

                private void UpdatePointsForShapeType(GMObjectPhysicsShapeType shapeType)
                {
                    int w = gmObject.Sprite?.Width ?? 32;
                    int h = gmObject.Sprite?.Height ?? 32;

                    points.Clear();
                    switch (shapeType)
                    {
                        case GMObjectPhysicsShapeType.Box:
                            Create(0, 0);
                            Create(w, 0);
                            Create(w, h);
                            Create(0, h);
                            break;

                        case GMObjectPhysicsShapeType.Circle:
                            Create(w / 2, h / 2);
                            Create(w / 2, h / 2);
                            break;
                    }
                }

                public IEnumerator<GMPoint> GetEnumerator()
                {
                    return points.GetEnumerator();
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }
            }
        }

        public sealed class EventManager : IReadOnlyList<GMEvent>
        {
            public int Count => events.Count;

            public GMEvent this[int index] => events[index];

            private readonly List<GMEvent> events;
            private readonly GMObject gmObject;

            internal EventManager(GMObject gmObject)
            {
                if (gmObject == null)
                    throw new ArgumentNullException(nameof(gmObject));

                events = new List<GMEvent>();

                this.gmObject = gmObject;
            }

            public GMEvent Get(GMEventType eventType, GMEventNumber eventNumber, GMObject collision)
            {
                return events.FirstOrDefault(
                    x => x.EventType == eventType
                      && x.EventNumber == eventNumber
                      && (x.EventType != GMEventType.Collision || x.Collision == collision)
                );
            }

            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public GMEvent Get(GMEventType eventType, GMEventNumber eventNumber)
            {
                return Get(eventType, eventNumber, null);
            }

            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public GMEvent Get(GMEventType eventType, GMObject collision)
            {
                return Get(eventType, GMEventNumber.Default, collision);
            }

            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public GMEvent Get(GMEventType eventType)
            {
                return Get(eventType, GMEventNumber.Default, null);
            }

            public GMEvent Create(GMEventType eventType, GMEventNumber eventNumber, GMObject collision)
            {
                GMEvent @event = Get(eventType, eventNumber, collision);
                if (@event != null)
                    return @event;

                @event = new GMEvent(gmObject)
                {
                    Project = gmObject.Project,
                    Id = Guid.NewGuid(),
                    EventType = eventType,
                    EventNumber = eventNumber,
                    Collision = collision,
                    IsDnD = gmObject.Project.DragAndDrop
                };

                events.Add(@event);

                return @event;
            }

            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public GMEvent Create(GMEventType eventType, GMObject collision)
            {
                return Create(eventType, GMEventNumber.Default, collision);
            }

            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public GMEvent Create(GMEventType eventType, GMEventNumber eventNumber)
            {
                return Create(eventType, eventNumber, null);
            }

            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public GMEvent Create(GMEventType eventType)
            {
                return Create(eventType, GMEventNumber.Default, null);
            }

            public void Delete(GMEvent @event)
            {
                events.Remove(@event);
            }

            internal List<GMEventModel> Serialize()
            {
                return events.Select(x => (GMEventModel)x.Serialize()).ToList();
            }

            internal void Deserialize(List<GMEventModel> eventListModel)
            {
                foreach (var eventModel in eventListModel)
                {
                    var @event = new GMEvent(gmObject)
                    {
                        Project = gmObject.Project,
                        Id = eventModel.id
                    };

                    @event.Deserialize(eventModel);
                    events.Add(@event);
                }
            }

            public IEnumerator<GMEvent> GetEnumerator()
            {
                return events.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public sealed class PropertyManager : IReadOnlyList<GMObjectProperty>
        {
            public int Count => properties.Count;

            public GMObjectProperty this[int index] => properties[index];

            private readonly List<GMObjectProperty> properties;
            private readonly GMObject gmObject;

            public PropertyManager(GMObject gmObject)
            {
                if (gmObject == null)
                    throw new ArgumentNullException(nameof(gmObject));

                properties = new List<GMObjectProperty>();

                this.gmObject = gmObject;
            }

            public GMObjectProperty Create(GMObjectPropertyType type, string name, string value)
            {
                if (name == null)
                    throw new ArgumentNullException(nameof(name));

                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                var property = new GMObjectProperty
                {
                    Id = Guid.NewGuid(),
                    Type = type,
                    Name = name,
                    Value = value
                };

                properties.Add(property);

                return property;
            }

            public void Delete(GMObjectProperty property)
            {
                properties.Remove(property);
            }

            internal List<GMObjectPropertyModel> Serialize()
            {
                var parent = gmObject.Parent;
                if (parent == null)
                    return properties.Select(x => (GMObjectPropertyModel)x.Serialize()).ToList();

                var parentProperties = new HashSet<string>();
                while (parent != null)
                {
                    foreach (var pProp in parent.Properties)
                        parentProperties.Add(pProp.Name);

                    parent = parent.Parent;
                }

                var childProperties = new List<GMObjectPropertyModel>(properties.Count);
                foreach (var cProp in properties)
                {
                    if (parentProperties.Contains(cProp.Name))
                        continue;

                    childProperties.Add((GMObjectPropertyModel)cProp.Serialize());
                }

                return childProperties;
            }

            internal List<GMOverriddenPropertyModel> SerializeOverrides()
            {
                var overrides = new List<GMOverriddenPropertyModel>();

                var parent = gmObject.Parent;
                while (parent != null)
                {
                    foreach (var pProp in parent.Properties)
                    {
                        var cProp = properties.FirstOrDefault(x => x.Name == pProp.Name);
                        if (cProp == null)
                            continue;

                        overrides.Add(new GMOverriddenPropertyModel
                        {
                            id = Guid.NewGuid(), // TODO Don't regenerate these kinds of IDs
                            objectId = parent.Id,
                            propertyId = pProp.Id,
                            value = cProp.Value
                        });
                    }

                    parent = parent.Parent;
                }

                return overrides;
            }

            internal void Deserialize(List<GMObjectPropertyModel> objectPropertiesModel)
            {
                foreach (var objectPropertyModel in objectPropertiesModel)
                {
                    var property = new GMObjectProperty
                    {
                        Id = objectPropertyModel.id
                    };

                    property.Deserialize(objectPropertyModel);
                    properties.Add(property);
                }
            }

            public IEnumerator<GMObjectProperty> GetEnumerator()
            {
                return properties.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }

    public sealed class GMObjectProperty : ControllerBase
    {
        private string name;
        public string Name
        {
            get { return GetProperty(name); }
            set { SetProperty(value, ref name); }
        }
        
        private GMObjectPropertyType type;
        public GMObjectPropertyType Type
        {
            get { return GetProperty(type); }
            set { SetProperty(value, ref type); }
        }
        
        private string value_;
        public string Value
        {
            get { return GetProperty(value_); }
            set { SetProperty(value, ref value_); }
        }
        
        private Tuple<float, float> range;
        public Tuple<float, float> Range
        {
            get { return GetProperty(range); }
            set { SetProperty(value, ref range); }
        }
        
        private List<string> listItems;
        public List<string> ListItems
        {
            get { return GetProperty(listItems); }
            set { SetProperty(value, ref listItems); }
        }
        
        private bool multiSelect;
        public bool MultiSelect
        {
            get { return GetProperty(multiSelect); }
            set { SetProperty(value, ref multiSelect); }
        }
        
        private GMResourceType resourceFilter;
        public GMResourceType ResourceFilter
        {
            get { return GetProperty(resourceFilter); }
            set { SetProperty(value, ref resourceFilter); }
        }

        internal GMObjectProperty()
        {
            ResourceFilter = GMResourceType.AllResources;
        }

        internal override ModelBase Serialize()
        {
            return new GMObjectPropertyModel
            {
                id = Id,
                varName = Name,
                varType = Type,
                value = Value,
                rangeEnabled = Range != null,
                rangeMax = Range?.Item1 ?? 0,
                rangeMin = Range?.Item2 ?? 0,
                listItems = ListItems,
                multiselect = MultiSelect,
                resourceFilter = ResourceFilter
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            var propertyModel = (GMObjectPropertyModel)model;

            Id = propertyModel.id;
            Name = propertyModel.varName;
            Type = propertyModel.varType;
            Value = propertyModel.value;
            Range = propertyModel.rangeEnabled
                  ? Tuple.Create(propertyModel.rangeMin, propertyModel.rangeMax)
                  : null;
            ListItems = propertyModel.listItems;
            ResourceFilter = propertyModel.resourceFilter;
        }
    }
}
