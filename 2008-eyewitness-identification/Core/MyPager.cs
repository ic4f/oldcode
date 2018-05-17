using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Core
{
    public class MyPager : System.Web.UI.WebControls.WebControl
    {
        private const int DEFAULT_PAGESIZE = 50;
        private const string IMAGE_PATH = "../_gridhelper/pager/";
        private const string BUTTON_FIRST = "first.gif";
        private const string BUTTON_PREV = "prev.gif";
        private const string BUTTON_NEXT = "next.gif";
        private const string BUTTON_LAST = "last.gif";
        private const string BUTTON_FIRST_A = "first_a.gif";
        private const string BUTTON_PREV_A = "prev_a.gif";
        private const string BUTTON_NEXT_A = "next_a.gif";
        private const string BUTTON_LAST_A = "last_a.gif";

        protected ImageButton btnFirst;
        protected ImageButton btnPrev;
        protected ImageButton btnNext;
        protected ImageButton btnLast;
        protected DropDownList ddlPageSize;
        protected DropDownList ddlPages;
        protected Label lblTotalPages;

        private BindingDelegate bind;

        private int totalRecords;
        private int firstRecord;
        private int lastRecord;

        public BindingDelegate BindingMethod { set { bind = value; } }

        public void AdjustAfterBinding(int total)
        {
            totalRecords = total;
            int totalPages = (int)Math.Ceiling((double)totalRecords / PageSize);
            SetPagerButtons(totalPages);
            SetPagerNumbers(totalPages);
        }

        public void Reset()
        {
            ddlPages.SelectedValue = "1";
        }

        public int PageSize
        {
            get
            {
                if (ddlPageSize.SelectedValue == "")
                    return DEFAULT_PAGESIZE;
                else
                    return Convert.ToInt32(ddlPageSize.SelectedValue);
            }
        }

        public int CurrentPage
        {
            get
            {
                if (ddlPages.SelectedValue == "")
                    return 1;
                else
                    return Convert.ToInt32(ddlPages.SelectedValue);
            }
        }

        private void SetPagerButtons(int totalPages)
        {
            btnFirst.ImageUrl = IMAGE_PATH + BUTTON_FIRST_A;
            btnPrev.ImageUrl = IMAGE_PATH + BUTTON_PREV_A;
            btnNext.ImageUrl = IMAGE_PATH + BUTTON_NEXT_A;
            btnLast.ImageUrl = IMAGE_PATH + BUTTON_LAST_A;

            if (CurrentPage == 1)
            {
                btnFirst.ImageUrl = IMAGE_PATH + BUTTON_FIRST;
                btnPrev.ImageUrl = IMAGE_PATH + BUTTON_PREV;
            }
            if (CurrentPage == totalPages)
            {
                btnNext.ImageUrl = IMAGE_PATH + BUTTON_NEXT;
                btnLast.ImageUrl = IMAGE_PATH + BUTTON_LAST;
            }
        }

        private void SetPagerNumbers(int totalPages)
        {
            lblTotalPages.Text = totalPages.ToString();
            firstRecord = PageSize * (CurrentPage - 1) + 1;
            lastRecord = Math.Min(firstRecord + PageSize - 1, totalRecords);

            lblTotalPages.Text = totalPages.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CreateChildControls();
        }

        protected override void CreateChildControls()
        {
            lblTotalPages = new Label();

            ddlPageSize = new DropDownList();
            ddlPageSize.AutoPostBack = true;
            ddlPageSize.SelectedIndexChanged += new EventHandler(ddlPageSize_SelectedIndexChanged);

            ddlPages = new DropDownList();
            ddlPages.AutoPostBack = true;
            ddlPages.SelectedIndexChanged += new EventHandler(ddlPages_SelectedIndexChanged);

            btnFirst = new ImageButton();
            btnFirst.Click += new ImageClickEventHandler(btnFirst_Click);

            btnPrev = new ImageButton();
            btnPrev.Click += new ImageClickEventHandler(btnPrev_Click);

            btnNext = new ImageButton();
            btnNext.Click += new ImageClickEventHandler(btnNext_Click);

            btnLast = new ImageButton();
            btnLast.Click += new ImageClickEventHandler(btnLast_Click);

            Controls.Add(lblTotalPages);
            Controls.Add(ddlPageSize);
            Controls.Add(ddlPages);
            Controls.Add(btnFirst);
            Controls.Add(btnPrev);
            Controls.Add(btnNext);
            Controls.Add(btnLast);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitializePageSizeList();
                InitializePagesList();
            }
            base.OnLoad(e);
        }

        private void InitializePageSizeList()
        {
            for (int i = 50; i < 501; i += 50)
                ddlPageSize.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }

        private void InitializePagesList()
        {
            int totalPages = (int)Math.Ceiling((double)totalRecords / PageSize);
            ddlPages.Items.Clear();
            for (int i = 1; i <= totalPages; i++)
                ddlPages.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }

        public override ControlCollection Controls
        {
            get
            {
                base.EnsureChildControls();
                return base.Controls;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("border: solid 1px #999999; border-collapse:collapse; background-color: #cacaca; ");
            sb.Append("width: 100%; font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 8pt;");

            writer.Write("<table style=\"" + sb.ToString() + "\" cellpadding=\"1\" cellspacing=\"0\" width=\"100%\">");
            writer.Write("<tr>");
            writer.Write("<td height=\"25\">&nbsp;Results <b>");
            writer.Write(firstRecord.ToString());
            writer.Write("-");
            writer.Write(lastRecord.ToString());
            writer.Write("</b> of <b>");
            writer.Write(totalRecords.ToString());
            writer.Write("</b></td>");
            writer.Write("<td width=\"30\"></td>");
            writer.Write("<td align=\"right\">");
            writer.Write("<table style=\"background-color: #cacaca;font-size:8pt;\" cellpadding=\"0\" cellspacing=\"0\"><tr>");
            writer.Write("<td>Rows to display:&nbsp;");
            ddlPageSize.RenderControl(writer);
            writer.Write("</td><td width=\"30\"></td><td>Page ");
            ddlPages.RenderControl(writer);
            writer.Write(" of ");
            lblTotalPages.RenderControl(writer);
            writer.Write("</td><td width=\"30\"></td><td>");
            btnFirst.RenderControl(writer);
            writer.Write("&nbsp;");
            btnPrev.RenderControl(writer);
            writer.Write("&nbsp;");
            btnNext.RenderControl(writer);
            writer.Write("&nbsp;");
            btnLast.RenderControl(writer);
            writer.Write("&nbsp;</td></tr></table></td>");
            writer.Write("</tr>");
            writer.Write("</table>");
        }

        private void btnFirst_Click(Object sender, ImageClickEventArgs e)
        {
            ddlPages.SelectedValue = "1";
            bind();
        }

        private void btnPrev_Click(Object sender, ImageClickEventArgs e)
        {
            int currPage = Convert.ToInt32(ddlPages.SelectedValue);
            ddlPages.SelectedValue = (Math.Max(1, currPage - 1)).ToString();
            bind();
        }

        private void btnNext_Click(Object sender, ImageClickEventArgs e)
        {
            int currPage = Convert.ToInt32(ddlPages.SelectedValue);
            ddlPages.SelectedValue = Math.Min(currPage + 1, Convert.ToInt32(lblTotalPages.Text)).ToString();
            bind();
        }

        private void btnLast_Click(Object sender, ImageClickEventArgs e)
        {
            ddlPages.SelectedValue = lblTotalPages.Text;
            bind();
        }

        private void ddlPageSize_SelectedIndexChanged(Object sender, EventArgs e)
        {
            bind();
            InitializePagesList();
            ddlPages.SelectedValue = "1";
        }

        private void ddlPages_SelectedIndexChanged(Object sender, EventArgs e)
        {
            bind();
        }
    }
}
