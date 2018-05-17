using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using d = Giggle.Data;
using i = Giggle.Indexing;

namespace Giggle.DataTest
{
    [TestFixture]
    public class TestDataLoader
    {
        [Test]
        public void TestNumberOfDocs()
        {
            Assert.AreEqual(EnvironmentBuilder.DOCS, loader.NumberOfDocs);
        }

        [Test]
        public void TestNumberOfTerms()
        {
            Assert.AreEqual(EnvironmentBuilder.TERMS, loader.NumberOfTerms);
        }

        [Test]
        public void TestDocs()
        {
            string[] docs = loader.Docs;
            Assert.AreEqual(10, docs.Length);
            Assert.AreEqual("d1", docs[0]);
            Assert.AreEqual("d2", docs[1]);
            Assert.AreEqual("d3", docs[2]);
            Assert.AreEqual("d4", docs[3]);
            Assert.AreEqual("d5", docs[4]);
            Assert.AreEqual("d6", docs[5]);
            Assert.AreEqual("d7", docs[6]);
            Assert.AreEqual("d8", docs[7]);
            Assert.AreEqual("d9", docs[8]);
            Assert.AreEqual("d10", docs[9]);
        }

        [Test]
        public void TestTerms()
        {
            string[] terms = loader.Terms;
            Assert.AreEqual(6, terms.Length);
            Assert.AreEqual("t1", terms[0]);
            Assert.AreEqual("t2", terms[1]);
            Assert.AreEqual("t3", terms[2]);
            Assert.AreEqual("t4", terms[3]);
            Assert.AreEqual("t5", terms[4]);
            Assert.AreEqual("t6", terms[5]);
        }

        [Test]
        public void TestDocIds()
        {
            Hashtable docIds = loader.DocIds;
            Assert.AreEqual(10, docIds.Count);
            Assert.AreEqual(0, docIds["d1"]);
            Assert.AreEqual(1, docIds["d2"]);
            Assert.AreEqual(2, docIds["d3"]);
            Assert.AreEqual(3, docIds["d4"]);
            Assert.AreEqual(4, docIds["d5"]);
            Assert.AreEqual(5, docIds["d6"]);
            Assert.AreEqual(6, docIds["d7"]);
            Assert.AreEqual(7, docIds["d8"]);
            Assert.AreEqual(8, docIds["d9"]);
            Assert.AreEqual(9, docIds["d10"]);
        }

        [Test]
        public void TestTermIds()
        {
            Hashtable termIds = loader.TermIds;
            Assert.AreEqual(6, termIds.Count);
            Assert.AreEqual(0, termIds["t1"]);
            Assert.AreEqual(1, termIds["t2"]);
            Assert.AreEqual(2, termIds["t3"]);
            Assert.AreEqual(3, termIds["t4"]);
            Assert.AreEqual(4, termIds["t5"]);
            Assert.AreEqual(5, termIds["t6"]);
        }

        [Test]
        public void TestTermDocCounts()
        {
            short[] tdc = loader.TermDocCounts;
            Assert.AreEqual(6, tdc.Length);
            Assert.AreEqual(9, tdc[0]);
            Assert.AreEqual(5, tdc[1]);
            Assert.AreEqual(6, tdc[2]);
            Assert.AreEqual(5, tdc[3]);
            Assert.AreEqual(7, tdc[4]);
            Assert.AreEqual(5, tdc[5]);
        }

        [Test]
        public void TestDocTermCounts()
        {
            short[] dtc = loader.DocTermCounts;
            Assert.AreEqual(10, dtc.Length);
            Assert.AreEqual(4, dtc[0]);
            Assert.AreEqual(4, dtc[1]);
            Assert.AreEqual(3, dtc[2]);
            Assert.AreEqual(3, dtc[3]);
            Assert.AreEqual(4, dtc[4]);
            Assert.AreEqual(4, dtc[5]);
            Assert.AreEqual(3, dtc[6]);
            Assert.AreEqual(4, dtc[7]);
            Assert.AreEqual(4, dtc[8]);
            Assert.AreEqual(4, dtc[9]);
        }

