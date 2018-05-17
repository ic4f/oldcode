# 	Twitter crawler + information extractor

> **This project is deprecated and not maintained.**

**Twitter crawler + information extraction system.** Developed as part of a collaborative research project combining sentiment analysis with social network theory during grad studies at [UMD](http://umd.edu).

* **Technology:** ASP.NET, SQL Server; written in C#
* **Developed:** 2009

As a structured relational social network, Twitter offers a directed graph comprised of follower and followee relationships between users. The project leveraged this structural information to augment sentiment classification of Tweets.

This codebase was used for the data collection part of the project, which included (a) crawling Twitter's website; (b) extracting available user account information, user tweets and user followers (limited to one page, in accordance with Twitter's policy at the time), and tweet hashtags (as indicators of topics); and (c) storing the extracted data and relationships in a database.


Final data set (after cleaning and normalizing the data, eliminating duplicates and consolidating similar hashtags):



1. **Users:** 130,116 unique users (includes account name, location, bio, number of followers, followees and lists, and number of tweets)

2. **Tweets:** 3,072,737 unique tweets (includes raw text, plain no-html text, time of the tweet, author's account name)

3. **Hashtags:** 91,188 unique hashtags (includes counts of tweets and users in which and by whom the hashtag was used)

4. **Hashtag overlap:** 171,258 unique instances of a hashtag used by a user and one of their followees (includes counts of the hashtag used by the follower and the followee. A total of 7,656 hashtags overlapped)

5. **Misc. data relationships:** 4,887,990 follower/followee links; 621,465 hashtag/tweet links; 331,792 user/hashtag links

## What's inside
All relevant files from VS solution organized into 3 assemblies: core, data processing, and data collection/extraction.