using System;

namespace Giggle.Data
{
    /// <summary>
    /// Collection of DocTermItems for a given doc
    /// </summary>
    public class DocTermItemCollection
    {
        private DocTermItem[] items;
        private int cursor;

        public DocTermItemCollection(int size)
        {
            items = new DocTermItem[size];
            cursor = 0;
        }

        public int Size { get { return items.Length; } }

        public void Add(int termId, short termCount)
        {
            items[cursor++] = new DocTermItem(termId, termCount);
        }

        public DocTermItem[] DocTermItems { get { return items; } }
    }
}
