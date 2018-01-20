using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        private string views;
        public string Views
        {
            get { return GetProperty(views); }
            set { SetProperty(value, ref views); }
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

        public GMRLayerManager Layers { get; }

        public GMRoomPhysics Physics { get; }

        protected internal override string ResourcePath => $@"rooms\{Name}\{Name}.yy";

        public GMRoom()
        {
            Layers = new GMRLayerManager(this);
            Physics = new GMRoomPhysics();
        }

        protected internal override void Create()
        {
            throw new NotImplementedException();
        }

        protected internal override ModelBase Serialize()
        {
            throw new NotImplementedException();
        }
        
        public sealed class GMRoomPhysics : ControllerBase
        {
            private string enabled;
            public string Enabled
            {
                get { return GetProperty(enabled); }
                set { SetProperty(value, ref enabled); }
            }
            
            private string worldGravityX;
            public string WorldGravityX
            {
                get { return GetProperty(worldGravityX); }
                set { SetProperty(value, ref worldGravityX); }
            }
            
            private string worldGravityY;
            public string WorldGravityY
            {
                get { return GetProperty(worldGravityY); }
                set { SetProperty(value, ref worldGravityY); }
            }
            
            private string worldPixelsToMeters;
            public string WorldPixelsToMeters
            {
                get { return GetProperty(worldPixelsToMeters); }
                set { SetProperty(value, ref worldPixelsToMeters); }
            }

            protected internal override ModelBase Serialize()
            {
                throw new NotImplementedException();
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
            var layer = new TLayer();
            layer.Create(name, gmRoom);

            layers.Add(layer);

            return layer;
        }

        public void Delete(GMRLayer layer)
        {
            layers.Remove(layer);
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

    public class GMRLayer : ControllerBase
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

        protected internal override ModelBase Serialize()
        {
            throw new NotImplementedException();
        }
    }

    public sealed class GMRInstanceLayer : GMRLayer
    {
        public InstanceManager Instances { get; }

        public GMRInstanceLayer()
        {
            Instances = new InstanceManager(this);
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

            public GMRInstance Create()
            {
                var instance = new GMRInstance();

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
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
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
        
        protected internal override ModelBase Serialize()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class GMRLayerItemBase : ControllerBase
    {
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

    }
}
