using System;
using System.Collections;
using System.IO;

namespace DataMining.Data
{
    /// <summary>
    /// Provides access to docs, terms and termdocs
    /// </summary>
    public class Index
    {
        private string directoryPath; //index directory path
        private int numberOfTerms;
        private int numberOfDocs;
        private short[] termDocCounts; //# of docs each term occurs in		
        private short[] docTermCounts; //# of terms each doc occurs in	
        private TermDocItemCollection[] termDocs;
        private DocTermItemCollection[] docTerms;
        private float[] docNorms; //doc norms
        private float[] pageRanks; //doc page ranks		
        private short[] docLinksCount; //# of links a doc points to
        private short[] docCitationsCount; //# of citations a doc has (docs pointing to it)
        private Hashtable docIds; //key=url, value=docId
        private Hashtable termIds; //key=term, value=termId (used to get termId by term text)
        private string[] docs; //position=docId, value=url (used to get a url by docId)
        private string[] terms; //position=termId, value=term 
        private float[] idf;

        public Index(string directoryPath)
        {
            this.directoryPath = directoryPath;
            loadTermsData();
            loadTerms();
            loadDocsData();
            loadDocs();
            loadTermDocsDocTerms();
        }

        public int NumberOfTerms { get { return numberOfTerms; } }

        public int NumberOfDocs { get { return numberOfDocs; } }

        public float GetTermIDF(int termId) { return idf[termId]; }

        public short GetDocId(string url)
        {
            if (docIds.Contains(url))
                return Convert.ToInt16(docIds[url]);
            else
                return -1;
        }

        public string GetURL(int docId) { return docs[docId]; }

        public string GetTerm(int termId) { return terms[termId]; }

        public bool HasTerm(string term) { return termIds.Contains(term); }

        public float GetDocNorm(int docId) { return docNorms[docId]; }

        public float GetPageRank(int docId) { return pageRanks[docId]; }

        public TermDocItem[] TermDocs(string term)
        {
            return TermDocs(Convert.ToInt32(termIds[term]));
        }

        public TermDocItem[] TermDocs(int termId)
        {
            if (termId < numberOfTerms && termId >= 0)
                return termDocs[termId].TermDocItems;
            else
                return null;
        }

        public DocTermItem[] DocTerms(int docId)
        {
            if (docId < numberOfDocs && docId >= 0)
                return docTerms[docId].DocTermItems;
            else
                return null;
        }

        /* ------------------------------------private -------------------------------- */
        private void loadTermsData()
        {
            FileStream fs = new FileStream(directoryPath + Helper.INDEX_TERMSDATA_FILE, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);

            numberOfTerms = r.ReadInt32();
            termDocCounts = new short[numberOfTerms];
            idf = new float[numberOfTerms];

            long length = fs.Length - 4; //accounts for reading first int			
            int termId = 0;
            for (long i = 0; i < length; i += 6) //advance counter by 6 bytes			
            {
                termDocCounts[termId] = r.ReadInt16();  //reads 2 bytes	
                idf[termId] = r.ReadSingle(); //4 bytes
                termId++;
            }

            r.Close();
            fs.Close();
        }

        private void loadTerms()
        {
            termIds = new Hashtable(numberOfTerms);
            terms = new string[numberOfTerms];
            FileStream fs = new FileStream(directoryPath + Helper.INDEX_TERMS_FILE, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            int i = 0;
            while ((line = r.ReadLine()) != null)
            {
                termIds.Add(line, Convert.ToInt32(i));
                terms[i++] = line;
            }
            r.Close();
            fs.Close();
        }

        private void loadDocsData()
        {
            FileStream fs = new FileStream(directoryPath + Helper.INDEX_DOCSDATA_FILE, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);

            numberOfDocs = r.ReadInt16();
            docNorms = new float[numberOfDocs];
            pageRanks = new float[numberOfDocs];
            docLinksCount = new short[numberOfDocs];
            docCitationsCount = new short[numberOfDocs];
            docTermCounts = new short[numberOfDocs];

            long length = fs.Length - 2; //accounts for reading first short
            int docId = 0;
            for (long i = 0; i < length; i += 14) //advance counter by 12 bytes
            {
                docNorms[docId] = r.ReadSingle();   //reads 4 bytes	
                docLinksCount[docId] = r.ReadInt16(); //read 2 bytes
                docCitationsCount[docId] = r.ReadInt16(); //read 2 bytes
                pageRanks[docId] = r.ReadSingle(); //read 4 bytes				
                docTermCounts[docId] = r.ReadInt16(); //read 2 bytes
                docId++;
            }
            r.Close();
            fs.Close();
        }

        private void loadDocs()
        {
            docIds = new Hashtable(numberOfDocs);
            docs = new string[numberOfDocs];
            FileStream fs = new FileStream(directoryPath + Helper.INDEX_DOCS_FILE, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            int i = 0;
            while ((line = r.ReadLine()) != null)
            {
                docIds.Add(line, i);
                docs[i++] = line;
            }
            r.Close();
            fs.Close();
        }

        private void loadTermDocsDocTerms()
        {
            DocTermLoader dtl = new DocTermLoader(docTermCounts, termDocCounts);
            dtl.LoadFromBinaryFile(directoryPath + Helper.INDEX_TERMDOCS_FILE);
            termDocs = dtl.GetTermDocItemCollection;
            docTerms = dtl.GetDocTermItemCollection;
        }
    }
}
