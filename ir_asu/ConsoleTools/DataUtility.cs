using System;
using System.Collections;
using System.IO;
using d = Giggle.Data;

namespace Giggle.ConsoleTools
{
    //used for various data testing 
    public class DataUtility
    {

        public const int DOCUMENTS = 25053;
        public const int TERMS = 111967;

        public DataUtility() { }

        public void temp2()
        {
            FileStream fs = new FileStream(@"C:\_current\development\data\isr\giggleindex\1_1_1_1\source\hashedlinks.txt", FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            while ((line = r.ReadLine()) != null)
                if (line.IndexOf(".cgi") > -1)
                    Console.WriteLine(line);
            
            r.Close();
            fs.Close();
        }

        public void temp()
        {
            Hashtable docs1 = new Hashtable();
            string FILTERED_DOCS_DIRECTORY = @"C:\_current\development\data\isr\temp\filteredindex\";
            string DOCS_FILE = @"C:\_current\development\data\isr\giggleindex\1_1_1_1\source\docs.txt";
            string URL_PREFIX = @"C:\_current\development\data\isr\temp\filteredindex\";
            string[] files = Directory.GetFiles(FILTERED_DOCS_DIRECTORY);
            string s;
            for (int i = 0; i < files.Length; i++)
            {
                s = files[i];
                if (s.IndexOf(".cgi") > -1)
                    File.Delete(s);
            }

            Hashtable docs2 = new Hashtable();
            FileStream fs = new FileStream(DOCS_FILE, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            while ((line = r.ReadLine()) != null)
                docs2.Add(line, true);
            r.Close();
            fs.Close();

            Console.WriteLine(docs1.Count);
            Console.WriteLine(docs2.Count);


            IDictionaryEnumerator en = docs1.GetEnumerator();
            while (en.MoveNext())
            {
                if (!docs2.Contains(en.Key.ToString()))
                    Console.WriteLine(en.Key.ToString());

            }

            checkDocCount1(DOCS_FILE);
        }

        public void checkDocCount1(string path)
        {
            int counter = 0;
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            while ((line = r.ReadLine()) != null)
                counter++;
            r.Close();
            fs.Close();
            Console.WriteLine("docs = " + counter);
        }

        public void checkDocCount2()
        {
            FileStream fs = File.OpenRead(@"C:\_current\development\data\isr\giggleindex\1_1_1_1\source\docs.txt");
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            int count = 0;
            while (r.ReadLine() != null)
                count++;
            r.Close();
            fs.Close();
            Console.WriteLine("docs = " + count);
        }

        public void checkNewTermCount()
        {
            int counter;

            counter = 0;
            FileStream fs = new FileStream(@"C:\_current\development\data\isr\temp\newindexresults\sorted.txt", FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            while ((line = r.ReadLine()) != null)
                counter++;
            r.Close();
            fs.Close();
            Console.WriteLine("terms = " + counter);
        }

        public void findTermCollectionDifferences()
        {
            Hashtable newTerms = new Hashtable();
            Hashtable oldTerms = new Hashtable();
            Hashtable newNotOldTerms = new Hashtable();
            Hashtable oldNotNewTerms = new Hashtable();

            FileStream fs = new FileStream(@"C:\_current\development\data\isr\temp\newindexresults\newterms.txt", FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            while ((line = r.ReadLine()) != null)
                newTerms.Add(line.Substring(0, line.IndexOf(" ")), true);
            r.Close();
            fs.Close();

            fs = new FileStream(@"C:\_current\development\data\isr\temp\newindexresults\oldterms.txt", FileMode.Open);
            r = new StreamReader(fs, System.Text.Encoding.ASCII);
            while ((line = r.ReadLine()) != null)
                oldTerms.Add(line, true);
            r.Close();
            fs.Close();

            string term;

            IDictionaryEnumerator en = newTerms.GetEnumerator();
            while (en.MoveNext())
            {
                term = en.Key.ToString();
                if (!oldTerms.Contains(term))
                    newNotOldTerms.Add(term, true);
            }

            en = oldTerms.GetEnumerator();
            while (en.MoveNext())
            {
                term = en.Key.ToString();
                if (!newTerms.Contains(term))
                    oldNotNewTerms.Add(term, true);
            }

            Console.WriteLine("oldNotNewTerms: " + oldNotNewTerms.Count);
            en = oldNotNewTerms.GetEnumerator();
            while (en.MoveNext())
                Console.WriteLine(en.Key.ToString());

            //Console.WriteLine("terms = " + counter);
        }


        public void checkTitleCount()
        {
            int counter;

            counter = 0;
            FileStream fs = new FileStream(@"C:\_current\development\data\isr\temp\source\titles.txt", FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string line;
            while ((line = r.ReadLine()) != null)
                counter++;
            r.Close();
            fs.Close();
            Console.WriteLine("titles = " + counter);
        }

        public void makesamplelinkscount()
        {
            FileStream fs = new FileStream(@"C:\_current\development\data\isr\temp\test\source\doclinkscount.data", FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);

            short links = 1;

            for (short d = 0; d < 10; d++) //in my system docIds are implemented as shorts				
                w.Write(links);

            w.Close();
            fs.Close();
        }

        //public void MakeDocTermCountsFile()
        //{
        //    FileStream fs = File.OpenRead(sourceDirectory + d.Helper.SOURCE_TERMDOCS_FILE);
        //    StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

        //    short[] docTermCounts = new short[DOCUMENTS];

        //    int termId;
        //    short docId;
        //    int space1;
        //    int space2;
        //    string line;
        //    while ((line = r.ReadLine()) != null)
        //    {
        //        space1 = line.IndexOf(" ");
        //        space2 = line.IndexOf(" ", space1 + 1);
        //        termId = Convert.ToInt32(line.Substring(0, space1));
        //        docId = Convert.ToInt16(line.Substring(space1 + 1, space2 - space1 - 1));
        //        docTermCounts[docId] = (short)(docTermCounts[docId]++);
        //    }
        //    r.Close();
        //    fs.Close();
        //}


        public class DocLink : IComparable
		{
			public short fromId;
			public short toId;

			public int CompareTo(object o)
			{
				DocLink dl1 = (DocLink)o;
				if (fromId > dl1.fromId)
					return 1;				
				else if (fromId < dl1.fromId)
					return -1;
				else
				{
					if (toId > dl1.toId)
						return 1;
					else if (toId < dl1.toId)
						return -1;
					else
						return 0;
				}
			}

			public DocLink(short fromId, short toId)
			{
				this.fromId = fromId;
				this.toId = toId;
			}
		}

		//converts hashedlinks into binary using my IDs
		public void convertLinksFile()
		{
			//step 1: load links into hashtable
			d.Index index = new d.Index(d.Helper.INDEX_DIRECTORY_NAME);
			FileStream fs = new FileStream(d.Helper.SOURCE_DIRECTORY_NAME + "hashedLinks.txt", FileMode.Open);
			StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);	
			Hashtable linksHash = new Hashtable();
			string line;			
			while ((line = r.ReadLine()) != null)			
			{
				int endOfUrl = line.IndexOf("-->[");
				int startOfLinks = endOfUrl + 4;
				string url = line.Substring(7, endOfUrl-7);
				int lengthOfLinks = line.Length - 7 - url.Length - 4 - 1;
				string links = line.Substring(startOfLinks, lengthOfLinks);
				char[] del = {','};
				string[] linkUrls = links.Split(del);
				short docId = index.GetDocId(url);
				if (docId == -1) continue;
		
				for (int i=0; i<linkUrls.Length; i++)
				{
					string link = linkUrls[i].Trim();
					if (link.Length > 0)
					{
						short linkId =  index.GetDocId(link);
						if (linkId == -1)
							Console.WriteLine("linkId error: " + link);
						DocLink dl = new DocLink(docId, linkId);
						linksHash.Add(dl, true);
					}
				}
			}			
			r.Close();
			fs.Close();

			//step 2: load into array and sort them
			DocLink[] docLinkArray = new DocLink[linksHash.Count];			
			IDictionaryEnumerator en = linksHash.GetEnumerator();
			int cursor = 0;
			while (en.MoveNext())			
				docLinkArray[cursor++] = (DocLink)en.Key;

			Array.Sort(docLinkArray);
			

			//step 3: write out to file
			string path = d.Helper.INDEX_DIRECTORY_NAME + d.Helper.INDEX_DOCLINKS_FILE;			
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
			for (int i=0; i<docLinkArray.Length; i++)
			{
				DocLink dl = docLinkArray[i];
				if (dl == null)
					Console.WriteLine("null");
				w.Write(dl.fromId);
				w.Write(dl.toId);
				wText.WriteLine(dl.fromId + " " + dl.toId);
				counter++;
			}
			w.Close();
			wText.Close();
			fs1.Close();
			fsText.Close();

			Console.WriteLine("total links=" + counter);
		}





		public void GetNewTerms()
		{
			d.Index index = new d.Index(d.Helper.INDEX_DIRECTORY_NAME);
			Hashtable terms = index.TermIds;

			string path = d.Helper.SOURCE_DIRECTORY_NAME + "myterms.txt";			
			FileStream fs = new FileStream(path, FileMode.Open);
			StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
			string line;
			string term;
			Hashtable newTerms = new Hashtable();
			while ( (line = r.ReadLine()) != null)			
			{
				term = line.Substring(0, line.IndexOf(' '));
				if (!terms.Contains(term))				
					newTerms.Add(term, true);				
			}			
			r.Close();
			fs.Close();

			Console.WriteLine("_________________new terms = " + newTerms.Count);
			IDictionaryEnumerator en = newTerms.GetEnumerator();
			while (en.MoveNext())
				Console.WriteLine(en.Key);
		}

		public void GetMissedTerms()
		{
			string path = d.Helper.SOURCE_DIRECTORY_NAME + "myterms.txt";			
			FileStream fs = new FileStream(path, FileMode.Open);
			StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
			string line;
			Hashtable myterms = new Hashtable();
			while ( (line = r.ReadLine()) != null)						
				myterms.Add(line.Substring(0, line.IndexOf(' ')), true);		
			r.Close();
			fs.Close();

			Console.WriteLine("_________________my terms = " + myterms.Count);

			d.Index index = new d.Index(d.Helper.INDEX_DIRECTORY_NAME);
			Hashtable terms = index.TermIds;

			Hashtable missedterms = new Hashtable();

			
			IDictionaryEnumerator en = terms.GetEnumerator();
			while (en.MoveNext())
			{
				if (!myterms.Contains(en.Key))
					missedterms.Add(en.Key, true);
			}

			Console.WriteLine("_________________missed terms = " + missedterms.Count);
			IDictionaryEnumerator en2 = missedterms.GetEnumerator();
			while (en2.MoveNext())
				Console.WriteLine(en2.Key);
		}





		public void TestTextTermsRead()
		{				
			Hashtable terms = new Hashtable();
			string path = d.Helper.INDEX_DIRECTORY_NAME + "terms.txt";			
			FileStream fs = new FileStream(path, FileMode.Open);
			StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
			string line;
			int i = 0;
			while ( (line = r.ReadLine()) != null)			
				terms.Add(line, Convert.ToInt32(i++));				
			r.Close();
			fs.Close();
		}

		public void TestBinaryTermsRead()
		{	
			Hashtable terms = new Hashtable();
			string path = d.Helper.INDEX_DIRECTORY_NAME + "termsbinarytest.data";
			FileStream fs = new FileStream(path, FileMode.Open);
			BinaryReader r = new BinaryReader(fs, System.Text.Encoding.ASCII);	
			Console.WriteLine("l=" + fs.Length);
			string line;
			int i = 0;
			try
			{
				while ( (line = r.ReadString()) != null)
					terms.Add(line, Convert.ToInt32(i++));	
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			r.Close();
			fs.Close();
		}

		public void testReadBytes()
		{
			string path = d.Helper.INDEX_DIRECTORY_NAME + "testbytes.data";
			if (File.Exists(path))
				File.Delete(path);

			FileStream fs1 = new FileStream(path, FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs1);
			short a = 1;
			float b = 2f;
			w.Write(a);
			w.Write(b);
			w.Write(a+1);
			w.Write(b+1);
			w.Close();
			fs1.Close();

			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + "testbytes.data", FileMode.Open);
			BinaryReader r = new BinaryReader(fs);
			byte[] test = r.ReadBytes(Convert.ToInt32(fs.Length));

			for(int i=0; i<test.Length; i++)
				Console.WriteLine(test[i].ToString());

			Console.WriteLine("length=" + test.Length);
			r.Close();
			fs.Close();

		}

		public void testTermData()
		{					
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + "temp1.data", FileMode.Open);
			BinaryReader r = new BinaryReader(fs);	
			long length = fs.Length;			
			int count = 0;
			for (long i=0; i<length; i+=8) //advance counter by 6 bytes			
			{
				r.ReadInt16();	//reads 2 bytes			
				r.ReadSingle(); //read 4 bytes
				//count++;
				//Console.WriteLine(count);
			}
			Console.WriteLine(length/8);
			r.Close();
			fs.Close();
		}

		public void CalculateIDF()
		{
			FileStream fs1 = File.OpenRead(d.Helper.INDEX_DIRECTORY_NAME + "backupterms.data");
			BinaryReader r = new BinaryReader(fs1);	
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + d.Helper.INDEX_TERMSDATA_FILE, FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs);

			int docCount = 25;

			float idf;
			short docFreq;
			long length = fs1.Length;	
			int count = 0;
			for (long i=0; i<length; i+=2) //advance counter by 2 bytes			
			{
				docFreq = r.ReadInt16();	//reads 2 bytes			
				idf = Convert.ToSingle(Math.Log(docCount/docFreq));
				w.Write(docFreq);
				w.Write(idf);
				count++;
			}
			r.Close();
			w.Close();
			fs.Close();
			fs1.Close();
			Console.WriteLine(count);
		}

		public void examineTerms()
		{
			FileStream fs1 = File.OpenRead(d.Helper.INDEX_DIRECTORY_NAME + "terms.txt");
			StreamReader r = new StreamReader(fs1, System.Text.Encoding.ASCII);
			
			string line;			
			int c = 0;			
			while ( (line = r.ReadLine()) != null)
			{
				if (!isLettersOnly(line)) 				
					c++;
			}			
			r.Close();
			fs1.Close();
			Console.WriteLine(">not letter only = " + c);
		}



		public void convertTermsAndFreqToFreqData()
		{
			FileStream fs1 = File.OpenRead(d.Helper.INDEX_DIRECTORY_NAME + "termsAndFreq.txt");
			StreamReader r = new StreamReader(fs1, System.Text.Encoding.ASCII);
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + "termfreqs.data", FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs);

			int space;
			short freq; //number of docs the term occurs in
			string line;			
			while ( (line = r.ReadLine()) != null)
			{
				space = line.IndexOf(" ");		
				freq = Convert.ToInt16(line.Substring(space + 1));
				w.Write(freq);
			}			
			r.Close();
			fs1.Close();
			w.Close();
			fs.Close();
			Console.WriteLine("data written");
		}

		public void convertTermsAndFreqToData()
		{
			FileStream fs1 = File.OpenRead(d.Helper.INDEX_DIRECTORY_NAME + "termsAndFreq.txt");
			StreamReader r = new StreamReader(fs1, System.Text.Encoding.ASCII);
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + "termsAndFreq.data", FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs);

			int space;
			short freq; //number of docs the term occurs in
			string line;			
			string term;
			while ( (line = r.ReadLine()) != null)
			{
				space = line.IndexOf(" ");		
				term = line.Substring(0, space);
				freq = Convert.ToInt16(line.Substring(space + 1));
				w.Write(term);
				w.Write(freq);
			}			
			r.Close();
			fs1.Close();
			w.Close();
			fs.Close();
			Console.WriteLine("data written");
		}

