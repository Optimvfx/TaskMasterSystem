using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserAuthenticationSystemV2.Context;
using UserAuthenticationSystemV2.Generators.PasswordEncryptor;
using UserAuthenticationSystemV2.Models;
using UserAuthenticationSystemV2.Systems;

namespace UserAuthenticationSystemV2.Controllers.Base
{
    public abstract class CookieReadController : Controller
    {
        private const string LoginClaimName = ClaimsIdentity.DefaultNameClaimType;
        private const string PasswordClaimName = ClaimsIdentity.DefaultNameClaimType;
        
        public bool TryGetUserLoginFormByCookie(out LoginViewModel loginViewInfo)
        {
            loginViewInfo = default;
            
            if (HttpContext.User.Claims.Any(claim => claim.Type == LoginClaimName) == false ||
                HttpContext.User.Claims.Any(claim => claim.Type == PasswordClaimName) == false)
                return false;

            var userLogin = HttpContext.User.Claims.First(claim => claim.Type == LoginClaimName).Value;
            var userPassword = HttpContext.User.Claims.First(claim => claim.Type == PasswordClaimName).Value;

            loginViewInfo = new LoginViewModel(userLogin, userPassword);

            return true;
        }
    }
}