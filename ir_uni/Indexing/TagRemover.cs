using System;
using System.IO;
using System.Collections;
using System.Text;
using d = IrProject.Data;

namespace IrProject.Indexing
{
	/// <summary>
	/// removes ALL tags except b, h1-5, a and rewrites the entire doc collection to a DIFFERENT directory
	/// </summary>
	public class TagRemover
	{
		private string sourceDir;
		private string targetDir;
		private StringBuilder text;
		private StringBuilder tag;
		private bool seekTagStart;
		private char c;
		private char c1; 
		private TextReader r;

		public TagRemover(string sourceDir, string targetDir)
		{
			if (sourceDir == targetDir)
				throw new Exception("you're about to kill your index!");
			this.sourceDir = sourceDir;
			this.targetDir = targetDir;
		}

		public TagRemover()
		{
			//used by PageParser. Ugly design - must fix
		}

		public void RemoveTags()
		{
			string[] files = Directory.GetFiles(sourceDir);
			FileInfo fi;
			FileStream fs;
			for (int i=0; i<files.Length; i++)	
			{
				fs = File.OpenRead(files[i]);
				r = new StreamReader(fs, System.Text.Encoding.ASCII);		

				fi = new FileInfo(files[i]);
				writeFile(fi.Name, ProcessText(r));

				if (i % 1000 == 0)
					Console.WriteLine("processing file #" + i);
			}
		}

		public string ProcessText(TextReader reader)
		{
			r = reader;
			seekTagStart = true;
			text = new StringBuilder();
			tag = new StringBuilder();
						
			while (r.Peek() > -1) //ugly loop due to stackoverflow error with recursion
			{				
				c = (char)r.Read();				

				if (seekTagStart) //read text until next tag
				{
					if (c == '<') //fount tag start -> back to loop			
					{
						seekTagStart = false;
						processTag();
					}
					else
						text.Append(c);					
				}
				else if (c == '>')
					seekTagStart = true;
			}
			return text.ToString();
		}

		private void processTag() //'<' already read
		{
			char c = (char)r.Read();
			if (c == 'b' || c == 'B') //possible <b> tag
			{
				c = (char)r.Read();
				if (c == '>')
				{
					text.Append("<b>"); //found <b> tag
					seekTagStart = true;
				}						
			}			
			else if (c == 'h' || c == 'H') //possible h1-5 tag
			{				
				c = (char)r.Read();
				if (c == '1' || c == '2' || c == '3' || c == '4' || c == '5')
				{					
					c1 = (char)r.Read();
					if (c1 == '>')
					{
						text.Append("<h" + c + ">");
						seekTagStart = true;
					}
					else if (c1 == ' ')					
						text.Append("<h" + c + ">");					
				}
			}
			else if (c == '/') //possible closing b or h1-6 tag
			{
				c = (char)r.Read();
				if (c == 'b' || c == 'B')
				{
					c = (char)r.Read();
					if (c == '>')
					{
						text.Append("</b>");
						seekTagStart = true;
					}
				}
				else if (c == 'a' || c == 'A')
				{
					c = (char)r.Read();
					if (c == '>')
					{
						text.Append("</a>");
						seekTagStart = true;
					}
				}
				else if (c == 'h' || c == 'H')
				{
					c = (char)r.Read();
					if (c == '1' || c == '2' || c == '3' || c == '4' || c == '5')
					{
						c1 = (char)r.Read();
						if (c1 == '>' || c1 == ' ')
						{
							text.Append("</h" + c + ">");
							seekTagStart = true;
						}
					}
				}
			}
			else if (c == 'a' || c == 'A')
			{
				text.Append("<a");
				while ((c = (char)r.Read()) != '>')
					text.Append(c);
				
				text.Append(c);	
				seekTagStart = true;
			}
			return;
		}

		private void writeFile(string fileName, string content)
		{
			StreamWriter w = new StreamWriter(targetDir + fileName, false, System.Text.Encoding.ASCII);
			w.Write(content);
			w.Close();
		}
	}
}
