using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class KnjigaKomentariVM
    {
        public class Komentari
        {
             public string User { get; set; }
             public string Komentar { get; set; }
             public int Ocjena { get; set; }
        }
        public int KnjigaID { get; set; }
        public List<Komentari> komentari { get; set; }
    }
}
