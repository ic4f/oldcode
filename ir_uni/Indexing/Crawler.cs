using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;
using System.IO;
using d = IrProject.Data;

namespace IrProject.Indexing
{
//this class is no longer used. instead - use multithreaded multicrawler
	public class Crawler
	{
		public Crawler() {}

		public void Run()
		{
			Queue queue = makeQueue();
			Hashtable processedUrls = new Hashtable();

			UrlHelper h = new UrlHelper();			
			Regex regex = h.GetRegex;
			MatchCollection mc;

			Uri baseUri;
			string currentUrl;	
			HttpWebRequest wrq;
			HttpWebResponse wr;
			StreamReader sr;
			string html;			
			Uri childUri;
			int outbound = 0;	
			string linkToAdd;

			while (queue.Count > 0)
			{
				currentUrl = queue.Dequeue().ToString(); //get next URL from queue to process
				baseUri = new Uri(currentUrl);
				processedUrls.Add(currentUrl, true); //mark URL as processed

				Console.WriteLine("\nQueue size = " + queue.Count);
				Console.WriteLine("Processing " + currentUrl);
				
				try 
				{
					wrq = (HttpWebRequest)WebRequest.Create(currentUrl);
					wrq.UserAgent = "LOTW UNI Crawler/computer science research project";
					wrq.Headers.Add("From", "sergeigolitsinski@gmail.com");
					wrq.Timeout = 5000;
					wr = (HttpWebResponse)wrq.GetResponse();

					if (wr.ContentType.IndexOf("text/html") > -1)
					{
						sr = new StreamReader(wr.GetResponseStream());
						html = sr.ReadToEnd();
						Console.WriteLine("  visited " + processedUrls.Count + " pages so far");

						storeSource(currentUrl, html); //store this post and url

						//now process all its outbound links: add to queue + store link
						outbound = 0;
						mc = regex.Matches(html); 

						foreach (Match m in mc)
						{
							childUri = new Uri(baseUri, makeLink(m.Groups[3].ToString()));
							linkToAdd = normalizeUrl(childUri.AbsoluteUri);

							if (isRelevant(linkToAdd))
							{
								addLink(currentUrl, linkToAdd); //db checks for duplicates
								
								if (!queue.Contains(linkToAdd) && !processedUrls.Contains(linkToAdd))
								{
									queue.Enqueue(linkToAdd); 
									outbound++;
								}
							}			
						}									
						Console.WriteLine("  added " + outbound + " links to queue");
					}	
					wr.Close();
				}
				catch (Exception e)
				{
					Console.WriteLine("Caught exception for " + currentUrl + ": " + e.ToString());
					continue;
				}
			}		
		}

		private bool isRelevant(string url)
		{
			return url.IndexOf("uni.edu") > -1;
		}

		private string makeLink(string link)
		{
			link = link.Trim();
			if ((link.StartsWith("'")) | (link.StartsWith("\"")))
				link = link.Substring(1);
			if ((link.EndsWith("'")) | (link.EndsWith("\"")))
				link = link.Substring(0, link.Length-1);
			return link.Trim();
		}

		private string normalizeUrl(string url)
		{
			if (url.EndsWith("/"))
				return url.Substring(0, url.Length-1);
			else if (url.EndsWith("/index.html"))
        return url.Substring(0, url.Length-11);
			else if (url.EndsWith("/default.html"))
				return url.Substring(0, url.Length-13);
			else if (url.EndsWith("/index.shtml"))
				return url.Substring(0, url.Length-12);
			else if (url.EndsWith("/default.shtml"))
				return url.Substring(0, url.Length-14);
			else if (url.EndsWith("/index.htm"))
				return url.Substring(0, url.Length-10);
			else if (url.EndsWith("/default.htm"))
				return url.Substring(0, url.Length-12);
			else if (url.EndsWith("/index.asp"))
				return url.Substring(0, url.Length-10);
			else if (url.EndsWith("/default.asp"))
				return url.Substring(0, url.Length-12);
			else if (url.EndsWith("/index.aspx"))
				return url.Substring(0, url.Length-11);
			else if (url.EndsWith("/default.aspx"))
				return url.Substring(0, url.Length-13);
			else if (url.EndsWith("/index.php"))
				return url.Substring(0, url.Length-10);
			else if (url.EndsWith("/default.php"))
				return url.Substring(0, url.Length-12);
			else if (url.EndsWith("/index.jsp"))
				return url.Substring(0, url.Length-10);
			else if (url.EndsWith("/default.jsp"))
				return url.Substring(0, url.Length-12);
			else
				return url;
		}

		private void storeSource(string url, string content)
		{
			//store in db
			int pageId = (new d.DocData()).Create(url);
			//write source to a file
			string outputDir = Helper.PAGES_PATH;					
			StreamWriter writer = File.CreateText(outputDir + pageId + ".html");
			writer.Write(content);
			writer.Flush();
			writer.Close();		
		}

		private Queue makeQueue()
		{
			Queue queue = new Queue(1000);
			queue.Enqueue(System.Configuration.ConfigurationSettings.AppSettings["InitUrl"]);
			return queue;
		}

		private void addLink(string parentUrl, string childUrl)
		{
			new d.LinkData().Create(parentUrl, childUrl);
		}
	}
}
