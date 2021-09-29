using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class ObavijestiPrikazVM
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime Datum { get; set; }
        
        public string SlikaString { get; set; }
        public byte[] Slika { get; set; }
    }
}
