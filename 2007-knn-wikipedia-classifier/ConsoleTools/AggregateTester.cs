using System;
using System.Text;
using System.Collections;
using System.IO;
using d = DataMining.Data;

namespace DataMining.ConsoleTools
{
    public class AggregateTester
    {
        private ArrayList dataResults;
        private int minDoc;
        private int maxDoc;
        private int docIncr;
        private int minK;
        private int maxK;
        private int kIncr;
        private int minC;
        private int maxC;
        private int cIncr;
        private d.DocsLoader dLoader;
        private d.CatsLoader cLoader;
        private d.DocCatsLoader dcLoader;
        private string tab;

        public AggregateTester(
            int minDoc, int maxDoc, int docIncr, int minK, int maxK, int kIncr, int minC, int maxC, int cIncr)
        {
            dLoader = new d.DocsLoader();
            cLoader = new d.CatsLoader();
            dcLoader = new d.DocCatsLoader(cLoader);
            dataResults = new ArrayList();
            this.minDoc = minDoc;
            this.maxDoc = maxDoc;
            this.docIncr = docIncr;
            this.minK = minK;
            this.maxK = maxK;
            this.kIncr = kIncr;
            this.minC = minC;
            this.maxC = maxC;
            this.cIncr = cIncr;
            tab = "\t";
        }

        public void Run()
        {
            d.Tester t = new d.Tester(dLoader, cLoader, dcLoader);

            for (int k = minK; k < maxK; k += kIncr)
            {
                float p = 0;
                float r = 0;
                float f = 0;

                int c = 5;

                for (int docId = minDoc; docId < maxDoc; docId += docIncr)
                {
                    Hashtable result = t.GetNewCategories(docId, k, c);

                    Hashtable relevant = new Hashtable();
                    ArrayList arrRelevant = dcLoader.GetDocCategories(docId);
                    if (arrRelevant != null)
                        foreach (int catId in arrRelevant)
                            relevant.Add(catId, true);

                    d.PerformanceCalculator pc = new d.PerformanceCalculator(result, relevant);
                    p += pc.Precision;
                    r += pc.Recall;
                    f += pc.FMeasure;
                }
                float numOfDocs = (float)(maxDoc - minDoc) / docIncr;
                p = p / numOfDocs;
                r = r / numOfDocs;
                f = f / numOfDocs;
                dataResults.Add(k + tab + c + tab + p + tab + r + tab + f);
            }

            writeResultsToFile();
        }

        private void writeResultsToFile()
        {
            string path = Helper.DATARESULTS_PATH + "AggregateTesterResults.xls";
            if (File.Exists(path))
                File.Delete(path);

            StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
            foreach (string s in dataResults)
                w.WriteLine(s);

            w.Close();
        }
    }
}
