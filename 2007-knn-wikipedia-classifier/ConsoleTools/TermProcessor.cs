using System;
using System.Text;
using System.Collections;
using System.IO;

namespace DataMining.ConsoleTools
{
    public class TermProcessor
    {
        private Hashtable terms; //key=term, value=Hashtable of docs where term occurs!	
        private char[] delims;
        private string[] temp;
        private StopList sList;
        private StringBuilder sb;
        private int docId;
        private int termInstanceCounter;
        private PorterStemmerAlgorithm.PorterStemmer stemmer;

        public TermProcessor()
        {
            delims = getDelims();
            sList = new StopList();
            sb = new StringBuilder();
            docId = 0;
            termInstanceCounter = 0;
            stemmer = new PorterStemmerAlgorithm.PorterStemmer();
            terms = new Hashtable();
        }

        public void CreateTermsFile()
        {
            loadTerms();
            writeToFile();
        }

        private void writeToFile()
        {
            string path = Helper.TEMPINDEX_PATH + "unsortedterms.txt";
            if (File.Exists(path))
                File.Delete(path);

            StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
            IDictionaryEnumerator en = terms.GetEnumerator();
            while (en.MoveNext())
            {
                string term = en.Key.ToString();
                Hashtable termDocs = (Hashtable)en.Value;
                int docCount = termDocs.Count;
                w.WriteLine(term + " " + docCount);
            }

            w.Close();
            Console.WriteLine("created " + "unsortedterms.txt");
            Console.WriteLine("total terms=" + terms.Count);
            Console.WriteLine("total instances=" + termInstanceCounter);
        }

        private void loadTerms()
        {
            FileStream fs = new FileStream(Helper.DOCS_ORIGINALS_PATH, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            /*
			 * 0 : look for next doc
			 * 1 : build-up current doc
			 */
            int state = 0;

            string line;
            while ((line = r.ReadLine()) != null)
            {
                if (line.IndexOf("<text>") > -1)
                    state = 1;
                if (line.IndexOf("</text>") > -1)
                {
                    processDoc(sb.ToString());
                    docId++;
                    sb.Remove(0, sb.Length);
                    state = 0;
                    //if (docId > 100) break; //REMOVE THIS!
                }
                if (state == 1)
                    sb.Append(line);
            }

            r.Close();
            fs.Close();
        }

        private void processDoc(string text)
        {
            if (docId % 100 == 0)
                Console.WriteLine("processing doc #" + docId);

            temp = text.Split(delims);
            string term;
            for (int i = 0; i < temp.Length; i++)
            {
                term = stemmer.stemTerm(temp[i].ToLower().Trim());
                if (term.Length > 0 && !sList.Contains(term) && isAsciiLetters(term))
                {
                    termInstanceCounter++;
                    if (!terms.Contains(term))
                    {
                        Hashtable termDocs = new Hashtable();
                        termDocs.Add(docId, true);
                        terms.Add(term, termDocs);
                    }
                    else
                    {
                        Hashtable termDocs = (Hashtable)terms[term];
                        if (!termDocs.Contains(docId))
                            termDocs.Add(docId, true);
                    }
                }
            }
            return;
        }

        private char[] getDelims()
        {
            char[] d = new char[47];
            d[0] = '`';
            d[1] = '1';
            d[2] = '2';
            d[3] = '3';
            d[4] = '4';
            d[5] = '5';
            d[6] = '6';
            d[7] = '7';
            d[8] = '8';
            d[9] = '9';
            d[10] = '0';
            d[11] = '-';
            d[12] = '=';
            d[13] = '[';
            d[14] = ']';
            d[15] = ';';
            d[16] = '\'';
            d[17] = ',';
            d[18] = '.';
            d[19] = '/';
            d[20] = '\\';
            d[21] = '~';
            d[22] = '!';
            d[23] = '#';
            d[24] = '$';
            d[5] = '%';
            d[26] = '^';
            d[27] = '&';
            d[28] = '*';
            d[29] = '(';
            d[30] = ')';
            d[31] = '_';
            d[32] = '+';
            d[33] = '{';
            d[34] = '}';
            d[35] = ':';
            d[36] = '"';
            d[37] = '<';
            d[38] = '>';
            d[39] = '?';
            d[40] = '|';
            d[41] = ' ';
            d[42] = '\t';
            d[43] = '\r';
            d[44] = '\v';
            d[45] = '\f';
            d[46] = '\n';
            return d;
        }

        private bool isAsciiLetters(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                int x = Convert.ToInt32(s[i]);
                if (x < 65 || x > 122 || (x > 90 && x < 97))
                    return false;
            }
            return true;
        }
    }
}
