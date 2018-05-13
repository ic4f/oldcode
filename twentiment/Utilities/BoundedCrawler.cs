using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Data;
using d = TwitterCrawler.Data;

namespace TwitterCrawler.Utilities
{
    class BoundedCrawler
    {
        private d.TwuserData twuserData;
        private d.TweetData tweetData;
        private d.TwuserLinkData twuserLinkData;
        private int seedCounter;
        private bool skipAll; //used to skip accounts already processed and resume processing after a given account

        public BoundedCrawler()
        {
            twuserData = new d.TwuserData();
            tweetData = new d.TweetData();
            twuserLinkData = new d.TwuserLinkData();
            seedCounter = 0;
            // skipAll = true;
        }

        public void Run()
        {
            string seedAccount, seedPageUrl, seedPageSource;
            string followeeAccount, followeePageUrl, followeePageSource;
            DataTable dtFollowees;

            HashSet<string> visited = new HashSet<string>();

            DataTable dt = twuserData.GetSeedIds();

            foreach (DataRow dr in dt.Rows)
            {
                seedAccount = dr[0].ToString();

                if (!visited.Contains(seedAccount))
                {
                    visited.Add(seedAccount);

                    seedPageUrl = "http://twitter.com/" + seedAccount;
                    seedPageSource = getPageSource(seedPageUrl);

                    if (seedPageSource != null && seedPageSource != "")
                    {
                        seedCounter++;
                        Console.WriteLine("Processing account " + (seedCounter) + " of " + dt.Rows.Count + ": " + seedAccount);

                        processTweets(seedAccount, seedPageSource); //must be done again: the seed account's tweets must be from the same time as the followee's tweets

                        dtFollowees = twuserData.GetFollowees(seedAccount); //because we use only existing accounts for this loop - i.e., they already have been processed, so we have followees for them.
                        foreach (DataRow drFollowee in dtFollowees.Rows)
                        {
                            followeeAccount = drFollowee[0].ToString();

                            if (!visited.Contains(followeeAccount))
                            {
                                visited.Add(followeeAccount);

                                followeePageUrl = "http://twitter.com/" + followeeAccount;
                                followeePageSource = getPageSource(followeePageUrl);

                                if (followeePageSource != null && followeePageSource != "")
                                {
                                    if (!twuserData.Exists(followeeAccount))
                                    {
                                        processTwuser(followeeAccount, followeePageSource);
                                    }
                                    processFollowing(followeeAccount, followeePageSource);
                                    processTweets(followeeAccount, followeePageSource);  //again, same tweets are added to repeat users! 
                                }
                            }
                        }

                        twuserData.MarkFolloweesAdded(seedAccount);
                    }
                    else
                        Console.WriteLine("ERROR: empty page for " + seedAccount);
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
                twuserLinkData.Create(id, followingId);

                matchPattern = matchPattern.NextMatch();
                counter++;
            }
            Console.WriteLine("\tAdded " + (counter) + " followees to " + id + " (seedCounter=" + seedCounter + ")");
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
            Console.WriteLine("\tCreated: " + counter + " tweets by " + id + " (seedCounter=" + seedCounter + ")");
        }

        private string getPageSource(string accountUrl)
        {
            Thread.Sleep(1000);

            HttpWebRequest wRequest;
            HttpWebResponse wResponse;
            string html = null;
            try
            {
                wRequest = (HttpWebRequest)WebRequest.Create(accountUrl);
                wRequest.UserAgent = ConfigHelper.Crawler_UserAgent;
                wRequest.Headers.Add("From", ConfigHelper.Crawler_Header_From);
                wRequest.Timeout = 10000;

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
    }
}
