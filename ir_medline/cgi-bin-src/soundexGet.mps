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

# Start with these characters...
	set alphabet="abcdefghijklmnopqrstuvwxyz"

# And translate them to these numbers.
	set trans="01230120022455012623010202"

	write "Please enter a term: "
	read input

	write !,!,!,"You searched for: ",input,!
	write "Possible matches: "

	set temp=$$^^Lower(input)
	set sES=$extract(temp,1)
	set sEE=$extract(temp,2,$len(temp))
	set sEE=$translate(sEE,alphabet,trans)
	set sEntry=$translate(sES_sEE,".0",".")
	set sEntry=$$^^removeDups(sEntry)

	set test=""

	if $data(^soundex(sEntry)) do
	. for  do
	.. set test=$order(^soundex(sEntry,test))
	.. if test="" break
	.. write test," "