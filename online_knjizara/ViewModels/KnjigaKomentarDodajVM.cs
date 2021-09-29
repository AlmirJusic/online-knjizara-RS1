using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class KnjigaKomentarDodajVM
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Komentar { get; set; }
        public int Ocjena { get; set; }
    }
}
