using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebControls
{
    /// <summary>
    /// Base class for Combo implementations
    /// Functionality: paging, sorting, disable paging/sorting, get Id of a record
    /// </summary>
    public abstract class Combo : WebControl
    {
        public const string JS_POST_PAGE = "ComboPostPage";
        public const string JS_ROW_MOUSEOVER = "ComboRowMouseover";
        public const string JS_CHANGE_PAGE = "ComboChangePage";
        public const string JS_CHANGE_SORT_INDEX = "ComboChangeSort";
        public const string JS_DISABLE_PAGER = "ComboDisablePager";
        public const string JS_DISABLE_SORT = "ComboDisableSort";
        public const string JS_GET_ID = "ComboGetId";
        public const string JS_SELECT_ROW = "ComboSelectRow";
        public const string JS_CONFIRM_DELETE = "comboConfirmDelete";
        public const string JS_DISABLE_BUTTONS = "ComboDisableButtons";

        public const int MAX_NUMBER_OF_FIELDS = 30;
        public const int DEFAULT_PAGE_SIZE = 50;

        #region public properties: cssstyles
        /// <summary>
        /// CSS class for entire combo (outer table or division) 
        /// </summary>
        public string CssClassCombo
        {
            get { return cssClassCombo; }
            set { cssClassCombo = value; }
        }

        /// <summary>
        /// CSS class for the grid part of the combo
        /// </summary>
        public string CssClassGrid
        {
            get { return cssClassGrid; }
            set { cssClassGrid = value; }
        }

        /// <summary>
        /// CSS class for the grid'a header
        /// </summary>
        public string CssClassGridHeader
        {
            get { return cssClassGridHeader; }
            set { cssClassGridHeader = value; }
        }

        /// <summary>
        /// CSS class fot the pager
        /// </summary>
        public string CssClassPager
        {
            get { return cssClassPager; }
            set { cssClassPager = value; }
        }
        #endregion

        #region public methods and evens

        public event CommandEventHandler Command;

        public event DataGridItemEventHandler ItemDataBound;

        public string NoRecordsMessage
        {
            get { return noRecordsMessage; }
            set { noRecordsMessage = value; }
        }

        protected int TotalPages() { return (int)Math.Ceiling((double)TotalRecords() / PageSize); }

        public string GetInitScript()
        {
            string nameOfJsButtonArray = "arrRowCommandButtons";

            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\" type=\"text/javascript\">");
            sb.Append(InitJsRowActionButtonsArray(nameOfJsButtonArray));
            sb.Append("\n\t\t\tInitComboHelper(");
            sb.AppendFormat("\"{0}\", ", formId);
            sb.AppendFormat("\"{0}\", ", dataGrid.ID);
            sb.AppendFormat("\"{0}\", ", idPagerDropDownList);
            sb.AppendFormat("\"{0}\", ", hiddenFieldIdList);
            sb.AppendFormat("\"{0}\", ", hiddenFieldSelectedRow);
            sb.AppendFormat("\"{0}\", ", hiddenFieldSelectedId);
            sb.AppendFormat("\"{0}\", ", hiddenFieldSortColumnIndex);
            sb.AppendFormat("\"{0}\", ", hiddenFieldPageNumber);
            sb.AppendFormat("\"{0}\", ", prefixRowId);
            sb.AppendFormat("\"{0}\", ", prefixHiddenFieldSortOrder);
            sb.AppendFormat("\"{0}\", ", prefixColumnHeaderId);
            sb.AppendFormat("\"{0}\", ", TotalPages());
            sb.AppendFormat("\"{0}\", ", MAX_NUMBER_OF_FIELDS);
            sb.AppendFormat("\"{0}\", ", idImgFirst);
            sb.AppendFormat("\"{0}\", ", idImgPrev);
            sb.AppendFormat("\"{0}\", ", idImgNext);
            sb.AppendFormat("\"{0}\", ", idImgLast);
            sb.AppendFormat("\"{0}\", ", imgFirst);
            sb.AppendFormat("\"{0}\", ", imgPrev);
            sb.AppendFormat("\"{0}\", ", imgNext);
            sb.AppendFormat("\"{0}\", ", imgLast);
            sb.AppendFormat("{0}, ", nameOfJsButtonArray);
            sb.AppendFormat("\"{0}\", ", GetJsUpdateButtonId());
            sb.AppendFormat("\"{0}\", ", GetJsResetButtonId());
            sb.AppendFormat("\"{0}\", ", GetJsAddAllButtonId());
            sb.AppendFormat("\"{0}\");", GetJsRemoveAllButtonId());
            sb.Append("\n\t\t</script>\n");
            return sb.ToString();
        }

        public string PagerImagePath
        {
            get { return pagerImagePath; }
            set { pagerImagePath = value; }
        }

        public string DefaultSortExpression
        {
            get
            {
                if (defaultSortExpression != null)
                    return defaultSortExpression;
                else
                    return fields[0].SortExpression;
            }
            set { defaultSortExpression = value; }
        }

        public string DefaultSortExpressionDisplay
        {
            get
            {
                if (defaultSortExpressionDisplay != null)
                    return defaultSortExpressionDisplay;
                else
                    return fields[0].Display;
            }
            set { defaultSortExpressionDisplay = value; }
        }

        public string SortExpression
        {
            get
            {
                int currSort = SortColumnIndex();
                if (currSort > -1)
                {
                    string result = fields[currSort].SortExpression;
                    if (GetSortOrder(currSort) == 0)
                        return result += " desc";
                    else
                        return result += " asc";
                }
                else
                    return DefaultSortExpression;
            }
        }

        public object DataSource { get { return dataGrid.DataSource; } }

        public void SetDataSource(IDataTable ds, string dataKeyField)
        {
            dataGrid.DataSource = ds;
            dataGrid.DataKeyField = dataKeyField;
            totalRowCount = ds.TotalRowCount;
        }

        public void SetDataSource(IEnumerable ds, string dataKeyField, int totalRowCount)
        {
            dataGrid.DataSource = ds;
            dataGrid.DataKeyField = dataKeyField;
            this.totalRowCount = totalRowCount;
        }

        public void SetDataSource(DataTable ds, string dataKeyField, int totalRowCount)
        {
            dataGrid.DataSource = ds;
            dataGrid.DataKeyField = dataKeyField;
            this.totalRowCount = totalRowCount;
        }

        public override void DataBind()
        {
            dataGrid.DataBind();
            idList = DataKeyList;
        }

        /// <summary>
        /// This property can be only set before calling DataBind()
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        /// <summary>
        /// Returns current page number, 1-based
        /// </summary>
        public int PageNumber { get { return GetNumericFieldValue(hiddenFieldPageNumber); } }

        /// <summary>
        /// Adds an attribute to a clickable control (javascript)
        /// When the control is clicked, the current page number is reset to 1 and the page is posted to the server
        /// </summary>
        /// <param name="c"></param>
        public void RegisterPostbackControl(WebControl c)
        {
            c.Attributes.Add("onClick", JS_CHANGE_PAGE + "(0);");
        }

        /// <summary>
        /// Adds an attribute to a textbox (javascript) which checks if the enter key has been pressed.
        /// If so, it simulates a click on the clickable control c
        /// </summary>
        /// <param name="source"></param>
        /// <param name="c"></param>
        public void RegisterPostbackSearchTextbox(TextBox source, WebControl c)
        {
            string js = "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + c.ID + "').click();return false;}} else {return true}; ";
            source.Attributes.Add("onkeydown", js);
        }

        public BoundColumn AddField(IDataField field, string align)
        {
            return AddField(field, align, "");
        }

        public BoundColumn AddField(IDataField field, string align, string format)
        {
            return AddField(field.DataField, field.SortExpression, field.Display, align, format);
        }

        public BoundColumn AddField(string dataField, string display, string align)
        {
            return AddField(dataField, dataField, display, align, "");
        }

        public BoundColumn AddField(string dataField, string display, string align, string format)
        {
            return AddField(dataField, dataField, display, align, format);
        }

        public BoundColumn AddField(string dataField, string sortExpression, string display, string align, string format)
        {
            fields[columnsCount++] = new ComboDataField(dataField, sortExpression, display);

            BoundColumn c = new BoundColumn();
            c.DataField = dataField;
            c.DataFormatString = format;
            c.HeaderText = display;
            c.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            if (align == "left")
                c.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            else if (align == "right")
                c.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            else if (align == "center")
                c.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            dataGrid.Columns.Add(c);
            return c;
        }

        public Button AddTableCommandButton(string text, string commandName)
        {
            Button b = new Button();
            b.ID = "btn" + this.ID + commandName;
            b.Text = text;
            b.CommandName = commandName;
            b.Command += new CommandEventHandler(button_Command);
            tableButtonHolder.Controls.Add(b);
            tableButtonHolder.Controls.Add(new LiteralControl("&nbsp;"));
            return b;
        }

        public void AddSpacer(int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
                sb.Append("&nbsp");
            tableButtonHolder.Controls.Add(new LiteralControl(sb.ToString()));
        }

        #endregion

        #region protected variables (controls)
        // Asp.net controls
        protected System.Web.UI.WebControls.DataGrid dataGrid;  // displays the data in a grid layout
        protected PlaceHolder tableButtonHolder;                        //command buttons applicable to all records
        #endregion

        #region protected accessors

        protected int LastAddedColumnIndex { get { return columnsCount - 1; } }

        protected IDataField GetField(int index) { return fields[index]; }

        protected bool IsIE() { return Page.Request.UserAgent.IndexOf("MSIE") != -1; }

        protected string FieldIdList { get { return hiddenFieldIdList; } }

        protected string FieldPageNumber { get { return hiddenFieldPageNumber; } }

        protected string FieldSortIndex { get { return hiddenFieldSortColumnIndex; } }

        protected string FieldPrefixSortOrder { get { return prefixColumnHeaderId; } }

        protected string IdPrefixRow { get { return prefixRowId; } }

        protected string IdPagerDropDownList { get { return idPagerDropDownList; } }

        protected string HiddenFieldSelectedRow { get { return hiddenFieldSelectedRow; } }

        protected string HiddenFieldSelectedId { get { return hiddenFieldSelectedId; } }

        #endregion

        #region protected methods

        protected virtual string InitJsRowActionButtonsArray(string nameOfJsButtonArray) //used in RowSelectCombo
        {
            return "\n\t\t\tvar " + nameOfJsButtonArray + " = \"\";";
        }

        protected virtual string GetJsUpdateButtonId() { return ""; }   //used in MultiRowSelectCombo

        protected virtual string GetJsResetButtonId() { return ""; }    //used in MultiRowSelectCombo

        protected virtual string GetJsAddAllButtonId() { return ""; }   //used in MultiRowSelectCombo

        protected virtual string GetJsRemoveAllButtonId() { return ""; }    //used in MultiRowSelectCombo

        protected void button_Command(object sender, CommandEventArgs e) { Command(sender, e); }

        protected void combo_ItemDataBound(object sender, DataGridItemEventArgs e) { ItemDataBound(sender, e); }

        protected Button AddTableCommandButton(Button b)
        {
            tableButtonHolder.Controls.Add(b);
            tableButtonHolder.Controls.Add(new LiteralControl("&nbsp;"));
            return b;
        }

        private string GetFormId()
        {
            Control current;
            while ((current = this.Parent) != null)
                if (current is System.Web.UI.HtmlControls.HtmlForm)
                    return current.ClientID;
            throw new Exception("Unable to locate html form id");
        }

        protected override void OnInit(EventArgs e)
        {
            formId = GetFormId();

            fields = new ComboDataField[MAX_NUMBER_OF_FIELDS];
            pageSize = DEFAULT_PAGE_SIZE;
            columnsCount = 0;
            idList = ""; //populated in DataBind()

            cssClassCombo = "comboOuterTable";
            cssClassGrid = "comboInnerTable";
            cssClassGridHeader = "comboInnerHeader";
            cssClassPager = "comboPager";
            pagerImagePath = "_comboHelper/pager/";
            noRecordsMessage = "No records were found";

            imgFirst = "first.gif";
            imgFirstA = "first_a.gif";
            imgPrev = "prev.gif";
            imgPrevA = "prev_a.gif";
            imgNext = "next.gif";
            imgNextA = "next_a.gif";
            imgLast = "last.gif";
            imgLastA = "last_a.gif";
            idImgFirst = "btnFirst";
            idImgPrev = "btnPrev";
            idImgNext = "btnNext";
            idImgLast = "btnLast";

            hiddenFieldIdList = this.ID + "_IdList";
            hiddenFieldPageNumber = this.ID + "_pageNumber";
            hiddenFieldSortColumnIndex = this.ID + "_sortIndex";
            hiddenFieldSelectedRow = this.ID + "_selectedRow";
            hiddenFieldSelectedId = this.ID + "_selectedId";
            prefixHiddenFieldSortOrder = this.ID + "_sortOrder_";
            prefixRowId = this.ID + "_row_";
            prefixColumnHeaderId = this.ID + "_columnheader_";
            idPagerDropDownList = this.ID + "_pageList";

            dataGrid = new System.Web.UI.WebControls.DataGrid();
            dataGrid.ID = this.ID + "_table";
            dataGrid.AutoGenerateColumns = false;
            dataGrid.CellPadding = 3;
            dataGrid.CellSpacing = 0;
            dataGrid.CssClass = cssClassGrid;
            dataGrid.HeaderStyle.CssClass = cssClassGridHeader;
            dataGrid.ItemDataBound += new DataGridItemEventHandler(dataGrid_ItemDataBound);
            Controls.Add(dataGrid);

            tableButtonHolder = new PlaceHolder();
            Controls.Add(tableButtonHolder);

            base.OnInit(e);
        }

        protected virtual void BindGridItem(DataGridItemEventArgs e, int rowIndex)
        {
            e.Item.Attributes.Add("id", prefixRowId + rowIndex);
            e.Item.Attributes.Add("onMouseOver", JS_ROW_MOUSEOVER + "(" + rowIndex + ", true);");
            e.Item.Attributes.Add("onMouseOut", JS_ROW_MOUSEOVER + "(" + rowIndex + ", false);");
        }

        //empty stub for child classes
        protected virtual void AddControls(HtmlTextWriter writer) { }

        protected override void Render(HtmlTextWriter writer)
        {
            //check if there is smth to display (i.e. datasource not empty + fields have been added)
            if (dataGrid != null && dataGrid.DataSource != null && columnsCount > 0)
            {
                writer.Write("\n<table class=\"" + cssClassCombo + "\" cellspacing=\"0\" cellpadding=\"3\"><tr><td>");
                tableButtonHolder.RenderControl(writer);
                AddControls(writer);
                writer.Write("</td><td align=\"right\">\n");
                //if you ever need to add somethign to the right from the buttons - do it here
                writer.Write("</td></tr>\n");
                writer.Write("<tr><td colspan=\"2\">\n");
                RenderPager(TotalPages(), writer);
                writer.Write("</td></tr>\n");
                writer.Write("<tr><td colspan=\"2\">\n");

                if (dataGrid.Items.Count > 0)
                    dataGrid.RenderControl(writer);
                else
                    writer.Write(noRecordsMessage);

                writer.Write("\n</td></tr></table>\n");
                writer.Write(MakeHiddenFields());
            }
        }

        protected virtual string MakeHiddenFields()
        {
            StringBuilder sb = new StringBuilder();
            //next line: MUST use name in addition to id: request doesn't not work on id without posting back...
            sb.Append("<input id=\"" + hiddenFieldIdList + "\" name=\"" + hiddenFieldIdList + "\" type=\"hidden\" value=\"" + idList + "\">\n");
            sb.Append("<input id=\"" + hiddenFieldPageNumber + "\" name=\"" + hiddenFieldPageNumber + "\" type=\"hidden\" value=\"" + PageNumber + "\">\n");
            sb.Append("<input id=\"" + hiddenFieldSortColumnIndex + "\" name=\"" + hiddenFieldSortColumnIndex + "\" type=\"hidden\" value=\"" + SortColumnIndex() + "\">\n");
            sb.Append("<input id=\"" + hiddenFieldSelectedRow + "\" name=\"" + hiddenFieldSelectedRow + "\" type=\"hidden\" value=\"-1\">\n");
            sb.Append("<input id=\"" + hiddenFieldSelectedId + "\" name=\"" + hiddenFieldSelectedId + "\" type=\"hidden\" value=\"-1\">\n");

            for (int i = 0; i < columnsCount; i++)
                sb.Append("<input id=\"" + prefixHiddenFieldSortOrder + i + "\" name=\"" + prefixHiddenFieldSortOrder + i + "\" type=\"hidden\" value=\"" + GetSortOrder(i) + "\">\n");

            return sb.ToString();
        }

        protected int TotalRecords() { return totalRowCount; }

        // Returns the sort order (0=descending, 1=ascending) by column index
        private int GetSortOrder(int col)
        {
            return GetNumericFieldValue(prefixHiddenFieldSortOrder + col);
        }

        #endregion

        #region private methods

        private int GetNumericFieldValue(string fieldName)
        {
            string s = Page.Request[fieldName];
            if (s == null) return 1;
            else return Convert.ToInt32(s);
        }

        private void OnItemDataBound(object source, DataGridItemEventArgs e)
        {
            if (ItemDataBound != null) { ItemDataBound(this, e); }
        }

        private void dataGrid_ItemDataBound(object source, DataGridItemEventArgs e)
        {
            this.OnItemDataBound(source, e);

            if (e.Item.ItemType == ListItemType.Header)
            {
                TableCellCollection cells = e.Item.Cells;
                for (int i = 0; i < cells.Count; i++)
                    cells[i].Text = makeHeadeLink(i, cells);
            }
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
                BindGridItem(e, e.Item.ItemIndex);
        }

        protected virtual string makeHeadeLink(int columnIndex, TableCellCollection cells)
        {
            return "<a id=\"" + prefixColumnHeaderId + columnIndex + "\" class=\"" + cssClassGridHeader +
                "\" href='#' onClick=\"" + JS_CHANGE_SORT_INDEX + "(" + columnIndex + ");\">" + cells[columnIndex].Text + "</a>";
        }

        private string DataKeyList
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                DataKeyCollection keys = dataGrid.DataKeys;

                string list = "";
                if (keys.Count > 0)
                {
                    for (int i = 0; i < keys.Count; i++)
                        sb.Append(keys[i] + ",");

                    list = sb.ToString();
                    list = list.Substring(0, list.LastIndexOf(","));
                }
                return list;
            }
        }

        private int FirstRecord() { return (PageNumber - 1) * pageSize + 1; }

        private int LastRecord() { return Math.Min(FirstRecord() + pageSize - 1, TotalRecords()); }

        /// <summary>
        /// Returns the index of the column by which the results are sorted
        /// </summary>
        /// <returns></returns>
        private int SortColumnIndex()
        {
            string s = Page.Request[hiddenFieldSortColumnIndex];
            if (s == null) return -1;
            else return Convert.ToInt32(s);
        }

        private void RenderPager(int totalPages, HtmlTextWriter writer)
        {
            int currPage = PageNumber;
            int sortColumnIndex = SortColumnIndex();

            string sortField;
            string sortFieldDisplay;
            if (sortColumnIndex > -1)
            {
                sortField = fields[sortColumnIndex].SortExpression;
                sortFieldDisplay = fields[sortColumnIndex].Display;
            }
            else
            {
                sortField = DefaultSortExpression;
                sortFieldDisplay = DefaultSortExpressionDisplay;
            }

            writer.Write("\n<table class=\"" + cssClassPager + "\" cellpadding=\"1\" cellspacing=\"0\" width=\"100%\">\n\t<tr>");
            writer.Write("\n\t\t<td height=25>");
            writer.Write("&nbsp;Results <b>" + FirstRecord() + "-" + LastRecord() + "</b> of <b>" + TotalRecords() +
                "</b> sorted by <b>" + sortFieldDisplay + "</b></td>");

            if (totalPages > 1)
            {
                writer.Write("\n\t\t<td width=\"30\"></td>\n\t\t<td align=\"right\">");
                writer.Write("Page <select id=\"" + idPagerDropDownList + "\" onChange=\"" + JS_CHANGE_PAGE + "(4);\">");

                for (int i = 1; i <= totalPages; i++)
                    if (i == currPage)
                        writer.Write("\n\t\t\t<option selected>" + i + "</option>");
                    else
                        writer.Write("\n\t\t\t<option>" + i + "</option>");

                writer.Write("\n\t\t\t</select> of " + totalPages + "</td>");
                writer.Write("\n\t\t<td width=\"110\" align=\"right\">");

                string first = "\n\t\t\t<img id=\"" + idImgFirst + "\" src=\"" + pagerImagePath + imgFirstA + "\" style=\"margin-top:3px; cursor:pointer;\" onClick=\"" + JS_CHANGE_PAGE + "(0);\">";
                string prev = "\n\t\t\t<img id=\"" + idImgPrev + "\" src=\"" + pagerImagePath + imgPrevA + "\" style=\"margin-top:3px; cursor:pointer;\" onClick=\"" + JS_CHANGE_PAGE + "(1);\">";
                string next = "\n\t\t\t<img id=\"" + idImgNext + "\" src=\"" + pagerImagePath + imgNextA + "\" style=\"margin-top:3px; cursor:pointer;\" onClick=\"" + JS_CHANGE_PAGE + "(2);\">";
                string last = "\n\t\t\t<img id=\"" + idImgLast + "\" src=\"" + pagerImagePath + imgLastA + "\" style=\"margin-top:3px; cursor:pointer;\" onClick=\"" + JS_CHANGE_PAGE + "(3);\">";

                if (currPage == 1)
                {
                    first = "\n\t\t\t<img id=\"" + idImgFirst + "\" src=\"" + pagerImagePath + imgFirst + "\" style=\"margin-top:3px\">";
                    prev = "\n\t\t\t<img id=\"" + idImgPrev + "\" src=\"" + pagerImagePath + imgPrev + "\" style=\"margin-top:3px\">";
                }
                if (currPage == totalPages)
                {
                    next = "\n\t\t\t<img id=\"" + idImgNext + "\" src=\"" + pagerImagePath + imgNext + "\" style=\"margin-top:3px\">";
                    last = "\n\t\t\t<img id=\"" + idImgLast + "\" src=\"" + pagerImagePath + imgLast + "\" style=\"margin-top:3px\">";
                }

                writer.Write(first);
                writer.Write(prev);
                writer.Write(next);
                writer.Write(last);

                writer.Write("&nbsp;\n\t\t</td>");
            }
            writer.Write("\n\t</tr>\n</table>\n\n");
        }

        #endregion

        #region private variables

        private string cssClassCombo;
        private string cssClassGrid;
        private string cssClassGridHeader;
        private string cssClassPager;

        private string formId;                                              //id of the html form
        private string hiddenFieldIdList;                           //hidden input field for list of record IDs
        private string hiddenFieldPageNumber;                   //hidden input field for current page number
        private string hiddenFieldSortColumnIndex;      //hidden input field for index of column by which grid is sorted 
        private string hiddenFieldSelectedRow;              //hidden input field for selected row
        private string hiddenFieldSelectedId;                   //hidden input field for selected id
        private string prefixHiddenFieldSortOrder;      //hidden input field for the sort order (asc/desc)
        private string prefixRowId;                                     //prefix for grid row ids
        private string prefixColumnHeaderId;                    //prefix for grid column header ids
        private string idPagerDropDownList;                     //id of dropdown list with page numbers in pager

        //pager images
        private string imgFirst;
        private string imgFirstA;
        private string imgPrev;
        private string imgPrevA;
        private string imgNext;
        private string imgNextA;
        private string imgLast;
        private string imgLastA;
        private string idImgFirst;
        private string idImgPrev;
        private string idImgNext;
        private string idImgLast;
        private string pagerImagePath;

        private ComboDataField[] fields;                        //holds the set of IDataFields defining the content of the grid
        private int pageSize;                                               //paging: page size
        private int columnsCount;                                       //number of columns (serves also as cursor)
        private string idList;                                          //CSV string with IDs of listed records
        private int totalRowCount;                                  //total rows (all pages)
        private string defaultSortExpression;               //can be set by default, returns first field's sortexp if not set.
        private string defaultSortExpressionDisplay; //same as above, but holds the displayed value
        private string noRecordsMessage;                        //displayed if ther are no rows in the grid		

        #endregion

        private class ComboDataField : IDataField
        {
            public ComboDataField(string dataField, string sortExpression, string display)
            {
                this.dataField = dataField;
                this.sortExpression = sortExpression;
                this.display = display;
            }

            public string DataField { get { return dataField; } }
            public string SortExpression { get { return sortExpression; } }
            public string Display { get { return display; } }

            private string dataField;
            private string sortExpression;
            private string display;
        }
    }
}
