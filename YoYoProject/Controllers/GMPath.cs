using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMPath : GMResource
    {
        private GMPathKind kind;
        public GMPathKind Kind
        {
            get { return GetProperty(kind); }
            set { SetProperty(value, ref kind); }
        }
        
        private bool closed;
        public bool Closed
        {
            get { return GetProperty(closed); }
            set { SetProperty(value, ref closed); }
        }
        
        private int precision;
        public int Precision
        {
            get { return GetProperty(precision); }
            set { SetProperty(value, ref precision); }
        }
        
        private int snapHorizontal;
        public int SnapHorizontal
        {
            get { return GetProperty(snapHorizontal); }
            set { SetProperty(value, ref snapHorizontal); }
        }
        
        private int snapVertical;
        public int SnapVertical
        {
            get { return GetProperty(snapVertical); }
            set { SetProperty(value, ref snapVertical); }
        }

        public PointManager Points { get; }

        internal override string ResourcePath => $@"paths\{Name}\{Name}.yy";

        public GMPath()
        {
            Points = new PointManager(this);
        }

        internal override void Create(string name)
        {
            Name = Project.Resources.GenerateValidName(name ?? "path");
            Kind = GMPathKind.StraightLines;
            Closed = false;
            Precision = 4;
            SnapHorizontal = 0;
            SnapVertical = 0;
            AddResourceToFolder("GMPath");
        }

        internal override ModelBase Serialize()
        {
            return new GMPathModel
            {
                id = Id,
                name = Name,
                kind = Kind,
                closed = Closed,
                precision = Precision,
                hsnap = SnapHorizontal,
                vsnap = SnapVertical,
                points = Points.Serialize()
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            // TODO Implement
            var pathModel = (GMPathModel)model;

            Id = pathModel.id;
            Name = pathModel.name;
        }

        public sealed class PointManager : IReadOnlyList<GMPathPoint>
        {
            public int Count => points.Count;

            public GMPathPoint this[int index] => points[index];

            private readonly List<GMPathPoint> points;
            private readonly GMPath path;

            internal PointManager(GMPath path)
            {
                if (path == null)
                    throw new ArgumentNullException(nameof(path));

                points = new List<GMPathPoint>();

                this.path = path;
            }

            public GMPathPoint Create(int x = 0, int y = 0, int speed = 100)
            {
                var point = new GMPathPoint
                {
                    Id = Guid.NewGuid(),
                    X = x,
                    Y = y,
                    Speed = speed
                };

                points.Add(point);

                return point;
            }

            public void Delete(GMPathPoint point)
            {
                points.Remove(point);
            }

            internal List<GMPathPointModel> Serialize()
            {
                return points.Select(x => (GMPathPointModel)x.Serialize()).ToList();
            }

            public IEnumerator<GMPathPoint> GetEnumerator()
            {
                return points.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }

    public sealed class GMPathPoint : ControllerBase
    {
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
        
        private float speed;
        public float Speed
        {
            get { return GetProperty(speed); }
            set { SetProperty(value, ref speed); }
        }

        internal GMPathPoint()
        {
            // NOTE Empty internal ctor to enforce manager pattern
        }

        internal override ModelBase Serialize()
        {
            return new GMPathPointModel
            {
                id = Id,
                x = X,
                y = Y,
                speed = Speed
            };
        }
    }
}
