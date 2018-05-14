using System;
using System.IO;
using System.Collections;
using System.Text;
using d = IrProject.Data;

namespace IrProject.Indexing
{
	//count for exach document, then update term and termdoc
	public class TermParser
	{
		private Hashtable termCollectors;
	
		private FileStream fs;
		private StreamReader r;
		private StringBuilder sbText;							//*
		private StringBuilder sbBoldText;					//<b>*</b>
		private StringBuilder sbHeadingText;			//<h[1-6>*</h[1-6]>
		private StringBuilder sbAnchorText;				//<a[...]>*</a>
		private StringBuilder sbTitleText;				//<t>*</t>
		private StringBuilder sbUrlText;					//<u>*</u>
		
		private	d.StopList stopList;
		private char[] delims;
		private Stack stack; //holds the current text type
		private string[] textTerms;	
		private string term;

		private PorterStemmer stemmer;
		private d.TermData termData;
		private d.DocData DocData;
		private d.TermDocData termdocData;

		private ParseHelper parseHelper;

		public TermParser()
		{
			stopList = new d.StopList();
			delims = parseHelper.GetDelims();	
			stemmer = new PorterStemmer();
			termData = new d.TermData();
			DocData = new d.DocData();
			termdocData = new d.TermDocData();
			parseHelper = new ParseHelper();
		}

		public void ExtractTerms()
		{			
			string[] files = Directory.GetFiles(Helper.DOCS_PATH);			
			for (int i=0; i<files.Length; i++)
			{		
				Console.WriteLine(i + " " + files[i]);	

				termCollectors = new Hashtable();
				parseFile(files[i]);
				writeTermsToDatabase(getDocId(files[i]));						
			}			
		}

		private void writeTermsToDatabase(int docId)
		{	
			DocData.UpdateTermCount(docId, termCollectors.Count);		

			IDictionaryEnumerator en = termCollectors.GetEnumerator();
			string term;
			TermCollector tc;
			while (en.MoveNext())
			{
				term = en.Key.ToString();		
				tc = (TermCollector)termCollectors[term];				
				termData.UpdateCounts(term, tc.GetTotalCount());
				termdocData.Create(
					term, docId, 
					tc.GetCount(TermType.TEXT), tc.GetCount(TermType.BOLD_TEXT), tc.GetCount(TermType.HEADING_TEXT),
					tc.GetCount(TermType.ANCHOR_TEXT), tc.GetCount(TermType.TITLE_TEXT), tc.GetCount(TermType.URL_TEXT), tc.GetTotalCount());
			}

		}

		private int getDocId(string path)
		{
			FileInfo fi = new FileInfo(path);
			string name = fi.Name;
			name = name.Substring(0, name.IndexOf("."));			
			return Convert.ToInt32(name);
		}

		private void parseFile(string path)
		{
			sbText = new StringBuilder();
			sbBoldText = new StringBuilder();
			sbHeadingText = new StringBuilder();
			sbTitleText = new StringBuilder();
			sbUrlText = new StringBuilder();
			sbAnchorText = new StringBuilder();
			
			fs = File.OpenRead(path);
			r = new StreamReader(fs, System.Text.Encoding.ASCII);

			/*
				* I only care what the first letter is. 
					I am not checking for tag correcness - that has been checked in previous projects with this code.					
						 * state 0: accumulate text: (start AND final state) loop and accumulate text until '<' -> 1
						 * state 1: determine text type and -> 2
						 * state 2: find end of tag: loop until '>' -> 0
			*/

			stack = new Stack();
			int state = 0;
			stack.Push(TermType.TEXT);

			int i;
			char c;
			int textType;
			while( (i = r.Read()) > -1)
			{
				c = (char)i;						
				switch(state)
				{					
					case 0:
					{
						if (c == '<')				
						{
							state = 1;		
							//append delimeter
							sbText.Append(" ");		
							sbBoldText.Append(" ");		
							sbHeadingText.Append(" ");		
							sbAnchorText.Append(" ");		
							sbTitleText.Append(" ");		
							sbUrlText.Append(" ");		
						}
						else
						{			
							textType = Convert.ToInt32(stack.Peek());
							if (textType == TermType.BOLD_TEXT) 
								sbBoldText.Append(c);
							else if (textType == TermType.HEADING_TEXT) 
								sbHeadingText.Append(c);
							else if (textType == TermType.ANCHOR_TEXT) 
								sbAnchorText.Append(c);
							else if (textType == TermType.TITLE_TEXT) 
								sbTitleText.Append(c);
							else if (textType == TermType.URL_TEXT) 
								sbUrlText.Append(c);
							else 
								sbText.Append(c);
						}
						break;
					}	
					case 1:
					{
						if (c == 'b')				
							stack.Push(TermType.BOLD_TEXT);			//all text within this tag is bold							
						else if (c == 'h')								
							stack.Push(TermType.HEADING_TEXT);		//all text within this tag is a heading		
						else if (c == 'a')												
							stack.Push(TermType.ANCHOR_TEXT);		//all text within this tag is anchor text		
						else if (c == 't')												
							stack.Push(TermType.TITLE_TEXT);			//all text within this tag is a title
						else if (c == 'u')												
							stack.Push(TermType.URL_TEXT);				//all text within this tag is a url		
						else if (c == '/')						
						{
							if (stack.Count > 1) //guards against situations like '</a></a>': always keep TEXT in the stack.
								stack.Pop();								//end of current text type
						}
						else
							throw new Exception("error in 1: " + c);

						state = 2;
						break;
					}
					case 2:
					{
						if (c == '>')									
							state = 0;
						break;
					}
				}
			}			
			r.Close();
			fs.Close();	
			
			accumTerms(sbText.ToString(), TermType.TEXT);
			accumTerms(sbBoldText.ToString(), TermType.BOLD_TEXT);
			accumTerms(sbHeadingText.ToString(), TermType.HEADING_TEXT);
			accumTerms(sbAnchorText.ToString(), TermType.ANCHOR_TEXT);
			accumTerms(sbTitleText.ToString(), TermType.TITLE_TEXT);
			accumTerms(sbUrlText.ToString(), TermType.URL_TEXT);

			//cleanup everything
			sbText = null;
			sbBoldText = null;
			sbHeadingText = null;
			sbTitleText = null;
			sbUrlText = null;
			sbAnchorText = null;

			return;
		}

		private void accumTerms(string input, int termType)
		{
			textTerms = input.Split(delims);							
			for (int i=0; i<textTerms.Length; i++)
			{
				term = stemmer.stemTerm(textTerms[i].ToLower().Trim());
				if (term.Length > 0 && term.Length < 25 && !stopList.Contains(term) && parseHelper.IsAsciiLetters(term)) //only allow terms < 25 chars
				{
					TermCollector tc;
					if (termCollectors.Contains(term))					
						tc = (TermCollector)termCollectors[term];
					else
					{
						tc = new TermCollector(term);						
						termCollectors.Add(term, tc);
					}
					tc.IncrementCount(termType);
				}
			}
			return;
		}
	}
}
