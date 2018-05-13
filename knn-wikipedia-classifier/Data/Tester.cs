using System;
using System.Text;
using System.Collections;
using System.IO;
using d = DataMining.Data;

namespace DataMining.Data
{
    public class Tester
    {
        private d.DocsLoader dLoader;
        private d.CatsLoader cLoader;
        private d.DocCatsLoader dcLoader;
        private d.Index index;
        private d.DocVSSearcher searcher;

        public Tester(d.DocsLoader dLoader, d.CatsLoader cLoader, d.DocCatsLoader dcLoader)
        {
            this.dLoader = dLoader;
            this.cLoader = cLoader;
            this.dcLoader = dcLoader;
            Console.WriteLine("Loading index...");
            index = new d.Index(d.Helper.INDEX_PATH);
            searcher = new d.DocVSSearcher(index, dLoader, cLoader);
        }

        //displays k most similar docs to docsToTest number of docs
        public void ShowSimilarDocs(int docsToTest, int k)
        {
            for (int docId = 0; docId < docsToTest; docId++)
            {
                Console.WriteLine(dLoader.GetDocTitle(docId) + ":");
                d.ResultDocument[] results = searcher.Search(docId, k);
                foreach (d.ResultDocument doc in results)
                    Console.WriteLine("  " + dLoader.GetDocTitle(doc.DocId));
            }
        }

        //displays existing cats for 1 doc
        public void DisplayExistingCategories(int docId, int k)
        {
            Console.WriteLine(dLoader.GetDocTitle(docId) + ":");
            d.ResultDocument[] results = searcher.Search(docId, k);
            foreach (d.ResultDocument doc in results)
            {
                Console.WriteLine("  " + dLoader.GetDocTitle(doc.DocId));
                ArrayList cats = dcLoader.GetDocCategories(doc.DocId);
                foreach (int catId in cats)
                    Console.WriteLine("    cat: " + cLoader.GetCategory(catId));
            }
        }

        //returns a hashtable of newly assigned cats tp docId based on k neighbors and c top cats
        public Hashtable GetNewCategories(int docId, int k, int c)
        {
            Console.WriteLine("calculating for docId=" + docId + " k=" + k + " c=" + c);

            Hashtable allCats = new Hashtable();

            d.ResultDocument[] results = searcher.Search(docId, k);
            foreach (d.ResultDocument doc in results)
            {
                ArrayList cats = dcLoader.GetDocCategories(doc.DocId);
                if (cats != null)
                    foreach (int catId in cats)
                    {
                        if (allCats.Contains(catId))
                        {
                            CategoryCount cc = (CategoryCount)allCats[catId];
                            cc.catCount = cc.catCount + 1;
                        }
                        else
                            allCats.Add(catId, new CategoryCount(catId, 1));
                    }
            }
            //now make an array
            CategoryCount[] arrCats = new CategoryCount[allCats.Count];
            IDictionaryEnumerator en = allCats.GetEnumerator();
            int cursor = 0;
            while (en.MoveNext())
                arrCats[cursor++] = (CategoryCount)en.Value;

            Array.Sort(arrCats); //sort by catCount

            ArrayList maincats = dcLoader.GetDocCategories(docId); //relevant cats

            Hashtable result = new Hashtable();
            int numberOfTopCats = Math.Min(c, arrCats.Length);
            for (int i = 0; i < numberOfTopCats; i++)
                result.Add(arrCats[i].catId, true);

            return result;
        }

        private class CategoryCount : IComparable
        {
            public int catId;
            public int catCount;

            public CategoryCount(int catId, int catCount)
            {
                this.catId = catId;
                this.catCount = catCount;
            }

            public int CompareTo(object o)
            {
                CategoryCount cc2 = (CategoryCount)o;
                if (catCount > cc2.catCount)
                    return -1;
                else if (catCount < cc2.catCount)
                    return 1;
                else
                    return 0;
            }
        }
    }
}
