﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MinhDucEvent.AdminApp.Models;
using MinhDucEvent.Utilities.Constants;
using System.Diagnostics;

namespace MinhDucEvent.AdminApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = User.Identity.Name;
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

        [HttpPost]
        public IActionResult Language(NavigationViewModel viewModel)
        {
            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId,
                viewModel.CurrentLanguageId);

            return Redirect(viewModel.ReturnUrl);
        }
    }
}