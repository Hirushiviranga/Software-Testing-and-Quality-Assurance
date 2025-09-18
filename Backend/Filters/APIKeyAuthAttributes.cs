/*using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
{
    private const string ApiKeyHeader = "X-API-KEY";
    private const string ApiKeyValue = "my-secret-key"; // 🔒 Replace with strong key

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var extractedKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!ApiKeyValue.Equals(extractedKey))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Backend.Filters   //  added proper namespace
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] //  specify usage
    public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeader = "X-API-KEY";
        private const string ApiKeyValue = "my-secret-key"; //  Replace with strong key

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var extractedKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!ApiKeyValue.Equals(extractedKey))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
