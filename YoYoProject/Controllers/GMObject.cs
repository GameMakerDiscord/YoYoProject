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

        protected internal override string ResourcePath => $@"objects\{Name}\{Name}.yy";

        public GMObject()
        {
            Physics = new GMObjectPhysics();
            Events = new EventManager(this);
            Properties = new PropertyManager(this);
        }

        protected internal override void Create()
        {
            Name = Project.Resources.GenerateValidName("object");
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
        }

        protected internal override ModelBase Serialize()
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

            internal GMObjectPhysics()
            {
                Shape = new ShapeManager();
            }

            protected internal override ModelBase Serialize()
            {
                throw new InvalidOperationException();
            }

            public sealed class ShapeManager : ControllerBase, IReadOnlyList<GMPoint>
            {
                private GMObjectPhysicsShapeType type;
                public GMObjectPhysicsShapeType Type
                {
                    get { return GetProperty(type); }
                    set { SetProperty(value, ref type); }
                }

                public int Count => points.Count;

                public GMPoint this[int index] => points[index];

                private readonly List<GMPoint> points;

                internal ShapeManager()
                {
                    points = new List<GMPoint>();
                }

                public GMPoint Create(int x, int y)
                {
                    var point = new GMPoint(x, y)
                    {
                        Id = Guid.NewGuid()
                    };

                    points.Add(point);

                    return point;
                }

                protected internal override ModelBase Serialize()
                {
                    throw new InvalidOperationException();
                }

                // HACK List<> does not inherit from ModelBase
                internal List<GMPointModel> SerializePoints()
                {
                    // TODO Serialize Box and Circle correctly
                    return points.Select(x => (GMPointModel)x.Serialize()).ToList();
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

            public EventManager(GMObject gmObject)
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

            public IEnumerator<GMEvent> GetEnumerator()
            {
                return events.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public sealed class PropertyManager
        {
            private readonly GMObject gmObject;

            public PropertyManager(GMObject gmObject)
            {
                if (gmObject == null)
                    throw new ArgumentNullException(nameof(gmObject));

                this.gmObject = gmObject;
            }

            internal List<GMObjectPropertyModel> Serialize()
            {
                // TODO Implement
                return new List<GMObjectPropertyModel>();
            }

            internal List<GMOverriddenPropertyModel> SerializeOverrides()
            {
                // TODO Implement
                return new List<GMOverriddenPropertyModel>();
            }
        }
    }
}
