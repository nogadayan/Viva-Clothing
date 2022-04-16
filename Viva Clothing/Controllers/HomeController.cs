using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Viva_Clothing.Models;

namespace Viva_Clothing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string connectionString = "Server=informatica.st-maartenscollege.nl; Port=3306; Database=110502; Uid=110502; Pwd=inf2021sql;";

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

                    while (reader.Read())
                    {
                        var product = new Product();

                        GetProduct(reader, product);

                        products.Add(product);
                    }
                }
            }

            return products;
        }

         public List<Maattabel> GetMaat(int limit)
        {
            List<Maattabel> maten = new List<Maattabel>();


            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"select * from product_maat limit 0,{limit}", conn);


                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var maat = new Maattabel();

                        GetProduct_maat(reader, maat);

                        maten.Add(maat);
                    }
                }
            }

            return maten;
        }

        private void GetProduct_maat(MySqlDataReader reader, Maattabel maat)
        {
            maat.Id = reader["id"].ToString();
            maat.Product_id = reader["product_id"].ToString();
            maat.Maats = reader.GetString("maats");
            maat.Maatm = reader.GetString("maatm");
            maat.Maatl = reader.GetString("maatl");
            maat.Voorraads = reader["voorraads"].ToString();
            maat.Voorraadm = reader["voorraadm"].ToString();
            maat.Voorraadl = reader["voorraadl"].ToString();
            maat.Prijs = reader.GetString("prijs");
            maat.Fotolos = reader.GetString("fotolos");
        }

        private static void GetProduct(MySqlDataReader reader, Product product)
        {
            product.Id = reader["id"].ToString();
            product.Name = reader.GetString("naam");
            product.Voorkant = reader.GetString("voorkant");
            product.Achterkant = reader.GetString("achterkant");
            product.Zijkant = reader.GetString("zijkant");
            product.Fotolos = reader.GetString("fotolos");
            product.Beschrijving = reader.GetString("beschrijving");
            product.Prijs = reader.GetString("prijs");
        }

        [Route("succes")]
        public IActionResult Succes()
        {
            return View();
        }

        private void SavePerson(Klant klant)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(voornaam, achternaam, email, opmerking) VALUES(?voornaam, ?achternaam, ?email, ?opmerking)", conn);

                cmd.Parameters.Add("?voornaam", MySqlDbType.Text).Value = klant.Voornaam;
                cmd.Parameters.Add("?achternaam", MySqlDbType.Text).Value = klant.Achternaam;
                cmd.Parameters.Add("?email", MySqlDbType.Text).Value = klant.Email;
                cmd.Parameters.Add("?opmerking", MySqlDbType.Text).Value = klant.Opmerking;
                cmd.ExecuteNonQuery();
            }
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [Route("contact")]
        public IActionResult Contact(Klant klant)
        {

            if (ModelState.IsValid)
            {
                SavePerson(klant);

                return Redirect("/succes");
            }
            return View(klant);
        }


        [Route("detail/{id}")]
        public IActionResult Detailpagina(string id)
        {

            var model = GetDetails(id);
            return View(model);
        }

        [Route("bestel/{id}")]
        public IActionResult Bestelpagina(string id)
        {

            var Model = GetDetails(id);
            return View(Model);
        }

        [Route("aankoop/{id}")]
        public IActionResult Aankoop(string id)
        {

            var Model = Get_MaatTabel_Details(id);
            return View(Model);
        }

        [Route("overzicht")]
        public IActionResult Overzicht()
        {
            var names = GetNames(99);
            return View(names);
        }

        [Route("betalen")]
        public IActionResult Betalen()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public Product GetDetails(string id)
        {
            List<Product> products = new List<Product>(); 


                using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"select * from product where id = {id}", conn);


                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var product = new Product();

                        GetProduct(reader, product);


                        products.Add(product);
                    }
                }
            }

            return products[0];
        }
        public Maattabel Get_MaatTabel_Details(string id)
        {
            List<Maattabel> maten = new List<Maattabel>();


            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"select * from product_maat where id = {id}", conn);


                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var maat = new Maattabel();

                        GetProduct_maat(reader, maat);


                        maten.Add(maat);
                    }
                }
            }

            return maten[0];
        }
    }
}
