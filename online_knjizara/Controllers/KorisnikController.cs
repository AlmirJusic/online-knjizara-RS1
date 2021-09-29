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
    public class KorisnikController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public KorisnikController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }
        
        public IActionResult Index(string option,string search)
        {

            List<KorisnikPrikazVM> model = _context.Korisnik
            .Select(
            x => new KorisnikPrikazVM
            {
                ID = x.ID,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Adresa = x.Adresa,
                Email = x.Email,
                Grad = x.Grad.Naziv + " [" + x.Grad.Drzava.Naziv + "]",
                Telefon = x.Telefon,
                DatumRodjenja = x.DatumRodjenja,
                KorisnickoIme = _context.CitaocKorisnik.Where(y=>y.Korisnik_ID==x.ID).Select(a=>a.KorisnickiNalog.KorisnickoIme).FirstOrDefault()
                    
                }).ToList();
            if (option == "ImePrezime")
            {
                return View(model.Where(x => search == null || (x.Ime + " " + x.Prezime).ToLower().Contains(search.ToLower()) || (x.Prezime + " " + x.Ime).ToLower().Contains(search.ToLower())).ToList());
            }
            else if(option=="Adresa")
            {
                return View(model.Where(x =>search == null ||(x.Adresa.ToLower().Contains(search.ToLower())) ).ToList());
            }
            else if (option == "Email")
            {
                return View(model.Where(x => search == null || (x.Email.ToLower().Contains(search.ToLower()))).ToList());
            }
            else if (option == "KorisnickoIme")
            {
                return View(model.Where(x => search == null || (x.KorisnickoIme.ToLower().Contains(search.ToLower()))).ToList());
            }
            else if(option=="Sve")
            {
                return View(model);
            }
            
            return View(model);
            
        }
        public IActionResult Dodaj()
        {
            KorisnikUrediVM model = new KorisnikUrediVM
            {
                Grad = _context.Grad.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv + " [" + a.Drzava.Naziv + "]"
                }).ToList()
            };

            return View(model);
        }
        
        public IActionResult Snimi(KorisnikUrediVM vm)
        {

            var citaoc = _context.CitaocKorisnik.Where(x=>x.Korisnik_ID==vm.ID);
            
            
            if (!ModelState.IsValid)
            {
                vm.Grad = _context.Grad.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv + " [" + a.Drzava.Naziv + "]"
                }).ToList();
                return View("Dodaj", vm);

            }
            Korisnik korisnik;
            if (vm.ID == 0)
            {
                korisnik = new Korisnik();
                _context.Add(korisnik);
                
            }
            else
            {
                korisnik = _context.Korisnik.Find(vm.ID);
            }
            


            korisnik.ID = vm.ID;
            korisnik.Ime = vm.Ime;
            korisnik.Prezime = vm.Prezime;
            korisnik.Adresa = vm.Adresa;
            korisnik.Email = vm.Email;
            korisnik.Telefon = vm.Telefon;
            korisnik.DatumRodjenja = vm.DatumRodjenja;
            korisnik.Grad_ID = vm.Grad_ID;
            



            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Uredi(int id)
        {
            
            Korisnik korisnik = _context.Korisnik.Find(id);

            
            if (korisnik == null)
            {
                return RedirectToAction(nameof(Index));
            }

            KorisnikUrediVM model = new KorisnikUrediVM
            {
                ID = korisnik.ID,
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Adresa = korisnik.Adresa,
                Email = korisnik.Email,
                Telefon= korisnik.Telefon,
                Grad = _context.Grad.OrderBy(x => x.Naziv).Select(o => new SelectListItem(o.Naziv + " [" + o.Drzava.Naziv + "]", o.ID.ToString())).ToList(),
                Grad_ID = korisnik.Grad_ID,
                DatumRodjenja=korisnik.DatumRodjenja,
                KorisnickoIme = _context.CitaocKorisnik.Where(y => y.Korisnik_ID == id).Select(a => a.KorisnickiNalog.KorisnickoIme).FirstOrDefault()

            };

            return View("Dodaj", model);
        }
       

        public IActionResult Obrisi(int id)
        {
            var korisnik = _context.Korisnik.Find(id);
            var citaockorisnik = _context.CitaocKorisnik.Where(x => x.Korisnik_ID == korisnik.ID);

            foreach (var item in _context.Zaposlenici)
            {
                if (item.KorisnickiNalog_ID == citaockorisnik.Select(y => y.KorisnickiNalog_ID).FirstOrDefault())
                {
                    _context.Zaposlenici.Remove(item);
                }
            }

            foreach (var item in _context.KorisnickiNalozi)
            {
                if(item.KorisnickiNalog_ID==citaockorisnik.Select(y=>y.KorisnickiNalog_ID).FirstOrDefault())
                {
                    _context.KorisnickiNalozi.Remove(item);
                }
            }


            
            _context.CitaocKorisnik.Remove(citaockorisnik.Single());
            _context.Korisnik.Remove(korisnik);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
