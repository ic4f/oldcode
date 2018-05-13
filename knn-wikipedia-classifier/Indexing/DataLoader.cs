using System;
using System.IO;
using System.Collections;
using d = DataMining.Data;

namespace DataMining.Indexing
{
    /// <summary>
    /// Loads initial source data: 
    /// - terms (term per line)
    /// - docs (doc per line), 
    ///	- termdocs (termId docId [# of terms per doc]) 
    ///	- termdoccounts ([# docs per term] per line)
    ///	- doctermcounts
    ///	Provides access to this data, which is used to generate final data files. 
    /// </summary>
    public class DataLoader
    {
        private string[] docs;
        private Hashtable docIds;
        private string[] terms;
        private Hashtable termIds;
        private short[] termDocCounts;
        private short[] docTermCounts;
        private d.TermDocItemCollection[] termDocs;
        private d.DocTermItemCollection[] docTerms;

        public DataLoader(string sourceDirectoryPath)
        {
            float c = 0.6f;
            load(sourceDirectoryPath, c);
        }

        public short NumberOfDocs { get { return Convert.ToInt16(docs.Length); } }

        public int NumberOfTerms { get { return terms.Length; } }

        public string[] Docs { get { return docs; } }

        public string[] Terms { get { return terms; } }

        public Hashtable DocIds { get { return docIds; } }

        public Hashtable TermIds { get { return termIds; } }

        public short[] TermDocCounts { get { return termDocCounts; } }

        public short[] DocTermCounts { get { return docTermCounts; } }

        public d.TermDocItem[] GetTermDocItems(int termId)
        {
            return termDocs[termId].TermDocItems;
        }

        public d.DocTermItem[] GetDocTermItems(int termId)
        {
            return docTerms[termId].DocTermItems;
        }

        private void load(string sourceDirectory, float c)
        {
            loadDocs(sourceDirectory);
            loadTerms(sourceDirectory);
            loadCounts(sourceDirectory);
            loadTermDocsDocTerms(sourceDirectory);
        }

        private void loadDocs(string sourceDirectory)
        {
            string sourceFile = sourceDirectory + d.Helper.SOURCE_DOCS_FILE;
            docs = new string[countItems(sourceFile)];
            docIds = new Hashtable();

            FileStream fs = File.OpenRead(sourceFile);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            int docId = 0;
            string line;
            while ((line = r.ReadLine()) != null)
            {
                docs[docId] = line;
                docIds.Add(line, docId);
                docId++;
            }
            r.Close();
            fs.Close();
        }

        private void loadTerms(string sourceDirectory)
        {
            string sourceFile = sourceDirectory + d.Helper.SOURCE_TERMS_FILE;
            terms = new string[countItems(sourceFile)];
            termIds = new Hashtable();

            FileStream fs = File.OpenRead(sourceFile);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            int termId = 0;
            string line;
            while ((line = r.ReadLine()) != null)
            {
                terms[termId] = line;
                termIds.Add(line, termId);
                termId++;
            }
            r.Close();
            fs.Close();
        }

        private void loadCounts(string sourceDirectory)
        {
            termDocCounts = new short[terms.Length];
            docTermCounts = new short[docs.Length];

            FileStream fs = File.OpenRead(sourceDirectory + d.Helper.SOURCE_TERMDOCS_FILE);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            int termId;
            short docId;
            int space1;
            int space2;
            string line;
            while ((line = r.ReadLine()) != null)
            {
                space1 = line.IndexOf(" ");
                space2 = line.IndexOf(" ", space1 + 1);
                termId = Convert.ToInt32(line.Substring(0, space1));
                docId = Convert.ToInt16(line.Substring(space1 + 1, space2 - space1 - 1));
                termDocCounts[termId] = (short)(termDocCounts[termId] + 1);
                docTermCounts[docId] = (short)(docTermCounts[docId] + 1);
            }
            r.Close();
            fs.Close();
        }

        private void loadTermDocsDocTerms(string sourceDirectory)
        {
            d.DocTermLoader dtl = new d.DocTermLoader(docTermCounts, termDocCounts);
            dtl.LoadFromTextFile(sourceDirectory + d.Helper.SOURCE_TERMDOCS_FILE);
            termDocs = dtl.GetTermDocItemCollection;
            docTerms = dtl.GetDocTermItemCollection;
        }

        private int countItems(string path)
        {
            FileStream fs = File.OpenRead(path);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            int count = 0;
            while (r.ReadLine() != null)
                count++;
            r.Close();
            fs.Close();
            return count;
        }
    }
}
