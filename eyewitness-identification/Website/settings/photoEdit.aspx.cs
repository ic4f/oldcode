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

namespace Ei.Website.settings
{
    public class photoEdit : b.EditBasePage
    {
        protected TextBox tbxNumber;
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
            d.Photo photo = new d.Photo(GetId());
            tbxNumber.Text = photo.ExternalReference;
            string urlMedium = b.UrlHelper.GetPhotoImageUrl(GetId(), b.ImageHelper.MEDIUM_PHOTO_SUFFIX);
            ltrPhoto.Text = "<img src='" + urlMedium + "' align='right' border='1'>";

            d.VisualCriteriaData vcd = new d.VisualCriteriaData();

            ddlGender.Items.Clear();
            ddlGender.Items.Add(new ListItem("male", "m"));
            ddlGender.Items.Add(new ListItem("female", "f"));
            ddlGender.SelectedValue = photo.Gender;

            ddlRace.DataSource = vcd.GetRaceList();
            ddlRace.DataTextField = "description";
            ddlRace.DataValueField = "id";
            ddlRace.DataBind();
            ddlRace.SelectedValue = photo.RaceId.ToString();

            ddlHairColor.DataSource = vcd.GetHairColorList();
            ddlHairColor.DataTextField = "description";
            ddlHairColor.DataValueField = "id";
            ddlHairColor.DataBind();
            ddlHairColor.SelectedValue = photo.HairColorId.ToString();

            ddlAgeRange.DataSource = vcd.GetAgeRangeList();
            ddlAgeRange.DataTextField = "description";
            ddlAgeRange.DataValueField = "id";
            ddlAgeRange.DataBind();
            ddlAgeRange.SelectedValue = photo.AgeRangeId.ToString();

            ddlWeightRange.DataSource = vcd.GetWeightRangeList();
            ddlWeightRange.DataTextField = "description";
            ddlWeightRange.DataValueField = "id";
            ddlWeightRange.DataBind();
            ddlWeightRange.SelectedValue = photo.WeightRangeId.ToString();
        }

        protected override int SaveItem()
        {
            int photoId = GetId();
            d.Photo photo = new d.Photo(photoId);
            photo.ExternalReference = tbxNumber.Text.Trim();
            photo.Gender = ddlGender.SelectedValue;
            photo.RaceId = Convert.ToInt32(ddlRace.SelectedValue);
            photo.HairColorId = Convert.ToInt32(ddlHairColor.SelectedValue);
            photo.AgeRangeId = Convert.ToInt32(ddlAgeRange.SelectedValue);
            photo.WeightRangeId = Convert.ToInt32(ddlWeightRange.SelectedValue);
            photo.ModifiedBy = Identity.UserId;
            photo.Update();
            return 1;
        }

        protected override void ResetFormFields() { BindData(); }

        protected override string ItemName { get { return "photo"; } }

        protected override string RedirectPage { get { return "photoList.aspx"; } }

        protected override int PageId { get { return 47; } }
    }
}
