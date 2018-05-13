using System;
using System.Collections;


namespace DataMining.Data
{
    /// <summary>
    /// same as VSSearcher, but takes as an argument a docId
    /// </summary>
    public class DocVSSearcher
    {
        private DocsLoader dLoader;
        private CatsLoader cLoader;
        private Index index;
        private const int TRAININGSET = 5000;

        public DocVSSearcher(Index index, DocsLoader dLoader, CatsLoader cLoader)
        {
            this.dLoader = dLoader;
            this.cLoader = cLoader;
            this.index = index;
        }

        public ResultDocument[] Search(int docId, int topResults)
        {
            //make a hashtable for terms in the doc which is being compared to all the rest
            Hashtable termIds = new Hashtable();
            DocTermItem[] docTerms = index.DocTerms(docId);
            foreach (DocTermItem dti in docTerms)
                termIds.Add(dti.TermId, dti.TermCount);

            ResultDocument[] allResults = new ResultDocument[TRAININGSET];
            float docNorm = index.GetDocNorm(docId);
            float doc2norm;
            float similarity;
            for (int doc2id = 0; doc2id < allResults.Length; doc2id++)
            {
                doc2norm = index.GetDocNorm(doc2id);
                similarity = getDotProduct(docId, doc2id, termIds) / (docNorm * doc2norm);
                allResults[doc2id] = new ResultDocument(doc2id, similarity);
            }

            Array.Sort(allResults);

            ResultDocument[] results = new ResultDocument[topResults];
            int j = 0;
            for (int i = 0; i < topResults; i++)
            {
                if (allResults[j].DocId == docId) //do not return the doc itself!
                    j++;

                results[i] = allResults[j++];
            }

            return results;
        }

        private float getDotProduct(int doc1id, int doc2id, Hashtable termIds)
        {
            DocTermItem[] docTerms;
            docTerms = index.DocTerms(doc2id);
            float dotProduct = 0;
            float idf;
            int termCount;
            foreach (DocTermItem dti in docTerms)
            {
                if (termIds.Contains(dti.TermId))
                {
                    termCount = Convert.ToInt32(termIds[dti.TermId]);
                    idf = index.GetTermIDF(dti.TermId);
                    dotProduct += (termCount * idf * dti.TermCount * idf); //dot =+ (t1f * idf)(t2f * idf)
                }
            }
            return dotProduct;
        }
    }
}
