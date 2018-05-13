using System;
using System.IO;
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

namespace Ei.Website.settings
{
    public class photoCategorize : b.AddBasePage
    {
        protected Repeater rptPhotos;
        protected Literal ltrJavaScript;

        private DataTable dtRace;
        private DataTable dtHair;
        private DataTable dtAge;
        private DataTable dtWeight;
        private d.VisualCriteriaData vcd;
        private StringBuilder sbJavaScript;
        private int photosToDisplay;

        private const string GENDER_TYPE = "gender";
        private const string RACE_TYPE = "race";
        private const string HAIR_TYPE = "hair";
        private const string AGE_TYPE = "age";
        private const string WEIGHT_TYPE = "weight";
        private const string RADIO_HTML = "\n<p style='margin-bottom:0px;margin-top:3px;'><input id='p{0}_{1}_{2}' type='radio' name='p{0}_{1}' value='{2}' onChange=\"setValue('p{0}_{1}_hd', {2});\"/><span style='font-size:11px;'>{3}</span></p>";


        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        protected override string ItemName { get { return "10 photos"; } }

        protected override string RedirectPage { get { return "photoAdd.aspx"; } }

        protected override void ResetFormFields()
        {
            BindData();
        }

        protected override int SaveItem()
        {
            d.Photo photo;
            string photoId;
            DataTable dtPhotos = new d.PhotoData().GetNextNUncategorized(photosToDisplay);
            foreach (DataRow dr in dtPhotos.Rows)
            {
                photo = new d.Photo(Convert.ToInt32(dr[0]));
                photoId = photo.Id.ToString();
                photo.Gender = Page.Request[makeHiddenId(photoId, GENDER_TYPE)].Replace("'", "");
                photo.RaceId = Convert.ToInt32(Page.Request[makeHiddenId(photoId, RACE_TYPE)]);
                photo.HairColorId = Convert.ToInt32(Page.Request[makeHiddenId(photoId, HAIR_TYPE)]);
                photo.AgeRangeId = Convert.ToInt32(Page.Request[makeHiddenId(photoId, AGE_TYPE)]);
                photo.WeightRangeId = Convert.ToInt32(Page.Request[makeHiddenId(photoId, WEIGHT_TYPE)]);
                photo.IsCategorized = true;
                photo.ModifiedBy = Identity.UserId;
                photo.Update();
            }
            return 1;
        }

        private int getRaceId(int photoId)
        {
            return Convert.ToInt32(Page.Request[""]);
        }

        private void BindData()
        {
            generateJavascript1();
            btnSave.Attributes.Add("onClick", "return validateAll();");
            rptPhotos.DataSource = new d.PhotoData().GetNextNUncategorized(photosToDisplay);
            rptPhotos.DataBind();
            sbJavaScript.Remove(sbJavaScript.Length - 4, 4);
            generateJavascript2();
            ltrJavaScript.Text = sbJavaScript.ToString();
        }

        private void generateJavascript1()
        {
            sbJavaScript.Append("\n<script language=\"javascript\" type=\"text/javascript\">");
            sbJavaScript.Append("\n\tfunction setValue(hid, val) ");
            sbJavaScript.Append("\n\t{ ");
            sbJavaScript.Append("\n\t\tdocument.getElementById(hid).value = val; ");
            sbJavaScript.Append("\n\t} ");
            sbJavaScript.Append("\n\tfunction validateAll() ");
            sbJavaScript.Append("\n\t{ ");
            sbJavaScript.Append("\n\t\tif (");
        }

        private void generateJavascript2()
        {
            sbJavaScript.Append(") ");
            sbJavaScript.Append("\n\t\t{ ");
            sbJavaScript.Append("\n\t\t\talert('Please make ALL selections'); ");
            sbJavaScript.Append("\n\t\t\treturn false; ");
            sbJavaScript.Append("\n\t\t} ");
            sbJavaScript.Append("\n\t\treturn true; ");
            sbJavaScript.Append("\n\t} ");
            sbJavaScript.Append("\n</script>");
        }

        private void rptPhotos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                string photoId = DataBinder.Eval(e.Item.DataItem, "id").ToString();
                Literal ltrPhoto = (Literal)e.Item.FindControl("ltrPhoto");
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<img src='{0}{1}{2}.jpg' width='120px' style='border:1px solid #777777;'>",
                    b.ConfigurationHelper.PhotosDirectoryUrl, photoId, b.ImageHelper.SMALL_PHOTO_SUFFIX);
                ltrPhoto.Text = sb.ToString();

                bindGender(photoId, (Literal)e.Item.FindControl("ltrGender"));
                bindItems(photoId, (Literal)e.Item.FindControl("ltrRace"), RACE_TYPE, dtRace);
                bindItems(photoId, (Literal)e.Item.FindControl("ltrHair"), HAIR_TYPE, dtHair);
                bindItems(photoId, (Literal)e.Item.FindControl("ltrAge"), AGE_TYPE, dtAge);
                bindItems(photoId, (Literal)e.Item.FindControl("ltrWeight"), WEIGHT_TYPE, dtWeight);
            }
        }

        private void bindItems(string photoId, Literal ltrToBuild, string itemType, DataTable dtItems)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dtItems.Rows)
                sb.AppendFormat(RADIO_HTML, photoId, itemType, dr[0].ToString(), dr[1].ToString());

            sb.AppendFormat("\n<input type='hidden' id=\"{0}\" name=\"{0}\" runat='server'/>", makeHiddenId(photoId, itemType));
            sbJavaScript.AppendFormat("\n\t\tdocument.getElementById(\"{0}\").value == \"\" || ", makeHiddenId(photoId, itemType));
            ltrToBuild.Text = sb.ToString();
        }

        private void bindGender(string photoId, Literal ltrToBuild)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(RADIO_HTML, photoId, GENDER_TYPE, "'m'", "male");
            sb.AppendFormat(RADIO_HTML, photoId, GENDER_TYPE, "'f'", "female");

            sb.AppendFormat("\n<input type='hidden' id=\"{0}\" name=\"{0}\" runat='server'/>", makeHiddenId(photoId, GENDER_TYPE));
            sbJavaScript.AppendFormat("\n\t\tdocument.getElementById(\"{0}\").value == \"\" || ", makeHiddenId(photoId, GENDER_TYPE));
            ltrToBuild.Text = sb.ToString();
        }

        private string makeHiddenId(string photoId, string itemType)
        {
            return "p" + photoId + "_" + itemType + "_hd";
        }

        private int getHiddenValue(string photoId, string valueType)
        {
            return Convert.ToInt32(Page.Request["p" + photoId + "_" + valueType + "_hd"]);
        }

        protected override int PageId { get { return 51; } }

        protected override void OnInit(EventArgs e)
        {
            vcd = new d.VisualCriteriaData();
            dtRace = vcd.GetRaceList();
            dtHair = vcd.GetHairColorList();
            dtAge = vcd.GetAgeRangeList();
            dtWeight = vcd.GetWeightRangeList();
            sbJavaScript = new StringBuilder();
            photosToDisplay = 10;
            this.rptPhotos.ItemDataBound += new RepeaterItemEventHandler(rptPhotos_ItemDataBound);
            base.OnInit(e);
        }
    }
}
