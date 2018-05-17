using System;

namespace DataMining.Data
{
    public class TermDocItem
    {
        private short docId;
        private short termCount;
        private float termWeight;

        public TermDocItem(short docId, short termCount, float termWeight)
        {
            this.docId = docId;
            this.termCount = termCount;
            this.termWeight = termWeight;
        }

        public short DocId { get { return docId; } }
        public short TermCount { get { return termCount; } }
        public float TermWeight { get { return termWeight; } }
    }
}
