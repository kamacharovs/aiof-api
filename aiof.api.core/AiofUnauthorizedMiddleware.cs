using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using aiof.api.data;

namespace aiof.api.core
{
    public class AiofUnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;

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
            if (Constants.AllowedUnauthorizedStatusCodes.Contains(httpContext.Response.StatusCode) is false)
                return;

            var statusCode = httpContext.Response.StatusCode;
            var aiofProblem = new AiofProblemDetailBase();

            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    aiofProblem.Code = StatusCodes.Status401Unauthorized;
                    aiofProblem.Message = Constants.DefaultUnauthorizedMessage;
                    break;
                case StatusCodes.Status403Forbidden:
                    aiofProblem.Code = StatusCodes.Status403Forbidden;
                    aiofProblem.Message = Constants.DefaultForbiddenMessage;
                    break;
            }

            var aiofProblemJson = JsonSerializer
                .Serialize(aiofProblem, new JsonSerializerOptions { IgnoreNullValues = true });

            httpContext.Response.ContentType = Constants.ApplicationProblemJson;

            await httpContext.Response
                .WriteAsync(aiofProblemJson);
        }
    }
}