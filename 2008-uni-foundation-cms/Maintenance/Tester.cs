using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Foundation.Maintenance
{
    public class Tester
    {
        public Tester() { }

        public void TestPermissionsSource()
        {
            FileStream fs = new FileStream(ConfigurationHelper.PermissionTargetFile, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);
            int id;
            int parentId;
            string type;
            string name;
            string code;
            while (fs.Position < fs.Length)
            {
                id = r.ReadInt32();
                parentId = r.ReadInt32();
                type = r.ReadString();
                name = r.ReadString();
                code = r.ReadString();
                Console.WriteLine(id + "\t" + parentId + "\t" + type + "\t" + name + "\t" + code);
            }
            r.Close();
            fs.Close();
        }

        public void TestPermissionCatsSource()
        {
            FileStream fs = new FileStream(ConfigurationHelper.PermissionCategoryTargetFile, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);
            int id;
            int parentId;
            string name;
            while (fs.Position < fs.Length)
            {
                id = r.ReadInt32();
                parentId = r.ReadInt32();
                name = r.ReadString();
                Console.WriteLine(id + "\t" + parentId + "\t" + name);
            }
            r.Close();
            fs.Close();
        }

        public void TestPagesSource()
        {
            FileStream fs = new FileStream(ConfigurationHelper.PageTargetFile, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);
            int id;
            int parentId;
            bool inMenu;
            string menuTitle;
            string pageTitle;
            string url;
            string perms;
            while (fs.Position < fs.Length)
            {
                id = r.ReadInt32();
                parentId = r.ReadInt32();
                inMenu = r.ReadBoolean();
                menuTitle = r.ReadString();
                pageTitle = r.ReadString();
                url = r.ReadString();
                perms = r.ReadString();
                Console.WriteLine(id + "\t" + parentId + "\t" + inMenu + "\t" + menuTitle + "\t" + pageTitle + "\t" + url + "\t" + perms);
            }
            r.Close();
            fs.Close();
        }
    }
}
