using System;

namespace Giggle.Data
{
    public class ResultDocument : IComparable
    {
        private short docId;
        private float pageRank;
        private float similarity;

        public ResultDocument(short docId, float pageRank, float similarity)
        {
            this.docId = docId;
            this.pageRank = pageRank;
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

        public short DocId { get { return docId; } }

        public float PageRank { get { return pageRank; } }

        public float Similarity { get { return similarity; } }
    }
}
