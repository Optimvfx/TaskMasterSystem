using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAuthenticationSystemV2.Context;
using UserAuthenticationSystemV2.Controllers.Base;
using UserAuthenticationSystemV2.Generators.PasswordEncryptor;
using UserAuthenticationSystemV2.Models;
using UserAuthenticationSystemV2.Systems;

namespace UserAuthenticationSystemV2.Controllers
{
    public class AccountController : CookieReadController
    {
        private readonly IPasswordEncryptor _passwordEncryptor;

        private readonly AuthenticationSystem _authenticationSystem;

        public AccountController(ApplicationDbContext dbContext, IPasswordEncryptor passwordEncryptor)
        {
            _authenticationSystem = new AuthenticationSystem(dbContext, passwordEncryptor);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (_authenticationSystem.TryLogin(viewModel, out User user))
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (_authenticationSystem.TryRegister(model, out User user))
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(model);
        }

        [HttpPost]
        public async Task UnAuthenticate()
        {
            await HttpContext.SignOutAsync();
        }

        private async Task Authenticate(User user)
        {
            UnAuthenticate();

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim("password", _passwordEncryptor.Decrypt(user.Password))
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}