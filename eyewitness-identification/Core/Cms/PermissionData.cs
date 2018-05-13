using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Core.Cms
{
    public class PermissionData
    {
        public static int NO_PARENT = -1;

        public PermissionData()
        {
            catArrays = new Hashtable();
            permArrays = new Hashtable();

            perms = new Hashtable();
            cats = new Hashtable();
            topCats = new ArrayList();
            loadPermissionCats();
            loadPermissions();

        }

        public ArrayList TopCategories { get { return topCats; } }

        public PermissionCategory GetCategory(int categoryId)
        {
            return (PermissionCategory)cats[categoryId];
        }

        public ArrayList GetCategories(int parentCategoryId)
        {
            return (ArrayList)catArrays[parentCategoryId];
        }

        public ArrayList GetPermissions(int parentCategoryId)
        {
            return (ArrayList)permArrays[parentCategoryId];
        }

        private void loadPermissions()
        {
            FileStream fs = new FileStream(ConfigurationHelper.PermissionFile, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);
            int id;
            int parentId;
            string type;
            string name;
            string code;
            Permission perm;
            while (fs.Position < fs.Length)
            {
                id = r.ReadInt32();
                parentId = r.ReadInt32();
                type = r.ReadString();
                name = r.ReadString();
                code = r.ReadString();
                perm = new Permission(id, parentId, type, name, code);
                perms.Add(id, perm);

                ArrayList permList = (ArrayList)permArrays[parentId];
                permList.Add(perm);

            }
            r.Close();
            fs.Close();
        }

        private void loadPermissionCats()
        {
            FileStream fs = new FileStream(ConfigurationHelper.PermissionCategoryFile, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);
            int id;
            int parentId;
            string name;
            PermissionCategory cat;
            while (fs.Position < fs.Length)
            {
                id = r.ReadInt32();
                parentId = r.ReadInt32();
                name = r.ReadString();
                cat = new PermissionCategory(id, parentId, name);
                cats.Add(id, cat);
                if (parentId == NO_PARENT)
                {
                    catArrays.Add(id, new ArrayList());
                    topCats.Add(cat);
                }
                else
                {
                    ArrayList catList = (ArrayList)catArrays[parentId];
                    catList.Add(cat);
                    permArrays.Add(id, new ArrayList());
                }
            }
            r.Close();
            fs.Close();
        }

        private Hashtable perms;
        private Hashtable cats;
        private ArrayList topCats;


        private Hashtable catArrays;
        private Hashtable permArrays;


    }
}
