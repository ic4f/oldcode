using System;
using System.Collections;

namespace DataMining.Data
{
    public class DocTermMatrix
    {
        private float[,] matrix;
        private Hashtable docIds; //key=docId value=doc position in matrix
        private Hashtable termIds; //key=termId value=term position in matrix
        private Hashtable termDocCounts; //key=termId value=docCount per term
        private Hashtable docNorms; //key=docId, value=norm

        public DocTermMatrix(short[] docs, Index index)
        {
            init(docs, index);
        }

        public float GetSimilarity(short doc1Id, short doc2Id)
        {
            float doc1Norm = Convert.ToSingle(docNorms[doc1Id]);
            float doc2Norm = Convert.ToSingle(docNorms[doc2Id]);

            float dotProduct = 0;
            int d1 = docPosition(doc1Id);
            int d2 = docPosition(doc2Id);
            for (int i = 0; i < termIds.Count; i++)
                dotProduct += (matrix[d1, i] * matrix[d2, i]);

            float sim = dotProduct / (doc1Norm * doc2Norm);
            //Console.WriteLine("sim for " + doc1Id + " and " + doc2Id + " = " + sim);
            return sim;
        }

        private void init(short[] docs, Index index)
        {
            loadIds(docs, index);
            loadMatrix(docs, index);
            calculateNorms(docs);
        }

        private void loadIds(short[] docs, Index index)
        {
            int estimatedHashSize = docs.Length; //for performance only
            termDocCounts = new Hashtable(estimatedHashSize);
            docIds = new Hashtable(estimatedHashSize);
            termIds = new Hashtable(estimatedHashSize);
            int docCursor = 0;
            int termCursor = 0;

            DocTermItem[] docTerms;
            foreach (short docId in docs)
            {
                docIds.Add(docId, docCursor++);
                docTerms = index.DocTerms(docId);
                foreach (DocTermItem dt in docTerms)
                {
                    int termId = dt.TermId;
                    if (termIds[termId] == null)
                        termIds.Add(termId, termCursor++);

                    if (termDocCounts[termId] == null)
                        termDocCounts[termId] = 1;
                    else
                        termDocCounts[termId] = Convert.ToInt32(termDocCounts[termId]) + 1;
                }
            }
        }

        private void loadMatrix(short[] docs, Index index)
        {
            matrix = new float[docIds.Count, termIds.Count];

            DocTermItem[] docTerms;
            float idf;
            int termId;
            short termCount;
            double docCount; //double required for correct division
            foreach (short docId in docs)
            {
                docTerms = index.DocTerms(docId);
                foreach (DocTermItem dt in docTerms)
                {
                    termId = dt.TermId;
                    termCount = dt.TermCount;
                    docCount = Convert.ToDouble(termDocCounts[termId]);
                    idf = Convert.ToSingle(Math.Log((double)docs.Length / docCount, 2));
                    matrix[docPosition(docId), termPosition(termId)] = idf * (float)termCount;
                }
            }
        }

        private void calculateNorms(short[] docs)
        {
            docNorms = new Hashtable(docIds.Count);
            for (int i = 0; i < docs.Length; i++)
            {
                short docId = docs[i];
                double sumOfSquares = 0;
                int d = docPosition(docId);
                for (int j = 0; j < termIds.Count; j++)
                    sumOfSquares += Math.Pow(matrix[d, j], 2);
                docNorms.Add(docId, Convert.ToSingle(Math.Sqrt(sumOfSquares)));
            }
        }

        private int docPosition(short docId)
        {
            return Convert.ToInt32(docIds[docId]);
        }

        private int termPosition(int termId)
        {
            return Convert.ToInt32(termIds[termId]);
        }
    }
}
