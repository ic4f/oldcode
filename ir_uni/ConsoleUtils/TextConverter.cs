using System;
using System.Text;
using System.Data;
using d = IrProject.Data;

namespace IrProject.ConsoleUtils
{
    public class TextConverter
    {
        public TextConverter()
        {
        }

        public void ConvertTermDocTable()
        {

            DataTable dt = new d.TermDocData().GetAll();
            string term;
            string docid;
            string textcount;
            string boldcount;
            string headercount;
            string anchorcount;
            string titlecount;
            string urlcount;
            string externalanchorcount;
            string totalcount;
            string totalcount_w;
            string totalcount_a;
            string totalcount_wa;
            foreach (DataRow dr in dt.Rows)
            {
                StringBuilder sb = new StringBuilder();
                term = dr[0].ToString();
                docid = dr[1].ToString();
                textcount = dr[2].ToString();
                boldcount = dr[3].ToString();
                headercount = dr[4].ToString();
                anchorcount = dr[5].ToString();
                titlecount = dr[6].ToString();
                urlcount = dr[7].ToString();
                externalanchorcount = dr[8].ToString();
                totalcount = dr[9].ToString();
                totalcount_w = dr[10].ToString();
                totalcount_a = dr[11].ToString();
                totalcount_wa = dr[12].ToString();
                sb.AppendFormat("{0} ", term);
                sb.AppendFormat("{0} ", docid);
                sb.AppendFormat("{0} ", textcount);
                sb.AppendFormat("{0} ", boldcount);
                sb.AppendFormat("{0} ", headercount);
                sb.AppendFormat("{0} ", titlecount);
                sb.AppendFormat("{0} ", titlecount);
                sb.AppendFormat("{0} ", urlcount);
                sb.AppendFormat("{0} ", externalanchorcount);
                sb.AppendFormat("{0} ", totalcount);
                sb.AppendFormat("{0} ", totalcount_w);
                sb.AppendFormat("{0} ", totalcount_a);
                sb.AppendFormat("{0} ", totalcount_wa);
                Console.WriteLine(sb.ToString());
            }
        }

        public void ConvertTermWeightTable()
        {

            DataTable dt = new d.TermDocData().GetAllTermWeights();
            string term;
            string docid;
            string termweight;
            string termweight_w;
            string termweight_a;
            string termweight_wa;
            foreach (DataRow dr in dt.Rows)
            {
                StringBuilder sb = new StringBuilder();
                term = dr[0].ToString();
                docid = dr[1].ToString();
                termweight = dr[2].ToString();
                termweight_w = dr[3].ToString();
                termweight_a = dr[4].ToString();
                termweight_wa = dr[5].ToString();
                sb.AppendFormat("{0} ", term);
                sb.AppendFormat("{0} ", docid);
                sb.AppendFormat("{0} ", termweight);
                sb.AppendFormat("{0} ", termweight_w);
                sb.AppendFormat("{0} ", termweight_a);
                sb.AppendFormat("{0}", termweight_wa);
                Console.WriteLine(sb.ToString());
            }
        }

        public void ConvertLinkTable()
        {

            DataTable dt = new d.LinkData().GetAll();
            string fromid;
            string toid;
            string text;
            foreach (DataRow dr in dt.Rows)
            {
                StringBuilder sb = new StringBuilder();
                fromid = dr[1].ToString();
                toid = dr[2].ToString();
                text = dr[3].ToString();
                sb.AppendFormat("{0} ", fromid);
                sb.AppendFormat("{0} ", toid);
                sb.AppendFormat("[[{0}]]", text);
                Console.WriteLine(sb.ToString());
            }
        }

        public void ConvertTermTable()
        {

            DataTable dt = new d.TermData().GetAll();
            string term;
            string docccount;
            string doccount_a;
            string idf;
            string idf_a;
            string totalcount;
            string totalcount_w;
            string totalcount_a;
            string totalcount_wa;
            foreach (DataRow dr in dt.Rows)
            {
                StringBuilder sb = new StringBuilder();
                term = dr[0].ToString();
                docccount = dr[1].ToString();
                doccount_a = dr[2].ToString();
                idf = dr[3].ToString();
                idf_a = dr[4].ToString();
                totalcount = dr[5].ToString();
                totalcount_w = dr[6].ToString();
                totalcount_a = dr[7].ToString();
                totalcount_wa = dr[8].ToString();
                sb.AppendFormat("{0} ", term);
                sb.AppendFormat("{0} ", docccount);
                sb.AppendFormat("{0} ", doccount_a);
                sb.AppendFormat("{0} ", idf);
                sb.AppendFormat("{0} ", idf_a);
                sb.AppendFormat("{0} ", totalcount);
                sb.AppendFormat("{0} ", totalcount_w);
                sb.AppendFormat("{0} ", totalcount_a);
                sb.AppendFormat("{0} ", totalcount_wa);
                Console.WriteLine(sb.ToString());
            }
        }

        public void ConvertDocTable()
        {

            DataTable dt = new d.DocData().GetAll();
            string id;
            string url;
            string title;
            string inbound;
            string outbound;
            string maxterms;
            string maxterms_w;
            string maxterms_a;
            string maxterms_wa;
            string norm;
            string norm_w;
            string norm_a;
            string norm_wa;
            string pagerank;
            string termcount;
            foreach (DataRow dr in dt.Rows)
            {
                StringBuilder sb = new StringBuilder();
                id = dr[0].ToString();
                url = dr[1].ToString();
                title = dr[2].ToString();
                inbound = dr[3].ToString();
                outbound = dr[4].ToString();
                maxterms = dr[5].ToString();
                maxterms_w = dr[6].ToString();
                maxterms_a = dr[7].ToString();
                maxterms_wa = dr[8].ToString();
                norm = dr[9].ToString();
                norm_w = dr[10].ToString();
                norm_a = dr[11].ToString();
                norm_wa = dr[12].ToString();
                pagerank = dr[13].ToString();
                termcount = dr[14].ToString();
                sb.AppendFormat("{0} ", id);
                sb.AppendFormat("{0} ", url);
                sb.AppendFormat("[[{0}]] ", title);
                sb.AppendFormat("{0} ", inbound);
                sb.AppendFormat("{0} ", outbound);
                sb.AppendFormat("{0} ", maxterms);
                sb.AppendFormat("{0} ", maxterms_w);
                sb.AppendFormat("{0} ", maxterms_a);
                sb.AppendFormat("{0} ", maxterms_wa);
                sb.AppendFormat("{0} ", norm);
                sb.AppendFormat("{0} ", norm_w);
                sb.AppendFormat("{0} ", norm_a);
                sb.AppendFormat("{0} ", norm_wa);
                sb.AppendFormat("{0} ", pagerank);
                sb.AppendFormat("{0}", termcount);
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
