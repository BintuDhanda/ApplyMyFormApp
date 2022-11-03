using rojgar.Models;
using rojgar.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace rojgar.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public HomeController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            var Initializer = new AdminAndRolesInitializer(userManager, roleManager);
            await Initializer.Initialize();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string username, string password, bool rememberMe)
        {
            if(username == null)
            {
                return RedirectToAction("Index");
            }
            var user = await userManager.FindByNameAsync(username);
            if(user == null)
            {
                ModelState.AddModelError("", "Incorrect Login details");
                return View();
            }
            if (await userManager.IsLockedOutAsync(user))
            {
                ModelState.AddModelError("", "Your account has been blocked.");
                return View();
            }
            if ((await userManager.IsInRoleAsync(user, "Admin")) || (await userManager.IsInRoleAsync(user, "Staff")))
            {
                var signIn = await signInManager.PasswordSignInAsync(username, password, rememberMe, false);
                if (signIn.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            ModelState.AddModelError("", "Incorrect login details");
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult TermsAndConditions()
        {
            return View();
        }
        public IActionResult RefundPolicy()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
