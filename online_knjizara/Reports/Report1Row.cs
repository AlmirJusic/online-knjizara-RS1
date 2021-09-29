using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Report1Row
    {
        public string Knjiga { get; set; }

        public string Korisnik { get; set; }

        public int Kolicina { get; set; }
        public float Cijena { get; set; }
        public float UkupnaCijena { get; set; }

        public string DatumVrijeme { get; set; }

        public static List<Report1Row> Get()
        {
            return new List<Report1Row> { };
        }
    }
}