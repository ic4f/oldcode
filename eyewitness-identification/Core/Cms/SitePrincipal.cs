using System;
using System.Security;

namespace Core.Cms
{
    public class SitePrincipal : System.Security.Principal.IPrincipal
    {
        private SiteIdentity userIdentity;

        public SitePrincipal(SiteIdentity newIdentity)
        {
            userIdentity = newIdentity;
        }

        public System.Security.Principal.IIdentity Identity { get { return userIdentity; } }

        public bool IsInRole(string role)
        {
            return false; //this method is NOT implemented. Use HasPermission instead.
        }

        public bool HasPermission(int permissionCode)
        {
            return userIdentity.Permissions.Contains(permissionCode.ToString());
        }
    }
}