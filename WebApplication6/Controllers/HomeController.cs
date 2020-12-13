using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication6.Models;

namespace WebApplication6.Controllers
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

        public int Test()
        {
            return 1;
        }

        public int Test2()
        {
            
            var context = SessionSettings.sessionContext;
            context.UserID = 3;

            //SessionSettings.session.Set("SessionContext", context);
            SessionSettings.sessionContext = context;

            return (SessionSettings.sessionContext.UserID);
        }

        public int Test3()
        {
            SessionSettings.sessionContext.UserID = 3;
            return (SessionSettings.sessionContext.UserID);
        }
    }
}
