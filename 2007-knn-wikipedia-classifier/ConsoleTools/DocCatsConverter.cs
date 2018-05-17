using System;
using System.Text;
using System.Collections;
using System.IO;


namespace DataMining.ConsoleTools
{
    /// <summary>
    /// converts wikipedia doc/cats records
    /// </summary>
    public class DocCatsConverter
    {
        private Hashtable cats;

        public DocCatsConverter()
        {
        }

        public void Convert() //temp!
        {
            loadCats();
            loadDocs();
            loadDocCats();
            writeCats();
            writeDocCats();
        }

        private void loadCats()
        {
            cats = new Hashtable();
            string catsFile = @"C:\_current\development\data\datamining\source\cats.txt";

            FileStream fs = new FileStream(catsFile, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            int i = 0;
            while ((line = r.ReadLine()) != null)
            {
                if (!cats.Contains(line))
                    cats.Add(line, i++);
                else
                    Console.WriteLine(line);
            }

            r.Close();
            fs.Close();
        }

        private void loadDocs()
        {
            string docIdsFile = @"C:\_current\development\data\datamining\source\docids.txt";
        }

        private void loadDocCats()
        {
            string docCatsFile = @"C:\_current\development\data\datamining\source\doccats.data";
        }

        private void writeCats()
        {
            string targetCatsFile = @"C:\_current\development\data\datamining\index\cats.txt";
        }

        private void writeDocCats()
        {
            string targetDocCatsFile = @"C:\_current\development\data\datamining\index\doccats.data";
        }
    }
}
