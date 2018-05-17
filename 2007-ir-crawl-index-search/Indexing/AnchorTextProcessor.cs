using System;
using System.Collections;
using System.Data;
using System.Text;
using d = IrProject.Data;

namespace IrProject.Indexing
{
	public class AnchorTextProcessor
	{
		public AnchorTextProcessor()
		{
		}

		public void AddAnchorText()
		{
			d.StopList stopList = new d.StopList();
			ParseHelper parseHelper = new ParseHelper();
			char[] delims = parseHelper.GetDelims();	
			PorterStemmer stemmer = new PorterStemmer();
			d.LinkData ld = new d.LinkData();
			d.TermDocData tdd = new d.TermDocData();

			DataTable linksTable;
			int docId;
			StringBuilder sb;
			string[] terms;
			string term;
			Hashtable currTerms;
			DataTable dt = new d.DocData().GetIds();
			for (int i=0; i<dt.Rows.Count; i++)
			{
				if (i % 10 == 0) Console.WriteLine(i);

				//accumulate all link text for this doc into StringBuilder
				sb = new StringBuilder();
				docId = (int)dt.Rows[i][0];
				linksTable = ld.GetRecordsByToId(docId);				
				foreach (DataRow dr in linksTable.Rows) 				
					sb.AppendFormat("{0} ", dr[0].ToString());

				//accum terms + counts into currTerms hashtable
				currTerms = new Hashtable();
				terms = sb.ToString().Split(delims);							
				for (int j=0; j<terms.Length; j++)
				{
					term = stemmer.stemTerm(terms[j].ToLower().Trim());
					if (term != "home" && term.Length > 0 && term.Length < 25 && !stopList.Contains(term) && parseHelper.IsAsciiLetters(term)) 
					{
						if (!currTerms.Contains(term))
							currTerms.Add(term, 1);
						else						
							currTerms[term] = (int)currTerms[term] + 1;						
					}
				}

				//write terms and counts to database
				IDictionaryEnumerator en = currTerms.GetEnumerator();
				string currTerm;
				int currCount;
				while (en.MoveNext())
				{
					currTerm = en.Key.ToString();
					currCount = (int)currTerms[currTerm];
					tdd.UpdateAnchorTextCount(currTerm, docId, currCount);
				}
			}
		}
	}
}
