using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Test_Task_for_GeeksForLess.Other.Extensions
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
