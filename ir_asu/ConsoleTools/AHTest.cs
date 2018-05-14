using System;
using d = Giggle.Data;

namespace Giggle.ConsoleTools
{
    public class AHTest
    {
        private d.Index index;

        public AHTest(string indexPath)
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
                    d.AHPageLoader ahl = new d.AHPageLoader(index, searchResults, 50, 20, 20); //if root>100, then bad results
                    d.AHDocument[] authorities = ahl.Authorities;
                    for (int i = 0; i < 20; i++)
                        Console.WriteLine(" a=" + authorities[i].AuthorityScore + " h=" + authorities[i].HubScore + " " + index.GetURL(authorities[i].DocId));

                    Console.WriteLine();

                    d.AHDocument[] hubs = ahl.Hubs;
                    for (int i = 0; i < 20; i++)
                        Console.WriteLine(" a=" + hubs[i].AuthorityScore + " h=" + hubs[i].HubScore + " " + index.GetURL(hubs[i].DocId));
                }
                else
                    Console.WriteLine("Your search returned 0 results");
            }
        }
    }
}





