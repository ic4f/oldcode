using System;
using System.Text;
using System.Collections;
using System.IO;

namespace DataMining.ConsoleTools
{
    public class StopList
    {
        private Hashtable sl;

        public StopList()
        {
            sl = new Hashtable();
            load(Helper.STOPLIST_FILE_PATH);
        }

        public bool Contains(string term) { return sl.Contains(term); }

        private void load(string stopFile)
        {
            FileStream fs = File.OpenRead(stopFile);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            string line;
            while ((line = r.ReadLine()) != null)
                sl.Add(line, true);

            r.Close();
            fs.Close();
        }
    }
}
