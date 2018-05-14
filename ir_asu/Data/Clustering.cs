using System;
using System.Collections;

namespace Giggle.Data
{
	/// <summary>
	/// base class for clustering algorithms
	/// </summary>
	public abstract class Clustering
	{
		private Random randomGenerator;
		private Index index;
		private int k;

		public Clustering(Index index, int k) 
		{
			randomGenerator = new Random();
			this.index = index;
			this.k = k;
		}

		public Cluster[] GetClusters(short[] docIds, int commonTerms)
		{
			//check that there are docs to cluster
			if (docIds == null || docIds.Length == 0) throw new Exception("Cannot cluster 0 documents");
			//check that docIds.Length <= k, if not - return 1 cluster per doc
			if (docIds.Length <= k) return GetSimpleCase(docIds, commonTerms); 

			return CalculateClusters(docIds, commonTerms);
		}

		public abstract Cluster[] CalculateClusters(short[] docIds, int commonTerms);

		protected Index GetIndex { get { return index; } }

		protected Random GetRandomGenerator { get { return randomGenerator; } }

		protected int GetK { get { return k; } }

		protected Cluster[] GetEmptyClusters(int number)
		{
			Cluster[] clusters = new Cluster[number];
			for (int i=0; i<clusters.Length; i++)
				clusters[i] = new Cluster();
			return clusters;
		}

		//returns an array of randomnly selected k DISTINCT docIds
		protected Cluster[] GetInitialClusters(short[] docIds, int number)
		{
			Cluster[] clusters = GetEmptyClusters(number);	
			Hashtable selected = new Hashtable();
			int cursor = 0;
			for (int i=0; i<number; i++)
			{
				int position = GetRandomGenerator.Next(docIds.Length);
				while (selected.Contains(position))				
					if (++position == docIds.Length) position = 0; //circular search: if pos > max allowed, make it 0				
				selected.Add(position, true);
				clusters[cursor++].AddDoc(docIds[position]);
			}
			return clusters;
		}

		protected Cluster[] GetSimpleCase(short[] docIds, int commonTerms)
		{
			Cluster[] clusters = new Cluster[docIds.Length];
			for (int i=0; i<clusters.Length; i++)
			{
				clusters[i] = new Cluster();
				clusters[i].AddDoc(docIds[i]);
			}
			return LoadCommonTerms(clusters, commonTerms);
		}

		protected Cluster[] LoadCommonTerms(Cluster[] clusters, int commonTerms)
		{
			Hashtable[] clusterTerms = collectAllTerms(clusters);
			TermCountItem[][] sortedTerms = getSortedTermArrays(clusterTerms, clusters.Length);
			return assignDistinctTerms(clusters, sortedTerms, commonTerms);
		}

		private Hashtable[] collectAllTerms(Cluster[] clusters)
		{
			Hashtable[] clusterTerms = new Hashtable[clusters.Length]; //collect all term counts for each cluster
			for(int i=0; i<clusterTerms.Length; i++)
			{
				clusterTerms[i] = new Hashtable();
				IDictionaryEnumerator en = clusters[i].DocIds.GetEnumerator();
				while (en.MoveNext())
				{
					short docId = Convert.ToInt16(en.Key);
					DocTermItem[] docTerms = GetIndex.DocTerms(docId); //get all terms+counts for current doc
					foreach (DocTermItem dt in docTerms)
					{
						int termId = dt.TermId;
						short termCount = dt.TermCount;
						TermCountItem tc = new TermCountItem(termId, termCount);
						if (!clusterTerms[i].Contains(termId))
							clusterTerms[i].Add(termId, tc);
						else					
						{
							TermCountItem existingTC = (TermCountItem)clusterTerms[i][termId];
							existingTC.termCount = existingTC.termCount + termCount;
						}
					}
				}				
			}
			return clusterTerms;
		}

		private TermCountItem[][] getSortedTermArrays(Hashtable[] clusterTerms, int clusterCount)
		{
			TermCountItem[][] sortedTerms = new TermCountItem[clusterCount][];
			for (int i=0; i<clusterCount; i++)
			{
				int cursor = 0;
				sortedTerms[i] = new TermCountItem[clusterTerms[i].Count]; //array length = # of terms in cluster
				IDictionaryEnumerator en = clusterTerms[i].GetEnumerator();
				while (en.MoveNext())
					sortedTerms[i][cursor++] = (TermCountItem)en.Value;
				
				Array.Sort(sortedTerms[i]);
			}
			return sortedTerms;
		}

		private Cluster[] assignDistinctTerms(Cluster[] clusters, TermCountItem[][] sortedTerms, int commonTerms)
		{			
			Hashtable added = new Hashtable(); //keeps track of selected terms
			for (int i=0; i<clusters.Length; i++)  //for each cluster make list of top DISTINCT terms
			{
				TermCountItem[] tcItems = sortedTerms[i];
				int maxTerms = Math.Min(tcItems.Length, commonTerms);
				int termId = 0;
				int cursor = 0;
				for (int j=0; j<maxTerms; j++) //add at most maxTerms terms
				{
					while (cursor < tcItems.Length && added.Contains(termId)) //look for next available									
						termId = tcItems[cursor++].termId;

					clusters[i].AddCommonTerm(termId);
					added.Add(termId, true);
				}
			}
			return clusters;
		}

		private class TermCountItem : IComparable
		{
			public int termId;
			public int termCount;

			public TermCountItem(int termId, int termCount)
			{
				this.termId = termId;
				this.termCount = termCount;
			}

			public int CompareTo(object o)
			{
				TermCountItem x = (TermCountItem)o;
				if (x.termCount < termCount)
					return -1;
				else if (x.termCount > termCount)
					return 1;
				else
					return 0;
			}					
		}
	}
}
