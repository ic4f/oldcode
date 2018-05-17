using System;
using System.Collections;
using NUnit.Framework;
using d = Giggle.Data;
using i = Giggle.Indexing;

namespace Giggle.DataTest
{
    [TestFixture]
    public class TestIndex
    {
        [Test]
        public void TestNumbers()
        {
            //Assert.AreEqual(EnvironmentBuilder.DOCS, index.NumberOfDocs);
            //Assert.AreEqual(EnvironmentBuilder.TERMS, index.NumberOfTerms);
        }

        [Test]
        public void TestGetDocId()
        {
            Assert.AreEqual(1, index.GetDocId("d2"));
        }

        [Test]
        public void TestGetURL()
        {
            Assert.AreEqual("d2", index.GetURL(1));
        }

        [Test]
        public void TestHasTerm()
        {
            Assert.IsTrue(index.HasTerm("t1"));
            Assert.IsTrue(index.HasTerm("t2"));
            Assert.IsTrue(index.HasTerm("t3"));
            Assert.IsTrue(index.HasTerm("t4"));
            Assert.IsTrue(index.HasTerm("t5"));
            Assert.IsTrue(index.HasTerm("t6"));
            Assert.IsFalse(index.HasTerm("notaterm"));
        }

        [Test]
        public void TestGetDocNorm()
        {
            Assert.AreEqual((float)22.523344, index.GetDocNorm(0)); //no need to test further
        }

        [Test]
        public void TestGetPageRank()
        {
            Assert.AreEqual((float)0.545404255, index.GetPageRank(0)); //no need to test further
        }

        [Test]
        public void TestTermDocsByTerm()
        {
            d.TermDocItem[] term2Docs = index.TermDocs("t2");
            Assert.AreEqual(5, term2Docs.Length);
            Assert.AreEqual(0, term2Docs[0].DocId);
            Assert.AreEqual(21, term2Docs[0].TermCount);
            Assert.AreEqual(21, term2Docs[0].TermWeight);
            Assert.AreEqual(1, term2Docs[1].DocId);
            Assert.AreEqual(10, term2Docs[1].TermCount);
            Assert.AreEqual(10, term2Docs[1].TermWeight);
            Assert.AreEqual(2, term2Docs[2].DocId);
            Assert.AreEqual(16, term2Docs[2].TermCount);
            Assert.AreEqual(16, term2Docs[2].TermWeight);
            Assert.AreEqual(3, term2Docs[3].DocId);
            Assert.AreEqual(7, term2Docs[3].TermCount);
            Assert.AreEqual(7, term2Docs[3].TermWeight);
            Assert.AreEqual(4, term2Docs[4].DocId);
            Assert.AreEqual(31, term2Docs[4].TermCount);
            Assert.AreEqual(31, term2Docs[4].TermWeight);
        }

        [Test]
        public void TestTermDocsByTermId()
        {
            Assert.IsNull(index.TermDocs(99));

            d.TermDocItem[] term1Docs = index.TermDocs(0);
            Assert.AreEqual(9, term1Docs.Length);

            Assert.AreEqual(0, term1Docs[0].DocId);
            Assert.AreEqual(3.648074, term1Docs[0].TermWeight);
            Assert.AreEqual(24, term1Docs[0].TermCount);
            Assert.AreEqual(1, term1Docs[1].DocId);
            Assert.AreEqual(4.864099, term1Docs[1].TermWeight);
            Assert.AreEqual(2, term1Docs[2].DocId);
            Assert.AreEqual(1.824037, term1Docs[2].TermWeight);
            Assert.AreEqual(3, term1Docs[3].DocId);
            Assert.AreEqual(0.9120185, term1Docs[3].TermWeight);
            Assert.AreEqual(4, term1Docs[4].DocId);
            Assert.AreEqual(6.536133, term1Docs[4].TermWeight);
            Assert.AreEqual(5, term1Docs[5].DocId);
            Assert.AreEqual(0.3040062, term1Docs[5].TermWeight);
            Assert.AreEqual(7, term1Docs[6].DocId);
            Assert.AreEqual(0.4560093, term1Docs[6].TermWeight);
            Assert.AreEqual(8, term1Docs[7].DocId);
            Assert.AreEqual(0.1520031, term1Docs[7].TermWeight);
            Assert.AreEqual(9, term1Docs[8].DocId);
            Assert.AreEqual(0.9120185, term1Docs[8].TermWeight);
        }

