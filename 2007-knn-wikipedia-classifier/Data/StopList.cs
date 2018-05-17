using System;
using System.IO;
using System.Collections;

namespace DataMining.Data
{
    public class StopList
    {
        private Hashtable sl;

        public StopList(string stopFile)
        {
            sl = new Hashtable();
            load(stopFile);
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
