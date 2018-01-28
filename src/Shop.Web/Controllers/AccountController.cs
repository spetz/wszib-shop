using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;
using Shop.Web.Framework;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly IAuthenticator _authenticator;

        public AccountController(IUserService userService, ICartService cartService,
            IAuthenticator authenticator)
        {
            _userService = userService;
            _cartService = cartService;
            _authenticator = authenticator;
        }

        [HttpGet("login")]
        public IActionResult Login()
            => View();

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                _userService.Login(viewModel.Email, viewModel.Password);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(viewModel);
            }
            var user = _userService.Get(viewModel.Email);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await _authenticator.SignInAsync(principal);
            _cartService.Create(user.Id);

            return RedirectToAction("Index", "Cart");
        }

        //HTTP DELETE - idealnie
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            await _authenticator.SignOutAsync();
            try
            {
                _cartService.Delete(CurrentUserId);
            }
            catch
            {
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("register")]
        public IActionResult Register()
            => View(new RegisterViewModel());

        [HttpPost("register")]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                _userService.Register(viewModel.Email, viewModel.Password, viewModel.Role);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(viewModel);
            }

            return RedirectToAction(nameof(Login));
        }
    }
}