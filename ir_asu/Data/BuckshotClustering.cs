using System;
using System.Collections;

namespace Giggle.Data
{
    public class BuckshotClustering : Clustering
    {
        public BuckshotClustering(Index index, int k) : base(index, k) { }

        public override Cluster[] CalculateClusters(short[] docIds, int commonTerms)
        {
            KMeansClustering kmc = new KMeansClustering(GetIndex, GetK, GetCentroids(docIds), false);
            return kmc.GetClusters(docIds, commonTerms);
        }

        private Cluster[] GetCentroids(short[] docIds)
        {
            Cluster[] clusters = GetInitialClusters(docIds, getSampleSize(docIds.Length));
            DocTermMatrix matrix = new DocTermMatrix(docIds, GetIndex);

            do
            {
                int clusterToMerge1 = 0;
                int clusterToMerge2 = 0;
                float sim = 0;
                float newSim = 0;
                for (int i = 0; i < clusters.Length; i++)
                    for (int j = 0; j < clusters.Length; j++)
                        if (i != j)
                        {
                            newSim = getSimilarity(clusters[i], clusters[j], matrix);
                            if (newSim > sim)
                            {
                                clusterToMerge1 = i;
                                clusterToMerge2 = j;
                                sim = newSim;
                            }
                        }
                clusters = mergeClusters(clusterToMerge1, clusterToMerge2, clusters);
            }
            while (clusters.Length > GetK);

            return clusters;
        }

        private Cluster[] mergeClusters(int c1, int c2, Cluster[] clusters)
        {
            Cluster[] merged = new Cluster[clusters.Length - 1];

            int j = 0;
            int mergedCluster = -1;
            bool foundMerged = false;
            for (int i = 0; i < clusters.Length; i++)
                if (i == c1 || i == c2)
                {
                    if (!foundMerged)
                    {
                        foundMerged = true;
                        mergedCluster = i;
                        merged[mergedCluster] = clusters[i];
                        j++;
                    }
                    else
                    {
                        IDictionaryEnumerator en = clusters[i].DocIds.GetEnumerator();
                        while (en.MoveNext())
                            merged[mergedCluster].AddDoc(Convert.ToInt16(en.Key));
                    }
                }
                else
                    merged[j++] = clusters[i];

            return merged;
        }

        private float getSimilarity(Cluster c1, Cluster c2, DocTermMatrix matrix)
        {
            float centroid1 = matrix.GetCentroid(c1);
            float centroid2 = matrix.GetCentroid(c2);
            float norm1 = matrix.GetNorm(c1);
            float norm2 = matrix.GetNorm(c2);
            return (centroid1 * centroid2) / (norm1 * norm2);
        }

        private int getSampleSize(int docsCount)
        {
            int sampleSize = (int)Math.Min(Math.Sqrt(docsCount), GetK);
            while (sampleSize == GetK)
                sampleSize += 2; //increase by 2 (1 too small i think -> would be hardly any improvement)

            return sampleSize;
        }
    }
}
