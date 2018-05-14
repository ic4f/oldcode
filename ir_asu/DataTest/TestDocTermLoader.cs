using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using i = Giggle.Indexing;
using d = Giggle.Data;

namespace Giggle.DataTest
{
    /// <summary>
    /// The format of the sorce files is: termId(int) docId(short) termCount(short)
    /// </summary>
    [TestFixture]
    public class TestDocTermLoader
    {
        [Test]
        public void TestLoadFromTextFile()
        {
            d.DocTermLoader loader = new d.DocTermLoader(new short[] { 2, 2, 3 }, new short[] { 3, 2, 2 });
            loader.LoadFromTextFile(rootPath + TEXT_FILE);
            runTermDocsTest(loader);
            runDocTermsTest(loader);
        }

        [Test]
        public void TestLoadFromBinaryFile()
        {
            d.DocTermLoader loader = new d.DocTermLoader(new short[] { 2, 2, 3 }, new short[] { 3, 2, 2 });
            loader.LoadFromBinaryFile(rootPath + BINARY_FILE);
            runTermDocsTest(loader);
            //runDocTermsTest(loader);
        }

        private void runTermDocsTest(d.DocTermLoader loader)
        {
            d.TermDocItemCollection[] termDocs = loader.GetTermDocItemCollection;
            Assert.AreEqual(3, termDocs.Length);

            d.TermDocItem[] term0docs = termDocs[term0].TermDocItems;
            Assert.AreEqual(3, term0docs.Length);
            Assert.AreEqual(doc0, term0docs[0].DocId);
            Assert.AreEqual(termCount00, term0docs[0].TermCount);
            Assert.AreEqual(doc1, term0docs[1].DocId);
            Assert.AreEqual(termCount01, term0docs[1].TermCount);
            Assert.AreEqual(doc2, term0docs[2].DocId);
            Assert.AreEqual(termCount02, term0docs[2].TermCount);

            d.TermDocItem[] term1docs = termDocs[term1].TermDocItems;
            Assert.AreEqual(2, term1docs.Length);
            Assert.AreEqual(doc0, term1docs[0].DocId);
            Assert.AreEqual(termCount10, term1docs[0].TermCount);
            Assert.AreEqual(doc2, term1docs[1].DocId);
            Assert.AreEqual(termCount12, term1docs[1].TermCount);

            d.TermDocItem[] term2docs = termDocs[term2].TermDocItems;
            Assert.AreEqual(2, term2docs.Length);
            Assert.AreEqual(doc1, term2docs[0].DocId);
            Assert.AreEqual(termCount21, term2docs[0].TermCount);
            Assert.AreEqual(doc2, term2docs[1].DocId);
            Assert.AreEqual(termCount22, term2docs[1].TermCount);
        }

        private void runDocTermsTest(d.DocTermLoader loader)
        {
            d.DocTermItemCollection[] docTerms = loader.GetDocTermItemCollection;
            Assert.AreEqual(3, docTerms.Length);

            d.DocTermItem[] doc0terms = docTerms[doc0].DocTermItems;
            Assert.AreEqual(2, doc0terms.Length);
            Assert.AreEqual(term0, doc0terms[0].TermId);
            Assert.AreEqual(termCount00, doc0terms[0].TermCount);
            Assert.AreEqual(term1, doc0terms[1].TermId);
            Assert.AreEqual(termCount10, doc0terms[1].TermCount);

            d.DocTermItem[] doc1terms = docTerms[doc1].DocTermItems;
            Assert.AreEqual(2, doc1terms.Length);
            Assert.AreEqual(term0, doc1terms[0].TermId);
            Assert.AreEqual(termCount01, doc1terms[0].TermCount);
            Assert.AreEqual(term2, doc1terms[1].TermId);
            Assert.AreEqual(termCount21, doc1terms[1].TermCount);

            d.DocTermItem[] doc2terms = docTerms[doc2].DocTermItems;
            Assert.AreEqual(3, doc2terms.Length);
            Assert.AreEqual(term0, doc2terms[0].TermId);
            Assert.AreEqual(termCount02, doc2terms[0].TermCount);
            Assert.AreEqual(term1, doc2terms[1].TermId);
            Assert.AreEqual(termCount12, doc2terms[1].TermCount);
            Assert.AreEqual(term2, doc2terms[2].TermId);
            Assert.AreEqual(termCount22, doc2terms[2].TermCount);
        }

        [SetUp]
        public void SetUp()
        {
            rootPath = @"C:\_current\development\data\isr\test\";
            makeTextFile();
            makeBinaryFile();
        }

        [TearDown]
        public void TearDown()
        {
            string path = rootPath + TEXT_FILE;
            if (File.Exists(path)) File.Delete(path);

            path = rootPath + BINARY_FILE;
            if (File.Exists(path)) File.Delete(path);
        }

        private void makeTextFile()
        {
            string path = rootPath + TEXT_FILE;
            if (File.Exists(path)) File.Delete(path);

            StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
            w.WriteLine(term0 + " " + doc0 + " " + termCount00);
            w.WriteLine(term0 + " " + doc1 + " " + termCount01);
            w.WriteLine(term0 + " " + doc2 + " " + termCount02);
            w.WriteLine(term1 + " " + doc0 + " " + termCount10);
            w.WriteLine(term1 + " " + doc2 + " " + termCount12);
            w.WriteLine(term2 + " " + doc1 + " " + termCount21);
            w.WriteLine(term2 + " " + doc2 + " " + termCount22);
            w.Close();
        }

        private void makeBinaryFile()
        {
            string path = rootPath + BINARY_FILE;
            if (File.Exists(path)) File.Delete(path);

            float temp = 0;

            FileStream fs = new FileStream(path, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);
            w.Write(term0);
            w.Write(doc0);
            w.Write(temp);
            w.Write(termCount00);
            w.Write(term0);
            w.Write(doc1);
            w.Write(temp);
            w.Write(termCount01);
            w.Write(term0);
            w.Write(doc2);
            w.Write(temp);
            w.Write(termCount02);
            w.Write(term1);
            w.Write(doc0);
            w.Write(temp);
            w.Write(termCount10);
            w.Write(term1);
            w.Write(doc2);
            w.Write(temp);
            w.Write(termCount12);
            w.Write(term2);
            w.Write(doc1);
            w.Write(temp);
            w.Write(termCount21);
            w.Write(term2);
            w.Write(doc2);
            w.Write(temp);
            w.Write(termCount22);
            w.Close();
            fs.Close();
        }

        private string rootPath;
        private const string TEXT_FILE = "termdocstest.txt";
        private const string BINARY_FILE = "termdocstest.data";
        private const short doc0 = 0;
        private const short doc1 = 1;
        private const short doc2 = 2;
        private const int term0 = 0;
        private const int term1 = 1;
        private const int term2 = 2;
        private const short termCount00 = 10;
        private const short termCount01 = 15;
        private const short termCount02 = 5;
        private const short termCount10 = 345;
        private const short termCount12 = 2;
        private const short termCount21 = 45;
        private const short termCount22 = 9;
    }
}
