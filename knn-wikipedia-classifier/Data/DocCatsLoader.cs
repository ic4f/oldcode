using System;
using System.Text;
using System.Collections;
using System.IO;

namespace DataMining.Data
{
    public class DocCatsLoader
    {
        private ArrayList[] docCats;
        private CatsLoader cLoader;

        public DocCatsLoader(CatsLoader cLoader)
        {
            this.cLoader = cLoader;
            docCats = new ArrayList[Helper.NUMBER_OF_DOCS];
            load();
        }

        public ArrayList GetDocCategories(int docId)
        {
            return docCats[docId];
        }

        private void load()
        {
            FileStream fs = new FileStream(Helper.INDEX_PATH + Helper.DOCCATS_FILE, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);

            int docId;
            int catId;

            long length = fs.Length;
            for (long i = 0; i < length; i += 8) //advance counter by 8 bytes
            {
                docId = r.ReadInt32();
                catId = r.ReadInt32();

                if (docCats[docId] == null)
                    docCats[docId] = new ArrayList();

                if (!isWikiCrap(catId))
                    docCats[docId].Add(catId);
            }
            r.Close();
            fs.Close();
        }

        private bool isWikiCrap(int catId)
        {
            string cat = cLoader.GetCategory(catId).ToLower().Trim();
            return cat.IndexOf("articles") > -1 || cat.IndexOf("semi-protected") > -1;
        }
    }
}
