using System;
using System.IO;
using System.Collections;
using System.Text;
using d = Giggle.Data;

namespace Giggle.Indexing
{
    /// <summary>
    /// augments the files in the existing collection with <t></t> and <w></w> for title and url (one letters simplifies parsing)
    /// </summary>
    public class TagAugmentor
    {
        private const string FILTERED_DOCS_DIRECTORY = @"C:\_current\development\data\isr\temp\filteredindex\";
        private const string TITLES_FILE = @"C:\_current\development\data\isr\temp\newindexresults\titles.txt";
        private const string URL_PREFIX = @"C:\_current\development\data\isr\temp\filteredindex\www.asu.edu%%";

        private Hashtable titles;
        private FileStream fs;
        private StreamReader r;
        private StreamWriter w;
        private StringBuilder sb;

        public TagAugmentor() { }

        public void temp()
        {
            string[] files = Directory.GetFiles(FILTERED_DOCS_DIRECTORY);
            string s = @"C:\_current\development\data\isr\temp\filteredindex\www.asu.edu%%";
            for (int i = 0; i < files.Length; i++)
            {
                string url = files[i];
                Console.WriteLine(getUrlTerms(url.Substring(s.Length)));
            }
        }

        private string getUrlTerms(string s)
        {
            s = s.Replace(".html", "");
            s = s.Replace(".htm", "");
            s = s.Replace(".aspx", "");
            s = s.Replace(".asp", "");
            s = s.Replace(".jsp", "");
            s = s.Replace("-", " ");
            s = s.Replace("_", " ");
            s = s.Replace(",", " ");
            s = s.Replace(".", " ");
            s = s.Replace("~", " ");
            s = s.Replace("!", " ");
            s = s.Replace("%", " ");
            s = s.Replace("&", " ");
            s = s.Replace(":", " ");
            s = s.Replace(";", " ");
            s = s.Replace("?", " ");
            return s;
        }

        public void Augment()
        {
            loadTitles();

            string[] files = Directory.GetFiles(FILTERED_DOCS_DIRECTORY);
            for (int i = 0; i < files.Length; i++)
            {
                processFile(files[i], i);
                if (i % 100 == 0)
                    Console.WriteLine("processing file " + i);
            }
        }

        private void processFile(string path, int docId)
        {
            string url = getUrlTerms(path.Substring(URL_PREFIX.Length));

            sb = new StringBuilder();
            fs = File.OpenRead(path);
            r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            while ((line = r.ReadLine()) != null)
                sb.Append(line);

            r.Close();
            fs.Close();

            w = new StreamWriter(path, true, System.Text.Encoding.ASCII);
            w.WriteLine("\n<t>" + titles[docId] + "</t>\n");
            w.WriteLine("\n<w>" + url + "</w>\n");
            w.Close();
        }

        private void loadTitles()
        {
            titles = new Hashtable();
            FileStream fs = new FileStream(TITLES_FILE, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            int docId = 0;
            string line;
            while ((line = r.ReadLine()) != null)
                titles.Add(docId++, line);
            r.Close();
            fs.Close();
        }
    }
}
