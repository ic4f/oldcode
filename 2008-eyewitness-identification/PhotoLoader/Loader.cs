using System;
using System.IO;
using b = Ei.Business;
using d = Ei.Data;

namespace Ei.PhotoLoader
{
    public class Loader
    {
        public Loader() { }

        public void Run()
        {
            string sourcePath = "";
            Console.WriteLine("Please provide the path for the directory containing the photos");
            try
            {
                sourcePath = Console.ReadLine();
                string[] files = Directory.GetFiles(sourcePath);
                d.PhotoData pd = new d.PhotoData();
                int total = files.Length;
                Console.WriteLine("There are " + total + " files in the directory. Process all of them? (y/n)");
                string answer = Console.ReadLine();
                if (answer == "y")
                {
                    string externalref;
                    FileInfo fi;
                    for (int i = 0; i < total; i++)
                    {
                        if (i % 10 == 0)
                            Console.WriteLine("processing photo " + i + " out of " + total);

                        fi = new FileInfo(files[i]);
                        externalref = fi.Name;
                        externalref = externalref.Replace(fi.Extension, "");
                        externalref = externalref.Substring(0, Math.Min(15, externalref.Length));
                        makeImages(pd.Create(externalref), files[i]);
                    }
                    Console.WriteLine("Completed.");
                }
                else
                    Console.WriteLine("Operation aborted.");
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR: " + sourcePath + " does not exist");
            }
        }

        private void makeImages(int photoId, string sourcePath)
        {
            double ratio = b.ImageHelper.DIMENSIONS_RATIO;

            int sWidth = b.ImageHelper.SMALL_PHOTO_WIDTH;
            int sHeight = (int)(sWidth / ratio);
            string sPath = b.ImageHelper.PHOTOS_PATH + photoId + b.ImageHelper.SMALL_PHOTO_SUFFIX + ".jpg";

            int mWidth = b.ImageHelper.MEDIUM_PHOTO_WIDTH;
            int mHeight = (int)(mWidth / ratio);
            string mPath = b.ImageHelper.PHOTOS_PATH + photoId + b.ImageHelper.MEDIUM_PHOTO_SUFFIX + ".jpg";

            int lWidth = b.ImageHelper.LARGE_PHOTO_WIDTH;
            int lHeight = (int)(lWidth / ratio);
            string lPath = b.ImageHelper.PHOTOS_PATH + photoId + b.ImageHelper.LARGE_PHOTO_SUFFIX + ".jpg";

            b.ImageHelper iHelper = new b.ImageHelper();
            iHelper.MakeImage(sourcePath, sPath, sWidth, sHeight, ratio);
            iHelper.MakeImage(sourcePath, mPath, mWidth, mHeight, ratio);
            iHelper.MakeImage(sourcePath, lPath, lWidth, lHeight, ratio);
        }
    }
}
