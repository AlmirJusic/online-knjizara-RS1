using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class ZaposlenikUrediVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Unesite strucnu spremu zaposlenika.")]
        public string StrucnaSprema { get; set; }
        [Required(ErrorMessage = "Odaberite korisnika!")]

        public int Korisnik_ID { get; set; }
        public List<SelectListItem> korisnici { get; set; }


        
    }
}
