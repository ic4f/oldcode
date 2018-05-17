using System;
using System.Collections;
using System.IO;
using d = Giggle.Data;

namespace Giggle.ConsoleTools
{
	public class LinksUtility
	{
		public LinksUtility()
		{
		}

		public class Link : IComparable
		{
			public short docId;
			public short linkId;

			public Link(short docId, short linkId)
			{
				this.docId = docId;
				this.linkId = linkId;
			}

			public int CompareTo(object o)
			{
				Link c = (Link)o;
				if (c.linkId > linkId)
					return -1;
				else if (c.linkId < linkId)
					return 1;
				else
				{
					if (c.docId > docId)
						return -1;
					else if (c.docId < docId)
						return 1;
					else
						return 0;
				}
			}
		}

		public void MakeCitationsDataFile()
		{
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY + d.Helper.DOCLINKS_FILE, FileMode.Open);
			BinaryReader r = new BinaryReader(fs);							

			long length = fs.Length;				
			int cursor=0;
			short docId;
			short linkId;
			Link[] docs = new Link[length/4];
			for (long i=0; i<length; i+=4) //advance counter by 4 bytes
			{				
				docId = r.ReadInt16();
				linkId = r.ReadInt16();
				docs[cursor] = new Link(docId, linkId);
				cursor++;
			}
			r.Close();
			fs.Close();		

			Array.Sort(docs);

			string path = d.Helper.INDEX_DIRECTORY + d.Helper.DOCCITATIONS_FILE;
			if (File.Exists(path))
				File.Delete(path);

			string pathText = path + ".txt";
			if (File.Exists(pathText))
				File.Delete(pathText);

			FileStream fs1 = new FileStream(path, FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs1);
			StreamWriter wText = new StreamWriter(pathText, false, System.Text.Encoding.ASCII);

			for (int i=0; i<docs.Length; i++)
			{
				w.Write(docs[i].linkId);
				w.Write(docs[i].docId);
				wText.WriteLine(docs[i].linkId + " " + docs[i].docId);
			}
				
			w.Close();
			wText.Close();
			fs1.Close();
		}
              
		public void MakeCitationsCountFile()
		{			
			short[] docCitations = new short[25053];

			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY + d.Helper.DOCLINKS_FILE, FileMode.Open);
			BinaryReader r = new BinaryReader(fs);							

			short docId;
			short linkToId; 
			long length = fs.Length;
			for (long i=0; i<length; i+=4) //advance counter by 4 bytes
			{				
				docId = r.ReadInt16(); 
				linkToId = r.ReadInt16();
				docCitations[linkToId] = Convert.ToInt16(docCitations[linkToId] + 1);
			}
			r.Close();
			fs.Close();		

			Console.WriteLine(docCitations[1]);
	


			string path = d.Helper.INDEX_DIRECTORY + "doccitationscount.data";
			if (File.Exists(path))
				File.Delete(path);

			string pathText = path + ".txt";
			if (File.Exists(pathText))
				File.Delete(pathText);

			FileStream fs1 = new FileStream(path, FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs1);
			StreamWriter wText = new StreamWriter(pathText, false, System.Text.Encoding.ASCII);

			for (int i=0; i<docCitations.Length; i++)
			{
				w.Write(docCitations[i]);
				wText.WriteLine(i + " " + docCitations[i]);
			}
				
			w.Close();
			wText.Close();
			fs1.Close();
		}

		public void TestLinks()
		{
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY + "linkstemp.data", FileMode.Open);
			BinaryReader r = new BinaryReader(fs);		
			short docId; 
			for (long i=0; i<48; i+=4) 			
				Console.WriteLine(r.ReadInt16());
			
			Console.WriteLine("done");
			r.Close();
			fs.Close();
		}

		public void CountLinksPerDoc(int numberOfDocs)
		{
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY + d.Helper.DOCLINKS_FILE, FileMode.Open);
			BinaryReader r = new BinaryReader(fs);							

			short[] docLinks = new short[numberOfDocs]; //position = docId, value = # of links

			short docId; 
			long length = fs.Length;				
			for (long i=0; i<length; i+=4) 
			{				
				docId = r.ReadInt16(); 
				r.ReadInt16(); //read link
				docLinks[docId] = Convert.ToInt16(docLinks[docId] + 1);
			}
			Console.WriteLine("done");
			r.Close();
			fs.Close();



			string path = d.Helper.INDEX_DIRECTORY + "linkstemp.data";			
			if (File.Exists(path))
				File.Delete(path);
			string textpath = path + ".txt";			
			if (File.Exists(textpath))
				File.Delete(textpath);

			FileStream fs1 = new FileStream(path, FileMode.CreateNew);
			FileStream fsText = new FileStream(textpath, FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs1);
			StreamWriter wText = new StreamWriter(fsText);

	
			int counter = 0;
			for (int i=0; i<docLinks.Length; i++)
			{
				w.Write(Convert.ToInt16(docLinks[i]));
				wText.WriteLine(i + " " + docLinks[i]);
				counter++;
			}
			w.Close();
			wText.Close();
			fs1.Close();
			fsText.Close();

			Console.WriteLine(counter + " " + numberOfDocs);


		}
	}
}