+	#include <stdio.h> 
+	#include <stdlib.h>
+	#include <string.h>
+	#define STR_LEN 10000

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

+	char* getData() //get all posted data
+	{
+		static char inputtext[STR_LEN+1];
+		char* temp = getenv("CONTENT_LENGTH");	
+		if (temp != NULL)
+		{
+			int inputlen = atoi(temp);
+			fread(inputtext, 1, inputlen, stdin);
+	
+			//replace "%40" with "@" - neccessary for firefox
+			static char outputtext[STR_LEN+3];
+			strcpy(outputtext, "");
+   			int current=0;
+        		for(int i=0;i<strlen(inputtext);i++)
+    			{
+        			if(inputtext[i]=='%' && inputtext[i+1]=='4' && inputtext[i+2]=='0')
+        			{
+            			strcat(outputtext,"@");
+            			i+=2;
+        			}
+        			else
+            			outputtext[current]=inputtext[i];
+            		current++;
+    			}
+			return outputtext;
+		}
+		else
+			return "";
+	}
+


^writeTab(mst,act,dv,txt)
	html <td><image src="../images/tableft&~mst~&~act~.gif" width=9 height=26></td> &!
	html <td background="../images/tabcenter&~act~.gif">
	html <a href="javascript:doPostBack('displayview','&~dv~')" class="tablink">&~txt~</a></td> &!
	html <td><image src="../images/tabright&~act~.gif" width=9 height=26></td> &!
	quit


	zmain

+    	global doc("doc");     // makes the ^doc() array known to MDH fcns.
+    	global index("index"); // makes the ^index() array known to MDH fcns.
+    	global query("query");
+    	mstring i("i");  // the ("i") is required for mumps linkage
+    	mstring c("c");
+	global dd("dd");
+	mstring d1("d1");
+	mstring d2("d2");

	html Content-type: text/html &!&!

+	mstring alldata("alldata");	//value of all data
+	alldata=getData();


#--------------------------------------------------------------
# Event targets: 
#	displayview 	-> switch to new view (arg: 0,1,2,3,4)
#	viewdoc 	-> display new doc in doc view (arg: doc#)
#	searchquery	-> exec query on term(s) (arg: term(s))
#	paging		-> change currPage, recalc pagingStart & pagingEnd
#	browsing	-> select char to browse
#	browseterm	-> select term within char
#
#--------------------------------------------------------------
# init variables
# views: 0 - browse, 1 - categories,  2 - list, 3 - doc view, 4 - history
#
# keyvals - key/value pairs of all form input fields
# skeyvals - key/value pairs from the parsed state hidden field
#--------------------------------------------------------------
	set searchClicked="0"			;flag depends on main submit button pressed
	set qfield="query"			;name of query textbox
	set qval=""				;value of 'query' textbox
	set displayview="0"			;default view to display	
	set etarget=""				;value of event target hidden field
	set earg=""				;value of event argument hidden field
	set stateval=""				;value of state hidden field
	set currDoc=""				;number of doc displayed in doc view
	set searchTime=""			;search time for query
	set soundexTerms=""			;possible matches
	set browseChar=""			;browse terms beginnign with this character
	set browseTerm=""			;browse term correlations

	set currPage="1"			;current page (starts with 1)
	set pageSize="20"			;page size
	set resultSize="0"			;total results
	set pagingStart="1"			;first displayed record
	set pagingEnd=pageSize			;last displayed record
	set totalPages="0"			;total pages (recalc only when loading results!)

	
#---------------------------------------------------------------
# build keyvals to hold key/value pairs from posted data
#--------------------------------------------------------------
	set kv=""
	for x=1:1 do
	. set kv=$piece(alldata,"&",x) if kv="" break
	. set key=$piece(kv,"=",1)
	. set val=$piece(kv,"=",2)
	. set val=$translate(val,"+"," ")  ;convert '+' to ' '
	. set keyvals(key)=val


