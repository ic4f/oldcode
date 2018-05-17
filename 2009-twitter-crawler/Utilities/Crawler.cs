using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using d = TwitterCrawler.Data;

namespace TwitterCrawler.Utilities
{
    public class Crawler
    {
        private Queue<string> accounts;
        private CookieContainer cookies;
        private d.TwuserData twuserData;
        private d.TweetData tweetData;
        private d.TwuserLinkData tlinkData;
        private bool mayEnqueue;
        private int skipNewAccounts;

        public Crawler(string seedAccountId)
        {
            accounts = new Queue<string>();
            accounts.Enqueue(seedAccountId);
            loadCookies();
            twuserData = new d.TwuserData();
            tweetData = new d.TweetData();
            tlinkData = new d.TwuserLinkData();
            mayEnqueue = true;
            skipNewAccounts = 1000;
        }

        public void Run()
        {
            string nextAccount;
            string accountPageUrl;
            string accountPageSource;

            int counter = 0;
            while (accounts.Count > 0)
            {
                Thread.Sleep(2000);

                nextAccount = accounts.Dequeue();

                if (!twuserData.Exists(nextAccount))
                {
                    accountPageUrl = "http://twitter.com/" + nextAccount;
                    accountPageSource = getPageSource(accountPageUrl, false);

                    if (accountPageSource != null && accountPageSource != "")
                    {
                        Console.WriteLine("Processing account #" + (counter++) + ": " + nextAccount);
                        Console.WriteLine("Queue size = " + accounts.Count);

                        processTwuser(nextAccount, accountPageSource);
                        processFollowing(nextAccount, accountPageSource);
                        processTweets(nextAccount, accountPageSource);
                    }
                    else
                        Console.WriteLine("ERROR: empty page for " + nextAccount);
                }
            }
        }

        private void processTwuser(string id, string html)
        {
            string namePattern = @"<span class=""fn"">(?<data>[^<]+)</span>";
            string locPattern = @"<span class=""adr"">(?<data>[^<]+)</span>";
            string bioPattern = @"<span class=""bio"">(?<data>[^<]+)</span>";
            string followingPattern = @"<span id=""following_count"" class=""stats_count numeric"">(?<data>[^<]+)</span>";
            string followersPattern = @"<span id=""follower_count"" class=""stats_count numeric"">(?<data>[^<]+)</span>";
            string listedPattern = @"<span id=""lists_count"" class=""stats_count numeric"">(?<data>[^<]+)</span>";
            string tweetsPattern = @"<span id=""update_count"" class=""stat_count"">(?<data>[^<]+)</span>";

            Match mName = Regex.Match(html, namePattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match mLoc = Regex.Match(html, locPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match mBio = Regex.Match(html, bioPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match mFollowing = Regex.Match(html, followingPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match mFollowers = Regex.Match(html, followersPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match mListed = Regex.Match(html, listedPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match mTweets = Regex.Match(html, tweetsPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string name = mName.Groups["data"].ToString().Trim();
            string loc = mLoc.Groups["data"].ToString().Trim();
            string bio = mBio.Groups["data"].ToString().Trim();
            string following = mFollowing.Groups["data"].ToString().Trim();
            string followers = mFollowers.Groups["data"].ToString().Trim();
            string listed = mListed.Groups["data"].ToString().Trim();
            string tweets = mTweets.Groups["data"].ToString().Trim();

            int ifollowing = 0;
            int ifollowers = 0;
            int ilisted = 0;
            int itweets = 0;

            if (following.Length > 0)
                ifollowing = Convert.ToInt32(following.Replace(",", ""));

            if (followers.Length > 0)
                ifollowers = Convert.ToInt32(followers.Replace(",", ""));

            if (listed.Length > 0)
                ilisted = Convert.ToInt32(listed.Replace(",", ""));

            if (tweets.Length > 0)
                itweets = Convert.ToInt32(tweets.Replace(",", ""));

            twuserData.Create(id, name, loc, bio, ifollowing, ifollowers, ilisted, itweets);
        }

        private void processFollowing(string id, string html)
        {
            string followingIdPattern = @"<a href=""/(?<data>[^""]+)"" class=""url"" hreflang=""en"" rel=""contact""";
            Match matchPattern = Regex.Match(html, followingIdPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            int counter = 0;
            while (matchPattern.Success)
            {
                string followingId = matchPattern.Groups["data"].ToString().Trim();
                tlinkData.Create(id, followingId);

                if (mayEnqueue)
                {
                    if (accounts.Count < ConfigHelper.Queue_MaxCount)
                        accounts.Enqueue(followingId);
                    else
                        mayEnqueue = false;
                }
                else
                {
                    if (accounts.Count < ConfigHelper.Queue_MaxCount - skipNewAccounts)
                    {
                        mayEnqueue = true;
                        accounts.Enqueue(followingId);
                    }
                }

                matchPattern = matchPattern.NextMatch();
            }
            Console.WriteLine("Created " + (counter++) + " user links for " + id + " followees");
        }

        private void processTweets(string id, string html)
        {
            string tweetPattern = @"<span class=""entry-content"">(?<data>[^\n]+)" + "\n";
            Match tweetMatch = Regex.Match(html, tweetPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string timePattern = @"{time:'(?<data>[^}']+)'}";
            Match timeMatch = Regex.Match(html, timePattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            ArrayList tweets = new ArrayList();

            int counter = 0;
            while (tweetMatch.Success)
            {
                string tweet = tweetMatch.Groups["data"].ToString().Trim();
                tweet = tweet.Replace("</span>", "");
                tweets.Add(tweet);
                tweetMatch = tweetMatch.NextMatch();
            }

            int pos = 0;
            while (timeMatch.Success)
            {
                string time = timeMatch.Groups["data"].ToString().Trim();
                tweetData.Create(id, tweets[pos++].ToString(), time);
                timeMatch = timeMatch.NextMatch();
                counter++;
            }
            Console.WriteLine("Created: " + counter + " tweets for " + id + " followees"); //incorrect message, but it's minor
        }

        private string getPageSource(string accountUrl, bool useCookies)
        {
            HttpWebRequest wRequest;
            HttpWebResponse wResponse;
            string html = null;
            try
            {
                wRequest = (HttpWebRequest)WebRequest.Create(accountUrl);
                wRequest.UserAgent = ConfigHelper.Crawler_UserAgent;
                wRequest.Headers.Add("From", ConfigHelper.Crawler_Header_From);
                wRequest.Timeout = 5000;

                if (useCookies)
                    wRequest.CookieContainer = cookies;

                wResponse = (HttpWebResponse)wRequest.GetResponse();
                html = new StreamReader(wResponse.GetResponseStream()).ReadToEnd();
                wResponse.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            return html;
        }

        private void loadCookies()
        {
            /* redacted */
        }
    }
}