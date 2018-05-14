using System;

namespace IrProject.Indexing
{
	public class TermCollector
	{
		private string term;
		private int[] count;

		public TermCollector(string term)
		{			
			this.term = term;
			count = new int[6];
			count[TermType.TEXT] = 0;
			count[TermType.BOLD_TEXT] = 0;
			count[TermType.HEADING_TEXT] = 0;
			count[TermType.ANCHOR_TEXT] = 0;
			count[TermType.TITLE_TEXT] = 0;
			count[TermType.URL_TEXT] = 0;
		}

		public void IncrementCount(int termType)
		{
			count[termType] = count[termType] + 1;
		}

		public int GetCount(int termType) { return count[termType]; }

		public int GetTotalCount() 
		{
			return count[TermType.TEXT] + count[TermType.BOLD_TEXT] + count[TermType.HEADING_TEXT] + 
				count[TermType.ANCHOR_TEXT] + count[TermType.TITLE_TEXT] + count[TermType.URL_TEXT];
		}
	}
}