#---------------------------------------------------------------
# build skeyvals to hold key/vals pairs from the hidden state field
# input format: key1@val1@@key2@val2 - i.e. @ = "=" and @@ = "&"
#---------------------------------------------------------------
	if $data(keyvals("state")) do
	. set temp=keyvals("state")
	. set skv=""
	. for x=1:1 do
	.. set skv=$piece(temp,"@@",x) if skv="" break
	.. set key=$piece(skv,"@",1)
	.. set val=$piece(skv,"@",2)
	.. set skeyvals(key)=val


#---------------------------------------------------------------
# load vars from fields and state, process event: load target & arg
#---------------------------------------------------------------
	if $data(keyvals("searchbutton")) set searchClicked="1"
	if $data(keyvals(qfield)) set qval=keyvals(qfield)
	if $data(keyvals("eventtarget")) set etarget=keyvals("eventtarget")
	if $data(keyvals("eventargument")) set earg=keyvals("eventargument")
	if $data(skeyvals("displayview")) set displayview=skeyvals("displayview")
	if $data(skeyvals("resultsize")) set resultSize=skeyvals("resultsize")
	if $data(skeyvals("currdoc")) set currDoc=skeyvals("currdoc")
	if $data(skeyvals("currpage")) set currPage=skeyvals("currpage")
	if $data(skeyvals("totalpages")) set totalPages=skeyvals("totalpages")
	if $data(skeyvals("browsechar")) set browseChar=skeyvals("browsechar")
	if $data(skeyvals("browseterm")) set browseTerm=skeyvals("browseterm")
	


# check if you're feelin high
#	if $data(keyvals("randombutton")) set displayview="3" set etarget="browsing"



# set new browsechar if char clicked + RESET BROWSETERM!!!
	if etarget="browsing" do
	. set browseChar=earg
	. set skeyvals("browsechar")=earg
	. set browseTerm=""
	. set skeyvals("browseterm")=""
	


# set new browseterm if term clicked
	if etarget="browseterm" do
	. set browseTerm=earg
	. set skeyvals("browseterm")=earg


# rebuild paging if currPage > 1
	if currPage>1 do
	. set pagingStart=((currPage-1)*pageSize)+1	
	. set pagingEnd=pagingStart+pageSize-1
	. if (pagingEnd>resultSize) set pagingEnd=resultSize


# check if new view should be displayed
	if etarget="displayview" do
	. set displayview=earg
	. set skeyvals(etarget)=earg


# check if new doc must be displayed in doc view (+ change displayview)
	if etarget="viewdoc" do
	. set displayview="2"
	. set currDoc=earg    //number of doc to be displayed
	. set skeyvals("currdoc")=currDoc


# check if new query must be executed
	if etarget="searchquery" do
	. set searchClicked="1"
	. set qval=earg
	. set displayview="0"


#---------------------------------------------------------------
# if search button was clicked - switch to result view

# if displayview = 0  (result list) - rebuild ^ans (result cats can use previous ^ans)
# Then - add to history if results>0 AND IF new search was executed
# you may add an upper limit to the loop -> that will be the # of saved queries per session
#---------------------------------------------------------------
	if searchClicked="1" do
	. set displayview="0" 
	. set currPage="1"
	. set skeyvals("currpage")="1"
	. set pagingStart=1
	. set pagingEnd=pageSize
	
	if displayview="0" do	
	. set %=$$loadResults
	. if (resultSize>0)&(searchClicked="1") do
	.. set num=0
	.. set duplicate=""
	.. for k=0:1 do
	... set num=k 	
	... if '$data(skeyvals("q"_k)) break
	... if skeyvals("q"_k)=qval set duplicate=1 break
	.. if duplicate="" set skeyvals("q"_num)=qval


