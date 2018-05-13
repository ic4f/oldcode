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
using c = Core;
using d = Ei.Data;

namespace Ei.Website.settings
{
    public class photoList : b.BasePage
    {
        protected CheckBoxList cblGender;
        protected CheckBoxList cblRace;
        protected CheckBoxList cblHair;
        protected CheckBoxList cblAge;
        protected CheckBoxList cblWeight;
        protected Button btnSearch;
        protected c.MyPager pager;
        protected c.MyGrid dgrPhotos;
        protected Panel pnlGrid;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            pager.BindingMethod = new c.BindingDelegate(BindData);
            dgrPhotos.BindingMethod = new c.BindingDelegate(BindData);
            dgrPhotos.SetPager(pager);

            if (!Page.IsPostBack)
            {
                dgrPhotos.SortColumnIndex = 9;
                dgrPhotos.SortOrder = " desc";
                BindData();
            }
        }

        private void BindData()
        {
            BindLists();
            BindGrid();
        }

        private void BindLists()
        {
            d.VisualCriteriaData vcd = new d.VisualCriteriaData();

            cblGender.Items.Clear();
            cblGender.Items.Add(new ListItem("male", "'m'"));
            cblGender.Items.Add(new ListItem("female", "'f'"));

            cblRace.DataSource = vcd.GetRaceList();
            cblRace.DataTextField = "description";
            cblRace.DataValueField = "id";
            cblRace.DataBind();

            cblHair.DataSource = vcd.GetHairColorList();
            cblHair.DataTextField = "description";
            cblHair.DataValueField = "id";
            cblHair.DataBind();

            cblAge.DataSource = vcd.GetAgeRangeList();
            cblAge.DataTextField = "description";
            cblAge.DataValueField = "id";
            cblAge.DataBind();

            cblWeight.DataSource = vcd.GetWeightRangeList();
            cblWeight.DataTextField = "description";
            cblWeight.DataValueField = "id";
            cblWeight.DataBind();
        }

        private void BindGrid()
        {
            DataSet ds = new d.PhotoData().GetPhotosWithQueryP(makeQuery(), pager.PageSize, pager.CurrentPage);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlGrid.Visible = true;
                dgrPhotos.Prefix = LinkPrefix;
                dgrPhotos.DataSource = ds.Tables[0];
                dgrPhotos.DataBind();
                pager.AdjustAfterBinding(Convert.ToInt32(ds.Tables[1].Rows[0][0]));
            }
            else
                pnlGrid.Visible = false;
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
            sb.AppendFormat(" order by {0} ", dgrPhotos.SortExpression);
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

        private void dgrPhotos_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                int photoId = Convert.ToInt16(dgrPhotos.DataKeys[e.Item.ItemIndex]);
                e.Item.Cells[0].Text = "<img src='" + b.UrlHelper.GetPhotoImageUrl(photoId, b.ImageHelper.SMALL_PHOTO_SUFFIX) + "' border='1'>";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.dgrPhotos.ItemDataBound += new DataGridItemEventHandler(dgrPhotos_ItemDataBound);
            base.OnInit(e);
        }

        protected override int PageId { get { return 46; } }
    }
}
