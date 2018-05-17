using System;
using System.Collections;
using System.IO;
using NUnit.Framework;

namespace Giggle.DataTest
{
    /// <summary>
    /// Verifies that the test environment files are built correctly 
    /// </summary>
    [TestFixture]
    public class TestEnvironmentBuilder : BaseTestWithEnvironment
    {
        [Test]
        public void TestDocsFile()
        {
            FileStream fs = File.OpenRead(EnvironmentBuilder.INDEX_DIRECTORY + EnvironmentBuilder.DOCS_FILE);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            Assert.AreEqual("d1", r.ReadLine());        //docId = 0
            Assert.AreEqual("d2", r.ReadLine());  //docId = 1
            Assert.AreEqual("d3", r.ReadLine());  //docId = 2
            Assert.AreEqual("d4", r.ReadLine());  //docId = 3
            Assert.AreEqual("d5", r.ReadLine());  //docId = 4
            Assert.AreEqual("d6", r.ReadLine());  //docId = 5
            Assert.AreEqual("d7", r.ReadLine());  //docId = 6
            Assert.AreEqual("d8", r.ReadLine());  //docId = 7
            Assert.AreEqual("d9", r.ReadLine());  //docId = 8
            Assert.AreEqual("d10", r.ReadLine()); //docId = 9
            Assert.IsNull(r.ReadLine());
            r.Close();
            fs.Close();
        }

        [Test]
        public void TestTermsFile()
        {
            FileStream fs = File.OpenRead(EnvironmentBuilder.INDEX_DIRECTORY + EnvironmentBuilder.TERMS_FILE);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            Assert.AreEqual("t1", r.ReadLine());    //termId = 0
            Assert.AreEqual("t2", r.ReadLine());    //termId = 1
            Assert.AreEqual("t3", r.ReadLine());    //termId = 2
            Assert.AreEqual("t4", r.ReadLine());    //termId = 3
            Assert.AreEqual("t5", r.ReadLine());    //termId = 4
            Assert.AreEqual("t6", r.ReadLine());    //termId = 5
            r.Close();
            fs.Close();
        }

        [Test]
        public void TestDocsDataFile()
        {
            Helper h = new Helper();
            float[] norms = h.GetNorms();
            float[] pr = h.GetPageRanks();

            FileStream fs = File.OpenRead(EnvironmentBuilder.INDEX_DIRECTORY + EnvironmentBuilder.DOCSDATA_FILE);
            BinaryReader r = new BinaryReader(fs);

            Assert.AreEqual(EnvironmentBuilder.DOCS, r.ReadInt16());
            Assert.AreEqual(norms[0], r.ReadSingle());
            Assert.AreEqual(pr[0], r.ReadSingle());
            Assert.AreEqual(norms[1], r.ReadSingle());
            Assert.AreEqual(pr[1], r.ReadSingle());
            Assert.AreEqual(norms[2], r.ReadSingle());
            Assert.AreEqual(pr[2], r.ReadSingle());
            Assert.AreEqual(norms[3], r.ReadSingle());
            Assert.AreEqual(pr[3], r.ReadSingle());
            Assert.AreEqual(norms[4], r.ReadSingle());
            Assert.AreEqual(pr[4], r.ReadSingle());
            Assert.AreEqual(norms[5], r.ReadSingle());
            Assert.AreEqual(pr[5], r.ReadSingle());
            Assert.AreEqual(norms[6], r.ReadSingle());
            Assert.AreEqual(pr[6], r.ReadSingle());
            Assert.AreEqual(norms[7], r.ReadSingle());
            Assert.AreEqual(pr[7], r.ReadSingle());
            Assert.AreEqual(norms[8], r.ReadSingle());
            Assert.AreEqual(pr[8], r.ReadSingle());
            Assert.AreEqual(norms[9], r.ReadSingle());
            Assert.AreEqual(pr[9], r.ReadSingle());
            r.Close();
            fs.Close();
        }

