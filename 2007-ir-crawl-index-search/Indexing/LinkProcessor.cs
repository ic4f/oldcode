using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;
using System.IO;
using d = IrProject.Data;

//extracts all links from the collected pages, matches them against existing docs
//and stores the text part of relevant links in db
namespace IrProject.Indexing
{
	public class LinkProcessor
	{
		public LinkProcessor() {}

		public void Run()
		{
			UrlHelper urlHelper = new UrlHelper();			
			Regex regex = makeRegex();
			MatchCollection mc;

			Regex rSpace = new Regex(@"\s");

			d.DocData pd = new d.DocData();
			d.LinkData ld = new d.LinkData();

			Uri baseUri;
			Uri childUri;
			FileInfo fi;
			d.Doc p;
			int pageId;
			StreamReader sr;
			string html;
			string linkToProcess;
			int linkId;
			string linkText;			
						
			string path = Helper.DOCS_PATH;	
			string[] files = Directory.GetFiles(path);

			for (int i=545; i<files.Length; i++)	//545 already done
			{
				Console.WriteLine("processing file #" + i);

				fi = new FileInfo(files[i]);
				pageId = Convert.ToInt32(fi.Name.Substring(0, fi.Name.IndexOf(".")));
				p = new d.Doc(pageId);
				baseUri = new Uri(p.Url);

				sr = new StreamReader(fi.OpenRead());
				html = sr.ReadToEnd();
				mc = regex.Matches(html); 

				Console.WriteLine("found " + mc.Count + " links");				

				foreach (Match m in mc)
				{
					try
					{
						childUri = new Uri(baseUri, urlHelper.MakeLink(m.Groups[3].ToString()));							
						linkToProcess = urlHelper.NormalizeUrl(childUri.AbsoluteUri);
						linkText = m.Groups[4].ToString();

						linkId = pd.GetIdByUrl(linkToProcess);
						if (linkId > 0 && linkText != "") //found page!										
						{
							linkText = rSpace.Replace(linkText, " ");
							linkText = linkText.Trim();
							linkText = linkText.Replace("          ", " ");						
							linkText = linkText.Replace("         ", " ");
							linkText = linkText.Replace("        ", " ");
							linkText = linkText.Replace("       ", " ");
							linkText = linkText.Replace("      ", " ");
							linkText = linkText.Replace("     ", " ");
							linkText = linkText.Replace("    ", " ");
							linkText = linkText.Replace("   ", " ");
							linkText = linkText.Replace("  ", " ");
							ld.UpdateText(pageId, linkId, linkText);	
						}			
					}
					catch (Exception) {}
				}

				//if (i % 100 == 0)
				//	Console.WriteLine("processing file #" + i);
			}				
		}

		private Regex makeRegex()
		{
			string first = @"[./\w]";
			string next = @"[~;!#\$%&\-\+/=,\?\|\\:/\.\w]";
			StringBuilder sb1 = new StringBuilder();
			sb1.AppendFormat(@"href\s*=\s*(?<3>(({0}+{1}*(\s|>))|(('|"")\s*{0}+({1}|\s)*('|""|>))))[^>]*>(?<4>[^<]*)</a>", first, next);
			Regex r = new Regex(sb1.ToString(), RegexOptions.IgnoreCase|RegexOptions.Compiled);
			return r;
		}
	}
}
