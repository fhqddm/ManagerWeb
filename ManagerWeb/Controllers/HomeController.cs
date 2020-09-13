using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ManagerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ManagerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult MyError(int errorId)
        {
            if (errorId == 1)
            {
                ViewData["ErrorMsg"] = "Wrong user name or password";
            }
            else if (errorId == 2)
            {
                ViewData["ErrorMsg"] = "Username already exists";
            }
            else if (errorId == 3)
            {
                ViewData["ErrorMsg"] = "Access Deny";
            }
            return View();
        }

        public IActionResult AccessDeny()
        {
            return View();
        }
    }
}
