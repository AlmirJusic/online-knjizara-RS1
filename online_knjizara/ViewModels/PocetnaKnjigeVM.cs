using online_knjizara.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class PocetnaKnjigeVM
    {
        public class Knjiga
        { 
             public int ID { get; set; }
             public string Naziv { get; set; }
             public string Autor { get; set; }
             public byte[] Slika { get; set; }
        }
        public class Zanr
        {
            public int ID { get; set; }
            public string Naziv { get; set; }
        }
        public List<Knjiga> knjige { get; set; }
        public List<Zanr> zanrovi { get; set; }
    }
}
