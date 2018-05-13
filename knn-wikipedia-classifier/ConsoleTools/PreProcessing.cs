using System;
using System.Text;
using System.Collections;
using System.IO;
using d = DataMining.Data;

namespace DataMining.ConsoleTools
{
    public class PreProcessing
    {
        private d.CatsLoader catsLoader = new d.CatsLoader();
        private d.DocsLoader docsLoader = new d.DocsLoader();

        private string[] docTitles; //pos=docId, value=title
        private int[] docIds; //pos=docid, value=external Id 
        private Hashtable docIdsHash; //key=external Id, val=docId

        public PreProcessing()
        {
            catsLoader = new d.CatsLoader();
            docsLoader = new d.DocsLoader();

            //docTitles = new string[Helper.NUMBER_OF_DOCS];
            //docIds = new int[Helper.NUMBER_OF_DOCS];
            //loadTitlesAndIds();
            //Console.WriteLine("docIdsHash.count=" + docIdsHash.Count);
        }

        public void ParseCatLinks()
        {
            FileStream fs = new FileStream(Helper.CATLINKS_ORIGINALS_PATH, FileMode.Open);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            ArrayList docCats = new ArrayList(); //relationships

            while (true)
                if (r.ReadLine().IndexOf("LOCK TABLES") > -1)
                    break;


            StringBuilder sb = new StringBuilder();
            string cat;
            string externalDocId;
            int docId = -1;
            int catId = -1;
            bool addDocCat = false; //true only if docId is part of my list of docIds
            int docCounter = 0;

            ArrayList arrCats = new ArrayList(); //accumulates cats

            /* 0 : start state; loop until '(' and go to 2
			 * 1 : read '(' + go to 2, or fail
			 * 2 : read '0-9' + add to sb + go to 3, or fail
			 * 3 : read '0-9' + add to sb, or read ',' + process docId from sb + go to 4, or fail
			 * 4 : read ''' + go to 5, or fail
			 * 5 : read ''' + process cat from sb + go to 6, or read anythign else + add to sb
			 * 6 : read ',' + go to 7, or fail
			 * 7 : read ''' + go to 8, or fail
			 * 8 : read ''' + go to 9, or read anything else
			 * 9 : read ',' + go to 10, or fail
			 * 10: read ')' + go to 11, or read anything
			 * 11: read ',' + go to 1, or read ';' + go to 0, or fail
			 * 12: final state; exit.
			 * */
            int state = 0;

            char c;
            while (true)
            {
                if (state == 12)
                    break;

                c = (char)r.Read();
                if (c == '\\') //ignore characters which follow escapes!
                {
                    r.Read();
                    c = (char)r.Read();
                }

                switch (state)
                {
                    case 0:
                        {
                            if (c == '(')
                                state = 2;
                            break;
                        }
                    case 1:
                        {
                            if (c == '(')
                                state = 2;
                            else
                                throw new Exception("error in state 1");
                            break;
                        }
                    case 2:
                        {
                            if (Char.IsDigit(c))
                            {
                                sb.Append(c);
                                state = 3;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (Char.IsDigit(c))
                                sb.Append(c);
                            else if (c == ',')
                            {
                                externalDocId = sb.ToString();
                                if (docsLoader.HasDoc(externalDocId)) //check if i have this docId in my collection. If so - add relationship
                                {
                                    docId = docsLoader.GetDocId(externalDocId);
                                    addDocCat = true;
                                    docCounter++;
                                }
                                sb.Remove(0, sb.Length);
                                state = 4;

                                if (docCounter > Helper.MAX_EXTERNAL_DOCID)
                                    state = 12;

                                if (docCounter % 100 == 0)
                                    Console.WriteLine("processing doc #" + docCounter);
                            }
                            else
                                throw new Exception("error in state 3: " + c);
                            break;
                        }
                    case 4:
                        {
                            if (c == '\'')
                                state = 5;
                            else
                                throw new Exception("error in state 4");
                            break;
                        }
                    case 5:
                        {
                            if (c == '\'')
                            {
                                if (addDocCat) //add this relationship only addDocCat is set to true, i.e. this doc is part of my doc collection
                                {
                                    cat = sb.ToString();
                                    if (!catsLoader.HasCategory(cat))
                                        throw new Exception("cat not found: " + cat);
                                    catId = catsLoader.GetCategoryId(cat);

                                    docCats.Add(new DocCat(docId, catId));
                                    addDocCat = false;
                                }
                                sb.Remove(0, sb.Length);
                                state = 6;
                            }
                            else
                                sb.Append(c);
                            break;
                        }
                    case 6:
                        {
                            if (c == ',')
                                state = 7;
                            else
                                throw new Exception("error in state 6 " + c);
                            break;
                        }
                    case 7:
                        {
                            if (c == '\'')
                                state = 8;
                            else
                                throw new Exception("error in state 7");
                            break;
                        }
                    case 8:
                        {
                            if (c == '\'')
                                state = 9;
                            break;
                        }
                    case 9:
                        {
                            if (c == ',')
                                state = 10;
                            else
                                throw new Exception("error in state 9");
                            break;
                        }
                    case 10:
                        {
                            if (c == ')')
                                state = 11;
                            break;
                        }
                    case 11:
                        {
                            if (c == ',')
                                state = 1;
                            else if (c == ';')
                                state = 0;
                            else
                                throw new Exception("error in state 11 " + c);
                            break;
                        }
                }
            }

            Console.WriteLine("cats: " + docCats.Count);

            r.Close();
            fs.Close();

            Console.WriteLine("new count=" + docCats.Count);
            createCats();
            createDocCats(docCats);
        }

        private void createCats()
        {
            string path = Helper.TEMPINDEX_PATH + d.Helper.CATS_FILE;
            if (File.Exists(path))
                File.Delete(path);

            StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
            IDictionaryEnumerator en = categories.GetEnumerator();
            while (en.MoveNext())
                w.WriteLine(en.Key.ToString());

            w.Close();
            Console.WriteLine("created " + d.Helper.CATS_FILE);
        }

        private void createDocCats(ArrayList docCats)
        {
            string path = Helper.INDEX_PATH + d.Helper.DOCCATS_FILE;
            if (File.Exists(path))
                File.Delete(path);

            string pathText = path + ".txt";
            if (File.Exists(pathText))
                File.Delete(pathText);

            FileStream fs = new FileStream(path, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);
            StreamWriter wText = new StreamWriter(pathText, false, System.Text.Encoding.ASCII);

            foreach (DocCat dc in docCats)
            {
                w.Write(dc.docId);
                w.Write(dc.catId);
                wText.WriteLine(dc.docId + " " + dc.catId);
            }

            w.Close();
            wText.Close();
            fs.Close();
            Console.WriteLine("created " + d.Helper.DOCCATS_FILE);
        }


        //		public void CreateIdsDoc()
        //		{
        //			string path = Helper.TEMPINDEX_PATH + d.Helper.DOCIDS_FILE;			
        //			if (File.Exists(path))
        //				File.Delete(path);
        //
        //			StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
        //			for (int i=0; i<docTitles.Length; i++)			
        //				w.WriteLine(docIds[i]);
        //			
        //			w.Close();
        //			Console.WriteLine("created " + d.Helper.DOCIDS_FILE);
        //		}
        //
        //		public void CreateTitlesDoc()
        //		{
        //			string path = Helper.TEMPINDEX_PATH + d.Helper.TITLES_FILE;			
        //			if (File.Exists(path))
        //				File.Delete(path);
        //
        //			StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.ASCII);
        //			for (int i=0; i<docTitles.Length; i++)			
        //				w.WriteLine(docTitles[i]);
        //			
        //			w.Close();
        //			Console.WriteLine("created " + d.Helper.TITLES_FILE);
        //		}

        //		public void WritePartOfFile()
        //		{
        //			FileStream fs = new FileStream(Helper.CATLINKS_ORIGINALS_PATH, FileMode.Open);
        //			StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
        //			
        //			StringBuilder sb = new StringBuilder();
        //			char c;
        //			for (int i=2000000; i>0; i--)
        //			{
        //				c = (char)r.Read();
        //				sb.Append(c);
        //			}
        //			Console.WriteLine(sb.ToString());
        //		}

        //		private void loadTitlesAndIds()
        //		{			
        //			docIdsHash = new Hashtable();
        //
        //			FileStream fs = new FileStream(Helper.ORIGINALS_PATH + "log.txt", FileMode.Open);
        //			StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
        //			string line;
        //			int i = 0;			
        //			while ( (line = r.ReadLine()) != null)			
        //			{
        //				int space1 = line.IndexOf(" (id=");
        //				int space2 = line.IndexOf(" contributor=");
        //				string title = line.Substring(0, space1);
        //				string id = line.Substring(space1+5, space2-space1-5);
        //				int docId = Convert.ToInt32(id);
        //				docTitles[i] = title;
        //				docIds[i] = docId;
        //				docIdsHash.Add(docId, i);
        //				i++;
        //			}				
        //			r.Close();
        //			fs.Close();			
        //		}

        private class DocCat
        {
            public int docId;
            public int catId;

            public DocCat(int docId, int catId)
            {
                this.docId = docId;
                this.catId = catId;
            }
        }
    }
}
