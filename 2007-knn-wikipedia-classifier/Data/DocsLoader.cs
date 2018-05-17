using System;
using System.Text;
using System.Collections;
using System.IO;

namespace DataMining.Data
{
    public class DocsLoader
    {
        private Hashtable docs; //key=extId, value=docId
        private Hashtable docIds; //key=docId, value=extId
        private Hashtable titles; //key=docId, value=title

        public DocsLoader()
        {
            docs = new Hashtable();
            docIds = new Hashtable();
            titles = new Hashtable();
            load();
        }

        public DocRecord[] GetDocs()
        {
            DocRecord[] dr = new DocRecord[docs.Count];
            int cursor = 0;
            IDictionaryEnumerator en = titles.GetEnumerator();
            while (en.MoveNext())
                dr[cursor++] = new DocRecord(Convert.ToInt32(en.Key), en.Value.ToString());

            Array.Sort(dr);
            return dr;
        }

        public int DocCount { get { return docs.Count; } }

        public bool HasDoc(int docId) { return docIds.Contains(docId); }

        public bool HasDoc(string externalId) { return docs.Contains(externalId); }

        public string GetDocTitle(int docId)
        {
            return titles[docId].ToString();
        }

        public string GetDocExternalId(int docId)
        {
            return docIds[docId].ToString();
        }

        public int GetDocId(string externalId)
        {
            return Convert.ToInt32(docs[externalId]);
        }

        private void load()
        {
            FileStream fs = new FileStream(Helper.INDEX_PATH + Helper.DOCIDS_FILE, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            int docId = 0;
            while ((line = r.ReadLine()) != null)
            {
                docs.Add(line, docId);
                docIds.Add(docId, line);
                docId++;
            }
            r.Close();
            fs.Close();

            fs = new FileStream(Helper.INDEX_PATH + Helper.TITLES_FILE, FileMode.Open);
            r = new StreamReader(fs, System.Text.Encoding.ASCII);
            docId = 0;
            while ((line = r.ReadLine()) != null)
            {
                titles.Add(docId, line);
                docId++;
            }
            r.Close();
            fs.Close();
        }
    }
}
