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

namespace Ei.Website.lineupadmin
{
    public class lineupDetails : b.BasePage
    {
        protected Literal ltrLineup;
        private static int POSITIONS = 6;
        protected Literal ltrDescription;
        protected Literal ltrCase;
        protected Literal ltrSuspect;
        protected Literal ltrNotes;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            int lineupId = Convert.ToInt32(Request["Id"]);
            d.Lineup lineup = new d.Lineup(lineupId);
            DataTable dtPhotos = new d.PhotoData().GetPhotosByLineup(lineupId);

            int photoId;
            int dtPhotosPosition = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < POSITIONS; i++)
            {
                sb.Append("\n\t<td style=\"width:96px;\">");
                if (i == lineup.SuspectPhotoPosition)
                    sb.AppendFormat("<img src='{0}' style=\"border:2px red solid;\">", b.UrlHelper.GetSuspectImageUrl(lineup.SuspectId, b.ImageHelper.SMALL_PHOTO_SUFFIX));
                else
                {
                    photoId = Convert.ToInt32(dtPhotos.Rows[dtPhotosPosition][0]);
                    sb.AppendFormat("<img src='{0}' style=\"border:1px #666666 solid;\">", b.UrlHelper.GetPhotoImageUrl(photoId, b.ImageHelper.SMALL_PHOTO_SUFFIX));
                    dtPhotosPosition++;
                }
                sb.Append("</td>");
            }
            ltrLineup.Text = sb.ToString();

            ltrDescription.Text = lineup.Description;
            ltrNotes.Text = lineup.Notes;

            d.CCase ccase = new d.CCase(lineup.CaseId);
            ltrCase.Text = ccase.Number;

            d.Suspect suspect = new d.Suspect(lineup.SuspectId);
            ltrSuspect.Text = suspect.Number;
        }

        protected override int PageId { get { return 8; } }
    }
}
