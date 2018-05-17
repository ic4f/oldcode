using System;
using System.IO;
using System.Collections;
using d = Giggle.Data;

namespace Giggle.Indexing
{
    /// <summary>
    /// Pre-computes IDFs, doc norms, page ranks
    /// Generates final data files used by the Index object: 
    ///		docs.txt, docs.data, terms.txt, terms.data, termdocs.data
    /// </summary>
    public class IndexBuilder
    {
        private DataLoader dataLoader;
        private string targetDirectoryPath;
        private float[] idf;

        public IndexBuilder(DataLoader dataLoader, string targetDirectoryPath)
        {
            this.dataLoader = dataLoader;
            this.targetDirectoryPath = targetDirectoryPath;
            calculateIDFs();
        }

        public void BuildIndex()
        {
            createDocsText();
            createDocsData();
            createTermsText();
            createTermsData();
            createTermDocs();
            createLinksData();
            createCitationsData();
        }

        private void calculateIDFs()
        {
            double docs = Convert.ToDouble(dataLoader.NumberOfDocs); //required for correct division
            idf = new float[dataLoader.NumberOfTerms];
            for (int i = 0; i < dataLoader.NumberOfTerms; i++)
                idf[i] = Convert.ToSingle(Math.Log(docs / dataLoader.TermDocCounts[i], 2));
        }

        private void createDocsText()
        {
            string path = targetDirectoryPath + d.Helper.INDEX_DOCS_FILE;
            if (File.Exists(path))
                File.Delete(path);

            StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
            for (int i = 0; i < dataLoader.Docs.Length; i++)
                w.WriteLine(dataLoader.Docs[i]);

            w.Close();
            Console.WriteLine("created " + d.Helper.INDEX_DOCS_FILE);
        }

        private void createDocsData()
        {
            short[] docLinks = dataLoader.DocLinkCounts;
            short[] docCitations = dataLoader.DocCitationCounts;

            DocNormLoader dnl = new DocNormLoader(dataLoader, idf);
            dnl.Load();
            float[] norms = dnl.Norms;

            float[] pr = dataLoader.PageRanks;

            string path = targetDirectoryPath + d.Helper.INDEX_DOCSDATA_FILE;
            if (File.Exists(path))
                File.Delete(path);

            string pathText = targetDirectoryPath + d.Helper.DATATXT_DIRECTORY_NAME + "\\" + d.Helper.INDEX_DOCSDATA_FILE + ".txt";
            if (File.Exists(pathText))
                File.Delete(pathText);

            FileStream fs = new FileStream(path, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);
            StreamWriter wText = new StreamWriter(pathText, false, System.Text.Encoding.ASCII);

            w.Write(dataLoader.NumberOfDocs);
            for (int docId = 0; docId < dataLoader.NumberOfDocs; docId++)
            {
                w.Write(norms[docId]);
                w.Write(docLinks[docId]);
                w.Write(docCitations[docId]);
                w.Write(pr[docId]);
                w.Write(dataLoader.DocTermCounts[docId]);
                wText.WriteLine(norms[docId] + " " + docLinks[docId] + " " + docCitations[docId] + " " + pr[docId]);
            }

            w.Close();
            wText.Close();
            fs.Close();
            Console.WriteLine("created " + d.Helper.INDEX_DOCSDATA_FILE);
        }

        private void createTermsText()
        {
            string path = targetDirectoryPath + d.Helper.INDEX_TERMS_FILE;
            if (File.Exists(path))
                File.Delete(path);

            StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
            for (int i = 0; i < dataLoader.Terms.Length; i++)
                w.WriteLine(dataLoader.Terms[i]);
            w.Close();
            Console.WriteLine("created " + d.Helper.INDEX_TERMS_FILE);
        }

        private void createTermsData()
        {
            string path = targetDirectoryPath + d.Helper.INDEX_TERMSDATA_FILE;
            if (File.Exists(path))
                File.Delete(path);

            string pathText = targetDirectoryPath + d.Helper.DATATXT_DIRECTORY_NAME + "\\" + d.Helper.INDEX_TERMSDATA_FILE + ".txt";
            if (File.Exists(pathText))
                File.Delete(pathText);

            FileStream fs = new FileStream(path, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);
            StreamWriter wText = new StreamWriter(pathText, false, System.Text.Encoding.ASCII);

            w.Write(dataLoader.NumberOfTerms);
            for (int termId = 0; termId < dataLoader.NumberOfTerms; termId++)
            {
                w.Write(dataLoader.TermDocCounts[termId]);
                wText.WriteLine(dataLoader.TermDocCounts[termId]);
            }
            w.Close();
            wText.Close();
            fs.Close();
            Console.WriteLine("created " + d.Helper.INDEX_TERMSDATA_FILE);
        }

