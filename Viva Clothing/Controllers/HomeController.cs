using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using MySql.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Viva_Clothing.Models;

namespace Viva_Clothing.Controllers
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

        public List<string> GetNames()
            string connectionString = "Server = 172.16.160.21; Port=3306; Database=fastfood; Uid=110502; Pwd=Inf2021sql;";

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Signup()
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
