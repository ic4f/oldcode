using System;
using System.Text;
using System.Collections;
using System.IO;

namespace DataMining.ConsoleTools
{
    public class TermFilter
    {
        private Hashtable acceptedTerms;

        public TermFilter()
        {
            acceptedTerms = new Hashtable();
        }

        public void CreateNewTermsFile()
        {
            loadTerms();
            writeToFile();
        }

        private void writeToFile()
        {
            string path = Helper.TEMPINDEX_PATH + "filteredterms.txt";
            if (File.Exists(path))
                File.Delete(path);

            StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
            IDictionaryEnumerator en = acceptedTerms.GetEnumerator();
            while (en.MoveNext())
            {
                string term = en.Key.ToString();
                w.WriteLine(term);
            }

            w.Close();
            Console.WriteLine("created " + "filteredterms.txt");
            Console.WriteLine("total terms=" + acceptedTerms.Count);
        }

        private void loadTerms()
        {
            FileStream fs = new FileStream(Helper.TEMPINDEX_PATH + Helper.TERMS_FILE, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            string line;
            string term;
            int docCount;
            int space;
            int minCounter = 0;
            int maxCounter = 0;
            int min = 2; // occur in 1 doc
            int max = 5000; //occur in more than 50% of all docs
            while ((line = r.ReadLine()) != null)
            {
                space = line.IndexOf(" ");
                term = line.Substring(0, space);
                docCount = Convert.ToInt32(line.Substring(space + 1));
                if (docCount <= min)
                    minCounter++;
                else if (docCount >= max)
                    maxCounter++;
                else
                    acceptedTerms.Add(term, true);
            }
            r.Close();
            fs.Close();

            Console.WriteLine(minCounter + " " + maxCounter);
        }

    }
}
