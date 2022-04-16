using System;
using System.ComponentModel.DataAnnotations;

namespace Viva_Clothing.Models
{
    public class Klant
    {
        [Required(ErrorMessage = "Vul alstublieft uw voornaam in!")]
        public string Voornaam { get; set; }
        [Required(ErrorMessage = "Vul alstublieft uw achternaam in!")]
        public string Achternaam { get; set; }
        [Required(ErrorMessage = "Vul alstublieft uw email in!")]
        [EmailAddress(ErrorMessage = "Vul alstublieft een geldig emailadres in!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vul alstublieft uw opmerking in!")]
        public string Opmerking { get; set; }

    }
}
