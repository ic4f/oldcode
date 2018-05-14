using System;
using System.IO;
using System.Collections;
using System.Text;
using d = Giggle.Data;

namespace Giggle.Indexing
{
    public class TermDocsGenerator
    {
        private const string FILTERED_DOCS_DIRECTORY = @"C:\_current\development\data\isr\temp\filteredindex\";
        private const string TARGET_DIRECTORY1 = @"C:\_current\development\data\isr\giggleindex\";
        private const string TARGET_DIRECTORY2 = @"\source\";

        private const int TEXT = 0;
        private const int BOLD_TEXT = 1;
        private const int HEADING_TEXT = 2;
        private const int TITLE_TEXT = 3;
        private const int URL_TEXT = 4;

        private FileStream fs;
        private StreamReader r;
        private StringBuilder sbText;                           //*
        private StringBuilder sbBoldText;                   //<b>*</b>
        private StringBuilder sbHeadingText;            //<h[1-6>*</h[1-6]>
        private StringBuilder sbTitleText;              //<t>*</t>
        private StringBuilder sbUrlText;                    //<w>*</w>

        private char[] delims;
        private int textWeight;
        private int boldWeight;
        private int headingWeight;
        private int titleWeight;
        private int urlWeight;
        private Stack stack;
        private string[] textTerms;
        private string term;

        private Hashtable[] termDocs;
        private TermLoader termLoader;

        public TermDocsGenerator(int boldWeight, int headingWeight, int titleWeight, int urlWeight)
        {
            delims = getDelims();
            this.textWeight = 1;
            this.boldWeight = boldWeight;
            this.headingWeight = headingWeight;
            this.titleWeight = titleWeight;
            this.urlWeight = urlWeight;

            termLoader = new TermLoader();
            termDocs = new Hashtable[termLoader.TermCount]; //key=termId, val=hashtable:docId/termCount
        }

        public void GenerateTermDocsFile()
        {
            string[] files = Directory.GetFiles(FILTERED_DOCS_DIRECTORY);
            for (int i = 0; i < files.Length; i++)
            {
                parseFile(files[i], i);
                if (i % 100 == 0)
                    Console.WriteLine("processing file " + i);
            }
            writeTermDocsToFile();
        }