        [Test]
        public void TestTermsDataFile()
        {
            Helper h = new Helper();
            float[] idf = h.GetIDFs();

            FileStream fs = File.OpenRead(EnvironmentBuilder.INDEX_DIRECTORY + EnvironmentBuilder.TERMSDATA_FILE);
            BinaryReader r = new BinaryReader(fs);

            Assert.AreEqual(EnvironmentBuilder.TERMS, r.ReadInt32());
            Assert.AreEqual(9, r.ReadInt16());
            Assert.AreEqual(idf[0], r.ReadSingle());
            Assert.AreEqual(5, r.ReadInt16());
            Assert.AreEqual(idf[1], r.ReadSingle());
            Assert.AreEqual(6, r.ReadInt16());
            Assert.AreEqual(idf[2], r.ReadSingle());
            Assert.AreEqual(5, r.ReadInt16());
            Assert.AreEqual(idf[3], r.ReadSingle());
            Assert.AreEqual(7, r.ReadInt16());
            Assert.AreEqual(idf[4], r.ReadSingle());
            Assert.AreEqual(5, r.ReadInt16());
            Assert.AreEqual(idf[5], r.ReadSingle());

            r.Close();
            fs.Close();
        }

        [Test]
        public void TestTermDocsFile()
        {
            FileStream fs = File.OpenRead(EnvironmentBuilder.INDEX_DIRECTORY + EnvironmentBuilder.TERMDOCS_FILE);
            BinaryReader r = new BinaryReader(fs);

            Assert.AreEqual(0, r.ReadInt32());
            Assert.AreEqual(0, r.ReadInt16());
            Assert.AreEqual(24, r.ReadInt16());

            Assert.AreEqual(0, r.ReadInt32());
            Assert.AreEqual(1, r.ReadInt16());
            Assert.AreEqual(32, r.ReadInt16());

            Assert.AreEqual(0, r.ReadInt32());
            Assert.AreEqual(2, r.ReadInt16());
            Assert.AreEqual(12, r.ReadInt16());

            Assert.AreEqual(0, r.ReadInt32());
            Assert.AreEqual(3, r.ReadInt16());
            Assert.AreEqual(6, r.ReadInt16());

            Assert.AreEqual(0, r.ReadInt32());
            Assert.AreEqual(4, r.ReadInt16());
            Assert.AreEqual(43, r.ReadInt16());

            Assert.AreEqual(0, r.ReadInt32());
            Assert.AreEqual(5, r.ReadInt16());
            Assert.AreEqual(2, r.ReadInt16());

            Assert.AreEqual(0, r.ReadInt32());
            Assert.AreEqual(7, r.ReadInt16());
            Assert.AreEqual(3, r.ReadInt16());

            Assert.AreEqual(0, r.ReadInt32());
            Assert.AreEqual(8, r.ReadInt16());
            Assert.AreEqual(1, r.ReadInt16());

            Assert.AreEqual(0, r.ReadInt32());
            Assert.AreEqual(9, r.ReadInt16());
            Assert.AreEqual(6, r.ReadInt16());

            Assert.AreEqual(1, r.ReadInt32());
            Assert.AreEqual(0, r.ReadInt16());
            Assert.AreEqual(21, r.ReadInt16());

            Assert.AreEqual(1, r.ReadInt32());
            Assert.AreEqual(1, r.ReadInt16());
            Assert.AreEqual(10, r.ReadInt16());

            Assert.AreEqual(1, r.ReadInt32());
            Assert.AreEqual(2, r.ReadInt16());
            Assert.AreEqual(16, r.ReadInt16());

            Assert.AreEqual(1, r.ReadInt32());
            Assert.AreEqual(3, r.ReadInt16());
            Assert.AreEqual(7, r.ReadInt16());

            Assert.AreEqual(1, r.ReadInt32());
            Assert.AreEqual(4, r.ReadInt16());
            Assert.AreEqual(31, r.ReadInt16());

            Assert.AreEqual(2, r.ReadInt32());
            Assert.AreEqual(0, r.ReadInt16());
            Assert.AreEqual(9, r.ReadInt16());

            Assert.AreEqual(2, r.ReadInt32());
            Assert.AreEqual(1, r.ReadInt16());
            Assert.AreEqual(5, r.ReadInt16());

            Assert.AreEqual(2, r.ReadInt32());
            Assert.AreEqual(2, r.ReadInt16());
            Assert.AreEqual(5, r.ReadInt16());

            Assert.AreEqual(2, r.ReadInt32());
            Assert.AreEqual(3, r.ReadInt16());
            Assert.AreEqual(2, r.ReadInt16());

            Assert.AreEqual(2, r.ReadInt32());
            Assert.AreEqual(4, r.ReadInt16());
            Assert.AreEqual(20, r.ReadInt16());

            Assert.AreEqual(2, r.ReadInt32());
            Assert.AreEqual(6, r.ReadInt16());
            Assert.AreEqual(1, r.ReadInt16());

            Assert.AreEqual(3, r.ReadInt32());
            Assert.AreEqual(5, r.ReadInt16());
            Assert.AreEqual(18, r.ReadInt16());

            Assert.AreEqual(3, r.ReadInt32());
            Assert.AreEqual(6, r.ReadInt16());
            Assert.AreEqual(32, r.ReadInt16());

            Assert.AreEqual(3, r.ReadInt32());
            Assert.AreEqual(7, r.ReadInt16());
            Assert.AreEqual(22, r.ReadInt16());

            Assert.AreEqual(3, r.ReadInt32());
            Assert.AreEqual(8, r.ReadInt16());
            Assert.AreEqual(34, r.ReadInt16());

            Assert.AreEqual(3, r.ReadInt32());
            Assert.AreEqual(9, r.ReadInt16());
            Assert.AreEqual(17, r.ReadInt16());

            Assert.AreEqual(4, r.ReadInt32());
            Assert.AreEqual(1, r.ReadInt16());
            Assert.AreEqual(3, r.ReadInt16());

            Assert.AreEqual(4, r.ReadInt32());
            Assert.AreEqual(4, r.ReadInt16());
            Assert.AreEqual(3, r.ReadInt16());

            Assert.AreEqual(4, r.ReadInt32());
            Assert.AreEqual(5, r.ReadInt16());
            Assert.AreEqual(7, r.ReadInt16());

            Assert.AreEqual(4, r.ReadInt32());
            Assert.AreEqual(6, r.ReadInt16());
            Assert.AreEqual(12, r.ReadInt16());

            Assert.AreEqual(4, r.ReadInt32());
            Assert.AreEqual(7, r.ReadInt16());
            Assert.AreEqual(4, r.ReadInt16());

            Assert.AreEqual(4, r.ReadInt32());
            Assert.AreEqual(8, r.ReadInt16());
            Assert.AreEqual(27, r.ReadInt16());

            Assert.AreEqual(4, r.ReadInt32());
            Assert.AreEqual(9, r.ReadInt16());
            Assert.AreEqual(4, r.ReadInt16());

            Assert.AreEqual(5, r.ReadInt32());
            Assert.AreEqual(0, r.ReadInt16());
            Assert.AreEqual(3, r.ReadInt16());

            Assert.AreEqual(5, r.ReadInt32());
            Assert.AreEqual(5, r.ReadInt16());
            Assert.AreEqual(16, r.ReadInt16());

            Assert.AreEqual(5, r.ReadInt32());
            Assert.AreEqual(7, r.ReadInt16());
            Assert.AreEqual(2, r.ReadInt16());

            Assert.AreEqual(5, r.ReadInt32());
            Assert.AreEqual(8, r.ReadInt16());
            Assert.AreEqual(25, r.ReadInt16());

            Assert.AreEqual(5, r.ReadInt32());
            Assert.AreEqual(9, r.ReadInt16());
            Assert.AreEqual(23, r.ReadInt16());

            r.Close();
            fs.Close();
        }
    }
}
