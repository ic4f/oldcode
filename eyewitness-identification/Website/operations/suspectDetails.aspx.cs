using System;
using System.IO;
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
    public class suspectDetails : b.BasePage
    {
        protected Button btnDelete;
        protected Literal ltrNoDelete;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindDelete();
        }

        private void BindDelete()
        {
            int suspectId = Convert.ToInt32(Request["Id"]);
            DataTable dtLineups = new d.LineupData().GetLockedLineupsBySuspect(suspectId);
            if (dtLineups.Rows.Count > 0)
            {
                btnDelete.Visible = false;
                ltrNoDelete.Text = "To delete this suspect, you must first find and delete all the associated <a href='lineups.aspx'>lineups</a>";
            }
            else
                btnDelete.Attributes["onclick"] = "javascript:return confirm('Are you sure you want to delete this suspect?')";
        }

        protected override int PageId { get { return 22; } }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int suspectId = Convert.ToInt32(Request["Id"]);
            d.SuspectData sd = new d.SuspectData();
            sd.Delete(suspectId);

            string sPath = Server.MapPath(b.UrlHelper.GetSuspectImageUrl(suspectId, b.ImageHelper.SMALL_PHOTO_SUFFIX));
            string mPath = Server.MapPath(b.UrlHelper.GetSuspectImageUrl(suspectId, b.ImageHelper.MEDIUM_PHOTO_SUFFIX));
            string lPath = Server.MapPath(b.UrlHelper.GetSuspectImageUrl(suspectId, b.ImageHelper.LARGE_PHOTO_SUFFIX));

            File.Delete(sPath);
            File.Delete(mPath);
            File.Delete(lPath);

            Response.Redirect("suspectList.aspx");
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            base.OnInit(e);
        }
    }
}
