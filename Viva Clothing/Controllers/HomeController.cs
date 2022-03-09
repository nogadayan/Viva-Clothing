using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using MySql.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Viva_Clothing.Models;
using MySql.Data.MySqlClient;

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
            var names = GetNames();
            return View(names);
        }

        public List<Product> GetNames()
        {
            List<Product> products = new List<Product>();


            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from product limit 0,2", conn);


                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read()) { 
                        var product = new Product();

                        product.Name = reader.GetString("naam");
                        product.Image = reader.GetString("foto"); 

                        products.Add(product);
                    }
                }
            }

            return products;
        }


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
