
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserRegistration.Models;
using UserRegistration.Services.Contracts;

namespace CookieAuthDemoProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IPasswordEncryption _passwordEncryption;

        public LoginController(IUserRegistrationService userRegistrationService,IPasswordEncryption passwordEncryption)
        {
            _userRegistrationService = userRegistrationService;
           _passwordEncryption = passwordEncryption;
        }
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin([Bind] UserDetails user)
        {
            var userDetails = _userRegistrationService.GetUsers().FirstOrDefault(u => u.UserName == user.UserName);
            if(userDetails!=null)
            {
                var hashedPassword = _passwordEncryption.HashPassword(user.Password);
                if(!userDetails.Password.Equals(hashedPassword))
                {
                    return Redirect("\\Login\\UserLoginError");
                }
            }
            if (userDetails != null)
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userDetails.UserName),
                    new Claim(ClaimTypes.Email, userDetails.Email),
                    new Claim(ClaimTypes.Role, userDetails.Role),
                 };

                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
        [HttpGet]
        public ActionResult UserAccessDenied()
        {
            return View();
        }
    }
}
