using Microsoft.AspNetCore.Mvc.Rendering;
using online_knjizara.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class KorisnickiProfilUrediVM
    {
        public int ProfilId { get; set; }
        [Required(ErrorMessage = "Unesite ime!")]
        [RegularExpression("[A-Z]{1}[a-zA-Z]+", ErrorMessage = "npr. John")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Unesite prezime!")]
        [RegularExpression("[A-Z]{1}[a-zA-Z]+", ErrorMessage = "npr. Doe")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Unesite datum rodjenja!")]

        public DateTime DatumRodjenja { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }

        public string KorisnickoIme { get; set; }
        public string Password { get; set; }

        public int Grad_ID { get; set; }
        public List<SelectListItem> Grad { get; set; }
        public string Adresa { get; set; }

        public bool JelZaposlenik { get; set; }

    }
}