        [Test]
        public void TestDocTerms()
        {
            d.DocTermItem[] doc1Terms = index.DocTerms(0);
            Assert.AreEqual(4, doc1Terms.Length);
            Assert.AreEqual(0, doc1Terms[0].TermId);
            Assert.AreEqual(24, doc1Terms[0].TermCount);
            Assert.AreEqual(1, doc1Terms[1].TermId);
            Assert.AreEqual(21, doc1Terms[1].TermCount);
            Assert.AreEqual(2, doc1Terms[2].TermId);
            Assert.AreEqual(9, doc1Terms[2].TermCount);
            Assert.AreEqual(5, doc1Terms[3].TermId);
            Assert.AreEqual(3, doc1Terms[3].TermCount);

            d.DocTermItem[] doc2Terms = index.DocTerms(1);
            Assert.AreEqual(4, doc2Terms.Length);
            Assert.AreEqual(0, doc2Terms[0].TermId);
            Assert.AreEqual(32, doc2Terms[0].TermCount);
            Assert.AreEqual(1, doc2Terms[1].TermId);
            Assert.AreEqual(10, doc2Terms[1].TermCount);
            Assert.AreEqual(2, doc2Terms[2].TermId);
            Assert.AreEqual(5, doc2Terms[2].TermCount);
            Assert.AreEqual(4, doc2Terms[3].TermId);
            Assert.AreEqual(3, doc2Terms[3].TermCount);

            d.DocTermItem[] doc3Terms = index.DocTerms(2);
            Assert.AreEqual(3, doc3Terms.Length);
        }

        [Test]
        public void TestGetDocLinks()
        {
            short[] doc1Links = index.GetDocLinks(0);
            Assert.AreEqual(1, doc1Links.Length);
            Assert.AreEqual(2, doc1Links[0]);
            short[] doc2Links = index.GetDocLinks(1);
            Assert.AreEqual(1, doc2Links.Length);
            Assert.AreEqual(2, doc2Links[0]);
            short[] doc3Links = index.GetDocLinks(2);
            Assert.AreEqual(1, doc3Links.Length);
            Assert.AreEqual(3, doc3Links[0]);
            short[] doc4Links = index.GetDocLinks(3);
            Assert.AreEqual(2, doc4Links.Length);
            Assert.AreEqual(0, doc4Links[0]);
            Assert.AreEqual(1, doc4Links[1]);
            Assert.IsNull(index.GetDocLinks(4));
        }

        [Test]
        public void TestGetDocCitations()
        {
            short[] doc1Cit = index.GetDocCitations(0);
            Assert.AreEqual(1, doc1Cit.Length);
            Assert.AreEqual(3, doc1Cit[0]);
            short[] doc2Cit = index.GetDocCitations(1);
            Assert.AreEqual(1, doc2Cit.Length);
            Assert.AreEqual(3, doc2Cit[0]);
            short[] doc3Cit = index.GetDocCitations(2);
            Assert.AreEqual(2, doc3Cit.Length);
            Assert.AreEqual(0, doc3Cit[0]);
            Assert.AreEqual(1, doc3Cit[1]);
            short[] doc4Cit = index.GetDocCitations(3);
            Assert.AreEqual(1, doc4Cit.Length);
            Assert.AreEqual(2, doc4Cit[0]);
            Assert.IsNull(index.GetDocCitations(4));
        }

        [SetUp]
        public void SetUp()
        {
            string sourcePath = @"C:\_current\development\data\isr\testindex\source\";
            string indexPath = @"C:\_current\development\data\isr\testindex\index\";
            i.DataLoader dataLoader = new i.DataLoader(sourcePath);
            i.IndexBuilder indexBuilder = new i.IndexBuilder(dataLoader, indexPath);
            indexBuilder.BuildIndex();
            index = new d.Index(indexPath);
        }

        private d.Index index;
    }
}
