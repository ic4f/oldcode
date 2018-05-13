using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using b = Ei.Business;
using c = Core;
using cc = Core.Cms;

namespace Ei.Utilities.Cms
{
    public class PermissionsPagesFileBuilder
    {
        public PermissionsPagesFileBuilder()
        {
            permsHash = new Hashtable();
            catsHash = new Hashtable();
            pagesHash = new Hashtable();
            orderedPerms = new ArrayList();
            orderedCats = new ArrayList();
            orderedPages = new ArrayList();
        }

        public void Build()
        {
            try
            {
                loadPermissions();
                validatePermissions();
                loadPages();
                validatePageParentPermissions();
                writePermsToFile();
                writeCatsToFile();
                writePagesToFile();
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION: " + e.Message);
                Environment.Exit(1);
            }
        }

        private void loadPermissions()
        {
            FileStream fs = File.OpenRead(ConfigurationHelper.PermissionSourceFile);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            string patternCat = @"\s*(?<id>c\d+)\s+(?<parentid>c-?\d+)\s+""(?<name>[^""]+)""";
            string patternPerm = @"\s*(?<id>\d+)\s+(?<parentid>c\d+)\s+(?<type>(and)|(or))\s+""(?<name>[^""]+)""\s+""(?<code>\w+)""";

            Regex regexPerm = new Regex(patternPerm, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex regexCat = new Regex(patternCat, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            MatchCollection mc;

            string line;
            int id;
            int parentId;
            string type;
            string name;
            string code;
            cc.Permission perm;
            cc.PermissionCategory cat;
            cc.PermissionCategory parentCat;
            int counter = 0;
            while ((line = r.ReadLine()) != null)
            {
                counter++;
                if (line != "" && !line.StartsWith("//"))
                {
                    if (line.StartsWith("c"))
                    {
                        mc = regexCat.Matches(line);
                        if (mc.Count != 1)
                            throw new Exception("Invalid format in permissions file -- line " + counter);

                        id = Convert.ToInt32(mc[0].Groups["id"].ToString().Substring(1));
                        parentId = Convert.ToInt32(mc[0].Groups["parentid"].ToString().Substring(1));
                        name = mc[0].Groups["name"].ToString();

                        if (parentId != cc.PermissionData.NO_PARENT)
                        {
                            if (!catsHash.ContainsKey(parentId))
                                throw new Exception("Nonexistant category as parentId -- line " + counter);

                            parentCat = (cc.PermissionCategory)catsHash[parentId];
                            if (parentCat.ParentId != cc.PermissionData.NO_PARENT)
                                throw new Exception("A category's parent must be a first-level category -- line " + counter);
                        }
                        cat = new cc.PermissionCategory(id, parentId, name);
                        catsHash.Add(id, cat);
                        orderedCats.Add(cat);
                    }
                    else
                    {
                        mc = regexPerm.Matches(line);
                        if (mc.Count != 1)
                            throw new Exception("Invalid format in permissions file -- line " + counter);

                        id = Convert.ToInt32(mc[0].Groups["id"].ToString());
                        parentId = Convert.ToInt32(mc[0].Groups["parentid"].ToString().Substring(1));
                        type = mc[0].Groups["type"].ToString();
                        name = mc[0].Groups["name"].ToString();
                        code = mc[0].Groups["code"].ToString();

                        if (!catsHash.ContainsKey(parentId))
                            throw new Exception("Nonexistant category as parentId -- line " + counter);

                        parentCat = (cc.PermissionCategory)catsHash[parentId];
                        if (parentCat.ParentId == cc.PermissionData.NO_PARENT)
                            throw new Exception("A permission's parent must be a second-level category -- line " + counter);

                        perm = new cc.Permission(id, parentId, type, name, code);
                        permsHash.Add(id, perm);
                        orderedPerms.Add(perm);
                    }
                }
            }
            r.Close();
            fs.Close();
        }

        private void validatePermissions()
        {
            Type permCodeObjectType = new b.PermissionCode().GetType();
            FieldInfo[] fields = permCodeObjectType.GetFields();

            Hashtable fieldsHash = new Hashtable();
            foreach (FieldInfo field in fields)
                fieldsHash.Add(field.Name, field);

            Hashtable permCodes = new Hashtable();
            foreach (cc.Permission perm in orderedPerms)
                permCodes.Add(perm.Code, perm);

            foreach (cc.Permission perm in orderedPerms)
            {
                if (!fieldsHash.ContainsKey(perm.Code))
                    throw new Exception("The class " + permCodeObjectType.FullName + " is missing the field: " + perm.Code);
                FieldInfo fi = (FieldInfo)fieldsHash[perm.Code];
                int val = Convert.ToInt32(fi.GetValue(fi));
                if (val != perm.Id)
                    throw new Exception("The field " + perm.Code + " in class  " + permCodeObjectType.FullName + " has a wrong value");
            }

            foreach (FieldInfo fieldinfo in fields)
            {
                if (!permCodes.ContainsKey(fieldinfo.Name))
                    throw new Exception("The class " + permCodeObjectType.FullName + " contains an illegal field: " + fieldinfo.Name);

            }
        }

        private void loadPages()
        {
            FileStream fs = File.OpenRead(ConfigurationHelper.PageSourceFile);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            string pattern = @"\s*(?<id>\d+)\s+(?<parentid>-?\d+)\s+""(?<menutitle>[^""]*)""\s+""(?<pagetitle>[^""]+)""\s+""(?<url>[^""]+)""\s+""(?<permissions>-?(\d|,|\p{Z})*)""";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            MatchCollection mc;

            string line;
            int id;
            int parentId;
            bool inMenu;
            string menuTitle;
            string pageTitle;
            string url;
            string permissions;
            cc.CmsPage page;
            int counter = 0;
            while ((line = r.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                counter++;
                if (line != "" && !line.StartsWith("//"))
                {
                    mc = regex.Matches(line);
                    if (mc.Count != 1)
                        throw new Exception("Invalid format -- line " + counter);
                    id = Convert.ToInt32(mc[0].Groups["id"].ToString());
                    parentId = Convert.ToInt32(mc[0].Groups["parentid"].ToString());
                    menuTitle = mc[0].Groups["menutitle"].ToString();
                    pageTitle = mc[0].Groups["pagetitle"].ToString();
                    url = mc[0].Groups["url"].ToString();
                    permissions = mc[0].Groups["permissions"].ToString();
                    inMenu = (menuTitle.Length != 0);

                    validatePagePermissions(permissions, counter);

                    if (parentId != cc.CmsPageData.NO_PARENT && !pagesHash.ContainsKey(parentId))
                        throw new Exception("Nonexistant page as parentId -- line " + counter);

                    if (!inMenu)
                    {
                        cc.CmsPage parentPage = (cc.CmsPage)pagesHash[parentId];
                        if (!parentPage.InMenu)
                            throw new Exception("Parent page must be in menu -- line " + counter);
                    }

                    page = new cc.CmsPage(id, parentId, menuTitle, pageTitle, url, inMenu, permissions);
                    pagesHash.Add(id, page);
                    orderedPages.Add(page);
                }
            }
            r.Close();
            fs.Close();
        }

        private void validatePagePermissions(string perms, int lineNumber)
        {
            if (perms.Trim().Length > 0)
            {
                string[] permIds = perms.Split(new char[] { ',' });
                int id;
                foreach (string s in permIds)
                {
                    id = Convert.ToInt32(s);
                    if (!permsHash.ContainsKey(id))
                        throw new Exception("Unknown permission id: " + id + " -- line " + lineNumber);
                }
            }
        }

        private void validatePageParentPermissions()
        {
            //check that the permissions of each page's parent include the permissions of the child page
            cc.CmsPage parent;
            ArrayList pagePerms;
            ArrayList parentPerms;
            foreach (cc.CmsPage page in orderedPages)
                if (page.ParentId != cc.CmsPageData.NO_PARENT)
                {
                    parent = (cc.CmsPage)pagesHash[page.ParentId];
                    if (!parent.IsOpenToAllUsers)
                    {
                        pagePerms = page.GetPermissionCodesStringArray();
                        parentPerms = parent.GetPermissionCodesStringArray();
                        foreach (string perm in pagePerms)
                            if (!parentPerms.Contains(perm))
                                throw new Exception("Parent page " + parent.MenuTitle + "(" + parent.Id + ") does not contain required permission " + perm);
                    }
                }
        }

        private void writePermsToFile()
        {
            string path = ConfigurationHelper.PermissionTargetFile;
            if (File.Exists(path))
                File.Delete(path);

            FileStream fs = new FileStream(path, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);

            cc.Permission perm;
            for (int i = 0; i < orderedPerms.Count; i++)
            {
                perm = (cc.Permission)orderedPerms[i];
                w.Write(perm.Id);
                w.Write(perm.ParentId);
                w.Write(perm.Type);
                w.Write(perm.Name);
                w.Write(perm.Code);
            }
            w.Close();
            fs.Close();
        }

        private void writeCatsToFile()
        {
            string path = ConfigurationHelper.PermissionCategoryTargetFile;
            if (File.Exists(path))
                File.Delete(path);

            FileStream fs = new FileStream(path, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);

            cc.PermissionCategory cat;
            for (int i = 0; i < orderedCats.Count; i++)
            {
                cat = (cc.PermissionCategory)orderedCats[i];
                w.Write(cat.Id);
                w.Write(cat.ParentId);
                w.Write(cat.Name);
            }
            w.Close();
            fs.Close();
        }

        private void writePagesToFile()
        {
            string path = ConfigurationHelper.PageTargetFile;
            if (File.Exists(path))
                File.Delete(path);

            FileStream fs = new FileStream(path, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);

            cc.CmsPage page;
            for (int i = 0; i < orderedPages.Count; i++)
            {
                page = (cc.CmsPage)orderedPages[i];
                w.Write(page.Id);
                w.Write(page.ParentId);
                w.Write(page.InMenu);
                w.Write(page.MenuTitle);
                w.Write(page.PageTitle);
                w.Write(page.Url);
                w.Write(page.Perms);
            }
            w.Close();
            fs.Close();
        }

        private Hashtable permsHash;
        private Hashtable catsHash;
        private ArrayList orderedPerms;
        private ArrayList orderedCats;
        private Hashtable pagesHash;
        private ArrayList orderedPages;
    }
}
