using Microsoft.AspNetCore.Mvc.Rendering;
using online_knjizara.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class AutorUrediVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Unesite ime autora.")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Unesite prezime autora.")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Unesite adresu autora.")]
        public string Adresa { get; set; }
        [Required(ErrorMessage = "Unesite email autora.")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Neispravan format e-mail-a.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Unesite grad autora.")]
        public int Grad_ID { get; set; }
        public List<SelectListItem> Grad { get; set; }

    }
}
