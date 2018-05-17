using System;

namespace DataMining.Data
{
    /// <summary>
    /// Stores termId + term count in a given doc
    /// </summary>
    public class TermIdTermCount
    {
        private int termId;
        private short termCount;

        public TermIdTermCount(int termId, short termCount)
        {
            this.termId = termId;
            this.termCount = termCount;
        }

        public int TermId { get { return termId; } }
        public short TermCount { get { return termCount; } }
    }
}
