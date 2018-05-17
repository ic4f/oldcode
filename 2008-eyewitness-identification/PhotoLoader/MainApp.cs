using System;

namespace Ei.PhotoLoader
{
    class MainApp
    {
        [STAThread]
        static void Main(string[] args)
        {
            Loader loader = new Loader();
            loader.Run();
        }
    }
}
