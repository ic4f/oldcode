using System;

namespace Foundation.Maintenance
{
    /// <summary>
    /// This class loads the CMS and checks it's integrtiy (pages + permissions)
    /// </summary>
    public class CmsLoader
    {
        public CmsLoader() { }

        public void Load()
        {
            PermissionsPagesFileBuilder builder = new PermissionsPagesFileBuilder();
            builder.Build();

            Tester t = new Tester();
            t.TestPermissionsSource();
            t.TestPermissionCatsSource();
            t.TestPagesSource();
        }
    }
}
