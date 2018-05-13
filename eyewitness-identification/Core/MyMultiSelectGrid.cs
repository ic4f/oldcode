using System;
using System.Collections;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Core
{
    public class MyMultiSelectGrid : MySortGrid
    {
        public ArrayList Selected
        {
            get
            {
                return new ArrayList();
            }
        }
    }
}
