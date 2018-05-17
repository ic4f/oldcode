using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class PageData : DataClass
    {
        #region constructor 
        public PageData() : base() { }
        #endregion

        #region bool SystemIsLoaded
        public bool SystemIsLoaded()
        {
            SqlCommand command = new SqlCommand("Page_SystemIsLoaded", Connection);
            command.CommandType = CommandType.StoredProcedure;
            Connection.Open();
            bool result = Convert.ToBoolean(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region Create
        public int Create(
            int menuid,
            int pageCategoryId,
            bool can_Delete,
            bool can_EditBody,
            bool can_EditAdminComment,
            bool can_ChangeMenu,
            string redirectLink,
            string pagetitle,
            string contenttitle,
            string bodydraft,
            bool displayContent,
            bool displayPublishedDate,
            bool displayBookmarking,
            bool displayPrintable,
            bool displayDiscussion,
            bool isPublished,
            string partialUrl,
            string admincomment,
            int modifiedby)
        {
            SqlCommand command = new SqlCommand("Page_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@menuid", menuid));
            command.Parameters.Add(new SqlParameter("@pageCategoryId", pageCategoryId));
            command.Parameters.Add(new SqlParameter("@can_Delete", can_Delete));
            command.Parameters.Add(new SqlParameter("@can_EditBody", can_EditBody));
            command.Parameters.Add(new SqlParameter("@can_EditAdminComment", can_EditAdminComment));
            command.Parameters.Add(new SqlParameter("@can_ChangeMenu", can_ChangeMenu));
            command.Parameters.Add(new SqlParameter("@redirectLink", redirectLink));
            command.Parameters.Add(new SqlParameter("@pagetitle", pagetitle));
            command.Parameters.Add(new SqlParameter("@contenttitle", contenttitle));
            command.Parameters.Add(new SqlParameter("@bodydraft", bodydraft));
            command.Parameters.Add(new SqlParameter("@displayContent", displayContent));
            command.Parameters.Add(new SqlParameter("@displayPublishedDate", displayPublishedDate));
            command.Parameters.Add(new SqlParameter("@displayBookmarking", displayBookmarking));
            command.Parameters.Add(new SqlParameter("@displayPrintable", displayPrintable));
            command.Parameters.Add(new SqlParameter("@displayDiscussion", displayDiscussion));
            command.Parameters.Add(new SqlParameter("@isPublished", isPublished));
            command.Parameters.Add(new SqlParameter("@partialUrl", partialUrl));
            command.Parameters.Add(new SqlParameter("@admincomment", admincomment));
            command.Parameters.Add(new SqlParameter("@modifiedby", modifiedby));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetTitles
        public DataTable GetTitles()
        {
            SqlCommand command = new SqlCommand("Page_GetTitles", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetList
        public DataTable GetList()
        {
            SqlCommand command = new SqlCommand("Page_GetList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Publish
        public void Publish(int id, int modifiedby)
        {
            SqlCommand command = new SqlCommand("Page_Publish", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@modifiedby", modifiedby));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetCustomPages
        public DataTable GetCustomPages(string sortexp)
        {
            SqlCommand command = new SqlCommand("Page_GetCustomPages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetStandardPages
        public DataTable GetStandardPages(string sortexp)
        {
            SqlCommand command = new SqlCommand("Page_GetStandardPages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetStandardPagesWithQuery
        public DataTable GetStandardPagesWithQuery(string query)
        {
            SqlCommand command = new SqlCommand("Page_GetStandardPagesWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPublishedPages
        public DataTable GetPublishedPages(string sortexp)
        {
            SqlCommand command = new SqlCommand("Page_GetPublishedPages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPublishedPagesWithQuery
        public DataTable GetPublishedPagesWithQuery(string query)
        {
            SqlCommand command = new SqlCommand("Page_GetPublishedPagesWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPublishedByMenuId
        public DataTable GetPublishedByMenuId(int menuId)
        {
            SqlCommand command = new SqlCommand("Page_GetPublishedByMenuId", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@menuId", menuId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPublishedPagesList
        public DataTable GetPublishedPagesList()
        {
            SqlCommand command = new SqlCommand("Page_GetPublishedPagesList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPublishedPagesListWithQuery
        public DataTable GetPublishedPagesListWithQuery(string query)
        {
            SqlCommand command = new SqlCommand("Page_GetPublishedPagesListWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAllPages
        public DataTable GetAllPages(string sortexp)
        {
            SqlCommand command = new SqlCommand("Page_GetAllPages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAllPagesWithQuery
        public DataTable GetAllPagesWithQuery(string query)
        {
            SqlCommand command = new SqlCommand("Page_GetAllPagesWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("Page_Delete", parameters);
        }
        #endregion

        #region GetInboundPageLinks
        public DataSet GetInboundPageLinks(int pageId)
        {
            SqlCommand command = new SqlCommand("Page_GetInboundPageLinks", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            return ExecDataSet(command);
        }
        #endregion

        #region GetInboundModuleLinks
        public DataSet GetInboundModuleLinks(int pageId)
        {
            SqlCommand command = new SqlCommand("Page_GetInboundModuleLinks", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            return ExecDataSet(command);
        }
        #endregion

        #region GetDependentMenus
        public DataTable GetDependentMenus(int pageId)
        {
            SqlCommand command = new SqlCommand("Page_GetDependentMenus", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetOutboundPageLinks
        public DataSet GetOutboundPageLinks(int pageId)
        {
            SqlCommand command = new SqlCommand("Page_GetOutboundPageLinks", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            return ExecDataSet(command);
        }
        #endregion

        #region GetOutboundFileLinks
        public DataSet GetOutboundFileLinks(int pageId)
        {
            SqlCommand command = new SqlCommand("Page_GetOutboundFileLinks", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            return ExecDataSet(command);
        }
        #endregion

        #region GetOutboundImageLinks
        public DataSet GetOutboundImageLinks(int pageId)
        {
            SqlCommand command = new SqlCommand("Page_GetOutboundImageLinks", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            return ExecDataSet(command);
        }
        #endregion

        #region GetAllPagesByTag
        public DataTable GetAllPagesByTag(int tagId)
        {
            SqlCommand command = new SqlCommand("Page_GetAllPagesByTag", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@tagId", tagId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
