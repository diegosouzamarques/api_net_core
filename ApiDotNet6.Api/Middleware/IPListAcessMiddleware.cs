using System.Net;
using System.Text;

namespace ApiDotNet6.Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class IPListAcessMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _iplist = new List<string>() { /*"::1",*/ "127.0.0.1", "192.168.1.2", "192.168.1.3", "168.195.210.12" };

        public IPListAcessMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext httpContext)
        {
           
            if (httpContext.Request.Path.Equals("/index.html") || httpContext.Request.Path.Equals("/swagger/v1/swagger.json"))
            {
                await _next(httpContext);
            }
            else
            {
                var remoteIpAddress = httpContext.Connection.RemoteIpAddress;
                var ip = remoteIpAddress.ToString();

                if (!_iplist.Contains(ip))
                    await ReturnErrorResponse(httpContext);
                else
                    await _next(httpContext);
            }                
        }

        private async Task ReturnErrorResponse(HttpContext context)
        {
            var file = new FileInfo(@"Middleware\notpermited.html");
            byte[] buffer;
            if (file.Exists)
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "text/html";

                buffer = File.ReadAllBytes(file.FullName);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "text/plain";
                buffer = Encoding.UTF8
                    .GetBytes("Unable to find the requested file");
            }

            context.Response.ContentLength = buffer.Length;

            using (var stream = context.Response.Body)
            {
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class IPListAcessMiddlewareExtensions
    {
        public static IApplicationBuilder UseIPListAcessMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IPListAcessMiddleware>();
        }
    }
}
