using System;
using System.Text;
using System.Collections;
using System.IO;

namespace Giggle.Indexing
{
    public class TermLoader
    {
        private const string TERMS_FILE = @"C:\_current\development\data\isr\temp\newindexresults\terms.txt";

        private Hashtable terms;

        public TermLoader() { loadTerms(); }

        public int TermCount { get { return terms.Count; } }

        public bool HasTerm(string term) { return terms.Contains(term); }

        public int GetTermId(string term)
        {
            if (terms.Contains(term))
                return Convert.ToInt32(terms[term]);
            else
                throw new Exception("Term not found: " + term);
        }

        private void loadTerms()
        {
            terms = new Hashtable();
            FileStream fs = new FileStream(TERMS_FILE, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            int termId = 0;
            while ((line = r.ReadLine()) != null)
                terms.Add(line, termId++);

            r.Close();
            fs.Close();
        }
    }
}