		public void convertTermsAndFreqToTerms()
		{
			FileStream fs1 = File.OpenRead(d.Helper.INDEX_DIRECTORY_NAME + "termsAndFreq.txt");
			StreamReader r = new StreamReader(fs1, System.Text.Encoding.ASCII);
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + d.Helper.INDEX_TERMS_FILE, FileMode.CreateNew);
			StreamWriter w = new StreamWriter(fs);

			int space;
			int freq;
			string line;			
			while ( (line = r.ReadLine()) != null)
			{
				space = line.IndexOf(" ");		
				w.WriteLine(line.Substring(0, space));
			}			
			r.Close();
			fs1.Close();
			w.Close();
			fs.Close();
			Console.WriteLine("data written");
		}

		public void countData()
		{
			int counter;
				
			counter = 0;
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + d.Helper.INDEX_DOCS_FILE, FileMode.Open);
			StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);			
			string line;			
			while ((line = r.ReadLine()) != null)
				counter++;				
			r.Close();
			fs.Close();
			Console.WriteLine("docs = " + counter);

			counter = 0;
			fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + d.Helper.INDEX_TERMS_FILE, FileMode.Open);
			r = new StreamReader(fs, System.Text.Encoding.ASCII);									
			while ((line = r.ReadLine()) != null)
				counter++;				
			r.Close();
			fs.Close();
			Console.WriteLine("terms = " + counter);
		}

		public void TestRead()
		{
			DateTime time1;
			DateTime time2;
			TimeSpan diff ;

			time1 = DateTime.Now;
			for (int i=0; i<3; i++)
				testBinaryTermDocsRead2();
			time2 = DateTime.Now;
			diff = time2 - time1;
			Console.WriteLine("BinaryReader = " + diff.Seconds + "." + diff.Milliseconds);

			time1 = DateTime.Now;
			for (int i=0; i<3; i++)
				testBinaryTermDocsRead1();
			time2 = DateTime.Now;
			diff = time2 - time1;
			Console.WriteLine("FileStream = " + diff.Seconds + "." + diff.Milliseconds);           
		}

		private void testBinaryTermDocsRead1()
		{
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + "termdocs.data", FileMode.Open);

			byte[] arrTermId = new byte[4];
			byte[] arrDocId = new byte[2];
			byte[] arrFreq = new byte[2];
			int termId;
			int docId;
			int freq;			
			
			long length = fs.Length;
			for (long i=0; i<length; i+=8) 
			{
				fs.Read(arrTermId, 0, 4);
				fs.Read(arrDocId, 0, 2);
				fs.Read(arrFreq, 0, 2);

				termId = BitConverter.ToInt32(arrTermId, 0);
				docId = BitConverter.ToInt16(arrDocId, 0);
				freq = BitConverter.ToInt16(arrFreq, 0);
			}
			fs.Close();			
		}

		private void testBinaryTermDocsRead2()
		{
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + "termdocs.data", FileMode.Open);
			BinaryReader r = new BinaryReader(fs);

			int termId;
			int docId;
			int freq;			
			
			long length = fs.Length;
			for (long i=0; i<length; i+=8) 
			{
				termId = r.ReadInt32();
				docId = r.ReadInt16();
				freq = r.ReadInt16();
			}
			r.Close();
			fs.Close();			
		}

		private void testBinaryTermDocsRead3()
		{
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + "termdocs.data", FileMode.Open);
			BinaryReader r = new BinaryReader(fs);

			int termId;
			int docId;
			int freq;			
			
			long length = fs.Length;
			for (long i=0; i<length; i+=8) 
			{
				termId = r.ReadInt32();
				docId = r.ReadInt16();
				freq = r.ReadInt16();
			}
			r.Close();
			fs.Close();			
		}

		private void testBinaryRead()
		{
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + "docs.data", FileMode.Open, FileAccess.Read);
			StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

			string line;			
			while ( (line = r.ReadLine()) != null)
			{
				
			}		
			r.Close();
			fs.Close();
		}

		private void testTextRead1()
		{
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + "docs.txt", FileMode.Open, FileAccess.Read);
			StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

			string line;			
			while ( (line = r.ReadLine()) != null)
			{
				
			}		
			r.Close();
			fs.Close();
		}

		private void testTextRead2()
		{			
			StreamReader r = new StreamReader(d.Helper.INDEX_DIRECTORY_NAME + "docs.txt");

			string line;			
			while ( (line = r.ReadLine()) != null)
			{
				
			}		
			r.Close();
		}


		public void convertDocsToBinary()
		{
			FileStream fs1 = File.OpenRead(d.Helper.INDEX_DIRECTORY_NAME + "docs.txt");
			StreamReader sr = new StreamReader(fs1, System.Text.Encoding.ASCII);
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + "docs.data", FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs);

			string line;			
			while ( (line = sr.ReadLine()) != null)
			{
				w.Write(line);
			}			
			sr.Close();
			fs1.Close();
			w.Close();
			fs.Close();
			Console.WriteLine("data written");
		}

		public void convertTermDocsToBinary()
		{
			FileStream fs1 = File.OpenRead(d.Helper.INDEX_DIRECTORY_NAME + d.Helper.INDEX_TERMDOCS_FILE);
			StreamReader sr = new StreamReader(fs1, System.Text.Encoding.ASCII);
			FileStream fs = new FileStream(d.Helper.INDEX_DIRECTORY_NAME + "termdocs.data", FileMode.CreateNew);
			BinaryWriter w = new BinaryWriter(fs);

			string line;
			int termId;
			short docId;
			short docFreq;
			string[] numbers;
			char[] delims = {' '};
			
			while ( (line = sr.ReadLine()) != null)
			{
				numbers = line.Split(delims);
				termId = Convert.ToInt32(numbers[0]);
				docId = Convert.ToInt16(numbers[1]);
				docFreq = Convert.ToInt16(numbers[2]);					
				w.Write(termId);
				w.Write(docId);
				w.Write(docFreq);
			}			
			sr.Close();
			fs1.Close();
			w.Close();
			fs.Close();
			Console.WriteLine("data written");
		}

		public void CleanTermsFile()
		{
			FileStream fs = File.OpenRead(d.Helper.INDEX_DIRECTORY_NAME + d.Helper.INDEX_TERMS_FILE);
			StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF7);
			StreamWriter sw = new StreamWriter(d.Helper.INDEX_DIRECTORY_NAME + "cleanedterms.txt", false, System.Text.Encoding.ASCII);

			string line;
			String term;
			int space;
			while ( (line = sr.ReadLine()) != null)			
			{
				space = line.IndexOf(" ");
				term = line.Substring(0, space);
				if (isAscii(term))
					sw.WriteLine(line);
			}
		}

		public void CountNonAsciiTerms()
		{
			FileStream fs = File.OpenRead(d.Helper.INDEX_DIRECTORY_NAME + d.Helper.INDEX_TERMS_FILE);
			StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF7);
			string line;
			String term;
			int nonascii = 0;
			int ascii = 0;
			int space;
			while ( (line = sr.ReadLine()) != null)			
			{
				space = line.IndexOf(" ");
				term = line.Substring(0, space);
				if (isAscii(term))
					ascii++;
				else
					nonascii++;
			}
			Console.WriteLine("ascii terms = " + ascii);
			Console.WriteLine("nonascii terms = " + nonascii);
		}

		private bool isAscii(string s)
		{
			for (int i=0; i<s.Length; i++)
			{
				int x = Convert.ToInt32(s[i]);
				if (x < 33 || x > 126)
					return false;
			}
			return true;
		}

		private bool isLettersOnly(string s)
		{
			for (int i=0; i<s.Length; i++)
			{
				if (!Char.IsLetter(s[i]))
					return false;
			}
			return true;
		}
    }
}
