using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class ZanrUrediVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Unesite naziv žanra.")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Unesite opis žanra.")]
        public string Opis { get; set; }
    }
}
