using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;
using System.IO;
using d = IrProject.Data;
using System.Threading;

namespace IrProject.Indexing
{
	public class MultiCrawler
	{
		private Queue queue;							//needs locking
		private Hashtable processedUrls;	//needs locking
		private UrlHelper urlHelper;
		private Regex regex;
		private int visited;
		private int threadCount;
		private bool addLinks;

		public MultiCrawler()
		{
			Thread.CurrentThread.Name = "main";

			threadCount = initQueue(); //size must be at least the number of threads!
			processedUrls = new Hashtable();
			urlHelper = new UrlHelper();			
			regex = urlHelper.GetRegex;
			visited = 0;
			addLinks = true;
		}

		private void saveQueue()
		{	
			StreamWriter writer = File.CreateText(@"C:\_development\data\IrProject\queue.txt");
			StringBuilder sb = new StringBuilder();
			while (queue.Count > 0)
				sb.AppendFormat("{0}\n", queue.Dequeue());

			writer.Write(sb.ToString());
			writer.Flush();
			writer.Close();		
		}

		public void Execute()
		{
			Thread[] threads = new Thread[threadCount];			
			for (int i=0; i<threads.Length; i++)
			{
				threads[i] = new Thread(new ThreadStart(runThread));
				threads[i].Name = i.ToString();
			}
			for (int i=0; i<threads.Length; i++)
				threads[i].Start();
		}

		private void runThread()
		{
			Uri baseUri;
			string currentUrl;	
			HttpWebRequest wrq;
			HttpWebResponse wr;
			StreamReader sr;
			string html;			
			Uri childUri;
			int outbound = 0;	
			string linkToAdd;
			MatchCollection mc;			

			while (queue.Count > 0)
			{
				Console.WriteLine("\nthread #" + Thread.CurrentThread.Name);
				lock(this) //dequeue next url and add it to processed hashtable
				{
					currentUrl = queue.Dequeue().ToString(); //get next URL from queue to process
					processedUrls.Add(currentUrl, true); //mark URL as processed
				}
				baseUri = new Uri(currentUrl);				
				Console.WriteLine("\nqueue size = " + queue.Count + " (more links = " + addLinks.ToString() + ")\nProcessing " + currentUrl);

				try 
				{
					wrq = (HttpWebRequest)WebRequest.Create(currentUrl);
					wrq.UserAgent = "LOTW UNI Crawler/computer science research project";
					wrq.Headers.Add("From", "sergeigolitsinski@gmail.com");
					wrq.Timeout = 2500;
					wr = (HttpWebResponse)wrq.GetResponse();

					if (wr.ContentType.IndexOf("text/html") > -1)
					{
						sr = new StreamReader(wr.GetResponseStream());
						html = sr.ReadToEnd();
						storeSource(currentUrl, html); //store this post and url

						//now process all its outbound links: add to queue + store link
						if (addLinks)
						{
							outbound = 0;
							mc = regex.Matches(html); 
							foreach (Match m in mc)
							{
								childUri = new Uri(baseUri, urlHelper.MakeLink(m.Groups[3].ToString()));							
								linkToAdd = urlHelper.NormalizeUrl(childUri.AbsoluteUri);
								outbound += processLink(currentUrl, linkToAdd, childUri);							
							}														
							Console.WriteLine("  added " + outbound + " links to queue");

							lock(this)
							{
								Console.WriteLine("  visited " + ++visited + " pages so far");
								if (visited > 40000) 
									addLinks = false;
							}
						}	
					}	
					wr.Close();
				}
				catch (Exception)
				{
					continue;
				}				
			}
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

		private int initQueue()
		{
			queue = new Queue(1000);
			queue.Enqueue("http://www.uni.edu");
			queue.Enqueue("http://www.grad.uni.edu");
			queue.Enqueue("http://www.cba.uni.edu");
			queue.Enqueue("http://www.uni.edu/coe");
			queue.Enqueue("http://www.uni.edu/chfa");
			queue.Enqueue("http://www.cns.uni.edu");
			queue.Enqueue("http://fp.uni.edu/csbs");
			queue.Enqueue("http://www.uni.edu/vpaa");
			queue.Enqueue("http://www.uni.edu/regist");
			queue.Enqueue("http://www.uni.edu/teached");
			return queue.Count;
		}

		private int processLink(string currentUrl, string linkToAdd, Uri childUri)
		{
			//determine if link is relevant. if so:
				//add link to db. If it is not in queue or in processed  - add to queue.
			int result = 0;
			if (urlHelper.LinkIsRelevant(linkToAdd, childUri))
			{
				new d.LinkData().Create(currentUrl, linkToAdd); //db checks for duplicates
				lock(this)
				{
					if (!queue.Contains(linkToAdd) && !processedUrls.Contains(linkToAdd))
					{
						queue.Enqueue(linkToAdd); 
						result = 1;						
					}
				}
			}
			return result;
		}
	}
}
