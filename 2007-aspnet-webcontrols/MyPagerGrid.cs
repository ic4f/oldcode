using System;
using System.Web.UI.WebControls;

namespace WebControls
{
    public class MyPagerGrid : MySortGrid
    {
        private MyPager pager;

        public void SetPager(MyPager p) { pager = p; }

        protected override void MyDataGrid_SortCommand(Object sender, DataGridSortCommandEventArgs e)
        {
            changeSort(e.SortExpression);
            pager.Reset();
            bind();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
    }
}
