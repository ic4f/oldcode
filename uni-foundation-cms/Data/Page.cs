using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Foundation.Data
{
    public class Page : DataClass
    {
        #region constructor
        public Page(int recordId) : base()
        {
            loadRecord(recordId);
        }
        #endregion

        #region int Id
        public int Id
        {
            get { return id; }
        }
        #endregion

        #region int MenuId
        public int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }
        #endregion

        #region int PageCategoryId
        public int PageCategoryId
        {
            get { return pageCategoryId; }
        }
        #endregion

        #region bool CanDelete
        public bool CanDelete
        {
            get { return canDelete; }
            set { canDelete = value; }
        }
        #endregion

        #region bool CanEditBody
        public bool CanEditBody
        {
            get { return canEditBody; }
            set { canEditBody = value; }
        }
        #endregion

        #region bool CanEditAdminComment
        public bool CanEditAdminComment
        {
            get { return canEditAdminComment; }
            set { canEditAdminComment = value; }
        }
        #endregion

        #region bool CanChangeMenu
        public bool CanChangeMenu
        {
            get { return canChangeMenu; }
            set { canChangeMenu = value; }
        }
        #endregion

        #region string RedirectLink
        public string RedirectLink
        {
            get { return redirectLink; }
            set { redirectLink = value; }
        }
        #endregion

        #region string PageTitle
        public string PageTitle
        {
            get { return pageTitle; }
            set { pageTitle = value; }
        }
        #endregion

        #region string ContentTitle
        public string ContentTitle
        {
            get { return contentTitle; }
            set { contentTitle = value; }
        }
        #endregion

        #region string Body
        public string Body
        {
            get { return body; }
        }
        #endregion

        #region string BodyDraft
        public string BodyDraft
        {
            get { return bodyDraft; }
            set { bodyDraft = value; }
        }
        #endregion

        #region DateTime Published
        public DateTime Published
        {
            get { return published; }
        }
        #endregion

        #region bool DisplayContent
        public bool DisplayContent
        {
            get { return displayContent; }
            set { displayContent = value; }
        }
        #endregion

        #region bool DisplayPublished
        public bool DisplayPublished
        {
            get { return displayPublished; }
            set { displayPublished = value; }
        }
        #endregion

        #region bool DisplayBookmarking
        public bool DisplayBookmarking
        {
            get { return displayBookmarking; }
            set { displayBookmarking = value; }
        }
        #endregion

        #region bool DisplayPrintable
        public bool DisplayPrintable
        {
            get { return displayPrintable; }
            set { displayPrintable = value; }
        }
        #endregion

        #region bool DisplayDiscussion
        public bool DisplayDiscussion
        {
            get { return displayDiscussion; }
            set { displayDiscussion = value; }
        }
        #endregion

        #region bool IsPublished
        public bool IsPublished
        {
            get { return isPublished; }
        }
        #endregion

        #region string Url
        public string Url
        {
            get { return url; }
        }
        #endregion

        #region string AdminComment
        public string AdminComment
        {
            get { return admincomment; }
            set { admincomment = value; }
        }
        #endregion

        #region DateTime Created
        public DateTime Created
        {
            get { return created; }
        }
        #endregion

        #region DateTime Modified
        public DateTime Modified
        {
            get { return modified; }
        }
        #endregion

        #region int ModifiedBy
        public int ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }
        #endregion

        #region int Update()
        public int Update()
        {
            SqlCommand command = new SqlCommand("Page_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Id", Id));

            command.Parameters.Add(new SqlParameter("@MenuId", MenuId));

            command.Parameters.Add(new SqlParameter("@can_delete", CanDelete));
            command.Parameters.Add(new SqlParameter("@can_editbody", CanEditBody));
            command.Parameters.Add(new SqlParameter("@can_editadmincomment", CanEditAdminComment));
            command.Parameters.Add(new SqlParameter("@can_changemenu", CanChangeMenu));

            if (RedirectLink != null)
                command.Parameters.Add(new SqlParameter("@RedirectLink", RedirectLink));
            else
                command.Parameters.Add(new SqlParameter("@RedirectLink", System.DBNull.Value));

            if (PageTitle != null)
                command.Parameters.Add(new SqlParameter("@PageTitle", PageTitle));
            else
                command.Parameters.Add(new SqlParameter("@PageTitle", System.DBNull.Value));

            if (ContentTitle != null)
                command.Parameters.Add(new SqlParameter("@ContentTitle", ContentTitle));
            else
                command.Parameters.Add(new SqlParameter("@ContentTitle", System.DBNull.Value));

            if (BodyDraft != null)
                command.Parameters.Add(new SqlParameter("@BodyDraft", BodyDraft));
            else
                command.Parameters.Add(new SqlParameter("@BodyDraft", System.DBNull.Value));

            command.Parameters.Add(new SqlParameter("@DisplayContent", DisplayContent));
            command.Parameters.Add(new SqlParameter("@DisplayPublished", DisplayPublished));
            command.Parameters.Add(new SqlParameter("@DisplayBookmarking", DisplayBookmarking));
            command.Parameters.Add(new SqlParameter("@DisplayPrintable", DisplayPrintable));
            command.Parameters.Add(new SqlParameter("@DisplayDiscussion", DisplayDiscussion));

            if (AdminComment != null)
                command.Parameters.Add(new SqlParameter("@AdminComment", AdminComment));
            else
                command.Parameters.Add(new SqlParameter("@AdminComment", System.DBNull.Value));

            command.Parameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));

            Connection.Open();
            int result = command.ExecuteNonQuery();
            Connection.Close();

            if (result >= 0)
                loadRecord(id); //some values might have been changed by the db code
            return result;
        }
        #endregion

        #region private

        private void loadRecord(int recordId)
        {
            SqlCommand command = new SqlCommand("Page_Get", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Id", recordId));

            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
                throw new Core.AppException("Record with id = " + recordId + " not found.");
            reader.Read();

            if (reader[0] != System.DBNull.Value)
                id = reader.GetInt32(0);

            if (reader[1] != System.DBNull.Value)
                menuId = reader.GetInt32(1);

            if (reader[2] != System.DBNull.Value)
                pageCategoryId = reader.GetInt32(2);

            if (reader[3] != System.DBNull.Value)
                canDelete = reader.GetBoolean(3);

            if (reader[4] != System.DBNull.Value)
                canEditBody = reader.GetBoolean(4);

            if (reader[5] != System.DBNull.Value)
                canEditAdminComment = reader.GetBoolean(5);

            if (reader[6] != System.DBNull.Value)
                canChangeMenu = reader.GetBoolean(6);

            if (reader[7] != System.DBNull.Value)
                redirectLink = reader.GetString(7);

            if (reader[8] != System.DBNull.Value)
                pageTitle = reader.GetString(8);

            if (reader[9] != System.DBNull.Value)
                contentTitle = reader.GetString(9);

            if (reader[10] != System.DBNull.Value)
                body = reader.GetString(10);

            if (reader[11] != System.DBNull.Value)
                bodyDraft = reader.GetString(11);

            if (reader[12] != System.DBNull.Value)
                published = reader.GetDateTime(12);

            if (reader[13] != System.DBNull.Value)
                displayContent = reader.GetBoolean(13);

            if (reader[14] != System.DBNull.Value)
                displayPublished = reader.GetBoolean(14);

            if (reader[15] != System.DBNull.Value)
                displayBookmarking = reader.GetBoolean(15);

            if (reader[16] != System.DBNull.Value)
                displayPrintable = reader.GetBoolean(16);

            if (reader[17] != System.DBNull.Value)
                displayDiscussion = reader.GetBoolean(17);

            if (reader[18] != System.DBNull.Value)
                isPublished = reader.GetBoolean(18);

            if (reader[19] != System.DBNull.Value)
                url = reader.GetString(19);

            if (reader[20] != System.DBNull.Value)
                admincomment = reader.GetString(20);

            if (reader[21] != System.DBNull.Value)
                created = reader.GetDateTime(21);

            if (reader[22] != System.DBNull.Value)
                modified = reader.GetDateTime(22);

            if (reader[23] != System.DBNull.Value)
                modifiedBy = reader.GetInt32(23);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private int menuId;
        private int pageCategoryId;
        private bool canDelete;
        private bool canEditBody;
        private bool canEditAdminComment;
        private bool canChangeMenu;
        private string redirectLink;
        private string pageTitle;
        private string contentTitle;
        private string body;
        private string bodyDraft;
        private DateTime published;
        private bool displayContent;
        private bool displayPublished;
        private bool displayBookmarking;
        private bool displayPrintable;
        private bool displayDiscussion;
        private bool isPublished;
        private string url;
        private string admincomment;
        private DateTime created;
        private DateTime modified;
        private int modifiedBy;

        #endregion
    }
}