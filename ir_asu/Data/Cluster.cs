using System;
using System.Collections;

namespace Giggle.Data
{
    /// <summary>
    /// holds a cluster of documents with common terms
    /// </summary>
    public class Cluster
    {
        private Hashtable docIds;
        private Hashtable termIds;

        public Cluster()
        {
            docIds = new Hashtable();
            termIds = new Hashtable();
        }

        public void AddDoc(short docId) { docIds.Add(docId, true); }

        public void AddCommonTerm(int termId) { termIds.Add(termId, true); }

        public bool HasDoc(short docId) { return docIds.Contains(docId); }

        public bool HasCommonTerm(short termId) { return termIds.Contains(termId); }

        public int DocumentCount { get { return docIds.Count; } }

        public int CommonTermCount { get { return termIds.Count; } }

        public Hashtable DocIds { get { return docIds; } }

        public Hashtable CommonTermIds { get { return termIds; } }
    }
}
