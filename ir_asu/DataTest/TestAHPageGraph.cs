using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using d = Giggle.Data;

namespace Giggle.DataTest
{
    [TestFixture]
    public class TestAHPageGraph
    {
        [Test]
        public void TestAuthoritiesAndHubs()
        {
            d.AHPageGraph g = new d.AHPageGraph(5);
            g.AddPage(0);
            g.AddPage(1);
            g.AddPage(2);
            g.AddPage(3);
            g.AddPage(4);
            g.AddLink(0, 4);
            g.AddLink(0, 3);
            g.AddLink(1, 4);
            g.AddLink(2, 4);
            g.AddLink(2, 3);
            g.AddLink(4, 0);

            d.AHDocument[] authorities = g.GetAuthorities();
            d.AHDocument[] hubs = g.GetHubs();

            Assert.AreEqual(5, g.BaseSetSize);

            Assert.AreEqual(5, authorities.Length);

            Assert.AreEqual(5, hubs.Length);

            Console.WriteLine("a:");
            foreach (d.AHDocument d in authorities)
                Console.WriteLine(d.ToString());

            Console.WriteLine("h:");
            foreach (d.AHDocument d in hubs)
                Console.WriteLine(d.ToString());
        }
    }
}
