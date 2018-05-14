using System;
using d = Giggle.Data;

namespace Giggle.ConsoleTools
{
    /// <summary>
    /// Search using the vector spcae model only
    /// </summary>
    public class SearchTest
    {
        private d.Index index;

        public SearchTest(string indexPath)
        {
            Console.WriteLine("Loading index...");
            index = new d.Index(indexPath);
        }

        public void run()
        {
            d.VSSearcher vs = new d.VSSearcher(index, 0.6f);

            while (true)
            {
                Console.WriteLine("Query: ");
                string query = Console.ReadLine();
                if (query == "") break;
                d.ResultDocument[] searchResults = vs.Search(query);
                if (searchResults != null && searchResults.Length > 0)
                {
                    Console.WriteLine(searchResults.Length + " results:");
                    int max = Math.Min(10, searchResults.Length);
                    for (int i = 0; i < max; i++)
                        Console.WriteLine("\t" + searchResults[i].DocId + " " + searchResults[i].Similarity + " " +
                            index.GetURL(searchResults[i].DocId));
                }
                else
                    Console.WriteLine("Your search returned 0 results");
            }
        }
    }
}