#--------------------------------------------------------------
# check if page number must be changed
# CALL THIS AFTER LOADING SEARCH RESULTS!!!
#--------------------------------------------------------------
	if etarget="paging" do
	. set displayview="0"
	. set currPage=earg
	. set skeyvals("currpage")=earg
	. set pagingStart=((currPage-1)*pageSize)+1	
	. set pagingEnd=pagingStart+pageSize-1
	. if (pagingEnd>resultSize) set pagingEnd=resultSize


#---------------------------------------------------------------
# if displayview = 1 (category list) AND ???????????????????? - call only once per query!
# make querydd (doc-doc for top 50 docs), cv (cluster vector), ct (cluster term matrix)
#---------------------------------------------------------------
	if displayview="1" do    //AND SOMETHING ELSE!!!
	. set %=$$makeQueryDocDoc
	. set %=$$makeClusters


#---------------------------------------------------------------
# if displayview=2 and currDoc!="" then record new doc view n history
#---------------------------------------------------------------
	if (displayview="2")&(currDoc'="") do
	. set num=0
	. set duplicate=""
	. for k=0:1 do
	.. set num=k 	
	.. if '$data(skeyvals("d"_k)) break
	.. if skeyvals("d"_k)=currDoc set duplicate=1 break
	. if duplicate="" set skeyvals("d"_num)=currDoc


#---------------------------------------------------------------
# rebuild state field (CALLED AFTER PROCESSING ALL VARS!!!)
#---------------------------------------------------------------
	set k=""
	set stateval=""
	for x=1:1 do
	. set k=$order(skeyvals(k)) if k="" break
	. set stateval=stateval_k_"@"_skeyvals(k)_"@@"


#---------------------------------------------------------------
# build html
#---------------------------------------------------------------
	html <html> &!
	html <head> &!
	html <title>Giggle Search: &~qval~</title> &!
	html <link rel="shortcut icon" href="../images/favicon3.ico"> &!
	html <link rel="stylesheet" href="../styles.css" type="text/css">
	html <script> &! function sf(){ document.Form1.&~qfield~.focus(); } &! </script> &!
	html </head> &!
	html <body bgcolor=#ffffff text=#000000 link=#0000cc vlink=#551a8b 
	html alink=#ff0000 onLoad=sf() topmargin=3 marginheight=3> &!
	html <form name="Form1" id="Form1" method="post" action="giggle.cgi"/> &!&!
	html <input type="hidden" name="state" value="&~stateval~"/> &!
	html <input type="hidden" name="eventtarget" value=""/> &!
	html <input type="hidden" name="eventargument" value=""/> &!&!

# postback javascript
	html <script language="javascript" type="text/javascript"> &!
	html <!-- &!
	html function doPostBack(eventTarget, eventArgument) { &!
	html   var theform; &!
	html   if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) { &!
	html     theform = document.Form1; &!
	html   } &!
	html   else { &!
	html     theform = document.forms["Form1"]; &!
	html   } &!
	html   theform.eventtarget.value = eventTarget; &!
	html   theform.eventargument.value = eventArgument; &!
	html   theform.submit(); &!
	html } &!
	html // --> &!
	html </script> &!&!

# build searchbox html
	html <p><table cellpadding=0 cellspacing=0><tr> &!
	html <td valign=top><a href="../giggle.html">
	html <img src="../images/gigglesmall.gif" border=0></a></td> &!
	html <td>&nbsp;&nbsp;</td> &!
	html <td><input maxLength=256 size=35 type="text" name="&~qfield~" value="&~qval~"> 
	html <input type="submit" name="searchbutton" value="Search"></td> &!
	html </tr></table> &!

