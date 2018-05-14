using System;
using System.IO;

namespace Giggle.Indexing
{
    /// <summary>
    ///Retrieves precomputed page ranks form pageranks.data
    /// </summary>
    public class PageRankLoader
    {
        private float[] ranks;
        private int numberOfDocs;

        public PageRankLoader(int numberOfDocs)
        {
            this.numberOfDocs = numberOfDocs;
        }

        public void LoadPageRanks(string sourcePath) { load(sourcePath); }

        public void CalculateAndWritePageRanks(short[][] links, short[][] citations, float c, string sourcePath)
        {
            PageRankCalculator prc = new PageRankCalculator(links, citations, c);
            ranks = prc.CalculateRanks();
            prc.WriteRanksToFile(sourcePath);
        }

        public float[] PageRanks { get { return ranks; } }

        private void load(string path)
        {
            ranks = new float[numberOfDocs];
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);
            long length = fs.Length;
            int docId = 0;
            for (long i = 0; i < length; i += 4) //advance counter by 4 bytes			
                ranks[docId++] = r.ReadSingle();    //reads 4 bytes				
            r.Close();
            fs.Close();
        }
    }
}