        [Test]
        public void TestGetTermDocItems()
        {
            d.TermDocItem[] term1docs = loader.GetTermDocItems(0);
            Assert.AreEqual(9, term1docs.Length);
            runTermDocItemTest(term1docs, 0, 0, 24);
            runTermDocItemTest(term1docs, 1, 1, 32);
            runTermDocItemTest(term1docs, 2, 2, 12);
            runTermDocItemTest(term1docs, 3, 3, 6);
            runTermDocItemTest(term1docs, 4, 4, 43);
            runTermDocItemTest(term1docs, 5, 5, 2);
            runTermDocItemTest(term1docs, 6, 7, 3);
            runTermDocItemTest(term1docs, 7, 8, 1);
            runTermDocItemTest(term1docs, 8, 9, 6);

            d.TermDocItem[] term2docs = loader.GetTermDocItems(1);
            Assert.AreEqual(5, term2docs.Length);
            runTermDocItemTest(term2docs, 0, 0, 21);
            runTermDocItemTest(term2docs, 1, 1, 10);
            runTermDocItemTest(term2docs, 2, 2, 16);
            runTermDocItemTest(term2docs, 3, 3, 7);
            runTermDocItemTest(term2docs, 4, 4, 31);

            d.TermDocItem[] term3docs = loader.GetTermDocItems(2);
            Assert.AreEqual(6, term3docs.Length);
            runTermDocItemTest(term3docs, 0, 0, 9);
            runTermDocItemTest(term3docs, 1, 1, 5);
            runTermDocItemTest(term3docs, 2, 2, 5);
            runTermDocItemTest(term3docs, 3, 3, 2);
            runTermDocItemTest(term3docs, 4, 4, 20);
            runTermDocItemTest(term3docs, 5, 6, 1);

            d.TermDocItem[] term4docs = loader.GetTermDocItems(3);
            Assert.AreEqual(5, term4docs.Length);
            runTermDocItemTest(term4docs, 0, 5, 18);
            runTermDocItemTest(term4docs, 1, 6, 32);
            runTermDocItemTest(term4docs, 2, 7, 22);
            runTermDocItemTest(term4docs, 3, 8, 34);
            runTermDocItemTest(term4docs, 4, 9, 17);

            d.TermDocItem[] term5docs = loader.GetTermDocItems(4);
            Assert.AreEqual(7, term5docs.Length);
            runTermDocItemTest(term5docs, 0, 1, 3);
            runTermDocItemTest(term5docs, 1, 4, 3);
            runTermDocItemTest(term5docs, 2, 5, 7);
            runTermDocItemTest(term5docs, 3, 6, 12);
            runTermDocItemTest(term5docs, 4, 7, 4);
            runTermDocItemTest(term5docs, 5, 8, 27);
            runTermDocItemTest(term5docs, 6, 9, 4);

            d.TermDocItem[] term6docs = loader.GetTermDocItems(5);
            Assert.AreEqual(5, term6docs.Length);
            runTermDocItemTest(term6docs, 0, 0, 3);
            runTermDocItemTest(term6docs, 1, 5, 16);
            runTermDocItemTest(term6docs, 2, 7, 2);
            runTermDocItemTest(term6docs, 3, 8, 25);
            runTermDocItemTest(term6docs, 4, 9, 23);
        }

        [Test]
        public void TestGetDocTermItems()
        {
            d.DocTermItem[] doc1terms = loader.GetDocTermItems(0);
            Assert.AreEqual(4, doc1terms.Length);
            runDocTermItemTest(doc1terms, 0, 0, 24);
            runDocTermItemTest(doc1terms, 1, 1, 21);
            runDocTermItemTest(doc1terms, 2, 2, 9);
            runDocTermItemTest(doc1terms, 3, 5, 3);

            d.DocTermItem[] doc2terms = loader.GetDocTermItems(1);
            Assert.AreEqual(4, doc2terms.Length);
            runDocTermItemTest(doc2terms, 0, 0, 32);
            runDocTermItemTest(doc2terms, 1, 1, 10);
            runDocTermItemTest(doc2terms, 2, 2, 5);
            runDocTermItemTest(doc2terms, 3, 4, 3);

            d.DocTermItem[] doc3terms = loader.GetDocTermItems(2);
            Assert.AreEqual(3, doc3terms.Length);

            d.DocTermItem[] doc4terms = loader.GetDocTermItems(3);
            Assert.AreEqual(3, doc4terms.Length);

            d.DocTermItem[] doc5terms = loader.GetDocTermItems(4);
            Assert.AreEqual(4, doc5terms.Length);

            d.DocTermItem[] doc6terms = loader.GetDocTermItems(5);
            Assert.AreEqual(4, doc6terms.Length);

            d.DocTermItem[] doc7terms = loader.GetDocTermItems(6);
            Assert.AreEqual(3, doc7terms.Length);

            d.DocTermItem[] doc8terms = loader.GetDocTermItems(7);
            Assert.AreEqual(4, doc8terms.Length);

            d.DocTermItem[] doc9terms = loader.GetDocTermItems(8);
            Assert.AreEqual(4, doc9terms.Length);

            d.DocTermItem[] doc10terms = loader.GetDocTermItems(9);
            Assert.AreEqual(4, doc10terms.Length);
        }

        [Test]
        public void TestPageRanks()
        {
        }

        [Test]
        public void TestDocLinksCount()
        {
        }

        [Test]
        public void TestDocCitationsCount()
        {
        }

        private void runTermDocItemTest(d.TermDocItem[] termDocs, int pos, short docId, short termCount)
        {
            Assert.AreEqual(docId, termDocs[pos].DocId);
            Assert.AreEqual(termCount, termDocs[pos].TermCount);
        }

        private void runDocTermItemTest(d.DocTermItem[] docTerms, int pos, short termId, short termCount)
        {
            Assert.AreEqual(termId, docTerms[pos].TermId);
            Assert.AreEqual(termCount, docTerms[pos].TermCount);
        }

        [SetUp]
        public void SetUp()
        {
            loader = new i.DataLoader(EnvironmentBuilder.SOURCE_DIRECTORY);
        }

        private i.DataLoader loader;
    }
}

