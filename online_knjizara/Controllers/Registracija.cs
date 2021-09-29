using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using online_knjizara.EF;
using online_knjizara.EntityModels;
using online_knjizara.ViewModels;

namespace online_knjizara.Controllers
{
    public class RegistracijaController : Controller
    {
        private readonly OnlineKnjizaraDbContext _db;
        public RegistracijaController(OnlineKnjizaraDbContext db)
        {
            _db = db;
        }

        public IActionResult Poruka()
        {

            return View();
        }
        public IActionResult Snimi(RegistracijaVM x)
        {
            

            Korisnik user = new Korisnik
            {
                Ime = x.Ime,
                Prezime = x.Prezime,
                Adresa = x.Adresa,
                Email = x.Email,
                Telefon = x.Telefon,
                Grad_ID = x.Grad_ID,
                DatumRodjenja=x.DatumRodjenja
                
            };
            _db.Korisnik.Add(user);
            _db.SaveChanges();

            TempData["user"] = x.Ime + " " + x.Prezime;

            return Redirect("/Registracija/Poruka");
        }

        public IActionResult Prikaz()
        {
            RegistracijaVM r = new RegistracijaVM();

            List<SelectListItem> gradovi = _db.Grad
                .OrderBy(g => g.Naziv)
                .Select(g => new SelectListItem
                {
                    Text = g.Naziv,
                    Value = g.ID.ToString()
                }).ToList();

            r.Gradovi = gradovi;

            return View(r);
        }
    }
}
