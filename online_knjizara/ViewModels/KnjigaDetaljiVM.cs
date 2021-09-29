using online_knjizara.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class KnjigaDetaljiVM
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Autor { get; set; }
        public float Cijena { get; set; }
        public string Zanr { get; set; }
        public string Stanje { get; set; }
        public string Izdavac { get; set; }
        public byte[] Slika { get; set; }
        public string Sadrzaj { get; set; }
    }
}
