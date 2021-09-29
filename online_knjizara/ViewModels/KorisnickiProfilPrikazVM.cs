using online_knjizara.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class KorisnickiProfilPrikazVM
    {
        public int Profil_ID { get; set; }
        public string KorisnickoIme { get; set; }
        public string Password { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string  Grad { get; set; }
        public int Grad_ID { get; set; }
        public string Adresa { get; set; }

        public bool JelZaposlenik { get; set; }



    }
}
