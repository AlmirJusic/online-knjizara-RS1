using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class LoginVM
    {
        [StringLength(100, ErrorMessage = "Korisničko ime mora sadržavati mininalno 3 karaktera.", MinimumLength = 3)]
        public string KorisnickoIme { get; set; }
        [StringLength(100, ErrorMessage = "Lozinka mora sadržavati minimalno 4 karaktera.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }
        public bool ZapamtiLozinku { get; set; }
    }
}
