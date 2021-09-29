using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class NarudzbaPrikazVM
    {
        public int ID { get; set; }
        public string Knjiga { get; set; }
        public string Korisnik { get; set; }
        public int Kolicina { get; set; }
        public float Cijena { get; set; }
        public DateTime DatumVrijeme { get; set; }
    }
}
