
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Backend.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeader = "X-API-KEY";
        private const string ApiKeyValue = "my-secret-key"; // Ideally move to config

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var extractedKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!ApiKeyValue.Equals(extractedKey, StringComparison.Ordinal))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
