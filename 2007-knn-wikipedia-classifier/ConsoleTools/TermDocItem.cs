using System;

namespace DataMining.ConsoleTools
{
    public class TermDocItem
    {
        private int docId;
        private int termCount;

        public TermDocItem(int docId, int termCount)
        {
            this.docId = docId;
            this.termCount = termCount;
        }

        public int DocId { get { return docId; } }

        public int TermCount { get { return termCount; } }
    }
}
