using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using d = TwitterCrawler.Data;

namespace TwitterCrawler.Utilities
{
    class Extractor
    {
        public Extractor() { }

        public void ExtractHashTags()
        {
            Hashtable hashtags = new Hashtable();
            d.TweetData tData = new d.TweetData();
            d.HahstagData hData = new d.HahstagData();
            d.TweetHashtagLinkData tweetHashtagData = new d.TweetHashtagLinkData();
            d.TwuserHashtagLinkData twuserHashtagData = new d.TwuserHashtagLinkData();
            DataTable dt = tData.GetHashTagTweeets();

            string hashtagPattern = @"(?<tag>#[^\s]+)";

            int counter = 0;

            int tweetId, tagId;
            string twuserId, text, nohtmltext, tag;
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("tweet# " + (++counter));

                tweetId = Convert.ToInt32(dr[0]);
                twuserId = dr[1].ToString();
                text = dr[2].ToString();
                nohtmltext = dr[3].ToString();

                Match matchPattern = Regex.Match(nohtmltext, hashtagPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

                while (matchPattern.Success)
                {
                    tag = matchPattern.Groups["tag"].ToString().Trim();

                    tag = normalize(tag);
                    if (tag != "")
                    {
                        if (!hashtags.Contains(tag))
                        {
                            tagId = hData.Create(tag);
                            hashtags.Add(tag, tagId);
                        }
                        else
                            tagId = Convert.ToInt32(hashtags[tag]);

                        tweetHashtagData.Create(tweetId, tagId);
                        twuserHashtagData.Create(twuserId, tagId);
                    }

                    matchPattern = matchPattern.NextMatch();
                }
            }
        }

        public void test()
        {
            string hashtagPattern = @"(?<tag>#[^\s]+)";
            string input = "#test-1 #test2, #test!@#$%^&*()_-+=/?.,<>;:'3 ##notest";
            Match matchPattern = Regex.Match(input, hashtagPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            while (matchPattern.Success)
            {
                string tag = matchPattern.Groups["tag"].ToString().Trim();

                tag = normalize(tag);
                Console.WriteLine("tag = " + tag);

                matchPattern = matchPattern.NextMatch();
            }
        }

        private string normalize(string tag)
        {
            if (!Char.IsLetterOrDigit(tag[1])) //first char after pound must be letter or digit (do not do this in regex format - may cause "#a" to be returned from "##a")
                return "";

            //remove any punctuation
            tag = Regex.Replace(tag, @"\W", "");

            return tag.ToLower();
        }

        public void ExtractText()
        {
            d.TweetData tData = new d.TweetData();
            DataTable dt = tData.GetRecords();
            Regex regex = new Regex("<[^>]*>");
            int id;
            string twuserId, text, nohtmltext;
            int counter = 0;
            foreach (DataRow dr in dt.Rows)
            {
                id = Convert.ToInt32(dr[0]);
                twuserId = dr[1].ToString();
                text = dr[2].ToString();
                nohtmltext = regex.Replace(text, "");
                tData.UpdateData(id, nohtmltext);
                Console.WriteLine(++counter);
            }
        }
    }
}
