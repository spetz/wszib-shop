using Microsoft.AspNetCore.Builder;

namespace Shop.Web.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<MyMiddleware>();
    }
}
