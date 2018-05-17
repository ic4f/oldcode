using System;

namespace DataMining.Data
{
    public class Helper
    {
        public const int NUMBER_OF_DOCS = 10000;

        //this one is for the website!
        public const string INDEX_PATH = @"C:\_development\VS projects\DataMining\Website\index\";


        public const string SOURCE_PATH = @"C:\_development\data\datamining\source\";
        //public const string INDEX_PATH = @"C:\_development\data\datamining\index\";

        public const string TITLES_FILE = "titles.txt";
        public const string DOCIDS_FILE = "docids.txt";
        public const string CATS_FILE = "cats.txt";
        public const string DOCCATS_FILE = "doccats.data";


        /*------------------------- DIRECTORIES -----------------------*/
        //directory for source files used to build up index files
        public const string SOURCE_DIRECTORY_NAME = "source";
        //directory for final index files
        public const string INDEX_DIRECTORY_NAME = "index";
        //data.txt directory name (stores text versions of data files)
        public const string DATATXT_DIRECTORY_NAME = "data.txt";

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


        /*------------------------- SOURCE FILES -----------------------*/
        //1 doc url per line, line# = docId
        public const string SOURCE_DOCS_FILE = "docs.txt";
        //1 term per line, line# = termId
        public const string SOURCE_TERMS_FILE = "terms.txt";
        //each line: termId docId [term freq in doc]
        public const string SOURCE_TERMDOCS_FILE = "termdocs.txt";
        //each line: # of docs containing term, which Id is the line#
        public const string SOURCE_TERMDOCCOUNTS_FILE = "termdoccounts.txt";
    }
}