# build tabs html
	html <p><table cellpadding=0 cellspacing=0 width=100%><tr><td background="../images/bg1.gif"> &!
	html <table cellpadding=0 cellspacing=0> &! <tr> &!

	set isAct=""
	if displayview="0" set isAct="_a"
	do ^writeTab("most",isAct,"0","Result List") set isAct=""
	if displayview="1" set isAct="_a"
	do ^writeTab("",isAct,"1","Result Clusters") set isAct=""
	if displayview="2" set isAct="_a"
	do ^writeTab("",isAct,"2","Document View") set isAct=""
	if displayview="3" set isAct="_a"
	do ^writeTab("",isAct,"3","Browse Terms") set isAct=""
	if displayview="4" set isAct="_a"
	do ^writeTab("",isAct,"4","History")

	html </tr> &! <tr> &!
	for k=0:1:4 do
	. if k=displayview html <td bgcolor=#E7EFFF colspan=3><img src="../images/blank.gif"></td> &!
	. html <td colspan=3><img src="../images/blank.gif"></td> &!
	html </tr> &! </table> &! </td></tr></table> &!

	html <table cellpadding=3 cellspacing=0 width=100%><tr bgcolor=#E7EFFF> &!
	html <td align=right nowrap>&nbsp;
	if displayview="0" do
	. html <span class="resultscount">Results <b>&~pagingStart~ - &~pagingEnd~</b> of 
	. html <b>&~resultSize~</b> for <b>&~qval~</b> (<b>&~searchTime~</b> seconds)</span>
	html </td></tr></table> &!


# if displayview="0" - display list of results
#---------------------------------------------------------------
	if displayview="0" do
	. if resultSize=0 do	
	.. html <p>Your search for <b>&~qval~</b> returned no results. 
	.. if soundexTerms'="" do
	... html &nbsp; Possible matches include:<p>
	... html <ul>&~soundexTerms~</ul>
	. if resultSize>0 do
	.. set count=1
	.. html <p><table cellpadding=6 cellspacing=0> &!
	.. html <tr><td class=gridttl>No.</td><td class=gridttl>Relevance</td><td class=gridttl>Document</td></tr> &!
	.. open 1:"osu.medline,old"
	.. set x=""
	.. for i=1:1:10 do
	... set x=$order(^ans(x),-1)   // cycle thru cosines in reverse (descending) order.
	... if x="" break
	... set i=""
	... for  do
	.... set i=$order(^ans(x,i))  // get the doc offsets for each cosine value.
	.... if i="" break
	.... use 1 set %=$zseek(i)    // move to correct spot in data file 
	.... for k=1:1:30 do          
	..... use 1
	..... read a                  // find the title
	..... if a="" break
	..... set temp=$extract(a,1,3)
	..... if (temp="TI ") do
	...... if (count'<pagingStart)&(count'>pagingEnd) do
	....... html <tr> &! <td align=right>&~count~</td><td align=center>&~x~</td>
	....... html <td><a href="javascript:doPostBack('viewdoc','&~^ans(x,i,1)~')">&~$extract(a,7,255)~</a></td></tr> &!
	...... set count=count+1
	.. close 1
	.. html </table>
	. html <p align=center>
	. for z=1:1:totalPages do
	.. if z=currPage html &nbsp;<b><font color=maroon size=+1>&~z~</font></b>  
	.. else html &nbsp;<a href="javascript:doPostBack('paging','&~z~')">&~z~</a>  
	

# if displayview = 1 - build clusters
#---------------------------------------------------------------
	if displayview="1" do displayClusters
	

