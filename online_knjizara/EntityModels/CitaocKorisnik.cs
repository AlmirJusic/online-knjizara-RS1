using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.EntityModels
{
    public class CitaocKorisnik
    {
        [Key]
        public int CitaocKorisnik_ID { get; set; }

        [ForeignKey("Korisnik")]
        public int Korisnik_ID { get; set; }
        public Korisnik Korisnik { get; set; }


        [ForeignKey("KorisnickiNalog")]
        public int? KorisnickiNalog_ID { get; set; }
        public KorisnickiNalozi KorisnickiNalog { get; set; }
    }
}
