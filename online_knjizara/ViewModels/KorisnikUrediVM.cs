using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class KorisnikUrediVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Unesite ime korisnika.")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Unesite prezime korisnika.")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Unesite adresu korisnika.")]
        public string Adresa { get; set; }
        [Required(ErrorMessage = "Unesite email korisnika.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Unesite telefon korisnika.")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Unesite datum rodjenja korisnika.")]
        public DateTime DatumRodjenja { get; set; }
        [Required(ErrorMessage = "Unesite grad korisnika.")]
        public int Grad_ID { get; set; }
        public List<SelectListItem> Grad { get; set; }

        [Required(ErrorMessage = "Unesite korisnicko ime korisnika.")]
        public string KorisnickoIme { get; set; }
    }
}
