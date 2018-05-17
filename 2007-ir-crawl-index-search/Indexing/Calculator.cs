using System;
using System.Data;
using d = IrProject.Data;

namespace IrProject.Indexing
{
	public class Calculator
	{
		public Calculator()
		{
		}

        public void CalculateIdfs()
        {
            double docCount = Convert.ToDouble(new d.DocData().GetCount()); //required for correct division
            d.TermData td = new d.TermData();
            DataTable dt = td.GetDocCounts();
            foreach (DataRow dr in dt.Rows)
            {
                td.UpdateIdf(dr[0].ToString(), Convert.ToSingle(Math.Log(docCount / (int)dr[1], 2)));
            }
        }
        //watch for divide by 0
        public void CalculateIdfsA()
        {
            double docCount = Convert.ToDouble(new d.DocData().GetCount()); //required for correct division
            d.TermData td = new d.TermData();
            DataTable dt = td.GetDocCountsA();
            foreach (DataRow dr in dt.Rows)
                td.UpdateIdfA(dr[0].ToString(), Convert.ToSingle(Math.Log(docCount / (int)dr[1], 2)));
        }

        public void CalculateTermWeights()
		{
			int coeff = 3;
			d.TermDocData tdd = new d.TermDocData();			
			DataTable dt = tdd.GetRecords();
			string term;
			int docId;
			int text;
			int bold;
			int heading;
			int anchor;
			int title;
			int url;
			int externalAnchor;
			int tf;
			int tfa;

			for (int i=0; i<dt.Rows.Count; i++)
			{
				if (i % 10 == 0) Console.WriteLine(i);

				term = dt.Rows[i][0].ToString();
				docId = (int)dt.Rows[i][1];
				text = (int)dt.Rows[i][2];
				bold = coeff * (int)dt.Rows[i][3];
				heading = coeff * (int)dt.Rows[i][4];
				anchor = (int)dt.Rows[i][5];
				title = coeff * (int)dt.Rows[i][6];
				url = coeff * (int)dt.Rows[i][7];
				externalAnchor = coeff * (int)dt.Rows[i][9];

				tf = text + bold + heading + anchor + title + url;
				tfa = tf + externalAnchor;
				
				tdd.UpdateTermFreqs(term, docId, tf, tfa);
			}
		}

		public void CalculateDocNorms()
		{

		}
	}
}
