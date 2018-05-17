using System;
using System.Collections;
using System.Security;
using System.Web;
using System.Web.Security;

namespace Foundation.BusinessAdmin
{
    public class SiteIdentity : System.Security.Principal.IIdentity
    {
        private ArrayList permissions;
        private int userId;
        private string firstName;
        private string lastName;
        private DateTime created;
        private DateTime expires;
        private bool isExpired;
        private bool isPersistent;

        public SiteIdentity(System.Web.Security.FormsAuthenticationTicket authTicket, HttpCookie infoCookie)
        {
            permissions = new ArrayList();
            string[] permArray = authTicket.UserData.Split('|');
            foreach (string p in permArray)
                permissions.Add(p);

            userId = Convert.ToInt32(authTicket.Name);
            created = authTicket.IssueDate;
            expires = authTicket.Expiration;
            isExpired = authTicket.Expired;
            isPersistent = authTicket.IsPersistent;
            firstName = infoCookie.Values["FirstName"];
            lastName = infoCookie.Values["LastName"];
        }

        public string Name { get { return firstName + " " + lastName; } }

        public string AuthenticationType { get { return "Custom Authentication"; } }

        public bool IsAuthenticated { get { return true; } }

        public int UserId { get { return userId; } }

        public string FirstName { get { return firstName; } }

        public string LastName { get { return lastName; } }

        public DateTime Created { get { return created; } }

        public DateTime Expires { get { return expires; } }

        public bool IsExpired { get { return isExpired; } }

        public bool IsPersistent { get { return isPersistent; } }

        public ArrayList Permissions { get { return permissions; } }
    }
}