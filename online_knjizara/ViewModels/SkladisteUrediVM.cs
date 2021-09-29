using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class SkladisteUrediVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Unesite naziv skladista.")]
        public string Naziv { get; set; }
       
        [Required(ErrorMessage = "Unesite adresu skladista.")]
        public string Adresa { get; set; }
        [Required(ErrorMessage = "Unesite kolicinu u skladistu.")]
        public int Kolicina { get; set; }
        [Required(ErrorMessage = "Unesite grad skladista.")]
        public int Grad_ID { get; set; }
        public List<SelectListItem> Grad { get; set; }
    }
}
