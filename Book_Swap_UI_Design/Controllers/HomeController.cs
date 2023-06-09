﻿using Book_Swap_Models;
using Book_Swap_Models.Models;
using Book_Swap_UI_Design.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Book_Swap_UI_Design.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = "https://localhost:7177/api/user";
                var obj = new User()
                {
                    UserName = login.UserName,
                    UserKey = login.Password,
                    EmailId =  login.UserName
                };
                var user = client.PostAsJsonAsync(apiUrl + string.Format("/Login"), obj).Result;
                if (user.IsSuccessStatusCode)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login.UserName),
                        new Claim(ClaimTypes.Email, login.UserName)                        
                    };

                    Claim role;

                    if (login.UserName.StartsWith("admin"))
                    {
                        role = new Claim(ClaimTypes.Role, "Admin");
                    }
                    else
                    {
                        role = new Claim(ClaimTypes.Role, "User");
                    }

                    claims.Add(role);

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {                        
                        IsPersistent = false,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return Redirect("/home/index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = "https://localhost:7177/api";
                var obj = new
                {
                    userName = registerModel.Email, userKey = registerModel.Password, emailId = registerModel.Email
                };
                var user = client.PostAsJsonAsync(apiUrl + string.Format("/user/adduser"), obj).Result;
                if (user.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Registration successful!";
                    return View(registerModel);
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again");
                }
            }
            return View(registerModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
               CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}