using System;
using System.Text;
using System.Collections;
using System.IO;

namespace DataMining.ConsoleTools
{
    public class TermDocsProcessor
    {
        private TermLoader termLoader;
        private Hashtable[] termDocs;
        private char[] delims;
        private string[] temp;
        private StopList sList;
        private StringBuilder sb;
        private int docId;
        private PorterStemmerAlgorithm.PorterStemmer stemmer;

        public TermDocsProcessor()
        {
            termLoader = new TermLoader();
            termDocs = new Hashtable[termLoader.TermCount];
            delims = getDelims();
            sList = new StopList();
            sb = new StringBuilder();
            docId = 0;
            stemmer = new PorterStemmerAlgorithm.PorterStemmer();
        }

        public void CreateTermDocsFile()
        {
            loadTermDocs();
            writeToFile();
        }

        private void loadTermDocs()
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
                if (termLoader.HasTerm(term))
                    processTerm(termLoader.GetTermId(term), docId);
            }
            return;
        }

        private void processTerm(int termId, int docId)
        {
            if (termDocs[termId] == null) //no doc/termCounts have been added to this term yet
            {
                Hashtable td = new Hashtable();
                td.Add(docId, 1);
                termDocs[termId] = td;
            }
            else
            {
                Hashtable td = (Hashtable)termDocs[termId];
                if (td[docId] == null) //this doc has no been added to this term yet				
                    td.Add(docId, 1);
                else //increment termCount for this doc in this termdoc				
                    td[docId] = Convert.ToInt32(td[docId]) + 1;
            }
        }

        private void writeToFile()
        {
            string path = Helper.SOURCE_PATH + Helper.TERMDOCS_FILE;
            if (File.Exists(path))
                File.Delete(path);

            StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);

            Hashtable td;
            IDictionaryEnumerator en;
            int docId;
            int termCount;
            for (int termId = 0; termId < termDocs.Length; termId++)
            {
                td = (Hashtable)termDocs[termId];
                en = td.GetEnumerator();
                while (en.MoveNext()) //loop through all termDocs(docIds/termCounts) for this term
                {
                    docId = Convert.ToInt32(en.Key);
                    termCount = Convert.ToInt32(en.Value);
                    w.WriteLine(termId + " " + docId + " " + termCount);
                }
            }

            w.Close();
            Console.WriteLine("created " + Helper.TERMDOCS_FILE);
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
    }
}
