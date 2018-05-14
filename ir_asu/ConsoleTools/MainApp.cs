using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using d = Giggle.Data;
using i = Giggle.Indexing;

namespace Giggle.ConsoleTools
{
    class MainApp
    {
        [STAThread]
        static void Main(string[] args)
        {
            string indexPath = @"C:\_current\development\data\isr\luceneindex\index\";

            if (args[0] == "1")
            {
                ClusteringTest cTest = new ClusteringTest(indexPath);
                cTest.RunKMeans(5, 50, 3);
            }
            else if (args[0] == "2")
            {
                ClusteringTest cTest = new ClusteringTest(indexPath);
                cTest.RunBuckshot(5, 50, 3);
            }
            else if (args[0] == "3")
            {
                ClusteringTest cTest = new ClusteringTest(indexPath);
                cTest.RunBisecting(5, 50, 3);
            }
            else if (args[0] == "4")
            {
                AHTest sTest = new AHTest(indexPath);
                sTest.run();
            }
            else
            {
                SearchTest sTest = new SearchTest(indexPath);
                sTest.run();
            }
        }
    }
}
