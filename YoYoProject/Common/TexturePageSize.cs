using System;

namespace YoYoProject.Common
{
    public sealed class TexturePageSize
    {
        public uint Width { get; set; }
        public uint Height { get; set; }

        public TexturePageSize()
        {

        }

        public TexturePageSize(uint width, uint height)
        {
            if (((width & (width - 1)) != 0) || ((height & (height - 1)) != 0))
                throw new ArgumentException("Width and Height must be a power of 2!");

            Width = width;
            Height = height;
        }

        public TexturePageSize(string size)
        {
            if (size == null)
                throw new ArgumentException("Size string cannot be null.");

            var arr = size.Split('x'); // "2048x2048" -> w = 2048, h = 2048

            if (arr.Length < 2)
                throw new ArgumentException("Invalid size string: " + size); // "2048x" ?????
            if (!uint.TryParse(arr[0], out uint width) || !uint.TryParse(arr[1], out uint height))
                throw new ArgumentException("Could not parse the size string: " + size);
            if (((width & (width - 1)) != 0) || ((height & (height - 1)) != 0))
                throw new ArgumentException("Width and Height must be a power of 2!");

            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{Width}x{Height}"; // "2048x2048"
        }
    }
}