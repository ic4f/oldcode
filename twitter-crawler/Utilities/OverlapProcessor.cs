using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using d = TwitterCrawler.Data;

namespace TwitterCrawler.Utilities
{
    class OverlapProcessor
    {
        public void Run()
        {
            DataTable dtFollowees, dtFollowerHashtags, dtFolloweeHashtags;
            string followerId, followeeId;
            string hashtagId;
            Hashtable hashFollowerTags;
            int tweetsByFollower, tweetsByFollowee;

            d.TwuserData uData = new d.TwuserData();
            d.TwuserHashtagLinkData uhData = new d.TwuserHashtagLinkData();
            d.HahstagOverlapData htovData = new d.HahstagOverlapData();

            DataTable dtTwusers = uData.GetFollowers(0);

            int counter = 1;

            foreach (DataRow drUser in dtTwusers.Rows)
            {
                followerId = drUser[0].ToString();
                Console.WriteLine("\nprocessing user " + (counter++) + " of " + dtTwusers.Rows.Count);

                dtFollowerHashtags = uhData.GetHashtags(followerId);
                dtFollowees = uData.GetProcessedFollowees(followerId);

                hashFollowerTags = new Hashtable();
                foreach (DataRow drTag1 in dtFollowerHashtags.Rows)
                    hashFollowerTags.Add(drTag1[0].ToString(), drTag1[1].ToString()); //load follower's hashtags + their tweet counts into hashtable

                foreach (DataRow drFollowee in dtFollowees.Rows)
                {
                    followeeId = drFollowee[0].ToString();
                    dtFolloweeHashtags = uhData.GetHashtags(followeeId);

                    Console.WriteLine("\tfollowee " + followeeId + "(" + dtFolloweeHashtags.Rows.Count + " hashtags) out of " + dtFollowees.Rows.Count + " followeees");

                    foreach (DataRow drTag2 in dtFolloweeHashtags.Rows)
                    {
                        hashtagId = drTag2[0].ToString();
                        if (hashFollowerTags.ContainsKey(hashtagId))
                        {
                            Console.WriteLine("\t\tadding overlap for tag " + hashtagId);

                            tweetsByFollower = Convert.ToInt32(hashFollowerTags[hashtagId]);
                            tweetsByFollowee = Convert.ToInt32(drTag2[1]);
                            htovData.Create(followerId, followeeId, Convert.ToInt32(hashtagId), tweetsByFollower, tweetsByFollowee);
                        }
                    }
                }
            }
        }
    }
}
