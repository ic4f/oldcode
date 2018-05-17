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
    public class lineupAdminister2 : b.BasePage
    {
        protected Literal ltrLineup;
        protected Literal ltrStart;

        private static int POSITIONS = 6;

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

            ltrStart.Text = "<a href=\"javascript:open_win(" + lineupId + ");\">Start Lineup Administration</a>";
        }

        protected override int PageId { get { return 53; } }
    }
}
