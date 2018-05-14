using System;
using System.IO;
using System.Collections;
using System.Text;
using d = Giggle.Data;

namespace Giggle.Indexing
{
    /// <summary>
    /// extracts title tag content from each doc and writes to standard out
    /// </summary>
    public class TitleExtractor
    {
        public TitleExtractor() { }

        public void Extract()
        {
            string[] files = Directory.GetFiles(d.Helper.DOCS_PATH);
            for (int i = 0; i < files.Length; i++)
            {
                string title = extractTitle(loadFile(files[i]));
                Console.WriteLine(title);
            }
        }

        private string loadFile(string path)
        {
            FileStream fs = File.OpenRead(path);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            StringBuilder sb = new StringBuilder();
            string line;
            while ((line = r.ReadLine()) != null)
                sb.Append(line);

            return sb.ToString();
        }

        private string extractTitle(string text)
        {
            int a = text.IndexOf("<title>");
            if (a < 0)
                a = text.IndexOf("<TITLE>");
            if (a >= 0)
            {
                a += 7;
                int b = text.IndexOf("</title>");
                if (b < 0)
                    b = text.IndexOf("</TITLE>");
                if (b >= 0)
                    return text.Substring(a, b - a).Trim();
            }
            return null;
        }
    }
}