        private void parseFile(string path, int docId)
        {
            sbText = new StringBuilder();
            sbBoldText = new StringBuilder();
            sbHeadingText = new StringBuilder();
            sbTitleText = new StringBuilder();
            sbUrlText = new StringBuilder();

            stack = new Stack();
            stack.Push(TEXT);

            fs = File.OpenRead(path);
            r = new StreamReader(fs, System.Text.Encoding.ASCII);


            /*
			 * state 0: (start AND final state) loop until '<' -> 1
			 * state 1: 'b' -> 2 or 'h' -> 3 or 'a' -> 4 or '/' -> 5
			 * state 2: '>' -> 0 and MAKE TAG
			 * state 3: '[1-6]' -> 2
			 * state 4: loop until '>' -> 0	
			 * state 5: 'b' -> 2 or 'h' -> 3 or 'a' -> 2
			 */
            int state = 0;

            int i;
            char c;
            while ((i = r.Read()) > -1)
            {
                c = (char)i;

                switch (state)
                {
                    case 0:
                        {
                            if (c == '<')
                                state = 1;
                            else
                            {
                                if (Convert.ToInt32(stack.Peek()) == BOLD_TEXT)
                                    sbBoldText.Append(c);
                                else if (Convert.ToInt32(stack.Peek()) == HEADING_TEXT)
                                    sbHeadingText.Append(c);
                                else if (Convert.ToInt32(stack.Peek()) == TITLE_TEXT)
                                    sbHeadingText.Append(c);
                                else if (Convert.ToInt32(stack.Peek()) == URL_TEXT)
                                    sbHeadingText.Append(c);
                                else
                                    sbText.Append(c);
                            }
                            break;
                        }
                    case 1:
                        {
                            if (c == 'b')
                            {
                                stack.Push(BOLD_TEXT); //all following text is bold
                                state = 2;
                            }
                            else if (c == 't')
                            {
                                stack.Push(TITLE_TEXT); //all following text is a title
                                state = 2;
                            }
                            else if (c == 'w')
                            {
                                stack.Push(URL_TEXT); //all following text is a url
                                state = 2;
                            }
                            else if (c == 'h')
                            {
                                stack.Push(HEADING_TEXT); //all following text is a heading
                                state = 3;
                            }
                            else if (c == 'a') //no time for this :-(
                                state = 4;
                            else if (c == '/')
                                state = 5;
                            else
                                throw new Exception("error in 1: " + c);
                            break;
                        }
                    case 2:
                        {
                            if (c == '>')
                                state = 0;
                            else
                                throw new Exception("error in 2: " + c);
                            break;
                        }
                    case 3:
                        {
                            if (Char.IsDigit(c))
                                state = 2;
                            else
                                throw new Exception("error in 3: " + c);
                            break;
                        }
                    case 4:
                        {
                            if (c == '>')
                                state = 0;
                            break;
                        }
                    case 5:
                        {
                            if (Convert.ToInt32(stack.Peek()) != TEXT) //have to check since i am not processing anchor tags
                                stack.Pop();

                            if (c == 'b' || c == 't' || c == 'w')
                                state = 2;
                            else if (c == 'h')
                                state = 3;
                            else if (c == 'a')
                                state = 2;
                            else
                                throw new Exception("error in 5: " + c);
                            break;
                        }
                }
                //check that we are in final state... not needed
                //if (state != 0)
                //throw new Exception("exiting in state " + state);
            }
            r.Close();
            fs.Close();

            Hashtable currTermCounts = new Hashtable(); //key=termId, value=weighted count (term counts for current doc)

            accumTerms(sbText.ToString(), textWeight, currTermCounts);
            accumTerms(sbBoldText.ToString(), boldWeight, currTermCounts);
            accumTerms(sbHeadingText.ToString(), headingWeight, currTermCounts);
            accumTerms(sbTitleText.ToString(), titleWeight, currTermCounts);
            accumTerms(sbUrlText.ToString(), urlWeight, currTermCounts);

            //now update termdocs
            IDictionaryEnumerator en = currTermCounts.GetEnumerator();
            while (en.MoveNext())
                processTerm(Convert.ToInt32(en.Key), Convert.ToInt32(en.Value), docId);

            //cleanup everything
            sbText = null;
            sbBoldText = null;
            sbHeadingText = null;
            sbTitleText = null;
            sbUrlText = null;
            return;
        }

        private void accumTerms(string input, int termWeight, Hashtable currTermCounts)
        {
            textTerms = input.Split(delims);
            for (int i = 0; i < textTerms.Length; i++)
            {
                term = textTerms[i].ToLower().Trim();
                if (termLoader.HasTerm(term))
                {
                    int termId = termLoader.GetTermId(term);
                    if (currTermCounts.Contains(termId))
                        currTermCounts[termId] = Convert.ToInt32(currTermCounts[termId]) + termWeight;
                    else
                        currTermCounts.Add(termId, termWeight);
                }
            }
            return;
        }

        private void processTerm(int termId, int termWeightedCount, int docId)
        {
            if (termDocs[termId] == null) //no doc/termCounts have been added to this term yet
            {
                Hashtable td = new Hashtable();
                td.Add(docId, termWeightedCount);
                termDocs[termId] = td;
            }
            else
            {
                Hashtable td = (Hashtable)termDocs[termId];
                if (td[docId] == null) //this doc has not been added to this term yet				
                    td.Add(docId, termWeightedCount);
                else //increment termCount for this doc in this termdoc				
                    throw new Exception("trying to process a doc for a term that has been processed!");
                //td[docId] = Convert.ToInt32(td[docId]) + termWeightedCount;
            }
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

        private void writeTermDocsToFile()
        {
            string weightsDir = boldWeight + "_" + headingWeight + "_" + titleWeight + "_" + urlWeight;
            string dirPath = TARGET_DIRECTORY1 + weightsDir + TARGET_DIRECTORY2;
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            string path = dirPath + "termdocs.txt";
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
            Console.WriteLine("created " + "termdocs.txt");
        }
    }
}
