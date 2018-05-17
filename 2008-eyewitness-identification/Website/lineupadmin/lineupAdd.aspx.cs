using System;
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
    public class lineupAdd : b.BasePage
    {
        protected TextBox tbxDescription;
        protected TextBox tbxNotes;
        protected DropDownList ddlCase;
        protected DropDownList ddlSuspect;
        protected Label lblSuspect;
        protected Label lblGender;
        protected Label lblRace;
        protected Label lblHair;
        protected Label lblAge;
        protected Label lblWeight;
        protected Label lblNotes;
        protected Button btnSave;
        protected Button btnCancel;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            ddlCase.DataSource = new d.CaseData().GetCasesWithSuspectsByUser(Identity.UserId);
            ddlCase.DataTextField = "externalref";
            ddlCase.DataValueField = "id";
            ddlCase.DataBind();
            ddlCase.AutoPostBack = true;

            BindSuspects();
            BindSuspect();
        }

        private void BindSuspects()
        {
            if (ddlCase.SelectedValue != "")
            {
                ddlSuspect.DataSource = new d.SuspectData().GetSuspectsByCase(Convert.ToInt32(ddlCase.SelectedValue));
                ddlSuspect.DataTextField = "externalref";
                ddlSuspect.DataValueField = "id";
                ddlSuspect.DataBind();
                ddlSuspect.AutoPostBack = true;
            }
        }

        private void BindSuspect()
        {
            if (ddlSuspect.SelectedValue != "")
            {
                int suspectId = Convert.ToInt32(ddlSuspect.SelectedValue);
                double width = b.ImageHelper.MEDIUM_PHOTO_WIDTH;
                double height = width * 0.75;
                string suspectUrl = b.UrlHelper.GetSuspectImageUrl(suspectId, b.ImageHelper.MEDIUM_PHOTO_SUFFIX);
                lblSuspect.Text = "<img src='" + suspectUrl + "' border='1'>";
                d.Suspect s = new d.Suspect(suspectId);
                lblGender.Text = s.Gender;
                lblRace.Text = s.Race;
                lblHair.Text = s.HairColor;
                lblAge.Text = s.AgeRange;
                lblWeight.Text = s.WeightRange;
                lblNotes.Text = s.Description;
            }
        }

        private int getCaseId()
        {
            return Convert.ToInt32(ddlCase.SelectedValue);
        }

        private void ddlSuspect_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSuspect();
        }

        private void ddlCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSuspects();
            BindSuspect();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int lineupId = new d.LineupData().Create(tbxDescription.Text.Trim(), tbxNotes.Text.Trim(),
                    Convert.ToInt32(ddlCase.SelectedValue), Convert.ToInt32(ddlSuspect.SelectedValue), Identity.UserId);
                Response.Redirect("lineupAdd2.aspx?Id=" + lineupId);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected override int PageId { get { return 5; } }

        protected override void OnInit(EventArgs e)
        {
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.ddlCase.SelectedIndexChanged += new EventHandler(ddlCase_SelectedIndexChanged);
            this.ddlSuspect.SelectedIndexChanged += new EventHandler(ddlSuspect_SelectedIndexChanged);
            base.OnInit(e);
        }
    }
}
