using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using dt = Foundation.Data;

namespace Foundation.BusinessAdmin
{
    public class ImageHelper
    {
        public static bool IsImage(string extension)
        {
            return new dt.FileTypeData().IsImage(extension);
        }

        public ImageHelper() { }

        public ImageDimensions MakeImage(string sourcePath, string targetPath, int maxWidth, int maxHeight)
        {
            return makeImage(new Bitmap(sourcePath), targetPath, maxWidth, maxHeight);
        }

        public ImageDimensions MakeImage(Stream stream, string targetPath, int maxWidth, int maxHeight)
        {
            return makeImage(new Bitmap(stream), targetPath, maxWidth, maxHeight);
        }

        public ImageDimensions MakeLeftHeader(Stream stream, string targetPath, string text)
        {
            Bitmap source = new Bitmap(stream);

            int width = ConfigurationHelper.HeaderImageLeftWidth;
            int photoHeight = ConfigurationHelper.HeaderImageLeftPhotoHeight;
            int lineHeight = ConfigurationHelper.HeaderImageLeftLineHight;
            if (source.Width != width || source.Height != photoHeight)
                throw new Exception("wrong dimensions!");

            Bitmap image = new Bitmap(width, photoHeight + lineHeight);
            Graphics g = Graphics.FromImage(image);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.DrawImage(source, new Rectangle(0, 0, width, photoHeight),
                0, 0, source.Width, source.Height, GraphicsUnit.Pixel);

            SolidBrush solidBrush1 = new SolidBrush(Color.FromArgb(252, 193, 3));
            g.FillRectangle(solidBrush1, 0, photoHeight, width, lineHeight);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;

            Font f = new Font(FontFamily.GenericSerif, 13, FontStyle.Italic);

            SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(102, 102, 102));
            g.DrawString(text, f, solidBrush2, width - 1, 113, sf);

            SolidBrush solidBrush3 = new SolidBrush(Color.White);
            g.DrawString(text, f, solidBrush3, width - 2, 112, sf);

            image.Save(targetPath, ImageFormat.Jpeg);

            source.Dispose();
            g.Dispose();
            image.Dispose();

            return new ImageDimensions(width, photoHeight + lineHeight);
        }



        #region makeImage
        private ImageDimensions makeImage(Bitmap source, string targetPath, int maxWidth, int maxHeight)
        {
            if (maxWidth <= 0) maxWidth = Int32.MaxValue;
            if (maxHeight <= 0) maxHeight = Int32.MaxValue;

            int newWidth = source.Width;
            int newHeight = source.Height;
            double sourceRatio = (double)source.Height / (double)source.Width;

            if (newWidth > maxWidth)
            {
                newWidth = maxWidth;
                newHeight = (int)(newWidth * sourceRatio);
            }
            if (newHeight > maxHeight)
            {
                newHeight = maxHeight;
                newWidth = (int)(newHeight / sourceRatio);
            }
            if (newWidth == 0) newWidth = 1;
            if (newHeight == 0) newHeight = 1;

            Bitmap image = new Bitmap(newWidth, newHeight);
            Graphics imageGraphic = Graphics.FromImage(image);
            imageGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            imageGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            imageGraphic.DrawImage(source, new Rectangle(0, 0, newWidth, newHeight),
                0, 0, source.Width, source.Height, GraphicsUnit.Pixel);
            image.Save(targetPath, ImageFormat.Jpeg);

            source.Dispose();
            imageGraphic.Dispose();
            image.Dispose();

            return new ImageDimensions(newWidth, newHeight);
        }
        #endregion

    }
}
