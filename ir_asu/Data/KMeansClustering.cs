using System;
using System.Collections;

namespace Giggle.Data
{
    public class KMeansClustering : Clustering
    {
        private Cluster[] centroids;
        private bool getTerms;

        public KMeansClustering(Index index, int k, bool getTerms) : base(index, k)
        {
            centroids = null;
            this.getTerms = getTerms;
        }

        public KMeansClustering(Index index, int k, Cluster[] centroids, bool getTerms) : base(index, k)
        {
            this.centroids = centroids;
            this.getTerms = getTerms;
        }

        public override Cluster[] CalculateClusters(short[] docIds, int commonTerms)
        {
            DocTermMatrix matrix = new DocTermMatrix(docIds, GetIndex);

            Cluster[] prevClusters = GetEmptyClusters(GetK);

            Cluster[] newClusters;
            if (centroids == null)
                newClusters = GetInitialClusters(docIds, GetK);
            else
                newClusters = centroids;

            int iterations = 0;
            int maxIterations = 10;

            do
            {
                prevClusters = newClusters; //save previous clusters
                newClusters = GetEmptyClusters(GetK); //empty new clusters and re-populate, use prevClusters as means

                for (int i = 0; i < docIds.Length; i++) //for each doc
                {
                    short docId = docIds[i];
                    float sim = 0;
                    int assignedCluster = -1;
                    for (int j = 0; j < GetK; j++) //calculate similarity with each clustern
                    {
                        float newSim = getAvgSimilarity(docId, prevClusters[j], matrix);
                        if (newSim > sim) //if new similarity is higher - update similarity and asign to new cluster
                        {
                            sim = newSim;
                            assignedCluster++;
                        }
                    }
                    if (assignedCluster > -1) //assign to cluster ONLY if sim to any cluster is > 0
                        newClusters[assignedCluster].AddDoc(docId);
                }
            } while (iterations++ < maxIterations && !areConverged(prevClusters, newClusters));

            if (getTerms)
                return LoadCommonTerms(removeEmptyClusters(newClusters), commonTerms);
            else
                return removeEmptyClusters(newClusters);
        }

        private float getAvgSimilarity(short docId, Cluster c, DocTermMatrix matrix)
        {
            float sim = 0;
            Hashtable docIds = c.DocIds;
            IDictionaryEnumerator en = docIds.GetEnumerator();
            while (en.MoveNext())
                sim += matrix.GetSimilarity(docId, Convert.ToInt16(en.Key));

            return sim / c.DocumentCount;
        }

        private bool areConverged(Cluster[] prevClusters, Cluster[] newClusters)
        {
            for (int i = 0; i < prevClusters.Length; i++)
                if (!areIdentical(prevClusters[i], newClusters[i]))
                    return false;
            return true;
        }

        private bool areIdentical(Cluster a, Cluster b)
        {
            if (a.DocumentCount != b.DocumentCount)
                return false;

            IDictionaryEnumerator en = a.DocIds.GetEnumerator();
            while (en.MoveNext())
                if (!b.HasDoc(Convert.ToInt16(en.Key)))
                    return false;

            return true;
        }

        private Cluster[] removeEmptyClusters(Cluster[] clusters)
        {
            ArrayList temp = new ArrayList();
            foreach (Cluster c in clusters)
                if (c.DocumentCount > 0) temp.Add(c);

            Cluster[] nonempty = new Cluster[temp.Count];
            for (int i = 0; i < temp.Count; i++)
                nonempty[i] = (Cluster)temp[i];

            return nonempty;
        }
    }
}
