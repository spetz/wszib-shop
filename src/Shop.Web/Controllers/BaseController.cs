using Microsoft.AspNetCore.Mvc;
using System;

namespace Shop.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Guid CurrentUserId => User.Identity.IsAuthenticated ?
                Guid.Parse(User.Identity.Name) : Guid.Empty;
    }
}
