using System;
using System.Text;
using System.Collections;
using System.IO;

namespace DataMining.Data
{
    public class CatsLoader
    {
        private Hashtable cats; //key=cat, value=catId
        private Hashtable catIds; //key=catId, value=cat

        public CatsLoader()
        {
            cats = new Hashtable();
            catIds = new Hashtable();
            load();
        }

        public int CategoryCount { get { return cats.Count; } }

        public bool HasCategory(int catId) { return catIds.Contains(catId); }

        public bool HasCategory(string category) { return cats.Contains(category); }

        public string GetCategory(int catId)
        {
            return catIds[catId].ToString();
        }

        public int GetCategoryId(string category)
        {
            return Convert.ToInt32(cats[category]);
        }

        private void load()
        {
            FileStream fs = new FileStream(Helper.INDEX_PATH + Helper.CATS_FILE, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            int catId = 0;
            while ((line = r.ReadLine()) != null)
            {
                cats.Add(line, catId);
                catIds.Add(catId, line);
                catId++;
            }
            r.Close();
            fs.Close();
        }
    }
}
