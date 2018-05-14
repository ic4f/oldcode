using System;
using System.IO;
using d = IrProject.Data;

namespace IrProject.Indexing
{	
	public class DataHelper
	{
		public DataHelper()
		{
		}

		//remove the extra 1.213 files
		public void RemoveExtraPageFiles()
		{
			string path = Helper.PAGES_PATH;	
			string[] files = Directory.GetFiles(path);
			d.DocData pd = new d.DocData();
			for (int i=0; i<files.Length; i++)						
				if (!pd.PageIdExists(i))
					File.Delete(path + i + ".html");				
		}

		//add <t> and <u> tags for title and url text
		public void AddTitleUrlTags()
		{
			string path = Helper.DOCS_PATH;	
			string[] files = Directory.GetFiles(path);
			d.Doc p;
			int pageId;
			FileInfo fi;
			StreamWriter sw;
			for (int i=0; i<files.Length; i++)						
			{
				fi = new FileInfo(files[i]);
				pageId = Convert.ToInt32(fi.Name.Substring(0, fi.Name.IndexOf(".")));
				p = new d.Doc(pageId);
				sw = new StreamWriter(files[i], true);
				sw.WriteLine("<u>" + p.Url + "</u>");
				sw.WriteLine("<t>" + p.Title + "</t>");
				sw.Close();
				Console.WriteLine(i);
			}			
		}
	}
}
