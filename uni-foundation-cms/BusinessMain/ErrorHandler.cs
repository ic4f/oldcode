using System;
using System.Web;
using c = Core;

namespace Foundation.BusinessMain
{
    public class ErrorHandler
    {
        public static void HandleError(Exception currentError)
        {
            if (ConfigurationHelper.HandleError)
            {
                if (!(currentError is c.AppException))
                    c.AppException.LogError(currentError);

                HttpContext.Current.Server.ClearError();
                HttpContext.Current.Response.Redirect(UrlHelper.GetPageErrorUrl());
            }
        }

        private ErrorHandler() { }
    }
}
