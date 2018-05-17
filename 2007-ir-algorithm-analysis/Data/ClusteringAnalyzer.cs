using System;

namespace Giggle.Data
{
    /// <summary>
    /// calculates inter and intra cluster similarity
    /// </summary>
    public class ClusteringAnalyzer
    {
        private Index index;
        private short[] allDocIds;
        private DocTermMatrix matrix;

        public ClusteringAnalyzer(Index index, short[] allDocIds)
        {
            this.index = index;
            this.allDocIds = allDocIds;
            matrix = new DocTermMatrix(allDocIds, index);
        }

        public float GetIntraDistance(Cluster c)
        {
            return (float)Math.Pow(matrix.GetCentroid(c), 2);
        }

        public float GetInterDistance(Cluster c1, Cluster c2)
        {
            float centroid1 = matrix.GetCentroid(c1);
            float centroid2 = matrix.GetCentroid(c2);
            float norm1 = matrix.GetNorm(c1);
            float norm2 = matrix.GetNorm(c2);
            return (centroid1 * centroid2) / (norm1 * norm2);
        }
    }
}
