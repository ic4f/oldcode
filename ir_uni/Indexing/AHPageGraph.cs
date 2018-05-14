using System;
using System.Collections;

namespace IrProject.Indexing
{
	/// <summary>
	/// Represents the page graph (or link graph) as an adjacency list. 
	/// Used to build the graph by adding pages and links and return the calculated hub and authority scores.
	/// </summary>
	public class AHPageGraph
	{
		public static float THRESHOLD = 0.001f; //reasonable value for converging
		private Hashtable forwardLinks;
		private Hashtable backLinks;
		private Hashtable globalIds; //key=global docId, value=local id
		private Hashtable localIds; //key=local id, value = global docId
		private bool calculatedAH;
		private bool pagesAdded;
		private float[] authScores; //position = docId
		private float[] hubScores; //position = docId
		private int cursor; //keeps track of current number of pages

		public AHPageGraph(int rootSize)
		{
			cursor = 0;
			globalIds = new Hashtable();
			localIds = new Hashtable();
			calculatedAH = false;
			pagesAdded = false;
		}

		public void AddPage(int docId)
		{
			if (pagesAdded)
				throw new Exception("you cannot add a page after adding a link");
			if (!globalIds.Contains(docId))
			{
				int id = cursor; //for clarity
				localIds.Add(id, docId);
				globalIds.Add(docId, id);
				cursor++;
			}
		}

		public void AddLink(int fromDocId, int toDocId)
		{
			if (!pagesAdded)
			{
				forwardLinks = new Hashtable(globalIds.Count);
				backLinks = new Hashtable(globalIds.Count);
				pagesAdded = true;
			}
			//convert to internal ids
			int fromId = Convert.ToInt16(globalIds[fromDocId]);
			int toId = Convert.ToInt16(globalIds[toDocId]);

			ArrayList links = (ArrayList)forwardLinks[fromId];
			if (links == null)
			{
				links = new ArrayList();
				forwardLinks.Add(fromId, links);			
			}
			links.Add(toId);
		
			links = (ArrayList)backLinks[toId];
			if (links == null)
			{
				links = new ArrayList();
				backLinks.Add(toId, links);			
			}
			links.Add(fromId);				
		}

		public Hashtable BaseSet { get { return globalIds; } }

		public int BaseSetSize { get { return globalIds.Count; } }

		public AHDocument[] GetAuthorities()
		{ 
			return getScores(new AHDocumentComparer(AHDocumentComparer.AUTHORITIES));			
		}

		public AHDocument[] GetHubs()
		{ 
			return getScores(new AHDocumentComparer(AHDocumentComparer.HUBS));			
		}

		private AHDocument[] getScores(AHDocumentComparer comparer)
		{
			if (!calculatedAH) //ensures that scores are calculated (only once) before returned
			{
				runAuthHubAlgorithm();
				calculatedAH = true;
			}
			AHDocument[] results = new AHDocument[globalIds.Count];
			for (int id=0; id<results.Length; id++)		
				results[id] = new AHDocument(Convert.ToInt16(localIds[id]), authScores[id], hubScores[id]);
			
			Array.Sort(results, comparer);
			return results;
		}

		private void runAuthHubAlgorithm()
		{
			int count = globalIds.Count;
			//initialize all scores to 1
			authScores = new float[count];
			hubScores = new float[count];

			for (int i=0; i<count; i++)
			{
				authScores[i] = 1;
				hubScores[i] = 1;
			}

			ArrayList list;
			float aCounter;
			float hCounter;
			float[] authTemp = new float[count]; //holds the difference of auth scores between iterations
			float[] hubsTemp = new float[count]; //holds the difference of hub scores between iterations
			while (notConverged(authTemp, hubsTemp))
			{			
				//add the hub scores of all backlinks to the authority score of the current page
				for (int i=0; i<count; i++)
				{
					//update difference arrays
					authTemp[i] = authScores[i];
					hubsTemp[i] = hubScores[i];	
							
					if (backLinks[i] != null)
					{
						list = (ArrayList)backLinks[i];
						authScores[i] = 0;
						foreach (int docId in list)
							authScores[i] += hubScores[docId];						
					}
				}
				//add the auth scores of all forwardlinks to the hub score of the current page
				for (int i=0; i<count; i++)
				{				
					list = (ArrayList)forwardLinks[i];
					hubScores[i] = 0;
					if (list != null)
						foreach (int docId in list)
							hubScores[i] += authScores[docId];				
				}

				// normalize
				aCounter = 0;
				hCounter = 0;
				for (int i=0; i<count; i++)
				{
					aCounter += Convert.ToSingle(Math.Pow(authScores[i], 2));
					hCounter += Convert.ToSingle(Math.Pow(hubScores[i], 2));
				}
				for (int i=0; i<count; i++)
				{
					authScores[i] = authScores[i] / (float)Math.Sqrt(aCounter);
					hubScores[i] = hubScores[i] / (float)Math.Sqrt(hCounter);
				}
			}
		}

		private bool notConverged(float[] authTemp, float[] hubsTemp)
		{		
			int count = globalIds.Count;
			for (int i=0; i<count; i++)						
				if ( (Math.Abs(authScores[i] - authTemp[i]) > THRESHOLD) ||		
					(Math.Abs(hubScores[i] - hubsTemp[i]) > THRESHOLD) )		
					return true;						
			return false;
		}
	}
}
