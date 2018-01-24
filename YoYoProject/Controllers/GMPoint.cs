using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMPoint : ControllerBase
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

        internal GMPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal override ModelBase Serialize()
        {
            return new GMPointModel
            {
                id = Id,
                x = X,
                y = Y
            };
        }
    }
}