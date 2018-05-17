using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using i = Giggle.Indexing;

namespace Giggle.DataTest
{
    [TestFixture]
    public class TestPageRankCalculator
    {
        [Test]
        public void TestRanks()
        {
            short[][] links = new short[4][];
            links[0] = new short[1] { 2 };
            links[1] = new short[1] { 2 };
            links[2] = new short[1] { 3 };
            links[3] = new short[2] { 0, 1 };
            short[][] citations = new short[4][];
            citations[0] = new short[1] { 3 };
            citations[1] = new short[1] { 3 };
            citations[2] = new short[2] { 0, 1 };
            citations[3] = new short[1] { 2 };

            i.PageRankCalculator prc = new i.PageRankCalculator(links, citations, 0.8f);
            float[] ranks = prc.CalculateRanks();
            for (int i = 0; i < ranks.Length; i++)
                Console.WriteLine(i + " " + ranks[i]);
        }
    }
}
