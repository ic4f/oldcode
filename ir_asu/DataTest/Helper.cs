using System;

namespace Giggle.DataTest
{
    public class Helper
    {
        public Helper() { }

        public float[] GetIDFs()
        {
            double docs = Convert.ToDouble(EnvironmentBuilder.DOCS);
            float[] idf = new float[EnvironmentBuilder.TERMS];
            idf[0] = Convert.ToSingle(Math.Log(docs / 9, 2));
            idf[1] = Convert.ToSingle(Math.Log(docs / 5, 2));
            idf[2] = Convert.ToSingle(Math.Log(docs / 6, 2));
            idf[3] = Convert.ToSingle(Math.Log(docs / 5, 2));
            idf[4] = Convert.ToSingle(Math.Log(docs / 7, 2));
            idf[5] = Convert.ToSingle(Math.Log(docs / 5, 2));

            return idf;
        }

        public float[] GetNorms()
        {
            float[] idf = GetIDFs();
            double temp;

            float[] norms = new float[EnvironmentBuilder.DOCS];
            temp = Math.Pow(24 * idf[0], 2) + Math.Pow(21 * idf[1], 2) + Math.Pow(9 * idf[2], 2) + Math.Pow(3 * idf[5], 2);
            norms[0] = Convert.ToSingle(Math.Sqrt(temp));

            temp = Math.Pow(32 * idf[0], 2) + Math.Pow(10 * idf[1], 2) + Math.Pow(5 * idf[2], 2) + Math.Pow(3 * idf[4], 2);
            norms[1] = Convert.ToSingle(Math.Sqrt(temp));

            temp = Math.Pow(12 * idf[0], 2) + Math.Pow(16 * idf[1], 2) + Math.Pow(5 * idf[2], 2);
            norms[2] = Convert.ToSingle(Math.Sqrt(temp));

            temp = Math.Pow(6 * idf[0], 2) + Math.Pow(7 * idf[1], 2) + Math.Pow(2 * idf[2], 2);
            norms[3] = Convert.ToSingle(Math.Sqrt(temp));

            temp = Math.Pow(43 * idf[0], 2) + Math.Pow(31 * idf[1], 2) + Math.Pow(20 * idf[2], 2) + Math.Pow(3 * idf[4], 2);
            norms[4] = Convert.ToSingle(Math.Sqrt(temp));

            temp = Math.Pow(2 * idf[0], 2) + Math.Pow(18 * idf[3], 2) + Math.Pow(7 * idf[4], 2) + Math.Pow(16 * idf[5], 2);
            norms[5] = Convert.ToSingle(Math.Sqrt(temp));

            temp = Math.Pow(1 * idf[2], 2) + Math.Pow(32 * idf[3], 2) + Math.Pow(12 * idf[4], 2);
            norms[6] = Convert.ToSingle(Math.Sqrt(temp));

            temp = Math.Pow(3 * idf[0], 2) + Math.Pow(22 * idf[3], 2) + Math.Pow(4 * idf[4], 2) + Math.Pow(2 * idf[5], 2);
            norms[7] = Convert.ToSingle(Math.Sqrt(temp));

            temp = Math.Pow(1 * idf[0], 2) + Math.Pow(34 * idf[3], 2) + Math.Pow(27 * idf[4], 2) + Math.Pow(25 * idf[5], 2);
            norms[8] = Convert.ToSingle(Math.Sqrt(temp));

            temp = Math.Pow(6 * idf[0], 2) + Math.Pow(17 * idf[3], 2) + Math.Pow(4 * idf[4], 2) + Math.Pow(23 * idf[5], 2);
            norms[9] = Convert.ToSingle(Math.Sqrt(temp));

            return norms;
        }

        public float[] GetPageRanks() //CHANGE THIS
        {
            float[] pr = new float[EnvironmentBuilder.DOCS];
            pr[0] = 0f;
            pr[1] = 0f;
            pr[2] = 0f;
            pr[3] = 0f;
            pr[4] = 0f;
            pr[5] = 0f;
            pr[6] = 0f;
            pr[7] = 0f;
            pr[8] = 0f;
            pr[9] = 0f;

            return pr;
        }
    }
}
