using System;
using System.Collections;

namespace DataMining.Data
{
    public class PerformanceCalculator
    {
        private Hashtable result;
        private Hashtable relevant;
        int tp;
        int fp;
        int fn;
        float p;
        float r;
        float f;

        public PerformanceCalculator(Hashtable result, Hashtable relevant)
        {
            if (result.Count != 0 && relevant.Count != 0)
            {
                this.result = result;
                this.relevant = relevant;
                tp = 0;
                fp = 0;
                fn = 0;
                calc();
            }
        }

        public float Precision { get { return p; } }
        public float Recall { get { return r; } }
        public float FMeasure { get { return f; } }

        private void calc()
        {
            calcCounts();
            p = (float)tp / (float)(tp + fp);
            r = (float)tp / (float)(tp + fn);
            if (p != 0 || r != 0)
                f = (2 * p * r) / (p + r);
        }

        private void calcCounts()
        {
            IDictionaryEnumerator en = result.GetEnumerator();
            while (en.MoveNext())
                if (relevant.Contains(Convert.ToInt32(en.Key))) tp++;
                else fp++;

            en = relevant.GetEnumerator();
            while (en.MoveNext())
                if (!result.Contains(Convert.ToInt32(en.Key))) fn++;
        }
    }
}
