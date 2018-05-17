using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using d = Foundation.Data;
using ch = Foundation.BusinessAdmin.ConfigurationHelper;

namespace Foundation.BusinessAdmin
{
    public class ContextLinkAnalyzer
    {
        public ContextLinkAnalyzer() { }

        public void UpdateContextualLinks(int pageId)
        {
            d.Page p = new d.Page(pageId);
            string text = p.Body + p.BodyDraft;

            d.PageFileContextualLinkData pfcl = new d.PageFileContextualLinkData();
            pfcl.DeleteByPage(pageId);

            d.PagePageContextualLinkData ppcl = new d.PagePageContextualLinkData();
            ppcl.DeleteByPage(pageId);

            ArrayList fileIds = getFileIds(text);
            foreach (int id in fileIds)
                pfcl.AddLink(pageId, id);

            ArrayList imageIds = getImageIds(text);
            foreach (int id in imageIds)
                pfcl.AddLink(pageId, id);

            ArrayList pageIds = getPageIds(text);
            foreach (int id in pageIds)
                ppcl.AddLink(pageId, id);
        }

        private ArrayList getFileIds(string text)
        {
            ArrayList arr = new ArrayList();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"{0}{1}(?<1>[0-9]+)\.", ch.FilesDirectoryUrl, UrlHelper.FILE_ID_PREFIX);
            Regex regex = new Regex(sb.ToString());
            MatchCollection mc = regex.Matches(text);
            foreach (Match m in mc)
            {
                int id = Convert.ToInt32(m.Groups[1].Value);
                if (!arr.Contains(id))
                    arr.Add(id);
            }
            return arr;
        }

        private ArrayList getImageIds(string text)
        {
            ArrayList arr = new ArrayList();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"{0}{1}(?<1>[0-9]+)s?\.", ch.FilesDirectoryUrl, UrlHelper.IMAGE_ID_PREFIX);
            Regex regex = new Regex(sb.ToString());
            MatchCollection mc = regex.Matches(text);
            foreach (Match m in mc)
            {
                int id = Convert.ToInt32(m.Groups[1].Value);
                if (!arr.Contains(id))
                    arr.Add(id);
            }
            return arr;
        }

        private ArrayList getPageIds(string text)
        {
            ArrayList arr = new ArrayList();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"aspx\?id={0}(?<1>([0-9]+))", UrlHelper.PAGE_ID_PREFIX);
            Regex regex = new Regex(sb.ToString());
            MatchCollection mc = regex.Matches(text);
            foreach (Match m in mc)
            {
                int id = Convert.ToInt32(m.Groups[1].Value);
                if (!arr.Contains(id))
                    arr.Add(id);
            }
            return arr;
        }
    }
}
