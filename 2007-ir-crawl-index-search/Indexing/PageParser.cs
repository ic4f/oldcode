using System;
using System.Data;
using System.IO;
using System.Text;
using System.Net;
using System.Collections;

namespace IrProject.Indexing
{
	public class PageParser
	{
		private string url;
		private string source;
		private string title;
		private int termCount;
		private DataTable termTable;

		public PageParser(string url)
		{
			this.url = url;
			load();
		}

		public string GetTitle() { return title; }

		public string GetSource() { return source; }

		public int GetTermCount() {	return termCount;	}

		public DataView GetTerms(string sort)
		{			
			loadTerms();			
			DataView dv = new DataView(termTable);
			dv.Sort = sort;
			return dv;
		}

		public QueryVector GetQuery(int[] weights)
		{			
			return new QueryVector(getTermCollectors(), weights);
		}

		private void load()
		{
			HttpWebRequest wrq;
			HttpWebResponse wr;
			try 
			{
				wrq = (HttpWebRequest)WebRequest.Create(url);
				wrq.UserAgent = "LOTW UNI Crawler/computer science research project";
				wrq.Timeout = 5000;				
				wr = (HttpWebResponse)wrq.GetResponse(); 
			}
			catch (Exception)
			{
				source = "";
				return;
			}
			if (wr.ContentType.IndexOf("text/html") > -1)	
			{
				source = new StreamReader(wr.GetResponseStream()).ReadToEnd();			
				title = extractTitle();
				TagRemover tr = new TagRemover();
				source = tr.ProcessText(new StringReader(source));
			}
			else
				source = "";
			wr.Close();
		}

		private Hashtable getTermCollectors()
		{
			return new TermExtractor().ExtractTerms(source);
		}

		private void loadTerms()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("\n<t>{0}</t>\n<u>{1}</u>", title, url);
			source += sb.ToString();

			termTable = new DataTable();
			termTable.Columns.Add(new DataColumn("term"));
			termTable.Columns.Add(new DataColumn("textcount", Type.GetType("System.Int32")));
			termTable.Columns.Add(new DataColumn("boldcount", Type.GetType("System.Int32")));
			termTable.Columns.Add(new DataColumn("headercount", Type.GetType("System.Int32")));
			termTable.Columns.Add(new DataColumn("anchorcount", Type.GetType("System.Int32")));
			termTable.Columns.Add(new DataColumn("titlecount", Type.GetType("System.Int32")));
			termTable.Columns.Add(new DataColumn("urlcount", Type.GetType("System.Int32")));
			termTable.Columns.Add(new DataColumn("totalcount", Type.GetType("System.Int32")));			

			Hashtable termCollectors = getTermCollectors();
			termCount = termCollectors.Count;

			IDictionaryEnumerator en = termCollectors.GetEnumerator();
			string term;
			TermCollector tc;
			while (en.MoveNext())
			{
				term = en.Key.ToString();		
				tc = (TermCollector)termCollectors[term];	
				DataRow row = termTable.NewRow();
				row[0] = term;
				row[1] = tc.GetCount(TermType.TEXT);
				row[2] = tc.GetCount(TermType.BOLD_TEXT);
				row[3] = tc.GetCount(TermType.HEADING_TEXT);
				row[4] = tc.GetCount(TermType.ANCHOR_TEXT);
				row[5] = tc.GetCount(TermType.TITLE_TEXT);
				row[6] = tc.GetCount(TermType.URL_TEXT);
				row[7] = tc.GetTotalCount();
				termTable.Rows.Add(row);
			}			
		}

		private string extractTitle()
		{			
			string t = "";
			int a = source.IndexOf("<title>");
			if (a < 0)
				a = source.IndexOf("<TITLE>"); 
			if (a >= 0)
			{
				a+=7;
				int b = source.IndexOf("</title>");
				if (b < 0)
					b = source.IndexOf("</TITLE>");
				if (b >= 0)				
				{
					t = source.Substring(a, b-a).Trim();
					t = t.Replace("<", "");
					t = t.Replace(">", "");
				}
			}
			return t;
		}
	}
}
