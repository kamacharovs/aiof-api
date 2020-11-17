using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;

using aiof.api.data;

namespace aiof.api.core
{
    public class AiofUnauthorizedMiddleware
    {
    private readonly RequestDelegate _next;

        private const string _defaultUnauthorizedMessage = "You don't have access to this API";
        private const string _defaultForbiddenMessage = "You don't have permission to access this API";
        private IEnumerable<int> _vallowedStatusCodes = new int[] 
        { 
            StatusCodes.Status401Unauthorized, 
            StatusCodes.Status403Forbidden
        };

        public AiofUnauthorizedMiddleware(
            RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _next(httpContext);
            await WriteUnauthorizedResponseAsync(httpContext);
        }

        public async Task WriteUnauthorizedResponseAsync(
            HttpContext httpContext)
        {
            if (_vallowedStatusCodes.Contains(httpContext.Response.StatusCode) is false)
                return;

            var statusCode = httpContext.Response.StatusCode;
            var aiofProblem = new AiofProblemDetailBase();

            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    aiofProblem.Code = StatusCodes.Status401Unauthorized;
                    aiofProblem.Message = _defaultUnauthorizedMessage;
                    break;
                case StatusCodes.Status403Forbidden:
                    aiofProblem.Code = StatusCodes.Status403Forbidden;
                    aiofProblem.Message = _defaultForbiddenMessage;
                    break;
            }

            var aiofProblemJson = JsonSerializer
                .Serialize(aiofProblem, new JsonSerializerOptions { IgnoreNullValues = true });

            httpContext.Response.ContentType = Keys.ApplicationProblemJson;
            
            await httpContext.Response
                .WriteAsync(aiofProblemJson);
        }
    }
}