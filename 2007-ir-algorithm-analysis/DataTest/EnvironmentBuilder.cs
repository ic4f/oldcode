using System;
using System.IO;

namespace Giggle.DataTest
{
	public class EnvironmentBuilder
	{
		public const string INDEX_DIRECTORY = @"C:\_current\development\data\isr\temp\test\";
		public const string SOURCE_DIRECTORY = @"C:\_current\development\data\isr\temp\test\source\";
		public const string DOCS_FILE = "docs.txt";
		public const string TERMS_FILE = "terms.txt";
		public const string DOCSDATA_FILE = "docs.data";
		public const string TERMSDATA_FILE = "terms.data";
		public const string TERMDOCS_FILE = "termdocs.data";
		public const string PAGERANKS_FILE = "pageranks.data";
		public const short DOCS = 10;
		public const int TERMS = 6;

		private short[,] dt;

		public EnvironmentBuilder()
		{
			initMatrix();
			writeFiles();
		}

		public void Close()
		{
			deleteFile(INDEX_DIRECTORY + DOCS_FILE);
			deleteFile(INDEX_DIRECTORY + TERMS_FILE);
			deleteFile(INDEX_DIRECTORY + DOCSDATA_FILE);
			deleteFile(INDEX_DIRECTORY + TERMSDATA_FILE);
			deleteFile(INDEX_DIRECTORY + TERMDOCS_FILE);
			//deleteFile(INDEX_DIRECTORY + PAGERANKS_FILE);
		}

		public short[,] DTMatrix { get { return dt; } }

		private void writeFiles()
		{
			writeDocsFile();
			writeDocsDataFile();
			writeTermsFile();
			writeTermsDataFile();
			writeTermDocsFile();
			writePageRanksFile();
		}

		private void writeDocsFile()
		{
			string path = INDEX_DIRECTORY + DOCS_FILE;
			deleteFile(path);

			StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
			for (int i=1; i<DOCS+1; i++)
				w.WriteLine("d" + i);
			w.Close();
		}

		private void writeDocsDataFile()
		{
			string path = INDEX_DIRECTORY + DOCSDATA_FILE;
			deleteFile(path);

			Helper h = new Helper();
			float[] norms = h.GetNorms();
			float[] pr = h.GetPageRanks();
			
			FileStream fs = new FileStream(path, FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs);

			w.Write(DOCS);

			w.Write(norms[0]);
			w.Write(pr[0]);
			w.Write(norms[1]);
			w.Write(pr[1]);
			w.Write(norms[2]);
			w.Write(pr[2]);
			w.Write(norms[3]);
			w.Write(pr[3]);
			w.Write(norms[4]);
			w.Write(pr[4]);
			w.Write(norms[5]);
			w.Write(pr[5]);
			w.Write(norms[6]);
			w.Write(pr[6]);
			w.Write(norms[7]);
			w.Write(pr[7]);
			w.Write(norms[8]);
			w.Write(pr[8]);
			w.Write(norms[9]);
			w.Write(pr[9]);

			w.Close();
			fs.Close();
		}

		private void writeTermsFile()
		{
			string path = INDEX_DIRECTORY + TERMS_FILE;
			deleteFile(path);

			StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
			for (int i=1; i<7; i++)
				w.WriteLine("t" + i);
			w.Close();
		}

		private void writeTermsDataFile() //termId docId termFreqInDoc
		{
			string path = INDEX_DIRECTORY + TERMSDATA_FILE;
			deleteFile(path);

			Helper h = new Helper();
			float[] idf = h.GetIDFs();

			FileStream fs = new FileStream(path, FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs);

			w.Write(TERMS);
			w.Write(Convert.ToInt16(9)); //docFreq (# of docs containing term)
			w.Write(idf[0]);
			w.Write(Convert.ToInt16(5)); 
			w.Write(idf[1]);
			w.Write(Convert.ToInt16(6)); 
			w.Write(idf[2]);
			w.Write(Convert.ToInt16(5)); 
			w.Write(idf[3]);
			w.Write(Convert.ToInt16(7)); 
			w.Write(idf[4]);
			w.Write(Convert.ToInt16(5)); 
			w.Write(idf[5]);
      
			w.Close();
			fs.Close();
		}

		private void writeTermDocsFile() //termId docId termFreqInDoc
		{
			string path = INDEX_DIRECTORY + TERMDOCS_FILE;
			deleteFile(path);

			FileStream fs = new FileStream(path, FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs);

			for (int t=0; t<TERMS; t++)
				for(short d=0; d<DOCS; d++) //in my system docIds are implemented as shorts				
					if (dt[d,t] > 0)
					{
						w.Write(t);
						w.Write(d);
						w.Write(dt[d,t]);
					}
			w.Close();
			fs.Close();
		}

		private void writePageRanksFile()
		{
			string path = INDEX_DIRECTORY + PAGERANKS_FILE;
			deleteFile(path);

			FileStream fs = new FileStream(path, FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs);

			float pr = 0f;

			for(short d=0; d<DOCS; d++) //in my system docIds are implemented as shorts				
				w.Write(pr);

			w.Close();
			fs.Close();
		}


		private void deleteFile(string path)
		{
			if (File.Exists(path))
				File.Delete(path);
		}

		private void initMatrix()
		{
			dt = new short[DOCS,TERMS];
			dt[0,0] = 24;
			dt[0,1] = 21;
			dt[0,2] = 9;
			dt[0,3] = 0;
			dt[0,4] = 0;
			dt[0,5] = 3;
			dt[1,0] = 32;
			dt[1,1] = 10;
			dt[1,2] = 5;
			dt[1,3] = 0;
			dt[1,4] = 3;
			dt[1,5] = 0;
			dt[2,0] = 12;
			dt[2,1] = 16;
			dt[2,2] = 5;
			dt[2,3] = 0;
			dt[2,4] = 0;
			dt[2,5] = 0;
			dt[3,0] = 6;
			dt[3,1] = 7;
			dt[3,2] = 2;
			dt[3,3] = 0;
			dt[3,4] = 0;
			dt[3,5] = 0;
			dt[4,0] = 43;
			dt[4,1] = 31;
			dt[4,2] = 20;
			dt[4,3] = 0;
			dt[4,4] = 3;
			dt[4,5] = 0;
			dt[5,0] = 2;
			dt[5,1] = 0;
			dt[5,2] = 0;
			dt[5,3] = 18;
			dt[5,4] = 7;
			dt[5,5] = 16;
			dt[6,0] = 0;
			dt[6,1] = 0;
			dt[6,2] = 1;
			dt[6,3] = 32;
			dt[6,4] = 12;
			dt[6,5] = 0;
			dt[7,0] = 3;
			dt[7,1] = 0;
			dt[7,2] = 0;
			dt[7,3] = 22;
			dt[7,4] = 4;
			dt[7,5] = 2;
			dt[8,0] = 1;
			dt[8,1] = 0;
			dt[8,2] = 0;
			dt[8,3] = 34;
			dt[8,4] = 27;
			dt[8,5] = 25;
			dt[9,0] = 6;
			dt[9,1] = 0;
			dt[9,2] = 0;
			dt[9,3] = 17;
			dt[9,4] = 4;
			dt[9,5] = 23;
		}
	}
}
