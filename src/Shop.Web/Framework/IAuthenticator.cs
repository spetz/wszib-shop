using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Web.Framework
{
    public interface IAuthenticator
    {
        Task SignInAsync(ClaimsPrincipal principal);
        Task SignOutAsync();
    }
}
