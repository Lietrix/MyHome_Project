using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace MyHome.CustomIdentity
{
    public static class UserIdentityFunctions
    {
        public static string GetUsername(string identity) => identity.Substring(0, identity.IndexOf('@'));
    }
}