using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class ObavijestiUrediVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Unesite naziv obavijesti!")]

        public string Naziv { get; set; }
        [Required(ErrorMessage = "Unesite sadrzaj obavijesti!")]
        public string Sadrzaj { get; set; }
        [Required(ErrorMessage = "Unesite datum!")]
        public DateTime Datum { get; set; }
        public IFormFile Slika { get; set; }
    }
}
