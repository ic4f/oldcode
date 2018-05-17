using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using d = Giggle.Indexing;

namespace Giggle.DataTest
{
    [TestFixture]
    public class TestIndexBuilder
    {
        [Test]
        public void TestDocs()
        {
            StreamReader r = new StreamReader(
                EnvironmentBuilder.INDEX_DIRECTORY + EnvironmentBuilder.DOCS_FILE, System.Text.Encoding.ASCII);
            Assert.AreEqual("d1", r.ReadLine());
            Assert.AreEqual("d2", r.ReadLine());
            Assert.AreEqual("d3", r.ReadLine());
            Assert.AreEqual("d4", r.ReadLine());
            Assert.AreEqual("d5", r.ReadLine());
            Assert.AreEqual("d6", r.ReadLine());
            Assert.AreEqual("d7", r.ReadLine());
            Assert.AreEqual("d8", r.ReadLine());
            Assert.AreEqual("d9", r.ReadLine());
            Assert.AreEqual("d10", r.ReadLine());
            Assert.IsNull(r.ReadLine());
            r.Close();
        }

        [Test]
        public void TestTerms()
        {
            StreamReader r = new StreamReader(
                EnvironmentBuilder.INDEX_DIRECTORY + EnvironmentBuilder.TERMS_FILE, System.Text.Encoding.ASCII);
            Assert.AreEqual("t1", r.ReadLine());
            Assert.AreEqual("t2", r.ReadLine());
            Assert.AreEqual("t3", r.ReadLine());
            Assert.AreEqual("t4", r.ReadLine());
            Assert.AreEqual("t5", r.ReadLine());
            Assert.AreEqual("t6", r.ReadLine());
            Assert.IsNull(r.ReadLine());
            r.Close();
        }

        [Test]
        public void TestTermsData()
        {
            FileStream fs = File.OpenRead(EnvironmentBuilder.INDEX_DIRECTORY + EnvironmentBuilder.TERMSDATA_FILE);
            BinaryReader r = new BinaryReader(fs);

            Assert.AreEqual(EnvironmentBuilder.TERMS, r.ReadInt32());
            Assert.AreEqual(9, r.ReadInt16());
            Assert.AreEqual(5, r.ReadInt16());
            Assert.AreEqual(6, r.ReadInt16());
            Assert.AreEqual(5, r.ReadInt16());
            Assert.AreEqual(7, r.ReadInt16());
            Assert.AreEqual(5, r.ReadInt16());

            r.Close();
            fs.Close();
        }

        [Test]
        public void TestTermDocs()
        {
            FileStream fs = File.OpenRead(EnvironmentBuilder.INDEX_DIRECTORY + EnvironmentBuilder.TERMDOCS_FILE);
            BinaryReader r = new BinaryReader(fs);

            Assert.AreEqual(0, r.ReadInt32());
            Assert.AreEqual(0, r.ReadInt16());
            Assert.AreEqual(3.648074, r.ReadSingle());
            Assert.AreEqual(24, r.ReadInt16());

            Assert.AreEqual(0, r.ReadInt32());
            Assert.AreEqual(1, r.ReadInt16());
            Assert.AreEqual(4.864099, r.ReadSingle());
            Assert.AreEqual(32, r.ReadInt16());

            r.Close();
            fs.Close();
        }

        [SetUp]
        public void SetUp()
        {
            ib = new d.IndexBuilder(
                new d.DataLoader(EnvironmentBuilder.SOURCE_DIRECTORY), EnvironmentBuilder.INDEX_DIRECTORY);
            ib.BuildIndex();
        }

        private d.IndexBuilder ib;
    }
}
