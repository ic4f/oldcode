using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.IO;
using d = Foundation.Data;
using ba = Foundation.BusinessAdmin;

namespace Foundation.ConsoleUtilities
{
    public class SystemLoader
    {
        private const string SQL_PATH = @"[path redacted]\ConsoleUtilities\sql\";
        private const string FILES_PATH = @"[path redacted]\Website\files\";
        private const string DSTORYIMAGES_PATH = @"[path redacted]\Website\files\dstoryimages\";
        private const string EVENTIMAGES_PATH = @"[path redacted]\Website\files\eventimages\";
        private const string HEADERIMAGES_PATH = @"[path redacted]\Website\files\headerimages\";
        private const string MENUIMAGES_PATH = @"[path redacted]\Website\files\menuimages\";
        private const string MODULEIMAGES_PATH = @"[path redacted]\Website\files\moduleimages\";
        private const string NEWSIMAGES_PATH = @"[path redacted]\Website\files\newsimages\";
        private const string RANDOMTEXT_PATH = @"[path redacted]\ConsoleUtilities\testdata\randomtext.txt";
        private const string TESTHEADERIMAGES_PATH = @"[path redacted]\ConsoleUtilities\testdata\headerimages\";
        private const string TESTIMAGES_PATH = @"[path redacted]\ConsoleUtilities\testdata\images\";
        private const string TESTFILES_PATH = @"[path redacted]\ConsoleUtilities\testdata\files\";

        private string[] text;
        private Random rnd;
        private SqlConnection connection;

        public SystemLoader()
        {
            loadtext();
            rnd = new Random();
            connection = new SqlConnection(ConfigurationHelper.ConnectionString);
        }

        public void ReloadSystem()
        {
            Console.WriteLine("ALL YOUR DATA IS ABOUT TO BE ERASED. Type 'yes' to proceed");
            if (Console.ReadLine() == "yes")
            {
                string root = ConfigurationHelper.PublicWebsiteRoot;
                deletePhysicalFiles();
                runSql("createTables.sql");
                createDefaultUser();
                runSql("loadDefaultData1.sql");
                Console.WriteLine("Data reloaded");
            }
            else
                Console.WriteLine("Process aborted");
        }

        public void LoadTestData()
        {
            loadTags();
            loadLabels();
            loadQuotes();
            loadHeaderImages();
            loadImages();
            loadFiles();
            loadStagingMenu();
            loadPages();
            loadPageTags();
            loadPageLabels();
            loadFileLabels();
            loadImageLabels();
            Console.WriteLine("Test data loaded");
        }


        #region ReloadSystem 
        private void deletePhysicalFiles()
        {
            string[] files = Directory.GetFiles(FILES_PATH);
            foreach (string f in files)
                File.Delete(f);

            string[] dstoryImages = Directory.GetFiles(DSTORYIMAGES_PATH);
            foreach (string f in dstoryImages)
                File.Delete(f);

            string[] headerImages = Directory.GetFiles(HEADERIMAGES_PATH);
            foreach (string f in headerImages)
                File.Delete(f);

            string[] menuImages = Directory.GetFiles(MENUIMAGES_PATH);
            foreach (string f in menuImages)
                File.Delete(f);

            string[] moduleImages = Directory.GetFiles(MODULEIMAGES_PATH);
            foreach (string f in moduleImages)
                File.Delete(f);

            string[] newsImages = Directory.GetFiles(NEWSIMAGES_PATH);
            foreach (string f in newsImages)
                File.Delete(f);
        }


