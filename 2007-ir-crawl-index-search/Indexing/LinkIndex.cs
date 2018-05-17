using System;
using System.Collections;
using System.Data;
using d = IrProject.Data;

namespace IrProject.Indexing
{
	public class LinkIndex
	{
		private Hashtable inboundLinks;
		private Hashtable outboundLinks;

		public LinkIndex() { load(); }

		public Hashtable InboundLinks { get { return inboundLinks; } }

		public Hashtable OutboundLinks { get { return outboundLinks; } }

		public int[] GetInboundLinks(int docid)
		{
			return (int[])inboundLinks[docid];
		}

		public int[] GetOutboundLinks(int docid)
		{
			return (int[])outboundLinks[docid];
		}

		private void load()
		{
			DataTable docs = new d.DocData().GetLinkCounts();

			inboundLinks = new Hashtable();
			outboundLinks = new Hashtable();

			int id;
			foreach (DataRow dr in docs.Rows)
			{
				id = Convert.ToInt32(dr[0]);
				inboundLinks.Add(id, new int[Convert.ToInt32(dr[1])]);
				outboundLinks.Add(id, new int[Convert.ToInt32(dr[2])]);
			}

			d.LinkData linkData = new d.LinkData();
			DataTable dt = linkData.GetLinksSortByTo();
			
			int currId = -1;		
			int cursor = 0;						
			int toid;
			int fromid;
			int[] currLinks = null;
			foreach (DataRow dr in dt.Rows)
			{				
				toid = Convert.ToInt32(dr[0]);
				fromid = Convert.ToInt32(dr[1]); 
				
				if (currId < toid)
				{
					cursor = 0;
					currId = toid;
					currLinks = (int[])inboundLinks[toid];										
				}
				currLinks[cursor++] = fromid;
			}

			dt = linkData.GetLinksSortByFrom();

			currId = -1;		
			cursor = 0;						
			currLinks = null;
			foreach (DataRow dr in dt.Rows)
			{			
				fromid = Convert.ToInt32(dr[0]); 
				toid = Convert.ToInt32(dr[1]);			
				if (currId < fromid)
				{
					cursor = 0;
					currId = fromid;
					currLinks = (int[])outboundLinks[fromid];										
				}
				currLinks[cursor++] = toid;
			}
		}
	}
}
