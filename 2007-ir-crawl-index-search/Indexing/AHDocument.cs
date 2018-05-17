using System;

namespace IrProject.Indexing
{
	public class AHDocument
	{
		private int docId;
		private float authScore;
		private float hubScore;

		public AHDocument(int docId, float authScore, float hubScore)
		{
			this.docId = docId;
			this.authScore = authScore;
			this.hubScore = hubScore;
		}

		public int DocId { get { return docId; } }

		public float AuthorityScore { get { return authScore; } }

		public float HubScore { get { return hubScore; } }

		public override string ToString()
		{
			return docId + " auth=" + authScore + " hub=" + hubScore;
		}
	}
}
