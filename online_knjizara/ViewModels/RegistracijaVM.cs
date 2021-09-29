using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class RegistracijaVM
    {
        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Ime ne smije biti prazno.", MinimumLength = 1)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Prezime ne smije biti prazno.", MinimumLength = 1)]
        public string Prezime { get; set; }


        //[RegularExpression(@"[0-9]{2}[.]{1}[0-9]{2}[.]{1}[0-9]{4}", ErrorMessage = "Datum je u formatu: dd.mm.yyyy")]
        [Required(ErrorMessage = "Obavezno**")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }

        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Adresa ne smije biti prazna.", MinimumLength = 1)]
        public string Adresa { get; set; }

        //[RegularExpression(@"[0-9]{9}", ErrorMessage = "Kontakt mora sadrzavati 9 znamenki.")]
        [Required(ErrorMessage = "Obavezno**")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Neispravan format e-mail-a.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Telefon ne smije biti prazno.", MinimumLength = 1)]
        public string Telefon { get; set; }


        [Required(ErrorMessage = "Unesite grad.")]
        public int Grad_ID { get; set; }
        public List<SelectListItem> Gradovi { get; set; }



        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Lozinka mora sadržavati mininalno 4 karaktera.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string korisnicko { get; set; }



        [Required(ErrorMessage = "Obavezno**")]
        [StringLength(100, ErrorMessage = "Lozinka mora sadržavati mininalno 4 karaktera.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string korisnikovalozinka { get; set; }

    }
}
