using System;
using NUnit.Framework;
using i = IrProject.Indexing;

namespace IrProject.Testing
{
    [TestFixture]
    public class UrlHelper
    {
        [Test]
        public void testRegex()
        {
            i.UrlHelper h = new i.UrlHelper();

            string[] url = new string[9];

            url[0] = "/";
            url[1] = "./";
            url[2] = "../";
            url[3] = "http://www.uni.edu";
            url[4] = "http://www.uni.edu#1";
            url[5] = @"1abc";
            url[6] = @"abc";
            url[7] = @"_abc";
            url[8] = @"1abc130_~!$%&-+=/.,?|\";

            for (int i = 0; i < url.Length; i++)
            {
                Assert.IsTrue(h.IsValidUrl("href=" + url[i] + ">"));
                Assert.IsTrue(h.IsValidUrl("href=" + url[i] + " "));
                Assert.IsTrue(h.IsValidUrl("href = " + url[i] + ">"));
                Assert.IsTrue(h.IsValidUrl("href \n = \n " + url[i] + ">"));
                Assert.IsTrue(h.IsValidUrl("href=" + url[i] + " \n >"));
                Assert.IsTrue(h.IsValidUrl("href='" + url[i] + "'>"));
                Assert.IsTrue(h.IsValidUrl("href= ' " + url[i] + " ' "));
                Assert.IsTrue(h.IsValidUrl("href= \" " + url[i] + " \""));
                Assert.IsTrue(h.IsValidUrl("href=\"" + url[i] + "\""));
                Assert.IsTrue(h.IsValidUrl("href='" + url[i] + " " + url[i] + "'>"));
                Assert.IsTrue(h.IsValidUrl("href='" + url[i] + "\n" + url[i] + "'>"));
                Assert.IsTrue(h.IsValidUrl("href='" + url[i] + "\t" + url[i] + "'>"));
                Assert.IsFalse(h.IsValidUrl("href=" + url[i]));
                Assert.IsFalse(h.IsValidUrl("href="));
                Assert.IsFalse(h.IsValidUrl("href=\"\""));
                Assert.IsFalse(h.IsValidUrl("href=\"  \""));
                Assert.IsFalse(h.IsValidUrl("href= "));
                Assert.IsFalse(h.IsValidUrl("abc"));
                Assert.IsFalse(h.IsValidUrl("\"abc\""));
                Assert.IsFalse(h.IsValidUrl("href='#abc'"));
                Assert.IsFalse(h.IsValidUrl("href='#'"));
            }
        }
    }
}
