using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using b = Ei.Business;
using d = Ei.Data;

namespace Ei.Website.operations
{
    public class resultDetails : b.BasePage
    {
        protected Button btnDelete;
        protected Literal ltrWitness;
        protected Literal ltrAdministered;
        protected Literal ltrRelevanceNotes;
        protected Repeater rptResults;
        private static int RESULT_NO = 0;
        private static int RESULT_YES = 1;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnDelete.Attributes["onclick"] = "javascript:return confirm('Are you sure you want to delete this record?')";
                BindData();
            }
        }

        private void BindData()
        {
            int lineupViewId = Convert.ToInt32(Request["Id"]);
            d.LineupView lv = new d.LineupView(lineupViewId);
            d.Lineup lineup = new d.Lineup(lv.LineupId);
            ltrWitness.Text = lv.WitnessFirstName + ", " + lv.WitnessLastName;
            ltrAdministered.Text = lv.Administered + " by " + lv.AdministeredByName;
            ltrRelevanceNotes.Text = lv.RelevanceNotes.Replace("\n", "<br>");

            DataTable dtResults = new d.PhotoViewData().GetByLineupView(lineupViewId);
            rptResults.DataSource = dtResults;
            rptResults.DataBind();
        }

        private void rptResults_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                string photoId = DataBinder.Eval(e.Item.DataItem, "photoid").ToString();
                int resultCode = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "resultcode"));
                string certainty = DataBinder.Eval(e.Item.DataItem, "certainty").ToString();

                Literal ltrPhoto = (Literal)e.Item.FindControl("ltrPhoto");
                Literal ltrPhotoRef = (Literal)e.Item.FindControl("ltrPhotoRef");
                Literal ltrResult = (Literal)e.Item.FindControl("ltrResult");
                Literal ltrCertainty = (Literal)e.Item.FindControl("ltrCertainty");

                StringBuilder sb = new StringBuilder();
                if (photoId == "")
                {
                    int lineupViewId = Convert.ToInt32(Request["Id"]);
                    d.LineupView lv = new d.LineupView(lineupViewId);
                    d.Lineup lineup = new d.Lineup(lv.LineupId);
                    sb.AppendFormat("<img src='{0}' width='120px' style='border:2px solid red;'>",
                        b.UrlHelper.GetSuspectImageUrl(lineup.SuspectId, b.ImageHelper.SMALL_PHOTO_SUFFIX));
                }
                else
                    sb.AppendFormat("<img src='{0}' width='120px' style='border:1px solid #777777;'>",
                        b.UrlHelper.GetPhotoImageUrl(Convert.ToInt32(photoId), b.ImageHelper.SMALL_PHOTO_SUFFIX));

                ltrPhoto.Text = sb.ToString();

                ltrPhotoRef.Text = photoId;
                ltrResult.Text = interpretResult(resultCode);
                ltrCertainty.Text = certainty;
            }
        }

        private string interpretResult(int code)
        {
            if (code == RESULT_NO)
                return "no";
            else if (code == RESULT_YES)
                return "<span style='font-weight:bold;color:red;'>yes</span>";
            else
                return "not sure";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int lineupViewId = Convert.ToInt32(Request["Id"]);
            d.LineupView lv = new d.LineupView(lineupViewId);
            d.LineupViewData lvd = new d.LineupViewData();
            lvd.Delete(Convert.ToInt32(Request["Id"]));
            Response.Redirect("results.aspx?LineupId=" + lv.LineupId);
        }

        protected override int PageId { get { return 27; } }

        protected override void OnInit(EventArgs e)
        {
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.rptResults.ItemDataBound += new RepeaterItemEventHandler(rptResults_ItemDataBound);
            base.OnInit(e);
        }
    }
}