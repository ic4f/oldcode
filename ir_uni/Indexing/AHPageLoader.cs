using System;
using System.Collections;

namespace IrProject.Indexing
{
	/// <summary>
	/// Takes as an argument an integer array of search results (page IDs), 
	/// calculates root and base sets, initializes a PageGraph and calls its calculateAuthorityHubs method. 
	/// Provides access to sorted PageRecord arrays of authorities and hubs.
	/// </summary>
	public class AHPageLoader
	{
		private LinkIndex index;
		private AHPageGraph pageGraph;

		public AHPageLoader(int[] resultDocs, int rootSize, int maxParents, int maxChildren)
		{		
			index = new LinkIndex();
			pageGraph = new AHPageGraph(rootSize);
			loadBaseSet(resultDocs, rootSize, maxParents, maxChildren);				
			loadLinks();					
		}

		public AHDocument[] Authorities { get { return pageGraph.GetAuthorities(); } }
		
		public AHDocument[] Hubs { get { return pageGraph.GetHubs(); } }

		private void loadBaseSet(int[] resultDocs, int rootSize, int maxParents, int maxChildren)
		{
			//make rootset
			int[] rootSet = new int[Math.Min(resultDocs.Length, rootSize)];
			for (int i=0; i< rootSet.Length; i++)
				rootSet[i] = resultDocs[i];

			//make baseset
			int[] parents;
			int[] children;
			for (int i=0; i< rootSet.Length; i++)
			{
				pageGraph.AddPage(rootSet[i]);

				parents = index.GetInboundLinks(rootSet[i]);
				if (parents != null)
				{
					maxParents = Math.Min(maxParents, parents.Length);
					for (int j=0; j<maxParents; j++)
						pageGraph.AddPage(parents[j]);
				}						
				children = index.GetOutboundLinks(rootSet[i]);	
				if (children != null)
				{
					maxChildren = Math.Min(maxChildren, children.Length);
					for (int j=0; j<maxChildren; j++)
						pageGraph.AddPage(children[j]);	
				}
			}
		}

		private void loadLinks()
		{			
			Hashtable docs = pageGraph.BaseSet;

			int docId;
			int[] links;
			IDictionaryEnumerator en = docs.GetEnumerator();
			while (en.MoveNext())
			{				
				docId = Convert.ToInt16(en.Key);
				links = index.GetOutboundLinks(docId);			
				if (links != null)
					foreach (int linkId in links)				
						if (docs.Contains(linkId))
							pageGraph.AddLink(docId, linkId);
						
				links = index.GetInboundLinks(docId);
				if (links != null)
					foreach (int linkId in links)				
						if (docs.Contains(linkId))
							pageGraph.AddLink(linkId, docId);		
			}
		}
	}
}
