using System;

namespace DataMining.ConsoleTools
{
    public class DocTermItem
    {
        private int termId;
        private int termCount;

        public DocTermItem(int termId, int termCount)
        {
            this.termId = termId;
            this.termCount = termCount;
        }

        public int TermId { get { return termId; } }

        public int TermCount { get { return termCount; } }
    }
}
