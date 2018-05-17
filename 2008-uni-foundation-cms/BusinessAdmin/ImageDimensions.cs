using System;

namespace Foundation.BusinessAdmin
{
    public struct ImageDimensions
    {
        public ImageDimensions(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int Width { get { return width; } }

        public int Height { get { return height; } }

        private int width;
        private int height;
    }
}
