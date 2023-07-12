using Practical20.Custom_Middleware;
using System.Net;

namespace Practical20.Custom_Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound && !httpContext.Response.HasStarted && !httpContext.Request.Path.StartsWithSegments("/ErrorHandler"))
            {
                httpContext.Response.Redirect("/ErrorHandler/NotFound");
            }

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.Ambiguous && !httpContext.Response.HasStarted && !httpContext.Request.Path.StartsWithSegments("/ErrorHandler"))
            {
                httpContext.Response.Redirect("/ErrorHandler/Ambiguous");
            }

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.BadRequest && !httpContext.Response.HasStarted && !httpContext.Request.Path.StartsWithSegments("/ErrorHandler"))
            {
                httpContext.Response.Redirect("/ErrorHandler/BadRequest");
            }

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.InternalServerError && !httpContext.Response.HasStarted && !httpContext.Request.Path.StartsWithSegments("/ErrorHandler"))
            {
                httpContext.Response.Redirect("/ErrorHandler/InternalServerError");
            }

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.LoopDetected && !httpContext.Response.HasStarted && !httpContext.Request.Path.StartsWithSegments("/ErrorHandler"))
            {
                httpContext.Response.Redirect("/ErrorHandler/LoopDetected");
            }

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized && !httpContext.Response.HasStarted && !httpContext.Request.Path.StartsWithSegments("/ErrorHandler"))
            {
                httpContext.Response.Redirect("/ErrorHandler/UnAuthorized");
            }

        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ErrorHandalingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandalingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
