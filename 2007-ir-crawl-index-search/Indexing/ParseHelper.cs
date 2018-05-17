using System;

namespace IrProject.Indexing
{
	public class ParseHelper
	{
		public ParseHelper()
		{
		}

		public char[] GetDelims()
		{
			char[] d = new char[47];
			d[0] = '`';
			d[1] = '1';
			d[2] = '2';
			d[3] = '3';
			d[4] = '4';
			d[5] = '5';
			d[6] = '6';
			d[7] = '7';
			d[8] = '8';
			d[9] = '9';
			d[10] = '0';
			d[11] = '-';
			d[12] = '=';
			d[13] = '[';
			d[14] = ']';
			d[15] = ';';
			d[16] = '\'';
			d[17] = ',';
			d[18] = '.';
			d[19] = '/';
			d[20] = '\\';
			d[21] = '~';
			d[22] = '!';
			d[23] = '#';
			d[24] = '$';
			d[5] = '%';
			d[26] = '^';
			d[27] = '&';
			d[28] = '*';
			d[29] = '(';
			d[30] = ')';
			d[31] = '_';
			d[32] = '+';
			d[33] = '{';
			d[34] = '}';
			d[35] = ':';
			d[36] = '"';
			d[37] = '<';
			d[38] = '>';
			d[39] = '?';
			d[40] = '|';
			d[41] = ' ';
			d[42] = '\t';
			d[43] = '\r';
			d[44] = '\v';
			d[45] = '\f';
			d[46] = '\n';
			return d;
		}

		public bool IsAsciiLetters(string s) //ignore non-letter terms
		{
			for (int i=0; i<s.Length; i++)			
				if (!Char.IsLetter(s, i))
					return false;			
			return true;
		}
	}
}
