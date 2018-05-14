using System;
using System.Collections;

namespace Giggle.Data
{
    public class BisectingClustering : Clustering
    {
        public BisectingClustering(Index index, int k) : base(index, k) { }

        public override Cluster[] CalculateClusters(short[] docIds, int commonTerms)
        {
            Cluster[] clusters = new Cluster[GetK];
            int currentPosition = 0;

            while (currentPosition < clusters.Length - 1)
            {
                //do the splits
                KMeansClustering kmc = new KMeansClustering(GetIndex, 2, false);
                Cluster[] split = kmc.GetClusters(docIds, commonTerms);
                docIds = getDocs(split, clusters, currentPosition);
                currentPosition++;
            }
            //process remaining
            clusters[currentPosition] = new Cluster();
            foreach (short docId in docIds)
                clusters[currentPosition].AddDoc(docId);

            return LoadCommonTerms(clusters, commonTerms);
        }

        private short[] getDocs(Cluster[] split, Cluster[] clusters, int currentPosition)
        {
            Cluster toSplit;
            Cluster toKeep;
            if (split[0].DocumentCount > split[1].DocumentCount) //change to better measure
            {
                toSplit = split[0];
                toKeep = split[1];
            }
            else
            {
                toSplit = split[1];
                toKeep = split[0];
            }
            clusters[currentPosition] = toKeep;

            short[] docs = new short[toSplit.DocumentCount];
            int cursor = 0;
            IDictionaryEnumerator en = toSplit.DocIds.GetEnumerator();
            while (en.MoveNext())
                docs[cursor++] = Convert.ToInt16(en.Key);

            return docs;
        }
    }
}
