using System;

namespace Foundation.BusinessMain
{
    public class WebHelper
    {
        public static string InserLineBreaks(string text)
        {
            return text.Replace("\n", "<br>");
        }

        public WebHelper() { }

        public string formatName(string name)
        {
            if (name.Length > 0)
            {
                string first = name.Substring(0, 1).ToUpper();
                string next = name.Substring(1).ToLower();
                return first + next;
            }
            return "";
        }

        public string FilterUserInput(string s, bool removeAllTags)
        {
            if (removeAllTags)
                s = s.Replace("<", "&#60;");
            else if (s.IndexOf("<script") > -1) //then remove all tags
                s = FilterUserInput(s, true);

            s = s.Replace("'", "''");
            if (s.IndexOf("/*") > -1 || s.IndexOf("--") > -1)
                return "";

            return s;
        }
    }
}
