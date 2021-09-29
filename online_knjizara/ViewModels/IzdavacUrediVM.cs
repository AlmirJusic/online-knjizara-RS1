using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class IzdavacUrediVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Unesite naziv izdavaca.")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Unesite telefon izdavaca.")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Unesite adresu izdavaca.")]
        public string Adresa { get; set; }
        [Required(ErrorMessage = "Unesite email izdavaca.")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Neispravan format e-mail-a.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Unesite grad izdavaca.")]
        public int Grad_ID { get; set; }
        public List<SelectListItem> Grad { get; set; }
    }
}
