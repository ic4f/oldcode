using System;
using System.Text;
using ch = Ei.Business.ConfigurationHelper;
using cc = Core.Cms;

namespace Ei.Business
{
    public class UrlHelper
    {
        private UrlHelper() { }

        public static string AdminHomepage
        {
            get { return ch.WebRoot + cc.CmsPageData.ADMIN_HOMEPAGE_URL; }
        }

        public static string GetSuspectImageUrl(int suspectId, string suffix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}{3}.jpg", ch.WebRoot, ch.SuspectsDirectoryUrl, suspectId, suffix);
            return sb.ToString();
        }

        public static string GetPhotoImageUrl(int photoId, string suffix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}.jpg", ch.PhotosDirectoryUrl, photoId, suffix);
            return sb.ToString();
        }
    }
}
