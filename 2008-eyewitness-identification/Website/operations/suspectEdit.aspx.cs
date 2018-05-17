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
    public class suspectEdit : b.EditBasePage
    {
        protected TextBox tbxNumber;
        protected TextBox tbxDescription;
        protected CustomValidator valUnique;
        protected CustomValidator valFileType;
        protected HtmlInputFile tbxFile;
        protected DropDownList ddlGender;
        protected DropDownList ddlRace;
        protected DropDownList ddlHairColor;
        protected DropDownList ddlAgeRange;
        protected DropDownList ddlWeightRange;
        protected Literal ltrPhoto;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            d.Suspect suspect = new d.Suspect(GetId());
            tbxNumber.Text = suspect.Number;
            tbxDescription.Text = suspect.Description;
            string urlMedium = b.UrlHelper.GetSuspectImageUrl(GetId(), b.ImageHelper.MEDIUM_PHOTO_SUFFIX);
            ltrPhoto.Text = "<img src='" + urlMedium + "' align='right' border='1'>";

            d.VisualCriteriaData vcd = new d.VisualCriteriaData();

            ddlGender.Items.Clear();
            ddlGender.Items.Add(new ListItem("male", "m"));
            ddlGender.Items.Add(new ListItem("female", "f"));
            ddlGender.SelectedValue = suspect.Gender;

            ddlRace.DataSource = vcd.GetRaceList();
            ddlRace.DataTextField = "description";
            ddlRace.DataValueField = "id";
            ddlRace.DataBind();
            ddlRace.SelectedValue = suspect.RaceId.ToString();

            ddlHairColor.DataSource = vcd.GetHairColorList();
            ddlHairColor.DataTextField = "description";
            ddlHairColor.DataValueField = "id";
            ddlHairColor.DataBind();
            ddlHairColor.SelectedValue = suspect.HairColorId.ToString();

            ddlAgeRange.DataSource = vcd.GetAgeRangeList();
            ddlAgeRange.DataTextField = "description";
            ddlAgeRange.DataValueField = "id";
            ddlAgeRange.DataBind();
            ddlAgeRange.SelectedValue = suspect.AgeRangeId.ToString();

            ddlWeightRange.DataSource = vcd.GetWeightRangeList();
            ddlWeightRange.DataTextField = "description";
            ddlWeightRange.DataValueField = "id";
            ddlWeightRange.DataBind();
            ddlWeightRange.SelectedValue = suspect.WeightRangeId.ToString();
        }

        protected override int SaveItem()
        {
            int suspectId = GetId();
            d.Suspect suspect = new d.Suspect(suspectId);
            suspect.Number = tbxNumber.Text.Trim();
            suspect.Description = tbxDescription.Text.Trim();
            suspect.Gender = ddlGender.SelectedValue;
            suspect.RaceId = Convert.ToInt32(ddlRace.SelectedValue);
            suspect.HairColorId = Convert.ToInt32(ddlHairColor.SelectedValue);
            suspect.AgeRangeId = Convert.ToInt32(ddlAgeRange.SelectedValue);
            suspect.WeightRangeId = Convert.ToInt32(ddlWeightRange.SelectedValue);
            suspect.ModifiedBy = Identity.UserId;

            if (suspect.Update() < 0)
            {
                valUnique.IsValid = false;
                return 0;
            }

            if (tbxFile.PostedFile.ContentLength > 0)
            {
                FileInfo fi = new FileInfo(tbxFile.PostedFile.FileName);
                if (!b.ImageHelper.IsImage(fi.Extension))
                {
                    valFileType.IsValid = false;
                    return 0;
                }
                else
                {
                    double ratio = b.ImageHelper.DIMENSIONS_RATIO;

                    int sWidth = b.ImageHelper.SMALL_PHOTO_WIDTH;
                    int sHeight = (int)(sWidth / ratio);
                    string sPath = Server.MapPath(b.UrlHelper.GetSuspectImageUrl(suspectId, b.ImageHelper.SMALL_PHOTO_SUFFIX));

                    int mWidth = b.ImageHelper.MEDIUM_PHOTO_WIDTH;
                    int mHeight = (int)(mWidth / ratio);
                    string mPath = Server.MapPath(b.UrlHelper.GetSuspectImageUrl(suspectId, b.ImageHelper.MEDIUM_PHOTO_SUFFIX));

                    int lWidth = b.ImageHelper.LARGE_PHOTO_WIDTH;
                    int lHeight = (int)(lWidth / ratio);
                    string lPath = Server.MapPath(b.UrlHelper.GetSuspectImageUrl(suspectId, b.ImageHelper.LARGE_PHOTO_SUFFIX));

                    b.ImageHelper iHelper = new b.ImageHelper();
                    iHelper.MakeImage(tbxFile.PostedFile, sPath, sWidth, sHeight, ratio);
                    iHelper.MakeImage(tbxFile.PostedFile, mPath, mWidth, mHeight, ratio);
                    iHelper.MakeImage(tbxFile.PostedFile, lPath, lWidth, lHeight, ratio);
                }
            }
            return 1;
        }

        protected override void ResetFormFields() { BindData(); }

        protected override string ItemName { get { return "suspect"; } }

        protected override string RedirectPage { get { return "suspectList.aspx"; } }

        protected override int PageId { get { return 23; } }
    }
}
