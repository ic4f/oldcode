using System;

namespace Foundation.ConsoleUtilities
{
    class MainApp
    {
        [STAThread]
        static void Main(string[] args)
        {
            SystemLoader loader = new SystemLoader();
            loader.ReloadSystem();
            //loader.LoadTestData();
        }
    }
}
