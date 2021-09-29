using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class StanjeUrediVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Unesite tip stanja.")]
        public string TipStanja { get; set; }
        
    }
}
