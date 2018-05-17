using System;
using System.Data;
using d = Foundation.Data;
using ba = Foundation.BusinessAdmin;
using bm = Foundation.BusinessMain;

namespace Foundation.ConsoleUtilities
{
    /// <summary>
    /// Summary description for MenuHelper.
    /// </summary>
    public class MenuHelper
    {
        public MenuHelper()
        {
        }

        public void Run()
        {
            DataTable dt = new d.StagingMenuData().GetOrdered().Tables[0];
            Console.WriteLine("count=" + dt.Rows.Count);
            ba.PublicMenuTree pmt = new ba.PublicMenuTree(ba.PublicMenuTree.ConvertData(dt), -1, "../");
        }

        private string[,] getMenu()
        {
            string[,] menu = new string[2, 5];

            menu[0, 0] = "1";
            menu[0, 1] = "-1";
            menu[0, 2] = "menu 1";
            menu[0, 3] = "rank";
            menu[0, 4] = "1.html";

            menu[1, 0] = "2";
            menu[1, 1] = "1";
            menu[1, 2] = "menu 2";
            menu[1, 3] = "rank";
            menu[1, 4] = "2.html";

            return menu;
        }
    }
}
