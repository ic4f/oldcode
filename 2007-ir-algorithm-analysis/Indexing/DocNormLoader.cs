using System;
using System.Collections;
using d = Giggle.Data;

namespace Giggle.Indexing
{
    /// <summary>
    /// Pre-computes norms for all documents
    /// </summary>
    public class DocNormLoader
    {
        private DataLoader dataLoader;
        private float[] norms;
        private float[] idfs;

        public DocNormLoader(DataLoader dataLoader, float[] idfs)
        {
            this.dataLoader = dataLoader;
            this.idfs = idfs;
        }

        public void Load()
        {
            double[] temp = new double[dataLoader.NumberOfDocs]; //accumulate doubles for convenience

            int numOfTerms = dataLoader.NumberOfTerms;
            int numOfDocs = dataLoader.NumberOfDocs;
            short[] termDocFrequences = dataLoader.TermDocCounts;
            short tdf; //# of docs this term occurs in
            short tf; //of of occurances of a term in a doc
            int docId;
            float idf;
            int k;
            d.TermDocItem[] termDocs;

            for (int termId = 0; termId < numOfTerms; termId++) //loop through all terms
            {
                tdf = termDocFrequences[termId];
                termDocs = dataLoader.GetTermDocItems(termId);
                k = termDocs.Length;
                for (int j = 0; j < k; j++)
                {
                    docId = termDocs[j].DocId;
                    tf = termDocs[j].TermCount;
                    idf = idfs[termId];
                    temp[docId] += Math.Pow(tf * idf, 2);
                }
            }

            norms = new float[dataLoader.NumberOfDocs];
            for (int i = 0; i < norms.Length; i++)
                norms[i] = Convert.ToSingle(Math.Sqrt(temp[i]));
        }

        public float[] Norms { get { return norms; } }
    }
}