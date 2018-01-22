using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YoYoProject.Common;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMRoom : GMResource
    {
        private GMRoom parent;
        public GMRoom Parent
        {
            get { return GetProperty(parent); }
            set { SetProperty(value, ref parent); }
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
        
        private bool persistent;
        public bool Persistent
        {
            get { return GetProperty(persistent); }
            set { SetProperty(value, ref persistent); }
        }

        private string creationCode;
        public string CreationCode
        {
            get
            {
                if (creationCode != null)
                {
                    if (File.Exists(CreationCodeFullPath))
                        creationCode = File.ReadAllText(CreationCodeFullPath);
                    else
                        creationCode = "";
                }

                return creationCode;
            }

            set
            {
                if (value == creationCode)
                    return;

                creationCode = value ?? "";
                Dirty = true;
            }
        }

        public GMRLayerManager Layers { get; }

        public GMRoomViews Views { get; }

        public GMRoomPhysics Physics { get; }

        protected internal override string ResourcePath => $@"rooms\{Name}\{Name}.yy";

        private string CreationCodeFile => "RoomCreationCode.gml";

        private string CreationCodeFullPath => Path.Combine(
            Project.RootDirectory,
            $@"rooms\{Name}\{CreationCodeFile}"
        );

        public GMRoom()
        {
            Layers = new GMRLayerManager(this);
            Views = new GMRoomViews();
            Physics = new GMRoomPhysics();
        }

        protected internal override void Create()
        {
            Name = Project.Resources.GenerateValidName("room");
            Width = 1024;
            Height = 768;
            Persistent = false;
            Physics.Enabled = false;
            Physics.WorldGravityX = 0;
            Physics.WorldGravityY = 10;
            Physics.WorldPixelsToMeters = 0.1f;

            foreach (var view in Views)
            {
                view.Id = Guid.NewGuid();
                view.Visible = false;
                view.XView = 0;
                view.YView = 0;
                view.WView = 1024;
                view.HView = 768;
                view.XPort = 0;
                view.YPort = 0;
                view.WPort = 1024;
                view.HPort = 768;
                view.HBorder = 32;
                view.VBorder = 32;
                view.HSpeed = -1;
                view.VSpeed = -1;
                view.Object = null;
            }
        }

        protected internal override ModelBase Serialize()
        {
            // TODO Unload OnSaveComplete
            bool creationCodeExists;
            if (creationCode != null)
            {
                if (Dirty)
                    File.WriteAllText(CreationCodeFullPath, creationCode);
                
                creationCodeExists = true;
            }
            else
                creationCodeExists = File.Exists(CreationCodeFullPath);

            return new GMRoomModel
            {
                id = Id,
                name = Name,
                IsDnD = Project.DragAndDrop,
                parentId = Parent?.Id ?? Guid.Empty,
                views = Views.SerializeViews(),
                layers = Layers.Serialize(),
                inheritLayers = false, // TODO Implement
                creationCodeFile = creationCodeExists ? CreationCodeFile : "",
                instanceCreationOrderIDs = new List<Guid>(), // TODO Implement
                inheritCode = false, // TODO Implement
                inheritCreationOrder = false, // TODO Implement
                roomSettings = new GMRoomSettingsModel
                {
                    id = Guid.NewGuid(), // TODO Don't regenerate an ID each time
                    inheritRoomSettings = false, // TODO Implement
                    Width = Width,
                    Height = Height,
                    persistent = Persistent
                },
                viewSettings = (GMRoomViewSettingsModel)Views.Serialize(),
                physicsSettings = (GMRoomPhysicsSettingsModel)Physics.Serialize()
            };
        }
        
        public sealed class GMRoomPhysics : ControllerBase
        {
            private bool enabled;
            public bool Enabled
            {
                get { return GetProperty(enabled); }
                set { SetProperty(value, ref enabled); }
            }
            
            private float worldGravityX;
            public float WorldGravityX
            {
                get { return GetProperty(worldGravityX); }
                set { SetProperty(value, ref worldGravityX); }
            }
            
            private float worldGravityY;
            public float WorldGravityY
            {
                get { return GetProperty(worldGravityY); }
                set { SetProperty(value, ref worldGravityY); }
            }
            
            private float worldPixelsToMeters;
            public float WorldPixelsToMeters
            {
                get { return GetProperty(worldPixelsToMeters); }
                set { SetProperty(value, ref worldPixelsToMeters); }
            }

            protected internal override ModelBase Serialize()
            {
                return new GMRoomPhysicsSettingsModel
                {
                    id = Id,
                    inheritPhysicsSettings = false, // TODO Implement
                    PhysicsWorld = Enabled,
                    PhysicsWorldGravityX = WorldGravityX,
                    PhysicsWorldGravityY = WorldGravityY,
                    PhysicsWorldPixToMeters = WorldPixelsToMeters
                };
            }
        }

        public sealed class GMRoomViews : ControllerBase, IReadOnlyList<GMRoomView>
        {
            private const int ViewCount = 8;

            private bool enabled;
            public bool Enabled
            {
                get { return GetProperty(enabled); }
                set { SetProperty(value, ref enabled); }
            }

            private bool clearViewBackground;
            public bool ClearViewBackground
            {
                get { return GetProperty(clearViewBackground); }
                set { SetProperty(value, ref clearViewBackground); }
            }

            private bool clearDisplayBuffer;
            public bool ClearDisplayBuffer
            {
                get { return GetProperty(clearDisplayBuffer); }
                set { SetProperty(value, ref clearDisplayBuffer); }
            }

            public int Count => views.Count;

            public GMRoomView this[int index] => views[index];

            // NOTE Using List over array because of generic IEnumerator implementation
            private readonly List<GMRoomView> views;

            internal GMRoomViews()
            {
                views = new List<GMRoomView>(ViewCount);
                for (int i = 0; i < ViewCount; ++i)
                    views.Add(new GMRoomView());
            }

            protected internal override ModelBase Serialize()
            {
                return new GMRoomViewSettingsModel
                {
                    id = Id,
                    inheritViewSettings = false, // TODO Implement
                    enableViews = Enabled,
                    clearViewBackground = ClearViewBackground,
                    clearDisplayBuffer = ClearDisplayBuffer
                };
            }

            internal List<GMRViewModel> SerializeViews()
            {
                return views.Select(x => (GMRViewModel)x.Serialize()).ToList();
            }

            public IEnumerator<GMRoomView> GetEnumerator()
            {
                return views.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public sealed class GMRoomView : ControllerBase
        {
            private bool visible;
            public bool Visible
            {
                get { return GetProperty(visible); }
                set { SetProperty(value, ref visible); }
            }
            
            private int xView;
            public int XView
            {
                get { return GetProperty(xView); }
                set { SetProperty(value, ref xView); }
            }
            
            private int yView;
            public int YView
            {
                get { return GetProperty(yView); }
                set { SetProperty(value, ref yView); }
            }
            
            private int wView;
            public int WView
            {
                get { return GetProperty(wView); }
                set { SetProperty(value, ref wView); }
            }
            
            private int hView;
            public int HView
            {
                get { return GetProperty(hView); }
                set { SetProperty(value, ref hView); }
            }
            
            private int xPort;
            public int XPort
            {
                get { return GetProperty(xPort); }
                set { SetProperty(value, ref xPort); }
            }
            
            private int yPort;
            public int YPort
            {
                get { return GetProperty(yPort); }
                set { SetProperty(value, ref yPort); }
            }
            
            private int wPort;
            public int WPort
            {
                get { return GetProperty(wPort); }
                set { SetProperty(value, ref wPort); }
            }
            
            private int hPort;
            public int HPort
            {
                get { return GetProperty(hPort); }
                set { SetProperty(value, ref hPort); }
            }
            
            private int hBorder;
            public int HBorder
            {
                get { return GetProperty(hBorder); }
                set { SetProperty(value, ref hBorder); }
            }
            
            private int vBorder;
            public int VBorder
            {
                get { return GetProperty(vBorder); }
                set { SetProperty(value, ref vBorder); }
            }
            
            private int hSpeed;
            public int HSpeed
            {
                get { return GetProperty(hSpeed); }
                set { SetProperty(value, ref hSpeed); }
            }
            
            private int vSpeed;
            public int VSpeed
            {
                get { return GetProperty(vSpeed); }
                set { SetProperty(value, ref vSpeed); }
            }
            
            private GMObject @object;
            public GMObject Object
            {
                get { return GetProperty(@object); }
                set { SetProperty(value, ref @object); }
            }

            protected internal override ModelBase Serialize()
            {
                return new GMRViewModel
                {
                    id = Id,
                    inherit = false, // TODO Implement
                    visible = Visible,
                    xview = XView,
                    yview = YView,
                    wview = WView,
                    hview = HView,
                    xport = XPort,
                    yport = YPort,
                    wport = WPort,
                    hport = HPort,
                    hborder = HBorder,
                    vborder = VBorder,
                    hspeed = HSpeed,
                    vspeed = VSpeed,
                    objId = Object?.Id ?? Guid.Empty
                };
            }
        }
    }

    public sealed class GMRLayerManager : IReadOnlyList<GMRLayer>
    {
        public int Count => layers.Count;

        public GMRLayer this[int index] => layers[index];

        private readonly List<GMRLayer> layers;
        private readonly GMRoom gmRoom;

        internal GMRLayerManager(GMRoom gmRoom)
        {
            if (gmRoom == null)
                throw new ArgumentNullException(nameof(gmRoom));

            layers = new List<GMRLayer>();

            this.gmRoom = gmRoom;
        }

        public TLayer Create<TLayer>(string name)
            where TLayer : GMRLayer, new()
        {
            var layer = new TLayer
            {
                Project = gmRoom.Project,
                Id = Guid.NewGuid()
            };

            layer.Create(name, gmRoom);

            layers.Add(layer);

            return layer;
        }

        public void Delete(GMRLayer layer)
        {
            layers.Remove(layer);
        }

        internal List<GMRLayerModel> Serialize()
        {
            return layers.Select(x => (GMRLayerModel)x.Serialize()).ToList();
        }

        public IEnumerator<GMRLayer> GetEnumerator()
        {
            return layers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public abstract class GMRLayer : ControllerBase
    {
        private bool hierarchyFrozen;
        public bool HierarchyFrozen
        {
            get { return GetProperty(hierarchyFrozen); }
            set { SetProperty(value, ref hierarchyFrozen); }
        }
        
        private bool visible;
        public bool Visible
        {
            get { return GetProperty(visible); }
            set { SetProperty(value, ref visible); }
        }
        
        private int depth;
        public int Depth
        {
            get { return GetProperty(depth); }
            set { SetProperty(value, ref depth); }
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
        
        private string name;
        public string Name
        {
            get { return GetProperty(name); }
            set { SetProperty(value, ref name); }
        }
        
        public GMRLayerManager Layers { get; private set; }

        internal GMRoom Room { get; private set; }

        internal void Create(string name, GMRoom gmRoom)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (gmRoom == null)
                throw new ArgumentNullException(nameof(gmRoom));

            Name = name;
            Layers = new GMRLayerManager(gmRoom);
            Room = gmRoom;
        }
    }

    public sealed class GMRInstanceLayer : GMRLayer
    {
        public InstanceManager Instances { get; }

        public GMRInstanceLayer()
        {
            Instances = new InstanceManager(this);
        }

        protected internal override ModelBase Serialize()
        {
            return new GMRInstanceLayerModel
            {
                id = Id,
                hierarchyFrozen = HierarchyFrozen,
                visible = Visible,
                inheritVisibility = false, // TODO Implement
                hierarchyVisible = false, // TODO Implement
                depth = Depth,
                userdefined_depth = 0, // TODO Implement
                inheritLayerDepth = false, // TODO Implement
                inheritLayerSettings = false, // TODO Implement
                grid_x = GridX,
                grid_y = GridY,
                name = Name,
                layers = Layers.Serialize(),
                inheritSubLayers = false, // TODO Implement

                instances = Instances.Select(x => (GMRInstanceModel)x.Serialize()).ToList()
            };
        }

        public sealed class InstanceManager : IReadOnlyList<GMRInstance>
        {
            public int Count => instances.Count;

            public GMRInstance this[int index] => instances[index];

            private readonly List<GMRInstance> instances;
            private readonly GMRInstanceLayer instanceLayer;

            public InstanceManager(GMRInstanceLayer instanceLayer)
            {
                if (instanceLayer == null)
                    throw new ArgumentNullException(nameof(instanceLayer));

                instances = new List<GMRInstance>();

                this.instanceLayer = instanceLayer;
            }

            public GMRInstance Create(GMObject @object)
            {
                var instance = new GMRInstance
                {
                    Project = instanceLayer.Project,
                    Id = Guid.NewGuid(),
                    Name = "inst_" + GenerateUniqueInstanceId(),
                    Object = @object,
                    Color = Color.White,
                    Rotation = 0,
                    ScaleX = 1,
                    ScaleY = 1
                };

                instance.Create(instanceLayer.Room);

                instances.Add(instance);

                return instance;
            }

            public IEnumerator<GMRInstance> GetEnumerator()
            {
                return instances.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private static string GenerateUniqueInstanceId()
            {
                return Guid.NewGuid().ToString("D").Split('-')[0].ToUpperInvariant();
            }
        }
    }

    public sealed class GMRInstance : GMRLayerItemBase
    {
        // TODO Manager
        //private string properties;
        //public string Properties
        //{
        //    get { return GetProperty(properties); }
        //    set { SetProperty(value, ref properties); }
        //}
        
        private GMObject @object;
        public GMObject Object
        {
            get { return GetProperty(@object); }
            set { SetProperty(value, ref @object); }
        }
        
        private string creationCode;
        public string CreationCode
        {
            get
            {
                if (creationCode != null)
                {
                    if (File.Exists(CreationCodeFullPath))
                        creationCode = File.ReadAllText(CreationCodeFullPath);
                    else
                        creationCode = "";
                }

                return creationCode;
            }

            set
            {
                if (value == creationCode)
                    return;

                creationCode = value ?? "";
                Dirty = true;
            }
        }
        
        private Color color;
        public Color Color
        {
            get { return GetProperty(color); }
            set { SetProperty(value, ref color); }
        }
        
        private float rotation;
        public float Rotation
        {
            get { return GetProperty(rotation); }
            set { SetProperty(value, ref rotation); }
        }
        
        private float scaleX;
        public float ScaleX
        {
            get { return GetProperty(scaleX); }
            set { SetProperty(value, ref scaleX); }
        }
        
        private float scaleY;
        public float ScaleY
        {
            get { return GetProperty(scaleY); }
            set { SetProperty(value, ref scaleY); }
        }

        private string CreationCodeFile => $"InstanceCreationCode_{Name}.gml";

        private string CreationCodeFullPath => Path.Combine(
            Project.RootDirectory,
            $@"rooms\{Room.Name}\{CreationCodeFile}"
        );
        
        protected internal override ModelBase Serialize()
        {
            // TODO Unload OnSaveComplete
            bool creationCodeExists;
            if (creationCode != null)
            {
                if (Dirty)
                    File.WriteAllText(CreationCodeFullPath, creationCode);

                creationCodeExists = true;
            }
            else
                creationCodeExists = File.Exists(CreationCodeFullPath);

            return new GMRInstanceModel
            {
                id = Id,
                m_originalParentID = Parent?.Id ?? Guid.Empty,
                name = Name,
                x = X,
                y = Y,

                name_with_no_file_rename = Name,
                properties = new List<GMOverriddenPropertyModel>(), // TODO Implement
                IsDnD = Project.DragAndDrop,
                inheritCode = false, // TODO Implement
                objId = Object?.Id ?? Guid.Empty,
                creationCodeFile = creationCodeExists ? CreationCodeFile : "",
                creationCodeType = creationCodeExists ? ".gml" : "",
                colour = Color,
                rotation = Rotation,
                scaleX = ScaleX,
                scaleY = ScaleY
            };
        }
    }

    public abstract class GMRLayerItemBase : ControllerBase
    {
        protected GMRoom Room { get; private set; }

        private GMRLayerItemBase parent;
        public GMRLayerItemBase Parent
        {
            get { return GetProperty(parent); }
            set { SetProperty(value, ref parent); }
        }
        
        private string name;
        public string Name
        {
            get { return GetProperty(name); }
            set { SetProperty(value, ref name); }
        }
        
        private float x;
        public float X
        {
            get { return GetProperty(x); }
            set { SetProperty(value, ref x); }
        }
        
        private float y;
        public float Y
        {
            get { return GetProperty(y); }
            set { SetProperty(value, ref y); }
        }

        internal void Create(GMRoom room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            Room = room;
        }
    }
}
