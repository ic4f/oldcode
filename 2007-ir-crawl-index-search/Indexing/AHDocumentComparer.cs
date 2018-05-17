using System;
using System.Collections;

namespace IrProject.Indexing
{
	public class AHDocumentComparer : IComparer
	{
		public const int AUTHORITIES = 0;
		public const int HUBS = 1;
		private int toCompare;

		public AHDocumentComparer(int toCompare)
		{
			this.toCompare = toCompare;
		}

		public int Compare(object a, object b)
		{
			AHDocument docA = (AHDocument)a;
			AHDocument docB = (AHDocument)b;

			if (toCompare == AUTHORITIES)
			{
				if (docA.AuthorityScore > docB.AuthorityScore)
					return -1;
				else if (docA.AuthorityScore < docB.AuthorityScore)
					return 1;
				else
					return 0;
			}
			else 
			{
				if (docA.HubScore > docB.HubScore)
					return -1;
				else if (docA.HubScore < docB.HubScore)
					return 1;
				else
					return 0;
			}
		}
	}
}
