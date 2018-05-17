using System;

namespace DataMining.Data
{
    public class ResultDocument : IComparable
    {
        private int docId;
        private float similarity;

        public ResultDocument(int docId, float similarity)
        {
            this.docId = docId;
            this.similarity = similarity;
        }

        public int CompareTo(object o) //reversed!
        {
            ResultDocument d = (ResultDocument)o;
            if (d.Similarity > similarity)
                return 1;
            else if (d.Similarity < similarity)
                return -1;
            else
                return 0;
        }

        public int DocId { get { return docId; } }

        public float Similarity { get { return similarity; } }
    }
}
