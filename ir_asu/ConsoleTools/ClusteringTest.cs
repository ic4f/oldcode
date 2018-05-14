using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using d = Giggle.Data;
using i = Giggle.Indexing;

namespace Giggle.ConsoleTools
{
    public class ClusteringTest
    {
        private d.Index index;

        public ClusteringTest(string path)
        {
            Console.WriteLine("Loading index...");
            index = new d.Index(path);
        }

        public void RunKMeans(int k, int toClusterCount, int topDocs)
        {
            run(new d.KMeansClustering(index, k, true), toClusterCount, topDocs);
        }

        public void RunBuckshot(int k, int toClusterCount, int topDocs)
        {
            run(new d.BuckshotClustering(index, k), toClusterCount, topDocs);
        }

        public void RunBisecting(int k, int toClusterCount, int topDocs)
        {
            run(new d.BisectingClustering(index, k), toClusterCount, topDocs);
        }

        private void run(d.Clustering clustering, int toClusterCount, int topDocs)
        {
            d.VSSearcher vs = new d.VSSearcher(index, 0.6f);

            while (true)
            {
                Console.WriteLine("Query: ");
                string query = Console.ReadLine();
                Console.WriteLine(query);
                if (query == "") break;
                d.ResultDocument[] searchResults = vs.Search(query);
                if (searchResults != null && searchResults.Length > 0)
                {
                    int docs = Math.Min(searchResults.Length, toClusterCount);
                    short[] toCluster = new short[docs];
                    for (int i = 0; i < docs; i++)
                        toCluster[i] = searchResults[i].DocId;

                    displayResults(clustering.GetClusters(toCluster, 10), topDocs, toCluster);
                }
                else
                    Console.WriteLine("Your search returned 0 results -> there's nothing to cluster");
            }
        }

        private void displayResults(d.Cluster[] clusters, int topDocs, short[] toCluster)
        {
            d.ClusteringAnalyzer analyzer = new d.ClusteringAnalyzer(index, toCluster);
            int docCount = 0;
            for (int i = 0; i < clusters.Length; i++)
            {
                Console.WriteLine("CLUSTER " + (i + 1) + " documents=" +
                    clusters[i].DocumentCount + " intra-Similarity: " + analyzer.GetIntraDistance(clusters[i]));
                IDictionaryEnumerator en = clusters[i].CommonTermIds.GetEnumerator();
                while (en.MoveNext())
                    Console.Write(" " + index.GetTerm(Convert.ToInt32(en.Key)));

                Console.WriteLine("\n");

                en = clusters[i].DocIds.GetEnumerator();
                int count = 0;
                while (count < topDocs && en.MoveNext())
                {
                    short docId = Convert.ToInt16(en.Key);
                    Console.WriteLine(" " + index.GetTitle(docId));
                    Console.WriteLine(" " + index.GetURL(docId));
                    Console.WriteLine();
                    count++;
                }
                docCount += clusters[i].DocumentCount;
            }
            Console.WriteLine("total clustered documents: " + docCount);
        }
    }
}
