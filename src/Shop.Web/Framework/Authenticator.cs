using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Web.Framework
{
    public class Authenticator : IAuthenticator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Authenticator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInAsync(ClaimsPrincipal principal)
        {
            await _httpContextAccessor.HttpContext
                .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}
