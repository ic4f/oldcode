using System;

namespace Ei.Utilities
{
    class MainApp
    {
        [STAThread]
        static void Main(string[] args)
        {
            new Cms.CmsLoader().Load();
        }
    }
}
