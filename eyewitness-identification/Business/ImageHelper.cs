using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
namespace Ei.Business
{
    public class ImageHelper
    {
        public static double DIMENSIONS_RATIO = 0.75;
        public static int SMALL_PHOTO_WIDTH = 96;
        public static int MEDIUM_PHOTO_WIDTH = 240;
        public static int LARGE_PHOTO_WIDTH = 480;
        public static string SMALL_PHOTO_SUFFIX = "s";
        public static string MEDIUM_PHOTO_SUFFIX = "m";
        public static string LARGE_PHOTO_SUFFIX = "l";

        public static string PHOTOS_PATH = @"C:\_development\VS projects\ei_data\photos\";

        public ImageHelper()
        {
        }

        public static bool IsImage(string extension)
        {
            return extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".png";
        }

        public void MakeImage(string sourcePath, string targetPath, int targetWidth, int targetHeight, double targetRatio)
        {
            processFile(new Bitmap(sourcePath), targetPath, targetWidth, targetHeight, targetRatio);
        }

        public void MakeImage(HttpPostedFile file, string targetPath, int targetWidth, int targetHeight, double targetRatio)
        {
            processFile(new Bitmap(file.InputStream), targetPath, targetWidth, targetHeight, targetRatio);
        }

        private void processFile(Bitmap source, string targetPath, int targetWidth, int targetHeight, double targetRatio)
        {
            double sourceRatio = (double)source.Width / (double)source.Height;

            int newWidth = source.Width;
            int newHeight = source.Height;

            //adjust source dimensions
            if (sourceRatio >= targetRatio) //first adjust height
            {
                newHeight = Math.Min(newHeight, source.Height);
                newWidth = (int)(newHeight * targetRatio);
            }
            else //first adjust width
            {
                newWidth = Math.Min(newWidth, source.Width);
                newHeight = (int)(newWidth / targetRatio);
            }

            int sourceX = (int)(source.Width - newWidth) / 2;
            int sourceY = (int)(source.Height - newHeight) / 2;

            if (newWidth < targetWidth) //height is implied: the ration is the same
            {
                targetWidth = newWidth;
                targetHeight = newHeight;
            }

            Bitmap image = new Bitmap(targetWidth, targetHeight);
            Graphics imageGraphic = Graphics.FromImage(image);

            imageGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            imageGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Rectangle targetRect = new Rectangle(0, 0, targetWidth, targetHeight);
            Rectangle souceRect = new Rectangle(sourceX, sourceY, newWidth, newHeight);

            imageGraphic.DrawImage(source, targetRect, souceRect, GraphicsUnit.Pixel);

            image.Save(targetPath, ImageFormat.Jpeg);

            source.Dispose();
            imageGraphic.Dispose();
            image.Dispose();
        }
    }
}
