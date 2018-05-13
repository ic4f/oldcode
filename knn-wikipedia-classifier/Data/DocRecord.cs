using System;

namespace DataMining.Data
{
    public class DocRecord : IComparable
    {
        private int docId;
        private string title;

        public DocRecord(int docId, string title)
        {
            this.docId = docId;
            this.title = title;
        }

        public int DocId { get { return docId; } }

        public string Title { get { return title; } }

        public int CompareTo(object o)
        {
            DocRecord dr = (DocRecord)o;
            return title.CompareTo(dr.Title);
        }
    }
}
