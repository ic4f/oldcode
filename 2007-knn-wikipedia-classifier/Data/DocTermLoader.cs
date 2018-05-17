using System;
using System.IO;

namespace DataMining.Data
{
    /// <summary>
    /// loads term-doc and doc-term collections
    /// source file format: termId(int) docId(short) termCount(short)
    /// </summary>
    public class DocTermLoader
    {
        private DocTermItemCollection[] docTerms;
        private TermDocItemCollection[] termDocs;
        private short[] docTermCounts;
        private short[] termDocCounts;

        public DocTermLoader(short[] docTermCounts, short[] termDocCounts)
        {
            this.docTermCounts = docTermCounts;
            this.termDocCounts = termDocCounts;
            docTerms = new DocTermItemCollection[docTermCounts.Length];
            termDocs = new TermDocItemCollection[termDocCounts.Length];
        }

        public void LoadFromTextFile(string path)
        {
            FileStream fs = File.OpenRead(path);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            int termId;
            short docId;
            short termCount;
            float termWeight = -1;  //load this only from binary file!

            int space1;
            int space2;
            string line;
            while ((line = r.ReadLine()) != null)
            {
                space1 = line.IndexOf(" ");
                space2 = line.IndexOf(" ", space1 + 1);
                termId = Convert.ToInt32(line.Substring(0, space1));
                docId = Convert.ToInt16(line.Substring(space1 + 1, space2 - space1 - 1));
                termCount = Convert.ToInt16(line.Substring(space2 + 1));

                if (termDocs[termId] == null)
                    termDocs[termId] = new TermDocItemCollection(termDocCounts[termId]);
                termDocs[termId].Add(docId, termCount, termWeight);

                if (docTerms[docId] == null)
                    docTerms[docId] = new DocTermItemCollection(docTermCounts[docId]);
                docTerms[docId].Add(termId, termCount);
            }
            r.Close();
            fs.Close();
        }

        public void LoadFromBinaryFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);

            int termId;
            short docId;
            short termCount;
            float termWeight;

            long length = fs.Length;
            for (long i = 0; i < length; i += 12)
            {
                termId = r.ReadInt32();
                docId = r.ReadInt16();
                termWeight = r.ReadSingle();
                termCount = r.ReadInt16();

                if (termDocs[termId] == null)
                    termDocs[termId] = new TermDocItemCollection(termDocCounts[termId]);
                termDocs[termId].Add(docId, termCount, termWeight);

                if (docTerms[docId] == null)
                    docTerms[docId] = new DocTermItemCollection(docTermCounts[docId]);
                docTerms[docId].Add(termId, termCount);
            }
            r.Close();
            fs.Close();
        }

        public DocTermItemCollection[] GetDocTermItemCollection { get { return docTerms; } }

        public TermDocItemCollection[] GetTermDocItemCollection { get { return termDocs; } }
    }
}
