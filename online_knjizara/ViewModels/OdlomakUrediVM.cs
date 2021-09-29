using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class OdlomakUrediVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Unesite sadrzaj odlomka.")]
        public string Sadrzaj { get; set; }
        [Required(ErrorMessage = "Unesite naziv odlomka.")]
        public string NazivOdlomka { get; set; }

    }
}
