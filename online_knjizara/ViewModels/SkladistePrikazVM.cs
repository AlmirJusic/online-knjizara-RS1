using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class SkladistePrikazVM
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public int Kolicina { get; set; }
        public string Grad { get; set; }
    }
}
