using System;
using System.Text;
using System.Text.RegularExpressions;

namespace IrProject.Indexing
{
	public class UrlHelper
	{
		public UrlHelper()
		{
			string first = @"[./\w]";
			string next = @"[~;!#\$%&\-\+/=,\?\|\\:/\.\w]";
			StringBuilder sb1 = new StringBuilder();
			sb1.AppendFormat(@"href\s*=\s*(?<3>(({0}+{1}*(\s|>))|(('|"")\s*{0}+({1}|\s)*('|""|>))))", first, next);
			regex1 = new Regex(sb1.ToString(), RegexOptions.IgnoreCase|RegexOptions.Compiled);

			string a = @"([\w\-]+.)";
			StringBuilder sb2 = new StringBuilder();
			sb2.AppendFormat(@"http(s)?://((www.)|{0})?{0}?uni.edu{1}*", a, next);
			regex2 = new Regex(sb2.ToString());
		}

		public Regex GetRegex { get { return regex1; } }

		public bool IsValidUrl(string url)
		{
			return regex1.IsMatch(url);
		}

		public bool LinkIsRelevant(string link, Uri uri)
		{
			if (
				link.EndsWith(".jpg") || 
				link.EndsWith(".jpeg") || 
				link.EndsWith(".gif") || 
				link.EndsWith("png") ||
				link.EndsWith("doc") ||
				link.EndsWith("xls") ||
				link.EndsWith("pdf") ||
				link.EndsWith("ps") ||
				link.EndsWith("js") ||
				link.EndsWith("css") ||
				link.EndsWith("ico"))
				return false;

			if (
				link.IndexOf("espresso.uni.edu") > -1 ||
				link.IndexOf("cdm.lib.uni.edu") > -1 ||
				link.IndexOf("bccd.cs.uni.edu") > -1 ||
				link.IndexOf("uni.edu/sasdoc/") > -1)
				return false; //ignore these sites - have tons of useless pages

			if (uri.Query.Length > 20 || uri.Query.IndexOf("&") > -1) //allow only 1 param and not too lengthy
				return false;

			return regex2.IsMatch(link); 
		}

		public string MakeLink(string link)
		{
			link = link.Trim();
			if ((link.StartsWith("'")) | (link.StartsWith("\"")))
				link = link.Substring(1);
			if ((link.EndsWith("'")) | (link.EndsWith("\"")))
				link = link.Substring(0, link.Length-1);
			return link.Trim();
		}

    public string NormalizeUrl(string url)
		{
			if (url.IndexOf("#") > -1)			
				url = url.Substring(0, url.Length - (url.Length - url.IndexOf("#")));
			
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

		private Regex regex1;
		private Regex regex2;
	}
}
