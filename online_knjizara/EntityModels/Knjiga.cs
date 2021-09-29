using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.EntityModels
{
    public class Knjiga
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey(nameof(Autor))]
        public int Autor_ID { get; set; }
        public Autor Autor { get; set; }

        [ForeignKey(nameof(Izdavac))]
        public int Izdavac_ID { get; set; }
        public Izdavac Izdavac { get; set; }

        [ForeignKey(nameof(Skladiste))]
        public int Skladiste_ID { get; set; }
        public Skladiste Skladiste { get; set; }

        [ForeignKey(nameof(Zanr))]
        public int Zanr_ID { get; set; }
        public Zanr Zanr { get; set; }

        [ForeignKey(nameof(Stanje))]
        public int Stanje_ID { get; set; }
        public Stanje Stanje { get; set; }

        [ForeignKey(nameof(Odlomak))]
        public int Odlomak_ID { get; set; }
        public Odlomak Odlomak { get; set; }

        public string Naziv { get; set; }
        public float Cijena { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public byte[] Slika { get; set; }

    }
}
