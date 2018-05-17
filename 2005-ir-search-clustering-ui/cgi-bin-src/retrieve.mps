      zmain

+     global doc("doc");     // makes the ^doc() array known to MDH fcns.
+     global index("index"); // makes the ^index() array known to MDH fcns.
+     global query("query");

# retreive.mps March 5, 2005

+     mstring i("i");  // the ("i") is required for mumps linkage
+     mstring c("c");

      open 1:"osu.medline,old"
      if '$test write "osu.medline not found",! halt

readquery
      read "Enter query: ",a
      if '$test halt
      if a="" halt

      set time0=$zd1

      kill ^query
      kill ^ans
      kill ^tmp


# check against stoplist and build new query excluding stop terms:

	open 2:"stop,old"
	set %=$zwi(a)
	set found="0"			//flag
	set nq=""  			//new query
	for  do				//loop thru query terms
	. set w=$zwn
	. if w="" break	
	. for  do			//loop thru stoplist lines
	.. use 2 read slst	
	.. if '$t break
	.. if $find(slst,w) set found="1" break
	. if found="0" set nq=nq_w_" " 	//add term to new query
	. set found="0"			//reset flag
	. set %%=$zseek(0)		//move pointer to beginning of file
	close 2
	set a=nq			//use new query
	use 5

	if nq="" write "please be more specific",! goto readquery

	read "Number of abstracts: ",maxCount


	set message="You searched for: "

	set count=0			//count for abstracts


      set %=$zwi(a)
      for  do                    // extract query words to query vector
      . set w=$zwn
      . if w="" break
      . if '$data(^dict(w)) write w," not a known term",! halt
      . set message=message_w_" "
      . set ^query(w)=1

# Find documents containing one or more query terms.

      . set d=""
      . for  do
      .. set d=$order(^index(w,d))   // scan for docs containing w
      .. if d="" break
      .. set tmp(d)=""          // retain doc id


	write !,message,!		//new friendly message


      set i=""
      for  do                    // calculate cosine between query and each doc
      . set i=$order(tmp(i))    // use list of docs from above
      . if i="" write i,! break

#	write "reached cos",!

+     c=doc(i).Cosine(query());  // MDH cosine calculation

# If cosine is > zero, put it and the doc offset (^doc(i)) into an asnwer vector.
# Make the consine a right justified string of length 5 with 3 didgits to the
# right of the decimal point.  This will force numeric ordering on the first key.

      . if c>0 set ans($justify(c,5,3),^doc(i),1)=i

      set x=""
      for i=1:1:10 do
      . set x=$order(ans(x),-1)   // cycle thru cosines in reverse (descending) order.
      . if x="" break
      . set i=""
      . for  do
      .. set i=$order(ans(x,i))  // get the doc offsets for each cosine value.
      .. if i="" break
      .. use 1 set %=$zseek(i)    // move to correct spot in data file 
      .. for k=1:1:500 do          // changed limit from 30 to 500
      ... use 1
      ... read a                  // find the title

      ... set temp=$extract(a,1,3)
      ... if temp="TI " do      
      .... use 5 write !,x," ",ans(x,i,1)," ",$extract(a,7,80),!

      ... if (temp="AB ")&(count<maxCount) do
      .... set count=count+1
      .... use 5 write ?5,$extract(a,7,80)
      .... for  do
      ..... use 1 read abst
      ..... if '$t break
      ..... if abst="" break //end of abstract
      ..... if '($extract(abst,1,6)="      ") break
      ..... use 5 write ?5,$extract(abst,7,80)
      
        

 
      use 5 write !,"Time used: ",$zd1-time0," seconds",!