# if display = 2 - display document
#---------------------------------------------------------------
	if displayview="2" do
	. if currDoc="" html <p>You haven't selected a document yet.
	. else  do
	.. set ref=currDoc
	.. open 1:"osu.medline,old" 
	.. set flg=0 

	.. html <p><table cellpadding=6 cellspacing=0> &! 
	.. html <tr valign=top>&!<td align=right class=docviewttl>Mesh Headings:</td><td>

	.. use 1 set %=$zseek(^doc(ref)) 
	.. for  do 
	... use 1 read a if a="" break 
	... if $extract(a,1,3)="MH " do 	
	.... set i=$find(a,"/") 
	.... if i=0 set i=255 
	.... else set i=i-2 
	.... set a=$extract(a,7,i) 
	.... set %=$zwi(a) 	
	.... for  do 
	..... set ww=$zwp 
	..... if ww="" break 
	..... if ww?.p continue 
	..... set wx=$$^Lower(ww) 
	..... if qval=wx set ww="<span class=highlight>"_ww_"</span>"
	..... if $data(^lib($e(wx,1,1),wx)) html <a href="javascript:doPostBack('searchquery', '&~ww~')">&~ww~</a> 
	..... else html &~ww~ 

	.. html </td></tr> &! <tr valign=top><td align=right class=docviewttl>Title:</td><td>

	.. use 1 set %=$zseek(^doc(ref)) 
	.. for  do
	... use 1 read a if a="" break 
	... if $extract(a,1,3)="TI " do 	
	.... set %=$zwi($extract(a,7,255)) 
	.... for  do 
	..... set ww=$zwp 
	..... if ww="" break 
	..... set wx=$$^Lower(ww) 
	..... if qval=wx set ww="<span class=highlight>"_ww_"</span>"
	..... if $data(^lib($e(wx,1,1),wx)) html <a href="javascript:doPostBack('searchquery','&~ww~')">&~ww~</a>&nbsp;
	..... else html &~ww~&nbsp;

	.. html </td></tr> &! <tr valign=top><td align=right class=docviewttl>Abstract:</td><td>

	.. use 1 set %=$zseek(^doc(ref)) 
	.. for  do
	... use 1 read a if a="" break 
	... if $extract(a,1,3)="AB " 
	... for  do
	.... set %=$zwi($extract(a,7,255)) 
	.... for  do 
	..... set ww=$zwp 
	..... if ww="" set flg=1 break 
	..... set wx=$$^Lower(ww) 
	..... if qval=wx set ww="<span class=highlight>"_ww_"</span>"
	..... if $data(^index(wx)) html <a href="javascript:doPostBack('searchquery','&~wx~')">&~ww~</a> 
	..... else html &~ww~ 
	.... if flg break 

	.. html </td></tr> &! <tr valign=top><td align=right class=docviewttl>Related Documents:</td><td>
	
	.. html <table cellpadding=3 cellspacing=0>
	.. set d="" 
	.. for  do 
	... set d=$order(^dd(ref,d)) 
	... if d="" break 
	... use 1 set %=$zseek(^doc(d)) 
	... for  do 
	.... use 1 read a if a="" break 
	.... if $extract(a,1,3)="TI " do 
	..... html <tr><td align=right><a href="javascript:doPostBack('viewdoc','&~d~')">&~d~</a></td><td>
	..... set %=$zwi($extract(a,7,255)) 
	..... for  do 
	...... set ww=$zwp 
	...... if ww="" break 
	...... set wx=$$^Lower(ww) 
	...... if $data(^index(wx)) do 
	....... if qval=wx set ww="<span class=highlight>"_ww_"</span>"
	....... html <a href="javascript:doPostBack*'searchquery','&~wx~')">&~ww~</a>
	...... else html &~ww~ 
	..... html </td></tr>

	.. html </table></td></tr></table> &!
	
	.. close 1	




# if display=3 - display browse categories
#---------------------------------------------------------------
	if displayview="3" do displayBrowse


