using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.EntityModels
{
    public class Skladiste
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public int Kolicina { get; set; }

        [ForeignKey(nameof(Grad))]
        public int Grad_ID { get; set; }
        public Grad Grad { get; set; }
    }
}