        private void createTermDocs()
        {
            string path = targetDirectoryPath + d.Helper.INDEX_TERMDOCS_FILE;
            if (File.Exists(path))
                File.Delete(path);

            string pathText = targetDirectoryPath + d.Helper.DATATXT_DIRECTORY_NAME + "\\" + d.Helper.INDEX_TERMDOCS_FILE + ".txt";
            if (File.Exists(pathText))
                File.Delete(pathText);

            FileStream fs = new FileStream(path, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);
            StreamWriter wText = new StreamWriter(pathText, false, System.Text.Encoding.ASCII);

            d.TermDocItem[] termDocs;
            float termWeight;
            short termCount;
            short docId;

            for (int termId = 0; termId < dataLoader.NumberOfTerms; termId++)
            {
                termDocs = dataLoader.GetTermDocItems(termId);
                for (int j = 0; j < termDocs.Length; j++)
                {
                    docId = termDocs[j].DocId;
                    termWeight = Convert.ToSingle(termDocs[j].TermCount * idf[termId]);
                    termCount = termDocs[j].TermCount;
                    w.Write(termId);
                    w.Write(docId);
                    w.Write(termWeight);
                    w.Write(termCount); //# of these terms in this doc
                    wText.WriteLine(termId + " " + docId + " " + termWeight + " " + termCount);
                }
            }
            w.Close();
            wText.Close();
            fs.Close();
            Console.WriteLine("created " + d.Helper.INDEX_TERMDOCS_FILE);
        }

        private void createLinksData()
        {
            string path = targetDirectoryPath + d.Helper.INDEX_DOCLINKS_FILE;
            if (File.Exists(path))
                File.Delete(path);

            string pathText = targetDirectoryPath + d.Helper.DATATXT_DIRECTORY_NAME + "\\" + d.Helper.INDEX_DOCLINKS_FILE + ".txt";
            if (File.Exists(pathText))
                File.Delete(pathText);

            FileStream fs = new FileStream(path, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);
            StreamWriter wText = new StreamWriter(pathText, false, System.Text.Encoding.ASCII);

            short[][] docLinks = dataLoader.DocLinks;
            for (short docId = 0; docId < docLinks.Length; docId++)
                if (docLinks[docId] != null)
                    for (short j = 0; j < docLinks[docId].Length; j++)
                    {
                        w.Write(docId);
                        w.Write(docLinks[docId][j]);
                        wText.WriteLine(docId + " " + docLinks[docId][j]);
                    }

            w.Close();
            wText.Close();
            fs.Close();
            Console.WriteLine("created " + d.Helper.INDEX_DOCLINKS_FILE);
        }

        private void createCitationsData()
        {
            string path = targetDirectoryPath + d.Helper.INDEX_DOCCITATIONS_FILE;
            if (File.Exists(path))
                File.Delete(path);

            string pathText = targetDirectoryPath + d.Helper.DATATXT_DIRECTORY_NAME + "\\" + d.Helper.INDEX_DOCCITATIONS_FILE + ".txt";
            if (File.Exists(pathText))
                File.Delete(pathText);

            FileStream fs = new FileStream(path, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);
            StreamWriter wText = new StreamWriter(pathText, false, System.Text.Encoding.ASCII);

            short[][] docCitations = dataLoader.DocCitations;
            for (short docId = 0; docId < docCitations.Length; docId++)
                if (docCitations[docId] != null)
                    for (short j = 0; j < docCitations[docId].Length; j++)
                    {
                        w.Write(docId);
                        w.Write(docCitations[docId][j]);
                        wText.WriteLine(docId + " " + docCitations[docId][j]);
                    }

            w.Close();
            wText.Close();
            fs.Close();
            Console.WriteLine("created " + d.Helper.INDEX_DOCCITATIONS_FILE);
        }
    }
}
