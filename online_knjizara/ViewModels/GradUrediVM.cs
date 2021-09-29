using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.ViewModels
{
    public class GradUrediVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Unesite naziv grada.")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Unesite državu.")]
        public int Drzava_ID { get; set; }
        public List<SelectListItem> Drzava { get; set; }

    }
}