# if display=4 - display history
#---------------------------------------------------------------
	if displayview="4" do
	. html <p><table cellpadding=6 cellspacing=0 width=100%><tr> &!
	. html <td class=gridttl width=50%>Viewed Documents</td>
	. html <td class=gridttl width=50%>Past Searches</td></tr> &! 
	. html <tr valign=top> &! <td><ol>

	. open 1:"osu.medline,old"
	. for k=0:1 do
	.. if '$data(skeyvals("d"_k)) break
	.. set ref=skeyvals("d"_k)
	.. use 1 set %=$zseek(^doc(ref))    // move to correct spot in data file 
	.. for g=1:1:30 do          
	... use 1
	... read a                  // find the title
	... if a="" break	
	... set temp=$extract(a,1,3)
	... if temp="TI " do
	.... html <li><a href="javascript:doPostBack('viewdoc','&~ref~')">&~$extract(a,7,80)~</a>
	.... break


	. close 1

	. html </ol></td><td><ol>
	. for k=0:1 do
	.. if '$data(skeyvals("q"_k)) break
	.. html <li><a href="javascript:doPostBack('searchquery','&~skeyvals("q"_k)~')">&~skeyvals("q"_k)~</a> &!
	. html </ol></td></tr></table>



#---------------------------------------------------------------
#	temporary debugging code
#---------------------------------------------------------------
	html <p><hr><hr><h4>For Demo in Class: Posted Data</h4>	
	html <p><b>Field key/values:</b>
	set k=""
	for x=1:1 do
	. set k=$order(keyvals(k)) if k="" break	
	. html <li> &~k~ -- &~keyvals(k)~
	html <p><b>State key/values:</b>
	set k=""
	for x=1:1 do
	. set k=$order(skeyvals(k)) if k="" break
	. html <li> &~k~ -- &~skeyvals(k)~
	


	html <p align=center><font size=-1>&copy;2005 Giggle &!
	html </form></body></html> &!

	halt


loadResults
	set searchTime=$zd1
	set a=qval 
	set resultSize="0"

	kill ^query
	kill ^ans
	kill ^tmp

# check against stoplist and build new query excluding stop terms:
	open 2:"stop,old"
	set %=$zwi(a)
	set found="0"			//flag
	set nq=""  			//new query
	for  do				//loop thru query terms
	. set w=$zwn if w="" break	
	. for  do			//loop thru stoplist lines
	.. use 2 read slst	
	.. if '$t break
	.. if $find(slst,w) set found="1" set soundexTerms=$$runSoundex(w) break
	. if found="0" set nq=nq_w_" " 	//add term to new query
	. set found="0"			//reset flag
	. set %%=$zseek(0)		//move pointer to beginning of file
	close 2
	set a=nq			//use new query

# extract query terms into query vector 
# then - find documents containing one or more query terms.	

	set %=$zwi(a)
	for  do                    // extract query words to query vector
	. set w=$zwn if w="" break
	. if '$data(^dict(w)) set soundexTerms=$$runSoundex(w)
	. set ^query(w)=1
	. set d=""
	. for  do
	.. set d=$order(^index(w,d)) if d="" break   // scan for docs containing w
	.. set ^tmp(d)=""          // retain doc id

# If cosine is > zero, put it and the doc offset (^doc(i)) into an asnwer vector.
# Make the consine a right justified string of length 5 with 3 didgits to the
# right of the decimal point.  This will force numeric ordering on the first key.

	set i=""	
	for  do                    // calculate cosine between query and each doc
	. set i=$order(^tmp(i)) if i="" break    // use list of docs from above
