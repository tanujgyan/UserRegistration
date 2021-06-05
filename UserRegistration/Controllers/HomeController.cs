using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserRegistration.Models;
using UserRegistration.Services.Contracts;

namespace UserRegistration.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRegistrationService _userRegistrationService;

        public HomeController( IUserRegistrationService userRegistrationService)
        {
            _userRegistrationService = userRegistrationService;
        }
        [Authorize]
        public ActionResult Index()
        {
            var userRole = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userName= User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            if(userRole.Equals("User"))
            {
                return Redirect("\\Home\\GetCurrentUser");
            }
            else
            {
                return Redirect("\\Home\\GetUsers"); 
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ActionResult GetUsers()
        {
            
            var users = _userRegistrationService.GetUsers();
            return View(users);
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult GetCurrentUser(string userName)
        {
            var u = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var users = _userRegistrationService.GetCurrentUser(u);
            return View(users);
        }
        [HttpPost]
        public  void AddUserDetails(UserDetails user)
        {
            _userRegistrationService.AddNewUser(user);
        }
        [HttpGet]
        public ActionResult AddUserDetails()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
