using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Shop.Web.Framework
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CookieAuthAttribute : AuthorizeAttribute
    {
        public CookieAuthAttribute(string policy = "")
        {
            AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme;
            Policy = policy;
        }
    }
}
