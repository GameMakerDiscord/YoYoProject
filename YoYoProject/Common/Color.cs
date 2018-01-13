using System.Runtime.Serialization;

namespace YoYoProject.Common
{
    [DataContract]
    public sealed class Color
    {
        public readonly static Color Transparent = new Color(0x00000000);
        public readonly static Color White = new Color(0xFFFFFFFF);
        public readonly static Color Black = new Color(0xFF000000);

        public float Red
        {
            get { return (Value & 0x000000FF) / 255f; }
            set { Value = (Value & 0xFFFFFF00) | (uint)(value * 255f) & 0x000000FF; }
        }

        public float Green
        {
            get { return (Value & 0x0000FF00) / 255f; }
            set { Value = (Value & 0xFFFF00FF) | (uint)(value * 255f) & 0x0000FF00; }
        }

        public float Blue
        {
            get { return (Value & 0x00FF0000) / 255f; }
            set { Value = (Value & 0xFF00FFFF) | (uint)(value * 255f) & 0x00FF0000; }
        }

        public float Alpha
        {
            get { return (Value & 0xFF000000) / 255f; }
            set { Value = (Value & 0x00FFFFFF) | (uint)(value * 255f) & 0xFF000000; }
        }

        [DataMember]
        public uint Value { get; set; }

        public Color()
        {
            
        }

        public Color(uint value)
        {
            Value = value;
        }

        public Color(float red, float green, float blue, float alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public Color(float red, float green, float blue)
            : this(red, green, blue, 1.0f)
        {
            
        }
    }
}