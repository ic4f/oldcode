# IR project: crawl, index, search



> **This project is deprecated and not maintained.**

**Crawling, indexing and searching the uni.edu doman.** This was my project in information science during my grad studies at UNI. 

* **Technology:** ASP.NET, SQL Server; written in C# (and HTML/CSS for demo page)
* **Developed:** 2007

## Project Description 

**Crawling**
* A multithreaded crawler that collected the entire uni.edu web domain (27,331 documents)

**Link processing** 
* Remove links to non-existent pages; then reduce set to unique links 
* Extract title text
* Remove HTML tags (leaving only b, h1-6, a tags)
* Collect anchor text
* Calculate total inbound/outbound links per page

**Indexing** 
* Built and improved upon [my previous project](https://github.com/ic4f/oldcode/tree/master/ir_asu).

**Search and browse UI**
* Search using a combination of PageRank weights and 4 different indexes: unweighted/weighted terms and with or without external anchor text
* For each page, view:
  * inbound and outbound links, including inbound link anchor text
  * term counts for *b*, *h*, *title*, *a* tags 
* Browse the index
* For any provided URL: 
  * extract content and display term counts
  * display a ranked list of similar pages from the index

## What's inside
All relevant files from VS solution organized into 7 assemblies.