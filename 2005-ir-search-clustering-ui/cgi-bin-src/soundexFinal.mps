+#include <stdlib.h>
+#include <string.h>

+	char * Lower(SV * SvPtr, char * ep, char *str) 
+	{
+		char * p1;
+		for (p1=str; *str!=0; str++) *str=tolower(*str);
+		return p1;
+	}

+	char* removeDups(SV * SvPtr, char * ep, char * str)
+	{
+		static char output[10000];
+		strcpy(output, "");
+		int current=0;
+
+		for(int i=0;i<=strlen(str);i++)
+		{
+			output[current]=str[i];
+			if(str[i]==str[i+1])
+				i++;
+			current++;		
+		}
+		return output;
+	}

	zmain

	kill ^soundex

	set word=""

# Start with these characters...
	set alphabet="abcdefghijklmnopqrstuvwxyz"

# And translate them to these numbers.
	set trans="01230120022455012623010202"  

	for  do
	. set word=$order(^index(word))
	. if word="" break

# Convert to upper case, retain the first letter, and convert all other
# characters to "trans".  Finally, remove all 0's.

	. set temp=$$^^Lower(word)
	. set sES=$extract(temp,1)
	. set sEE=$extract(temp,2,$len(temp))
	. set sEE=$translate(sEE,alphabet,trans)
	. set sEntry=$translate(sES_sEE,".0",".")
	. set sEntry=$$^^removeDups(sEntry)

# Place the temp in at the appropriate ^soundex location.

	. set ^soundex(sEntry,temp)=""
