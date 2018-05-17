using System;
using System.IO;
using System.Collections;
using System.Text;
using d = Giggle.Data;

namespace Giggle.Indexing
{
    public class Parser
    {
        private const string FILTERED_DOCS_DIRECTORY = @"C:\_current\development\data\isr\temp\filteredindex\";
        private const string TARGET_DIRECTORY = @"C:\_current\development\data\isr\temp\newindexresults\";

        private const int TEXT = 0;
        private const int BOLD_TEXT = 1;
        private const int HEADING_TEXT = 2;
        private const int TITLE_TEXT = 3;
        private const int URL_TEXT = 4;

        private Hashtable terms;
        private FileStream fs;
        private StreamReader r;
        private StringBuilder sbText;                           //*
        private StringBuilder sbBoldText;                   //<b>*</b>
        private StringBuilder sbHeadingText;            //<h[1-6>*</h[1-6]>
        private StringBuilder sbTitleText;              //<t>*</t>
        private StringBuilder sbUrlText;                    //<w>*</w>

        private d.StopList stopList;
        private char[] delims;
        private int textWeight;
        private int boldWeight;
        private int headingWeight;
        private int titleWeight;
        private int urlWeight;
        private Stack stack;
        private string[] textTerms;
        private string term;

        public Parser(int boldWeight, int headingWeight, int titleWeight, int urlWeight)
        {
            terms = new Hashtable();
            stopList = new d.StopList(d.Helper.STOPLIST_PATH);
            delims = getDelims();
            this.textWeight = 1;
            this.boldWeight = boldWeight;
            this.headingWeight = headingWeight;
            this.titleWeight = titleWeight;
            this.urlWeight = urlWeight;
        }

        public void ExtractTerms()
        {
            string[] files = Directory.GetFiles(FILTERED_DOCS_DIRECTORY);
            //for (int i=17800; i<files.Length; i++)
            for (int i = 0; i < files.Length; i++)
            {
                parseFile(files[i]);
                if (i % 100 == 0)
                    Console.WriteLine("processing file " + i);
            }
            writeTermsToFile();
        }


        private void parseFile(string path)
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

            accumTerms(sbText.ToString(), textWeight);
            accumTerms(sbBoldText.ToString(), boldWeight);
            accumTerms(sbHeadingText.ToString(), headingWeight);
            accumTerms(sbTitleText.ToString(), titleWeight);
            accumTerms(sbUrlText.ToString(), urlWeight);

            //cleanup everything
            sbText = null;
            sbBoldText = null;
            sbHeadingText = null;
            sbTitleText = null;
            sbUrlText = null;
            return;
        }

        private void accumTerms(string input, int termWeight)
        {
            textTerms = input.Split(delims);
            for (int i = 0; i < textTerms.Length; i++)
            {
                term = textTerms[i].ToLower().Trim();
                if (term.Length > 0 && !stopList.Contains(term) && isAsciiLetters(term))
                    if (terms.Contains(term))
                        terms[term] = Convert.ToInt32(terms[term]) + termWeight;
                    else
                        terms.Add(term, termWeight);
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

        private void writeTermsToFile()
        {
            string path = TARGET_DIRECTORY + "terms.txt";
            if (File.Exists(path))
                File.Delete(path);

            StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
            IDictionaryEnumerator en = terms.GetEnumerator();
            while (en.MoveNext())
                w.WriteLine(en.Key.ToString());

            w.Close();
            Console.WriteLine("created terms.txt");
        }
    }
}
