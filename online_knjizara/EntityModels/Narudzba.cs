using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.EntityModels
{
    public class Narudzba
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey(nameof(Knjiga))]
        public int Knjiga_ID { get; set; }
        public Knjiga Knjiga { get; set; }

        [ForeignKey(nameof(Korisnik))]
        public int Korisnik_ID { get; set; }
        public Korisnik Korisnik { get; set; }

        public int Kolicina { get; set; }
        public float  Cijena { get; set; }

        public DateTime DatumVrijeme { get; set; }

    }
}
