using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Simple_forum.Other.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserIdString(this HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return Convert.ToString(Guid.Empty);
            }
            var q = httpContext.User.Claims.Single(x => x.Type.Contains("nameidentifier")).Value;
            return q;
        }

        public static Guid GetUserIdGuid(this HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return Guid.Empty;
            }

            return Guid.Parse(httpContext.User.Claims.Single(x => x.Type.Contains("nameidentifier")).Value);
        }
    }
}
