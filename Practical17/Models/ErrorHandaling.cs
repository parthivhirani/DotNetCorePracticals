namespace Practical17.Models
{
    public class ErrorHandaling
    {
        private readonly RequestDelegate _next;

        public ErrorHandaling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            await _next(httpContext);

            if (httpContext.Response.StatusCode == 404 && !httpContext.Response.HasStarted && !httpContext.Request.Path.StartsWithSegments("/Error"))
            {
                httpContext.Response.Redirect("/Error/NotFound");
            }
        }
    }

    public static class ErrorHandalingExtensions
    {
        public static IApplicationBuilder UseErrorHandaling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandaling>();
        }
    }

}
