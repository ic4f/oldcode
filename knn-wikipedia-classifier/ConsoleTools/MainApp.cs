using System;
using System.Collections;
using d = DataMining.Data;
using i = DataMining.Indexing;

namespace DataMining.ConsoleTools
{
    class MainApp
    {
        [STAThread]
        static void Main(string[] args)
        {
            AggregateTester at = new AggregateTester(0, 10, 1, 10, 100, 10, 5, 10, 1);
            at.Run();

            Hashtable result = new Hashtable();
            result.Add(1, true);
            result.Add(2, true);
            result.Add(3, true);

            Hashtable relevant = new Hashtable();
            relevant.Add(1, true);
            relevant.Add(3, true);
            relevant.Add(5, true);
            relevant.Add(7, true);
            relevant.Add(8, true);

            d.PerformanceCalculator pc = new d.PerformanceCalculator(result, relevant);
            Console.WriteLine("Precision = " + pc.Precision);
            Console.WriteLine("Recall = " + pc.Recall);
            Console.WriteLine("FMeasure = " + pc.FMeasure);
            
            d.DocsLoader dl = new d.DocsLoader();
            d.CatsLoader cl = new d.CatsLoader();
            d.DocCatsLoader dc = new d.DocCatsLoader(cl);
            int docId = 1;
            ArrayList al = dc.GetDocCategories(docId);
            Console.WriteLine(dl.GetDocTitle(docId) + " has " + al.Count + " categories: ");
            foreach (int catId in al)
                Console.WriteLine("  " + cl.GetCategory(catId));                  


            d.Index index = new d.Index(Helper.INDEX_PATH);
            d.DocTermItem[] dterms = index.DocTerms(0);

            SearchVS s = new SearchVS(Helper.INDEX_PATH);
            s.run();

            i.DataLoader dal = new i.DataLoader(Helper.SOURCE_PATH);
            i.IndexBuilder ib = new i.IndexBuilder(dal, Helper.INDEX_PATH);
            ib.BuildIndex();


            PorterStemmerAlgorithm.PorterStemmer ps = new PorterStemmerAlgorithm.PorterStemmer();
            Console.WriteLine(ps.stemTerm("beautify"));

            TermFilter f = new TermFilter();
            f.CreateNewTermsFile();

            TermProcessor p = new TermProcessor();
            p.CreateTermsFile();

            TermDocsProcessor tdp = new TermDocsProcessor();
            tdp.CreateTermDocsFile();
            tdp.CreateTermDocsFile();
        }
    }
}
