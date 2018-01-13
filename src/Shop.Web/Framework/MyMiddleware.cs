using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Shop.Web.Framework
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine($"My middleware: {httpContext.Request.Path}");
            await _next.Invoke(httpContext);
        }
    }
}
