using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.EntityModels
{
    public class Autor
    {
        [Key]
        public int ID { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }

        [ForeignKey(nameof(Grad))]
        public int Grad_ID { get; set; }
        public Grad Grad { get; set; }
    }
}
