using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Responses.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyPlanner.Infrastructure.Authorization
{
    public sealed class AuthorizationErrorWriter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationErrorWriter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        internal async Task Write(Error error)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if(httpContext is null)
            {
                return;
            }

            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await httpContext.Response.WriteAsJsonAsync(Result.Failure(error));
        }
    }
}
