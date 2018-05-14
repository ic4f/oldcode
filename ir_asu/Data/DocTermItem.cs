using System;

namespace Giggle.Data
{
    public class DocTermItem
    {
        private int termId;
        private short termCount;

        public DocTermItem(int termId, short termCount)
        {
            this.termId = termId;
            this.termCount = termCount;
        }

        public int TermId { get { return termId; } }
        public short TermCount { get { return termCount; } }
    }
}
