using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.EntityModels
{
    public class Odlomak
    {
        [Key]
        public int ID { get; set; }
        public string NazivOdlomka { get; set; }
        public string Sadrzaj { get; set; }
    }
}
