using System;
using System.Collections;
using System.Text;
using d = IrProject.Data;

namespace IrProject.Indexing
{
	public class QueryVector
	{
		public QueryVector(string text) { loadText(text);	}

		public QueryVector(Hashtable termCollectors, int[] weights) 
		{ 
			loadTermCollectors(termCollectors, weights); 
		}

		public string QueryTerms { get { return queryTerms; } }

		public string QueryWeights { get { return queryWeights; } }

		private void loadText(string text)
		{
			d.StopList stoplist = new d.StopList();
			PorterStemmer stemmer = new PorterStemmer();
			ParseHelper ph = new ParseHelper();
			text = text.ToLower();
			char[] delims = ph.GetDelims();
			string[] temp = text.Split(delims);			
			Hashtable terms = new Hashtable();
			string term;

			for (int i=0; i<temp.Length; i++)	
			{
				term = stemmer.stemTerm(temp[i]);
				if (!stoplist.Contains(term))
					if (terms.Contains(term))					
						terms[term] = Convert.ToInt32(terms[term]) + 1;
					else
						terms.Add(term, 1);
			}

			StringBuilder sbt = new StringBuilder();
			StringBuilder sbw = new StringBuilder();
			IDictionaryEnumerator en = terms.GetEnumerator();
			string t;
			string w;
			while (en.MoveNext())
			{
				t = en.Key.ToString();	
				w = terms[t].ToString();
				sbt.AppendFormat("{0},", t);
				sbw.AppendFormat("{0},", w);
			}
			queryTerms = sbt.ToString();
			queryWeights = sbw.ToString();
		}

		private void loadTermCollectors(Hashtable termCollectors, int[] weights)
		{
			StringBuilder sbt = new StringBuilder();
			StringBuilder sbw = new StringBuilder();
			string t;
			int w;
			IDictionaryEnumerator en = termCollectors.GetEnumerator();
			TermCollector tc;
			while (en.MoveNext())
			{
				t = en.Key.ToString();		
				tc = (TermCollector)termCollectors[t];	
				w = tc.GetCount(TermType.TEXT) * weights[0] + 
					tc.GetCount(TermType.BOLD_TEXT) * weights[1] +
					tc.GetCount(TermType.HEADING_TEXT) * weights[2] +
					tc.GetCount(TermType.ANCHOR_TEXT) * weights[3] +
					tc.GetCount(TermType.TITLE_TEXT) * weights[4] +
					tc.GetCount(TermType.URL_TEXT) * weights[5];

				sbt.AppendFormat("{0},", t);
				sbw.AppendFormat("{0},", w);
			}
			queryTerms = sbt.ToString();
			queryWeights = sbw.ToString();
		}

		private string queryTerms;
		private string queryWeights;
	}
}
