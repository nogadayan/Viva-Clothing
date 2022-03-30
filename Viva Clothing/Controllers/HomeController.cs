using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System.Collections.Generic;
using System.Diagnostics;
using Viva_Clothing.Models;

namespace Viva_Clothing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string connectionString = "Server=172.16.160.21; Port=3306; Database=110502; Uid=110502; Pwd=inf2021sql;";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var names = GetNames(3);
            return View(names);
        }

        public List<Product> GetNames(int limit)
        {
            List<Product> products = new List<Product>();


            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"select * from product limit 0,{limit}", conn);


                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read()) { 
                        var product = new Product();

                        product.Name = reader.GetString("naam");
                        product.Voorkant = reader.GetString("voorkant");
                        product.Achterkant = reader.GetString("achterkant");
                        product.Zijkant = reader.GetString("zijkant");
                        product.Fotolos = reader.GetString("fotolos");

                        products.Add(product);
                    }
                }
            }

            return products;
        }

        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("detail")]
        public IActionResult Detailpagina()
        {
            return View();
        }

        [Route("overzicht")]
        public IActionResult Overzicht()
        {
            var names = GetNames(99);
            return View(names);
        }

        [Route("signup")]
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
