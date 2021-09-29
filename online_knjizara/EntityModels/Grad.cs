using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.EntityModels
{
    public class Grad
    {
        public int ID { get; set; }
        public string Naziv { get; set; }

        [ForeignKey(nameof(Drzava))]
        public int Drzava_ID { get; set; }
        public Drzava Drzava { get; set; }
    }
}
