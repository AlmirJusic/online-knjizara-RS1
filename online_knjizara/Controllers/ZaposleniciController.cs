using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using online_knjizara.EF;
using online_knjizara.EntityModels;
using online_knjizara.Helpers;
using online_knjizara.ViewModels;

namespace online_knjizara.Controllers
{
    [Autorizacija(zaposlenik: true, citaockorisnik: false)]
    public class ZaposleniciController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public ZaposleniciController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }
        public IActionResult Index(string option, string search)
        {
            List<ZaposlenikPrikazVM> model = _context.Zaposlenici.Select(
                x => new ZaposlenikPrikazVM
                {
                    ID = x.Zaposlenik_ID,
                    ImeIPrezime=x.Korisnik.Ime+" "+x.Korisnik.Prezime,
                    StrucnaSprema=x.StrucnaSprema,
                    KorisnickoIme =x.KorisnickiNalog.KorisnickoIme
                    
                }).ToList();
            if (option == "ImePrezime")
            {
                return View(model.Where(x => search == null || (x.ImeIPrezime ).ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "StrucnaSprema")
            {
                return View(model.Where(x => search == null || x.StrucnaSprema.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "KorisnickoIme")
            {
                return View(model.Where(x => search == null || x.KorisnickoIme.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Sve")
            {
                return View(model);
            }
            return View(model);
        }
        public IActionResult Dodaj()
        {
            ZaposlenikUrediVM model = new ZaposlenikUrediVM
            {
                korisnici = _context.Korisnik.Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Ime+" "+a.Prezime
                }).ToList(),
                
        };

            return View(model);
        }
        public IActionResult Snimi(ZaposlenikUrediVM vm)
        {
            Validiraj(vm);
            if (!ModelState.IsValid)
            {
                vm.korisnici = _context.Korisnik.Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Ime + " " + a.Prezime
                }).ToList();
                
                return View("Dodaj", vm);

            }
            Zaposlenici zaposlenici;
            if (vm.ID == 0)
            {
                zaposlenici = new Zaposlenici();
                _context.Add(zaposlenici);
            }
            else
            {
                zaposlenici = _context.Zaposlenici.Find(vm.ID);
            }


            zaposlenici.Zaposlenik_ID = vm.ID;
            zaposlenici.StrucnaSprema = vm.StrucnaSprema;
            zaposlenici.Korisnik_ID = vm.Korisnik_ID;

            zaposlenici.KorisnickiNalog_ID = _context.CitaocKorisnik.Where(y=>y.Korisnik_ID==vm.Korisnik_ID).Select(y=>y.KorisnickiNalog_ID).FirstOrDefault();
            
            
            
         
            


            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private void Validiraj(ZaposlenikUrediVM vm)
        {
            if (vm.Korisnik_ID == 0)
            {
                ModelState.AddModelError("korisnici", "Ne postoji dodan ni jedan korisnik u bazi!");
            }
            foreach (var item in _context.Zaposlenici)
            {
                if (item.Korisnik_ID==vm.Korisnik_ID&&item.StrucnaSprema==vm.StrucnaSprema)
                {
                    ModelState.AddModelError("StrucnaSprema", "Zaposlenik već postoji!");

                }
            }
        }



        public IActionResult Uredi(int id)
        {

            Zaposlenici zaposlenici = _context.Zaposlenici.Find(id);
            if (zaposlenici == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ZaposlenikUrediVM model = new ZaposlenikUrediVM
            {
                ID = zaposlenici.Zaposlenik_ID,
                Korisnik_ID = zaposlenici.Korisnik_ID,
                StrucnaSprema = zaposlenici.StrucnaSprema,
                korisnici = _context.Zaposlenici.Select(o => new SelectListItem(o.Korisnik.Ime + " " + o.Korisnik.Prezime, o.Zaposlenik_ID.ToString())).ToList(),
                

            };

            return View("Dodaj", model);
        }

        public IActionResult Obrisi(int id)
        {
            var zaposlenici = _context.Zaposlenici.Find(id);
            _context.Zaposlenici.Remove(zaposlenici);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

