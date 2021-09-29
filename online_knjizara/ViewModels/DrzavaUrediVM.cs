using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class DrzavaUrediVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Unesite naziv drzave.")]
        public string Naziv { get; set; }
    }
}
