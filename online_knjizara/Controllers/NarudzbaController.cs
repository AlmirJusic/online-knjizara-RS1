using Microsoft.AspNetCore.Mvc;
using online_knjizara.EF;
using online_knjizara.Helpers;
using online_knjizara.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.Controllers
{
    
    public class NarudzbaController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public NarudzbaController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }

        public IActionResult Index(string option, string search)
        {

            List<NarudzbaPrikazVM> model = _context.Narudzba
            .Select(
            x => new NarudzbaPrikazVM
            {
                ID = x.ID,
                Knjiga=x.Knjiga.Naziv,
                Korisnik=x.Korisnik.Ime+" "+x.Korisnik.Prezime,
                DatumVrijeme=x.DatumVrijeme,
                Cijena=x.Cijena,
                Kolicina=x.Kolicina

            }).ToList(); 
            if (option == "Knjiga")
            {
                return View(model.Where(x => search == null || (x.Knjiga.ToLower().Contains(search.ToLower()))).ToList());
            }
            else if (option == "Korisnik")
            {
                return View(model.Where(x => search == null || (x.Korisnik).ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Sve")
            {
                return View(model);
            }

            return View(model);

        }
        public IActionResult Obrisi(int id)
        {
            var narudzba = _context.Narudzba.Find(id);
            _context.Narudzba.Remove(narudzba);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
