using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class KnjigaUrediVM
    {
        public int ID { get; set; }
        
        

        [Required(ErrorMessage = "Unesite autora knjige.")]
        public int Autor_ID { get; set; }
        public List<SelectListItem> Autor { get; set; }

        [Required(ErrorMessage = "Unesite izdavaca knjige.")]
        public int Izdavac_ID { get; set; }
        public List<SelectListItem> Izdavac { get; set; }

        [Required(ErrorMessage = "Unesite skladiste knjige.")]
        public int Skladiste_ID { get; set; }
        public List<SelectListItem> Skladiste { get; set; }

        [Required(ErrorMessage = "Unesite zanr knjige.")]
        public int Zanr_ID { get; set; }
        public List<SelectListItem> Zanr { get; set; }

        [Required(ErrorMessage = "Unesite stanje knjige.")]
        public int Stanje_ID { get; set; }
        public List<SelectListItem> Stanje { get; set; }

        [Required(ErrorMessage = "Unesite odlomak knjige.")]
        public int Odlomak_ID { get; set; }
        public List<SelectListItem> Odlomak { get; set; }

        [Required(ErrorMessage = "Unesite naziv knjige.")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Unesite cijenu knjige.")]
        public float Cijena { get; set; }

        [Required(ErrorMessage = "Unesite datum izdavanja knjige.")]
        public DateTime DatumIzdavanja { get; set; }

        
        public IFormFile Slika { get; set; }
    }
}
