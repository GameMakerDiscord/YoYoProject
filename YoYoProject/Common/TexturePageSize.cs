using System;

namespace YoYoProject.Common
{
    public sealed class TexturePageSize
    {
        public uint Width = 2048;
        public uint Height = 2048;

        public override string ToString()
        {
            return $"{Width}x{Height}"; // "2048x2048"
        }

        public TexturePageSize (uint w, uint h)
        {
            if ((w % 2 != 0) || (h % 2 != 0))
                throw new ArgumentException("Width and Height must be a power of 2!");

            Width = w;
            Height = h;
        }

        public TexturePageSize (string size)
        {
            var arr = size.Split('x'); // "2048x2048" -> w = 2048, h = 2048
            if (arr.Length < 2)
                throw new ArgumentException("Invalid size string!"); // "2048x" ?????
            if (!uint.TryParse(arr[0], out uint w) || !uint.TryParse(arr[1], out uint h))
                throw new ArgumentException("Could not parse the size string!");
            if ((w % 2 != 0) || (h % 2 != 0))
                throw new ArgumentException("Width and Height must be a power of 2!");

            Width = w;
            Height = h;
        }

        public TexturePageSize()
        {

        }
    }
}