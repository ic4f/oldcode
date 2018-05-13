using System;

namespace DataMining.Data
{
    /// <summary>
    /// Collection of TermDocIems for a given term
    /// </summary>
    public class TermDocItemCollection
    {
        private TermDocItem[] items;
        private int cursor;

        public TermDocItemCollection(int size)
        {
            items = new TermDocItem[size];
            cursor = 0;
        }

        public int Size { get { return items.Length; } }

        public void Add(short docId, short termCount, float termWeight)
        {
            items[cursor++] = new TermDocItem(docId, termCount, termWeight);
        }

        public TermDocItem[] TermDocItems { get { return items; } }
    }
}
