using System;

namespace Giggle.Data
{
    public class Helper
    {
        /*------------------------- DIRECTORIES -----------------------*/
        //directory containing the docs 
        public const string DOCS_PATH = @"C:\_current\development\data\isr\docs\";
        //directory for source files used to build up index files
        public const string SOURCE_DIRECTORY_NAME = "source";
        //directory for final index files
        public const string INDEX_DIRECTORY_NAME = "index";
        //data.txt directory name (stores text versions of data files)
        public const string DATATXT_DIRECTORY_NAME = "data.txt";

        //stoplist for all 
        public const string STOPLIST_PATH = @"C:\_current\development\data\isr\stoplist.txt";



        /*------------------------- INDEX FILES -----------------------*/
        //1 doc url per line, line# = docId (same as in soure dir)
        public const string INDEX_DOCS_FILE = "docs.txt";
        //1 term per line, line# = termId (same as in soure dir)
        public const string INDEX_TERMS_FILE = "terms.txt";
        //# of docs; the rest: [doc norm (float)] [links (short)] [citations (short)] [doc page rank (float)] [term count for this doc (short)]; line# = docId
        public const string INDEX_DOCSDATA_FILE = "docs.data";
        //# of terms; the rest: [doc countfor this term (short)]; line# = termId
        public const string INDEX_TERMSDATA_FILE = "terms.data";
        //[term Id(int)] [doc Id (short)] [term weight within this doc(float): freq * idf (for now, later - count html markup weights)] 
        public const string INDEX_TERMDOCS_FILE = "termdocs.data";
        //docId docId (docId linkDocId)
        public const string INDEX_DOCLINKS_FILE = "doclinks.data";
        //docId docId (docId citationDocId)
        public const string INDEX_DOCCITATIONS_FILE = "doccitations.data";


        public const string INDEX_TITLES_FILE = "titles.txt";

        /*------------------------- SOURCE FILES -----------------------*/
        //1 doc url per line, line# = docId
        public const string SOURCE_DOCS_FILE = "docs.txt";
        //1 term per line, line# = termId
        public const string SOURCE_TERMS_FILE = "terms.txt";
        //each line: termId docId [term freq in doc]
        public const string SOURCE_TERMDOCS_FILE = "termdocs.txt";
        //each line: # of docs containing term, which Id is the line#
        public const string SOURCE_TERMDOCCOUNTS_FILE = "termdoccounts.txt";
        //each line: url --> [url,...]
        public const string SOURCE_LINKS_FILE = "hashedlinks.txt";
        //each line: pagerank of doc, which Id is the line# (this source file is calculated)
        public const string SOURCE_PAGERANK_FILE = "pageranks.data";
        //docId (short) #of links (short)
        public const string SOURCE_DOCLINKCOUNT_FILE = "doclinkcount.data";
        //docId (short) #of citations (short)
        public const string SOURCE_DOCCITATIONCOUNT_FILE = "doccitationscount.data";
        //docId docId (docId linkDocId)
        public const string SOURCE_DOCLINKS_FILE = "doclinks.txt";
        //docId docId (docId citationDocId)
        public const string SOURCE_DOCCITATIONS_FILE = "doccitations.txt";
    }
}
