using System;
using System.Collections;

namespace Giggle.Data
{
    public class VSSearcher
    {
        private Index index;
        private float w; //if w=1 -> then only VS, if w=0 -> then only PageRank

        public VSSearcher(Index index, float w)
        {
            this.index = index;
            this.w = w;
        }

        public ResultDocument[] Search(string query)
        {
            ArrayList queryTerms = MakeQuery(query); //exclude non-terms		

            if (queryTerms.Count == 0)
                return new ResultDocument[0]; //no relevant docs -> return empty array

            float queryNorm = Convert.ToSingle(Math.Sqrt(queryTerms.Count));
            Hashtable docs = GetRelevantDocs(queryTerms);//docs relevant to ALL terms + sums of term weights for each doc

            //cos(q,d) = dot(q,d)/|q|x|d| 
            short docId;
            float sumOfWeights;
            float docNorm;
            float cos;
            float pageRank;
            float similarity;
            ResultDocument[] results = new ResultDocument[docs.Count];
            int cursor = 0;
            IDictionaryEnumerator en = docs.GetEnumerator();
            while (en.MoveNext())
            {
                docId = Convert.ToInt16(en.Key);
                sumOfWeights = Convert.ToSingle(en.Value);
                docNorm = index.GetDocNorm(docId);
                cos = sumOfWeights / (queryNorm * docNorm);
                pageRank = index.GetPageRank(docId);

                similarity = cos;
                if (cos > 0)
                    similarity = w * cos + (1f - w) * pageRank;

                results[cursor++] = new ResultDocument(docId, pageRank, similarity);
            }
            Array.Sort(results);

            return results;
        }

        //made public ONLY for testing purposes!
        public Hashtable GetRelevantDocs(ArrayList queryTerms)
        {
            short docId;
            Hashtable prevDocTermWeights = new Hashtable(); //key = docId, value = term weights per document accumulator
            Hashtable nextDocTermWeights;

            TermDocItem[] termDocs = index.TermDocs(queryTerms[0].ToString()); //get first termDocs
            for (int i = 0; i < termDocs.Length; i++)
            {
                docId = termDocs[i].DocId;
                prevDocTermWeights.Add(docId, termDocs[i].TermWeight); //Add doc and term weight to relevant docs list
            }
            //continue if query has more terms, continue with i=1
            if (queryTerms.Count > 1)
                for (int j = 1; j < queryTerms.Count; j++)
                {
                    nextDocTermWeights = new Hashtable();
                    termDocs = index.TermDocs(queryTerms[j].ToString());
                    for (int i = 0; i < termDocs.Length; i++)
                    {
                        docId = termDocs[i].DocId;
                        if (prevDocTermWeights.Contains(docId))
                            nextDocTermWeights.Add(docId, Convert.ToSingle(prevDocTermWeights[docId]) + termDocs[i].TermWeight); //update
                    }
                    prevDocTermWeights = nextDocTermWeights;
                }
            return prevDocTermWeights;
        }

        //made public ONLY for testing purposes!
        //removes irrelevant temrs from query
        public ArrayList MakeQuery(string query)
        {
            query = query.ToLower();
            char[] delims = { ' ' };
            string[] temp = query.Split(delims);
            ArrayList queryTerms = new ArrayList();
            for (int i = 0; i < temp.Length; i++)
                if (index.HasTerm(temp[i]))
                    queryTerms.Add(temp[i]);

            return queryTerms;
        }
    }
}
