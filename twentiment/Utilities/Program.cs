using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterCrawler.Utilities
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("STARTING");

            //BoundedCrawler cr = new BoundedCrawler();
            //cr.Run();

            OverlapProcessor op = new OverlapProcessor();
            op.Run();

            //Extractor ex = new Extractor();
            //ex.ExtractHashTags();

            Console.WriteLine("TERMINATED");
        }
    }
}
