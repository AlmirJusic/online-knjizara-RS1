using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class KnjigaPrikazVM
    {
        public int ID { get; set; }
        public string Autor { get; set; }
        public string Izdavac { get; set; }
        public string Skladiste { get; set; }
        public string Zanr { get; set; }
        public string Stanje { get; set; }
        public string NazivOdlomka { get; set; }
        public string SadrzajOdlomka { get; set; }
        public string Naziv { get; set; }
        public float Cijena { get; set; }
        public DateTime DatumIzdavanja { get; set; }

        public byte[] Slika { get; set; }
    }

}
