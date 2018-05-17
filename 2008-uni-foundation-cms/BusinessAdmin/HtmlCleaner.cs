using System;
using System.IO;
using System.Text;
using System.Collections;

namespace Foundation.BusinessAdmin
{
    // Removes tags without text. for example: "<tag><tag>" is replaced with "". 
    // If there is text or "<img ...>", no changes are made.
    public class HtmlCleaner
    {
        public HtmlCleaner(string html)
        {
            this.html = html;
            string temp = html.Replace("&lt;", "<");
            temp = temp.Replace("&gt;", ">");

            clear = false;
            reader = new StringReader(temp);
            sbDebug = new StringBuilder();
        }

        public string CleanHtml()
        {
            state1(reader.Read());
            if (clear)
                return "";
            else
                return html;
            //return sbDebug.ToString();
        }

        private int next() { return reader.Read(); }

        private void state1(int k)
        {
            if (k == -1)
                clear = true;
            else
            {
                char c = (char)k;
                sbDebug.Append(" .q1 = " + c);
                //Console.WriteLine("q1 = " + c);
                if (Char.IsWhiteSpace(c))
                    state1(next());
                else if (c == '<')
                    state2(next());
                else if (c == '&')
                    state7(next());
            }
        }

        private void state2(int k)
        {
            if (k > -1)
            {
                char c = (char)k;
                sbDebug.Append(" .q2 = " + c);
                //Console.WriteLine("q2 = " + c);
                if (c == 'i')
                    state3(next());
                else if (Char.IsLetter(c) || c == '/')
                    state6(next());
            }
        }

        private void state3(int k)
        {
            if (k > -1)
            {
                char c = (char)k;
                sbDebug.Append(" .q3 = " + c);
                //Console.WriteLine("q3 = " + c);
                if (c == 'm')
                    state4(next());
                else if (c == '>')
                    state1(next());
                else if (c != '<')
                    state6(next());
            }
        }

        private void state4(int k)
        {
            if (k > -1)
            {
                char c = (char)k;
                sbDebug.Append(" .q4 = " + c);
                //Console.WriteLine("q4 = " + c);
                if (c == 'g')
                    state5(next());
                else if (c == '>')
                    state1(next());
                else if (c != '<')
                    state6(next());
            }
        }

        private void state5(int k)
        {
            if (k > -1)
            {
                char c = (char)k;
                sbDebug.Append(" .q5 = " + c);
                //Console.WriteLine("q5 = " + c);
                if (c == '>')
                    state1(next());
                else if (!(Char.IsWhiteSpace(c) || c == '<'))
                    state6(next());
            }
        }

        private void state6(int k)
        {
            if (k > -1)
            {
                char c = (char)k;
                sbDebug.Append(" .q6 = " + c);
                //Console.WriteLine("q6 = " + c);
                if (c == '>')
                    state1(next());
                else if (c != '<')
                    state6(next());
            }
        }

        private void state7(int k)
        {
            if (k > -1)
            {
                char c = (char)k;
                sbDebug.Append(" .q7 = " + c);
                //Console.WriteLine("q7 = " + c);
                if (c == 'n')
                    state8(next());
            }
        }

        private void state8(int k)
        {
            if (k > -1)
            {
                char c = (char)k;
                sbDebug.Append(" .q8 = " + c);
                //Console.WriteLine("q8 = " + c);
                if (c == 'b')
                    state9(next());
            }
        }

        private void state9(int k)
        {
            if (k > -1)
            {
                char c = (char)k;
                sbDebug.Append(" .q9 = " + c);
                //Console.WriteLine("q9 = " + c);
                if (c == 's')
                    state10(next());
            }
        }

        private void state10(int k)
        {
            if (k > -1)
            {
                char c = (char)k;
                sbDebug.Append(" .q10 = " + c);
                //Console.WriteLine("q10 = " + c);
                if (c == 'p')
                    state11(next());
            }
        }

        private void state11(int k)
        {
            if (k > -1)
            {
                char c = (char)k;
                sbDebug.Append(" .q11 = " + c);
                //Console.WriteLine("q11 = " + c);
                if (c == ';')
                    state1(next());
            }
        }

        private StringReader reader;
        private string html;
        private bool clear;
        private StringBuilder sbDebug;
    }
}
