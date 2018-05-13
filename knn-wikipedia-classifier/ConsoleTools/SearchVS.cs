using System;
using d = DataMining.Data;

namespace DataMining.ConsoleTools
{
    /// <summary>
    /// Search using the vector spcae model only
    /// </summary>
    public class SearchVS
    {
        private string indexPath;

        public SearchVS(string indexPath)
        {
            this.indexPath = indexPath;
        }

        public void run()
        {
            Console.WriteLine("Index loading...");

            DateTime t1 = DateTime.Now;

            d.Index index = new d.Index(indexPath);
            d.VSSearcher vs = new d.VSSearcher(index);

            DateTime t2 = DateTime.Now;

            Console.WriteLine("Query: ");
            string query = Console.ReadLine();
            Console.WriteLine("Searching for " + query);

            d.ResultDocument[] results = vs.Search(query);

            DateTime t3 = DateTime.Now;

            TimeSpan diff1 = t2 - t1;
            TimeSpan diff2 = t3 - t2;

            Console.WriteLine("load index time: " + diff1.Seconds + "." + diff1.Milliseconds);
            Console.WriteLine("search time: " + diff2.Seconds + "." + diff2.Milliseconds);

            if (results != null)
            {
                Console.WriteLine(results.Length + " results:");
                int max = Math.Min(10, results.Length);
                for (int i = 0; i < max; i++)
                    Console.WriteLine("\t" + results[i].DocId + " " + results[i].Similarity + " " + index.GetURL(results[i].DocId));
            }
        }
    }
}
