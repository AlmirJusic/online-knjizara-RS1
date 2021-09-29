using Microsoft.AspNetCore.Mvc.Rendering;
using online_knjizara.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class RegistracijaSnimiVM
    {
        public List<SelectListItem> gradovi { get; set; }

        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Ime ne smije biti prazno.", MinimumLength = 1)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Prezime ne smije biti prazno.", MinimumLength = 1)]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Adresa ne smije biti prazna.", MinimumLength = 1)]
        public string Adresa { get; set; }

        public int GradID { get; set; }
        [Required(ErrorMessage = "Obavezno**")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Neispravan format e-mail-a.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Telefon ne smije biti prazan.", MinimumLength = 1)]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Obavezno**")]
        public DateTime DatumRodjenja { get; set; }

        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Korisnicko ime ne smije biti prazno.", MinimumLength = 1)]
        public string korisnicko { get; set; }



        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Lozinka ne smije biti prazna.", MinimumLength = 1)]
        public string korisnikovalozinka { get; set; }

    }
}
