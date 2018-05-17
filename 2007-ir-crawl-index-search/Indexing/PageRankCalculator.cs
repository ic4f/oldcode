using System;
using System.Collections;
using System.Data;
using System.IO;
using d = IrProject.Data;

namespace IrProject.Indexing
{
	/// <summary>
	/// calculates PageRank, can return an array or write it to a file
	/// </summary>
	public class PageRankCalculator
	{
		private const float THRESHOLD = 0.001f;		//reasonable value: takes 10 iterations to converge 25.000 page ranks
		private float c;													//weight c, used in PageRank formula
		private Hashtable inboundLinks;						//key=docId, value = Hashtable of inboundlinks
		private Hashtable outboundLinkCouns;			//key=docId, value = number of outboundlinks
		private float[] ranks;										//rank values
		private int[] ids;												//key = position in ranks array, value = actual docid

		public PageRankCalculator() //arrays come from the Index object
		{
			c = 0.6f; //can change this to experiement
		}

		public void CalculateRanks()
		{			
			init();
			Console.WriteLine("system initialized. calculating...");
			runPRAlgorithm();	
			Console.WriteLine("normalizing...");
			normalize();
			Console.WriteLine("writing to database");
			writeToDatabase();
		}

		private void init()
		{		
			DataTable docIds = new d.DocData().GetIds();
			int doccount = docIds.Rows.Count;
			float initRank = 1f/doccount;
			LinkIndex index = new LinkIndex();

			inboundLinks = new Hashtable();
			ranks = new float[doccount];
			ids = new int[doccount];	

			int i = 0;
			int docid;
			int[] inboundArray;
			foreach(DataRow dr in docIds.Rows)
			{
				docid = Convert.ToInt32(dr[0]);	

				ranks[i] = initRank;	//load initial pagerank value
				ids[i] = docid;				//load docid into position i
				i++;
							
				Hashtable currInbounds = new Hashtable();			//make hashtable for current docid
				inboundLinks.Add(docid, currInbounds);				//store this hashtable of inboundlinks for current docid
				inboundArray = index.GetInboundLinks(docid);	//get inbound links array for current docid
				foreach (int fromid in inboundArray)					//store it in this hashtable
					currInbounds.Add(fromid, true);
			}

			outboundLinkCouns = new Hashtable();
			DataTable linkCounts = new d.DocData().GetLinkCounts(); 
			int countTo;
			foreach(DataRow dr in linkCounts.Rows)
			{
				docid = Convert.ToInt32(dr[0]);
				countTo = Convert.ToInt32(dr[2]);
				outboundLinkCouns.Add(docid, countTo);
			}
		}

		private void runPRAlgorithm()
		{			
			Console.WriteLine("start PageRank calculation");
			float rankAccum; //accumulates rank for a page
			float p = (1-c)/ranks.Length; //the minimum probability
			float[] newRanks;	//required for calculating ranks maintaining ranks of previous iteration
			float[] tempRanks = new float[ranks.Length]; //holds the difference between iterations
			Hashtable parents;

			int counter = 0;
			do 
			{
				Console.WriteLine("PageRank calculation counter=" + counter++);
				newRanks = new float[ranks.Length];			
				tempRanks = ranks;

				for (int i=0; i<ranks.Length; i++) //loop through all pages
				{		
					rankAccum = 0; //reset rank accumulator
					parents = (Hashtable)inboundLinks[ids[i]]; //parent pages / backlinks
					/*  
					 * p = (1-c)/|number of pages| 
					 * If page j has a link to page i, then rank(i) =+ rank(j) * (1/|children of j| * c + p)
					 * Else - rank(i) =+ rank(j) * p
					 */							
					for (int j=0; j<ranks.Length; j++) //loop through all pages (i.e. parents)						
					{					
						if (parents.Contains(ids[j]))
							rankAccum += (1f / Convert.ToInt32(outboundLinkCouns[ids[j]]) * c + p) * ranks[j];																												
						else					
							rankAccum += p * ranks[j];
					}																		
					newRanks[i] = rankAccum;	
				}	
				ranks = newRanks; //assign updated ranks		
			}
			while (notConverged(tempRanks));
		}

		private bool notConverged(float[] ranksTemp)
		{		
			for (int i=0; i<ranks.Length; i++)						
				if ( (Math.Abs(ranks[i] - ranksTemp[i]) > THRESHOLD))
					return true;			
			return false;
		}

		private void normalize()
		{
			float max = 0;		
			for (int i=0; i<ranks.Length; i++)
				if (ranks[i] > max)			
					max = ranks[i];			
			
			for (int i=0; i<ranks.Length; i++)
				ranks[i] = ranks[i]/max;			
		}

		private void writeToDatabase()
		{
			d.DocData dd = new d.DocData();
			for (int i=0; i<ranks.Length; i++)						
				dd.UpdatePageRank(ids[i], ranks[i]);
		}
	}
}
