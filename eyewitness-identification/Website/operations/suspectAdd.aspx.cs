using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
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
    public class suspectAdd : b.AddBasePage
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

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            d.VisualCriteriaData vcd = new d.VisualCriteriaData();

            ddlGender.Items.Clear();
            ddlGender.Items.Add(new ListItem("select one...", ""));
            ddlGender.Items.Add(new ListItem("male", "m"));
            ddlGender.Items.Add(new ListItem("female", "f"));

            ddlRace.Items.Clear();
            ddlRace.DataSource = vcd.GetRaceList();
            ddlRace.DataTextField = "description";
            ddlRace.DataValueField = "id";
            ddlRace.DataBind();
            ddlRace.Items.Insert(0, new ListItem("select one...", ""));

            ddlHairColor.Items.Clear();
            ddlHairColor.DataSource = vcd.GetHairColorList();
            ddlHairColor.DataTextField = "description";
            ddlHairColor.DataValueField = "id";
            ddlHairColor.DataBind();
            ddlHairColor.Items.Insert(0, new ListItem("select one...", ""));

            ddlAgeRange.Items.Clear();
            ddlAgeRange.DataSource = vcd.GetAgeRangeList();
            ddlAgeRange.DataTextField = "description";
            ddlAgeRange.DataValueField = "id";
            ddlAgeRange.DataBind();
            ddlAgeRange.Items.Insert(0, new ListItem("select one...", ""));

            ddlWeightRange.Items.Clear();
            ddlWeightRange.DataSource = vcd.GetWeightRangeList();
            ddlWeightRange.DataTextField = "description";
            ddlWeightRange.DataValueField = "id";
            ddlWeightRange.DataBind();
            ddlWeightRange.Items.Insert(0, new ListItem("select one...", ""));
        }

        protected override string ItemName { get { return "suspect"; } }

        protected override string RedirectPage { get { return "suspectList.aspx"; } }

        protected override int SaveItem()
        {
            string number = tbxNumber.Text.Trim();
            string description = tbxDescription.Text.Trim();
            string gender = ddlGender.SelectedValue;
            int raceId = Convert.ToInt32(ddlRace.SelectedValue);
            int hairColorId = Convert.ToInt32(ddlHairColor.SelectedValue);
            int ageRangeId = Convert.ToInt32(ddlAgeRange.SelectedValue);
            int weightRangeId = Convert.ToInt32(ddlWeightRange.SelectedValue);

            int suspectId = new d.SuspectData().Create(
                number, description, gender, raceId, hairColorId, ageRangeId, weightRangeId, Identity.UserId);
            if (suspectId < 0)
            {
                valUnique.IsValid = false;
                return 0;
            }
            else
            {
                FileInfo fi = new FileInfo(tbxFile.PostedFile.FileName);
                if (!b.ImageHelper.IsImage(fi.Extension))
                {
                    valFileType.IsValid = false;
                    return 0;
                }

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
            return 1;
        }

        protected override void ResetFormFields()
        {
            tbxNumber.Text = "";
            tbxDescription.Text = "";
        }

        protected override int PageId { get { return 20; } }
    }
}
