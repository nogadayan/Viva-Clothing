using System;

namespace Viva_Clothing.Models
{
    public class Product
    {
        public string Id { get; set; }

        public string? Voorkant { get; set; }

        public string Name { get; set; }
        public string Achterkant { get; internal set; }
        public string Zijkant { get; internal set; }
        public string Fotolos { get; internal set; }
    }
}
