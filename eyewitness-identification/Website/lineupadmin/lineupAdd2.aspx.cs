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
    public class lineupAdd2 : b.BasePage
    {
        protected Button btnSave;
        protected DropDownList ddlPosition;
        protected Literal ltrPositionCells;
        protected CheckBoxList cblGender;
        protected CheckBoxList cblRace;
        protected CheckBoxList cblHair;
        protected CheckBoxList cblAge;
        protected CheckBoxList cblWeight;
        protected Button btnSearch;
        protected Literal ltrResults;
        protected DropDownList ddlToDisplay;
        protected Literal ltrPhotos;
        protected Label lblError;

        protected HtmlInputHidden hdPos0;
        protected HtmlInputHidden hdPos1;
        protected HtmlInputHidden hdPos2;
        protected HtmlInputHidden hdPos3;
        protected HtmlInputHidden hdPos4;
        protected HtmlInputHidden hdPos5;

        protected Literal ltrJsHelper1;
        protected Literal ltrJsHelper2;

        private static int POSITIONS = 6;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            ltrJsHelper1.Text = System.Configuration.ConfigurationSettings.AppSettings["JsHelper1"];
            ltrJsHelper2.Text = System.Configuration.ConfigurationSettings.AppSettings["JsHelper2"];

            btnSave.Attributes["onclick"] = "javascript:return confirm('Are you sure you want to save this lineup? No further edits will be allowed.')";

            d.Lineup lineup = new d.Lineup(getLineupId());
            d.Suspect suspect = new d.Suspect(lineup.SuspectId);

            BindPosition();
            BindLineup(lineup.SuspectId);
            BindLists(suspect);
            BindToDisplayList();
            BindPhotos();
        }

        private int getLineupId()
        {
            return Convert.ToInt32(Request["Id"]);
        }

        private void BindPosition()
        {
            ddlPosition.AutoPostBack = true;
            ddlPosition.Items.Clear();
            for (int i = 0; i < POSITIONS; i++)
                ddlPosition.Items.Add(new ListItem((i + 1).ToString(), i.ToString()));
        }

        private void BindLineup(int suspectId)
        {
            int suspectPosition = Convert.ToInt32(ddlPosition.SelectedValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < POSITIONS; i++)
            {
                sb.Append("\n\t<td style=\"width:96px;\">");
                if (i == suspectPosition)
                    sb.AppendFormat("<img src='{0}' style=\"border:2px red solid;\">", b.UrlHelper.GetSuspectImageUrl(suspectId, b.ImageHelper.SMALL_PHOTO_SUFFIX));
                else
                    sb.AppendFormat("<img src='../_system/images/layout/blank.gif' width='96' height='128' style=\"border:1px #666666 solid;\" name=\"pos{0}\" onClick=\"remove({0});\">", i);
                sb.Append("</td>");
            }
            ltrPositionCells.Text = sb.ToString();
        }

        private void BindLists(d.Suspect suspect)
        {
            d.VisualCriteriaData vcd = new d.VisualCriteriaData();

            cblGender.Items.Clear();
            cblGender.Items.Add(new ListItem("male", "'m'"));
            cblGender.Items.Add(new ListItem("female", "'f'"));
            cblGender.SelectedValue = "'" + suspect.Gender + "'";

            cblRace.DataSource = vcd.GetRaceList();
            cblRace.DataTextField = "description";
            cblRace.DataValueField = "id";
            cblRace.DataBind();
            cblRace.SelectedValue = suspect.RaceId.ToString();

            cblHair.DataSource = vcd.GetHairColorList();
            cblHair.DataTextField = "description";
            cblHair.DataValueField = "id";
            cblHair.DataBind();
            cblHair.SelectedValue = suspect.HairColorId.ToString();

            cblAge.DataSource = vcd.GetAgeRangeList();
            cblAge.DataTextField = "description";
            cblAge.DataValueField = "id";
            cblAge.DataBind();
            cblAge.SelectedValue = suspect.AgeRangeId.ToString();

            cblWeight.DataSource = vcd.GetWeightRangeList();
            cblWeight.DataTextField = "description";
            cblWeight.DataValueField = "id";
            cblWeight.DataBind();
            cblWeight.SelectedValue = suspect.WeightRangeId.ToString();
        }

        private void BindToDisplayList()
        {
            ddlToDisplay.AutoPostBack = true;
            ddlToDisplay.Items.Clear();
            for (int i = 25; i < 1000; i += 25)
                ddlToDisplay.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }

        private void BindPhotos()
        {
            int toDisplay = Convert.ToInt32(ddlToDisplay.SelectedValue);
            DataSet ds = new d.PhotoData().GetPhotosWithQuery(makeQuery(), "order by p.id ", toDisplay);
            DataTable dtPhotos = ds.Tables[0];
            if (dtPhotos.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dtPhotos.Rows)
                    sb.AppendFormat("<img src='{0}' style=\"margin:3px;border:1px #666666 solid;\" onClick=\"add({1});\" id='{1}' name='{1}'>",
                        b.UrlHelper.GetPhotoImageUrl(Convert.ToInt32(dr[0]), b.ImageHelper.SMALL_PHOTO_SUFFIX), dr[0]);

                ltrPhotos.Text = sb.ToString();
                ltrResults.Text = "Displaying <b>" + Math.Min(dtPhotos.Rows.Count, toDisplay) + "</b> photos out of <b>" + ds.Tables[1].Rows[0][0] + "</b> matching your search criteria";
            }
            else
                ltrResults.Text = "There are no photos in the database that match your criteria";
        }

        private string makeQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" where p.iscategorized = 1 ");
            sb.Append(makeFieldQuery(cblGender, "p.gender"));
            sb.Append(makeFieldQuery(cblRace, "r.id"));
            sb.Append(makeFieldQuery(cblHair, "h.id"));
            sb.Append(makeFieldQuery(cblAge, "a.id"));
            sb.Append(makeFieldQuery(cblWeight, "w.id"));
            return sb.ToString();
        }

        private string makeFieldQuery(CheckBoxList cbl, string field)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListItem i in cbl.Items)
                if (i.Selected)
                    sb.AppendFormat(" {0} = {1} or ", field, i.Value);

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 3, 3); //removing last "or "
                sb.Insert(0, " and ( ");
                sb.Append(")");
            }
            return sb.ToString();
        }

        protected override int PageId { get { return 52; } }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!allPhotosSelected())
                lblError.Visible = true;
            else
            {
                int suspectPosition = Convert.ToInt32(ddlPosition.SelectedValue);
                int lineupId = getLineupId();
                d.LineupData ld = new d.LineupData();
                d.LineupPhotoLink lpl = new d.LineupPhotoLink();
                ld.Lock(lineupId, suspectPosition, Identity.UserId);

                if (suspectPosition != 0)
                    lpl.Create(lineupId, Convert.ToInt32(hdPos0.Value));
                if (suspectPosition != 1)
                    lpl.Create(lineupId, Convert.ToInt32(hdPos1.Value));
                if (suspectPosition != 2)
                    lpl.Create(lineupId, Convert.ToInt32(hdPos2.Value));
                if (suspectPosition != 3)
                    lpl.Create(lineupId, Convert.ToInt32(hdPos3.Value));
                if (suspectPosition != 4)
                    lpl.Create(lineupId, Convert.ToInt32(hdPos4.Value));
                if (suspectPosition != 5)
                    lpl.Create(lineupId, Convert.ToInt32(hdPos5.Value));

                Response.Redirect("default.aspx");
            }
        }

        private bool allPhotosSelected()
        {
            int suspectPosition = Convert.ToInt32(ddlPosition.SelectedValue);
            bool isValid = true;

            if (suspectPosition != 0)
                isValid = isValid && (Convert.ToInt32(hdPos0.Value) > 0);
            if (suspectPosition != 1)
                isValid = isValid && (Convert.ToInt32(hdPos1.Value) > 0);
            if (suspectPosition != 2)
                isValid = isValid && (Convert.ToInt32(hdPos2.Value) > 0);
            if (suspectPosition != 3)
                isValid = isValid && (Convert.ToInt32(hdPos3.Value) > 0);
            if (suspectPosition != 4)
                isValid = isValid && (Convert.ToInt32(hdPos4.Value) > 0);
            if (suspectPosition != 5)
                isValid = isValid && (Convert.ToInt32(hdPos5.Value) > 0);

            return isValid;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindPhotos();
        }

        private void ddlToDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPhotos();
        }

        private void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            d.Lineup lineup = new d.Lineup(getLineupId());
            BindLineup(lineup.SuspectId);
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.ddlToDisplay.SelectedIndexChanged += new EventHandler(ddlToDisplay_SelectedIndexChanged);
            this.ddlPosition.SelectedIndexChanged += new EventHandler(ddlPosition_SelectedIndexChanged);
            base.OnInit(e);
        }
    }
}
