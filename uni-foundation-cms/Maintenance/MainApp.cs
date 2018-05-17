using System;

namespace Foundation.Maintenance
{
    class MainApp
    {
        [STAThread]
        static void Main(string[] args)
        {
            new CmsLoader().Load();
        }
    }
}
