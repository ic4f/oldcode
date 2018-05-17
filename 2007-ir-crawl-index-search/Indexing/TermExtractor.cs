using System;
using System.Collections;
using System.IO;
using System.Text;
using d = IrProject.Data;

namespace IrProject.Indexing
{
	public class TermExtractor
	{
		private Hashtable termCollectors;
		private	d.StopList stopList;
		private PorterStemmer stemmer;
		private ParseHelper parseHelper;
		private char[] delims;

		public TermExtractor()
		{
			termCollectors = new Hashtable();
			stopList = new d.StopList();
			stemmer = new PorterStemmer();
			parseHelper = new ParseHelper();
			delims = parseHelper.GetDelims();	
		}

		public Hashtable ExtractTerms(string text)
		{
			StringBuilder sbText = new StringBuilder();
			StringBuilder sbBoldText = new StringBuilder();
			StringBuilder sbHeadingText = new StringBuilder();
			StringBuilder sbTitleText = new StringBuilder();
			StringBuilder sbUrlText = new StringBuilder();
			StringBuilder sbAnchorText = new StringBuilder();

			/*
				* I only care what the first letter is. 
					I am not checking for tag correcness - that has been checked in previous projects with this code.					
						 * state 0: accumulate text: (start AND final state) loop and accumulate text until '<' -> 1
						 * state 1: determine text type and -> 2
						 * state 2: find end of tag: loop until '>' -> 0
			*/
			StringReader r = new StringReader(text);

			Stack stack = new Stack();
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
			
			accumTerms(sbText.ToString(), TermType.TEXT);
			accumTerms(sbBoldText.ToString(), TermType.BOLD_TEXT);
			accumTerms(sbHeadingText.ToString(), TermType.HEADING_TEXT);
			accumTerms(sbAnchorText.ToString(), TermType.ANCHOR_TEXT);
			accumTerms(sbTitleText.ToString(), TermType.TITLE_TEXT);
			accumTerms(sbUrlText.ToString(), TermType.URL_TEXT);

			return termCollectors;
		}

		private void accumTerms(string input, int termType)
		{
			string term;
			string[] textTerms = input.Split(delims);							
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
		}
	}
}