        private void createDefaultUser()
        {
            SqlCommand command = new SqlCommand("CmsUser_Create", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Login", "admin"));
            command.Parameters.Add(new SqlParameter("@Password", (new Core.EncryptionTool()).Encrypt("1")));
            command.Parameters.Add(new SqlParameter("@FirstName", "administrator"));
            command.Parameters.Add(new SqlParameter("@Middle", ""));
            command.Parameters.Add(new SqlParameter("@LastName", ""));
            command.Parameters.Add(new SqlParameter("@ModifiedBy", System.DBNull.Value));
            connection.Open();
            int id = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            SqlCommand command2 = new SqlCommand("update [User] set modifiedby = 1 where id = 1 ", connection);
            command2.CommandType = CommandType.Text;
            connection.Open();
            command2.ExecuteNonQuery();
            connection.Close();
        }
        #endregion

        #region LoadTestData

        private void loadTags()
        {
            Console.WriteLine("loading tags");
            d.TagData td = new d.TagData();
            td.Create("students (public tag)", 1);
            td.Create("campaign (public tag)", 1);
            td.Create("events (public tag)", 1);
            td.Create("general information (public tag)", 1);
        }

        private void loadLabels()
        {
            Console.WriteLine("loading labels");
            d.ContentLabelData ld = new d.ContentLabelData();
            ld.Create("news (admin label)", 1);
            ld.Create("foundation info (admin label)", 1);
            ld.Create("announcements (admin label)", 1);
        }

        private void loadQuotes()
        {
            Console.WriteLine("loading quotes");
            d.QuoteData qd = new d.QuoteData();
            for (int i = 0; i < 30; i++)
                qd.Create(getRandomText(25, 100), "John Smith #" + i, "none", 1);
        }

        private void loadHeaderImages()
        {
            Console.WriteLine("loading header images");
            string[] files = Directory.GetFiles(TESTHEADERIMAGES_PATH);
            foreach (string f in files)
            {
                int imageId = new d.HeaderImageData().Create(ba.HeaderImageLocationCode.Right, "", 1);
                File.Copy(f, HEADERIMAGES_PATH + imageId + ".jpg");
            }
        }

        private void loadImages()
        {
            Console.WriteLine("loading images");
            d.FileData fd = new d.FileData();
            string[] files = Directory.GetFiles(TESTIMAGES_PATH);
            ba.ImageHelper ih = new ba.ImageHelper();

            foreach (string f in files)
            {
                FileInfo fi = new FileInfo(f);
                int id = fd.Create(fi.Extension, getRandomText(5, 100), getRandomText(50, 500), 1);
                if (id > 0) //neccessary check: might have been duplicate titles, since it's a random generator
                {
                    string imagePath = FILES_PATH + ba.UrlHelper.GetImageName(id, fi.Extension);
                    string imageThumbPath = FILES_PATH + ba.UrlHelper.GetImageName(id, "s" + fi.Extension);
                    ih.MakeImage(f, imagePath, 0, 0);
                    ih.MakeImage(f, imageThumbPath, 100, 75);
                    Bitmap im = new Bitmap(imagePath);
                    d.File df = new d.File(id);
                    df.Size = (int)fi.Length;
                    df.ImageWidth = im.Width;
                    df.ImageHeight = im.Height;
                    df.Update();
                    im.Dispose();
                }
            }
        }

        private void loadFiles()
        {
            Console.WriteLine("loading files");
            d.FileData fd = new d.FileData();
            string[] files = Directory.GetFiles(TESTFILES_PATH);

            foreach (string f in files)
                for (int i = 0; i < 50; i++)
                {
                    FileInfo fi = new FileInfo(f);
                    int id = fd.Create(fi.Extension, getRandomText(5, 100), getRandomText(50, 500), 1);
                    if (id > 0)
                    {
                        string filePath = FILES_PATH + ba.UrlHelper.GetFileName(id, fi.Extension);

                        File.Copy(f, filePath);
                        d.File df = new d.File(id);
                        df.Size = (int)fi.Length;
                        df.Update();
                    }
                }
        }

        private void loadStagingMenu()
        {
            Console.WriteLine("loading staging menu");
            runSql("loadstagingmenu.sql");
        }

        private void loadPages()
        {
            Console.WriteLine("loading pages");
            DataTable dtMenu = new d.StagingMenuData().GetOrdered().Tables[0];
            d.PageData pd = new d.PageData();
            //create 10 pages for each menu
            string partialUrl = ba.UrlHelper.BuildPagePartialUrlByPageCategory(ba.PageCategoryCode.StandardPage);
            foreach (DataRow dr in dtMenu.Rows)
                for (int i = 0; i < 10; i++)
                {
                    int menuId = Convert.ToInt32(dr[0]);
                    if (menuId != 1) //all except home
                        pd.Create(menuId, 1, true, true, true, true, "", "page title " + Convert.ToInt32(dr[0]) + "-" + i, getRandomText(4, 80), getRandomText(500, 5000),
                            true, false, false, false, false, true, partialUrl, getRandomText(50, 500), 1);
                }

            //assign menus to pages			
            d.StagingMenuData smd = new d.StagingMenuData();
            int pageId;
            d.Page page;
            DataTable dtPages;
            foreach (DataRow dr in dtMenu.Rows)
            {
                int menuId = Convert.ToInt32(dr[0]);
                if (menuId != 1)
                {
                    dtPages = pd.GetPublishedByMenuId(menuId);
                    pageId = Convert.ToInt32(dtPages.Rows[rnd.Next(dtPages.Rows.Count)][0]);
                    page = new d.Page(pageId);
                    smd.UpdatePage(menuId, pageId);
                }
            }
        }

        private void loadPageLabels()
        {
            Console.WriteLine("loading page labels");
            d.PageContentLabelData pl = new d.PageContentLabelData();
            DataTable dtPages = new d.PageData().GetList();
            DataTable dtLabels = new d.ContentLabelData().GetList();
            int pageId;
            int labelId;
            int labelsToAdd;
            int totalLabels = dtLabels.Rows.Count;
            foreach (DataRow dr in dtPages.Rows)
            {
                pageId = Convert.ToInt32(dr[0]);
                labelsToAdd = rnd.Next(totalLabels + 1);
                for (int i = 0; i < labelsToAdd; i++)
                {
                    labelId = Convert.ToInt32(dtLabels.Rows[rnd.Next(totalLabels)][0]);
                    pl.AddLink(pageId, labelId);
                }
            }
        }

        private void loadPageTags()
        {
            Console.WriteLine("loading page tags");
            d.PageTagData pt = new d.PageTagData();
            DataTable dtPages = new d.PageData().GetList();
            DataTable dtTags = new d.TagData().GetList();
            int pageId;
            int tagId;
            int tagsToAdd;
            int totalTags = dtTags.Rows.Count;
            foreach (DataRow dr in dtPages.Rows)
            {
                pageId = Convert.ToInt32(dr[0]);
                tagsToAdd = rnd.Next(totalTags + 1);
                for (int i = 0; i < tagsToAdd; i++)
                {
                    tagId = Convert.ToInt32(dtTags.Rows[rnd.Next(totalTags)][0]);
                    pt.AddLink(pageId, tagId);
                }
            }
        }

        private void loadFileLabels()
        {
            Console.WriteLine("loading file labels");
            d.FileContentLabelData fl = new d.FileContentLabelData();
            DataTable dtFiles = new d.FileData().GetNonImageFiles("f.id").Tables[0];
            DataTable dtLabels = new d.ContentLabelData().GetList();
            int fileId;
            int labelId;
            int labelsToAdd;
            int totalLabels = dtLabels.Rows.Count;
            foreach (DataRow dr in dtFiles.Rows)
            {
                fileId = Convert.ToInt32(dr[0]);
                labelsToAdd = rnd.Next(totalLabels + 1);
                for (int i = 0; i < labelsToAdd; i++)
                {
                    labelId = Convert.ToInt32(dtLabels.Rows[rnd.Next(totalLabels)][0]);
                    fl.AddLink(fileId, labelId);
                }
            }
        }

        private void loadImageLabels()
        {
            Console.WriteLine("loading image labels");
            d.FileContentLabelData fl = new d.FileContentLabelData();
            DataTable dtFiles = new d.FileData().GetImages("f.id").Tables[0];
            DataTable dtLabels = new d.ContentLabelData().GetList();
            int fileId;
            int labelId;
            int labelsToAdd;
            int totalLabels = dtLabels.Rows.Count;
            foreach (DataRow dr in dtFiles.Rows)
            {
                fileId = Convert.ToInt32(dr[0]);
                labelsToAdd = rnd.Next(totalLabels + 1);
                for (int i = 0; i < labelsToAdd; i++)
                {
                    labelId = Convert.ToInt32(dtLabels.Rows[rnd.Next(totalLabels)][0]);
                    fl.AddLink(fileId, labelId);
                }
            }
        }


        #endregion

        #region helpers

        private void runSql(string sqlFile)
        {
            string sql = readFile(SQL_PATH + sqlFile);
            SqlConnection connection = new SqlConnection(ConfigurationHelper.ConnectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandType = CommandType.Text;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        private string readFile(string path)
        {
            FileStream fs = File.OpenRead(path);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            string text = r.ReadToEnd();
            r.Close();
            fs.Close();
            return text;
        }

        private string getRandomText(int words, int maxChars)
        {
            int max = text.Length - 1;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < words; i++)
                sb.AppendFormat("{0} ", text[rnd.Next(max)].Trim());
            string result = sb.ToString();
            if (result.Length > maxChars)
                result = result.Substring(0, maxChars);
            return result;
        }

        private void loadtext()
        {
            FileStream fs = File.OpenRead(RANDOMTEXT_PATH);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);
            text = r.ReadToEnd().Split(getDelims());
            r.Close();
            fs.Close();
        }

        private char[] getDelims()
        {
            char[] d = new char[47];
            d[0] = '`';
            d[1] = '1';
            d[2] = '2';
            d[3] = '3';
            d[4] = '4';
            d[5] = '5';
            d[6] = '6';
            d[7] = '7';
            d[8] = '8';
            d[9] = '9';
            d[10] = '0';
            d[11] = '-';
            d[12] = '=';
            d[13] = '[';
            d[14] = ']';
            d[15] = ';';
            d[16] = '\'';
            d[17] = ',';
            d[18] = '.';
            d[19] = '/';
            d[20] = '\\';
            d[21] = '~';
            d[22] = '!';
            d[23] = '#';
            d[24] = '$';
            d[5] = '%';
            d[26] = '^';
            d[27] = '&';
            d[28] = '*';
            d[29] = '(';
            d[30] = ')';
            d[31] = '_';
            d[32] = '+';
            d[33] = '{';
            d[34] = '}';
            d[35] = ':';
            d[36] = '"';
            d[37] = '<';
            d[38] = '>';
            d[39] = '?';
            d[40] = '|';
            d[41] = ' ';
            d[42] = '\t';
            d[43] = '\r';
            d[44] = '\v';
            d[45] = '\f';
            d[46] = '\n';
            return d;
        }

        #endregion
    }
}
