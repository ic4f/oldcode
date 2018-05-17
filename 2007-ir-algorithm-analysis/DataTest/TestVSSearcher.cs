using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using d = Giggle.Data;

namespace Giggle.DataTest
{
    [TestFixture]
    public class TestVSSearcher
    {
        [Test]
        public void TestMakeQuery()
        {
            ArrayList q1 = vs.MakeQuery("t1");
            Assert.AreEqual(1, q1.Count);
            Assert.IsTrue(q1.Contains("t1"));

            ArrayList q2 = vs.MakeQuery("t1 t2 t6");
            Assert.AreEqual(3, q2.Count);
            Assert.IsTrue(q2.Contains("t1"));
            Assert.IsTrue(q2.Contains("t2"));
            Assert.IsTrue(q2.Contains("t6"));

            ArrayList q3 = vs.MakeQuery("t1 a t3 b c d");
            Assert.AreEqual(2, q3.Count);
            Assert.IsTrue(q3.Contains("t1"));
            Assert.IsTrue(q3.Contains("t3"));
            Assert.IsFalse(q3.Contains("a"));

            ArrayList q4 = vs.MakeQuery("a t1");
            Assert.AreEqual(1, q4.Count);
            Assert.IsTrue(q4.Contains("t1"));

            ArrayList q5 = vs.MakeQuery("a");
            Assert.AreEqual(0, q5.Count);
        }

        [Test]
        public void TestGetRelevantDocs()
        {
            short a;
            Hashtable results = vs.GetRelevantDocs(vs.MakeQuery("t1"));
            Assert.AreEqual(9, results.Count);

            results = vs.GetRelevantDocs(vs.MakeQuery("t1 t2 t6"));
            Assert.AreEqual(1, results.Count);
            a = 0;
            Assert.AreEqual(Convert.ToSingle(27.6480751), Convert.ToSingle(results[a]));    //tests sum of weights

            results = vs.GetRelevantDocs(vs.MakeQuery("t1 a t3 b c d"));
            Assert.AreEqual(5, results.Count);

            results = vs.GetRelevantDocs(vs.MakeQuery("a t5"));
            Assert.AreEqual(7, results.Count);
            a = 1;
            Assert.AreEqual(1.54372, Convert.ToSingle(results[a]));
            a = 4;
            Assert.AreEqual(1.54372, Convert.ToSingle(results[a]));
            a = 5;
            Assert.AreEqual(3.602012, Convert.ToSingle(results[a]));
            a = 6;
            Assert.AreEqual(6.174878, Convert.ToSingle(results[a]));
            a = 7;
            Assert.AreEqual(2.058293, Convert.ToSingle(results[a]));
            a = 8;
            Assert.AreEqual(13.89348, Convert.ToSingle(results[a]));
            a = 9;
            Assert.AreEqual(2.058293, Convert.ToSingle(results[a]));
        }

        [SetUp]
        public void SetUp()
        {
            vs = new d.VSSearcher(new d.Index(EnvironmentBuilder.INDEX_DIRECTORY), 0.6f);
        }

        private d.VSSearcher vs;
    }
}