+	c=doc(i).Cosine(query());  // MDH cosine calculation
	. if c>0 do
	.. set ^ans($justify(c,5,3),^doc(i),1)=i 
	.. set resultSize=resultSize+1

	set skeyvals("resultsize")=resultSize 	//load result size into state

	set searchTime=$zd1-searchTime

	if ((resultSize#pageSize)'=0) set totalPages=(resultSize\pageSize)+1
	else set totalPages=(resultSize\pageSize)

	set skeyvals("totalpages")=totalPages    //load total pages into state


	quit 


runSoundex(input)
	set result=""
# Start with these characters...
	set alphabet="abcdefghijklmnopqrstuvwxyz"	

# And translate them to these numbers.
	set trans="01230120022455012623010202"
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
	.. set result=result_"<li><a href=""javascript:doPostBack('searchquery','"_test_"')"">"_test_"</a>"

	quit result


makeQueryDocDoc
	kill ^querydd

	set cos=""
	set i=""

# Generate ^querydd(d1) array using doc numbers from ^ans as 1st index

	set count=0
	for  do
	. if count>49 break
	. set cos=$order(^ans(cos))
	. if cos="" break
	. for  do
	.. set i=$order(^ans(cos,i))
	.. if i="" break
	.. set d1=^ans(cos,i,1)
	.. set ^querydd(d1)=""
	.. set count=count+1

# Make ^querydd 2-d by re-iterating over the initial one, storing cosine values
# as it creates the 2nd indices.

	set d1=""
	set d2=""

	for  do
	. set d1=$order(^querydd(d1))
	. if d1="" break
	. set d2=d1
	. for  do 
	.. set d2=$order(^querydd(d2))
	.. if d2="" break
	.. if $data(^dd(d1,d2)) set ^querydd(d1,d2)=^dd(d1,d2)
	.. if '$data(^dd(d1,d2)) do
	... ; to set the indent
+	c=doc(d1).Cosine(doc(d2));
	... if c>0.005 set ^querydd(d1,d2)=c set count=count+1

	quit


makeClusters
	kill ^t		; temporary vector for doc/doc correlations
	kill ^cv	; cluster vector
	kill ^ct 	; temporary cluster/term matrix
	kill ^cterms	; clasters/weights & terms

	set minCorr=0.1    ;set minimum correlation value (244 clusters for 5000 docs)

#	loop through ^querydd matrix and create temporary vector ^t(c,d1,d2),
#	where c=correlation, d1=document1, d2=document2

	set d1=""
	for  do
	. set d1=$order(^querydd(d1)) if d1="" break
	. set d2=""
	. for  do
	.. set d2=$order(^querydd(d1,d2)) if d2="" break
	.. set c=^querydd(d1,d2)
	.. if '($data(^t(c,d1,d2))!$data(^t(c,d2,d1))) set ^t(c,d1,d2)=""

#	loop through vector t examining each pair of d1/d2 
#
	set cn=0 	;init number of clusters to 0
	set c=""
	for  do 
	. set c=$order(^t(c),-1) if c="" break
	. if c<minCorr break
	. set d1=""
	. for  do
	.. set d1=$order(^t(c,d1)) if d1="" break
	.. set d2=""
	.. for  do    
	... set d2=$order(^t(c,d1,d2)) if d2="" break  

#	loop through and build-up cluster vector ^cv:
#	if d1 exists add d2, and visa-versa; 
#	else - create new cluster and add both d1 and d2
#
	... set added=0 
	... set ci=""  	;current cluster index
	... for  do
	.... set ci=$order(^cv(ci)) if ci="" break	
	.... if $data(^cv(ci,d1)) set ^cv(ci,d2)="" set added=1
	.... if $data(^cv(ci,d2)) set ^cv(ci,d1)="" set added=1
	... if added=0 set cn=cn+1 set ^cv(cn,d1)="" set ^cv(cn,d2)=""

#	make centroid vectors for each cluster (cluster/term matrix)
#
	set cn=""
	for  do
	. set cn=$order(^cv(cn)) if cn="" break
	. set dn=""
	. for  do
	.. set dn=$order(^cv(cn,dn)) if dn="" break
	.. set t=""
	.. for  do
	... set t=$order(^doc(dn,t)) if t="" break
	... if $data(^ct(cn,t)) set ^ct(cn,t)=^ct(cn,t)+^doc(dn,t)
	... if '$data(^ct(cn,t)) set ^ct(cn,t)=^doc(dn,t)
	.. for  do
	... set t=$order(^ct(cn,t)) if t="" break
	... set w=$justify(^ct(cn,t),6)
	... set ^cterms(cn,w,t)=""

	quit


displayClusters	
# 	print out clusters w top terms and sample titles

	open 1:"osu.medline,old"
	set cn=""
	set clustercount=0
	for i=0:1:50 do		//50 is top limit - performance
	. set clustercount=clustercount+1
	. set cn=$order(^cv(cn)) if cn="" break
	. set dn=""
	. html <p><b>Cluster &~clustercount~</b><br>Top Terms: 
	. set termcount=0
	. for  do
	.. set w=$order(^cterms(cn,w),-1) if w="" break
	.. for  do
	... if termcount>4 break
	... set t=$order(^cterms(cn,w,t)) if t="" break
	... html <a href="javascript:doPostBack('searchquery','&~t~')">&~t~</a> 
	... set termcount=termcount+1
	. set num=1
	. html <ol>
	. for  do    
	.. set dn=$order(^cv(cn,dn)) if dn="" break
	.. use 1 set %=$zseek(^doc(dn))  ;won't reset zseek without "use 1" //USE 5 WAS WRONG!!!
	.. for  do	
	... use 1 read a if a="" break
	... if $extract(a,1,2)="TI" do
	.... html <li><a href="javascript:doPostBack('viewdoc','&~dn~')">&~$extract(a,7,78)~</a>
	.... set num=num+1
	. html </ol>
	close 1


	quit


displayBrowse
	html <p align=center>
	
	for g=97:1:122 do
	. if g=$ascii(browseChar) html <b><font color=maroon size=+1>&~browseChar~</font></b>&nbsp;&nbsp;
	. else html <a href="javascript:doPostBack('browsing','&~$char(g)~')">&~$char(g)~</a>&nbsp;&nbsp; 
	html </p>

# if char has been selected - 
	if (browseChar'="")&(browseTerm="") do
	. html <p><table cellpadding=15 cellspacing=0 align=center><tr valign=top>
	. set charterms=0
	. set t1=""
	. for  do
	.. set t1=$order(^tt(t1)) if t1="" break
	.. if $extract(t1,1)=browseChar set charterms=charterms+1
	. set rows=charterms\4
	. set count=0
	. for  do
	.. set t1=$order(^tt(t1)) if t1="" break
	.. if $extract(t1,1)=browseChar do	
	... if (count#rows)=0 html <td>
	... html <br><a href="javascript:doPostBack('browseterm','&~t1~')">
	... html <img src="../images/cfolder.gif" border=0> &~t1~</a>
	... if (count#rows)=(rows-1) html </td>
	... set count=count+1
	. html </tr></table>


# if char AND term have been selected
	if (browseChar'="")&(browseTerm'="") do
	. html <p><table cellpadding=15 cellspacing=0 align=center><tr valign=top>
	. set charterms=0
	. set t1=""
	. for  do
	.. set t1=$order(^tt(t1)) if t1="" break
	.. if $extract(t1,1)=browseChar set charterms=charterms+1
	. set rows=charterms\4
	. set count=0
	. for  do
	.. set t1=$order(^tt(t1)) if t1="" break
	.. if t1=browseTerm do
	... if (count#rows)=0 html <td>
	... html <br><a href="javascript:doPostBack('browseterm','')">
	... html <img src="../images/ofolder.gif" border=0> &~t1~</a>
	... set t2=""
	... for  do
	.... set t2=$order(^tt(t1,t2)) if t2="" break
	.... html <br><a href="javascript:doPostBack('searchquery','&~t1~ &~t2~')">
	.... html <img src="../images/blueball.gif" border=0> &~t1~ &amp; &~t2~</a>
	... set count=count+1
	... if (count#rows)=(rows-1) html </td>
	.. if (t1'=browseTerm)&($extract(t1,1)=browseChar) do	
	... if (count#rows)=0 html <td>
	... html <br><a href="javascript:doPostBack('browseterm','&~t1~')">
	... html <img src="../images/cfolder.gif" border=0> &~t1~</a>
	... if (count#rows)=(rows-1) html </td>
	... set count=count+1
	. html </tr></table>
	

	quit
