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
    public class AutentifikacijaController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public AutentifikacijaController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }


        public IActionResult Index()
        {
            return View(new LoginVM()
            {
                ZapamtiLozinku = true,
            });
        }

        public IActionResult Login(LoginVM input)
        {

            KorisnickiNalozi korisnik = _context.KorisnickiNalozi
                .SingleOrDefault(x => x.KorisnickoIme == input.KorisnickoIme && x.Lozinka == input.Lozinka);

            if (korisnik == null)
            {

                foreach (var item in _context.KorisnickiNalozi)
                {
                    if (input.KorisnickoIme != item.KorisnickoIme || input.Lozinka != item.Lozinka)
                    {
                        ModelState.AddModelError("Lozinka", "Pogresno korisnicko ime ili lozinka");
                    }
                }
                return View("Index", input);
            }
            
            
            

            HttpContext.SetLogiraniKorisnik(korisnik);
            CitaocKorisnik pk = _context.CitaocKorisnik.Where(s => s.KorisnickiNalog_ID == korisnik.KorisnickiNalog_ID).FirstOrDefault();
            Zaposlenici z = _context.Zaposlenici.Where(s => s.KorisnickiNalog_ID == korisnik.KorisnickiNalog_ID).FirstOrDefault();
            
            if (z != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "KlijentHome");
            }
            
        }



        public IActionResult Registracija()
        {
            RegistracijaSnimiVM model = new RegistracijaSnimiVM();
            model.korisnicko = "";
            model.korisnikovalozinka = "";
            model.gradovi = _context.Grad.Select(z => new SelectListItem
            {
                Value = z.ID.ToString(),
                Text = z.Naziv
            }).ToList();

            return View("Registracija", model);
        }



        [ValidateAntiForgeryToken]
        public IActionResult SnimiRegistraciju(RegistracijaSnimiVM input)
        {
            if (!ModelState.IsValid)
            {
                
                return RedirectToAction("Registracija");
            }
            if (input != null)
            {
                var n = _context.KorisnickiNalozi
                    .Where((x) => x.KorisnickoIme == input.korisnicko)
                    .FirstOrDefault();


                if (n != null)
                {
                    TempData["error_poruka"] = "Vec postoji korisnik sa tim korisničkim imenom";
                    
                    return RedirectToAction("Registracija");
                }
                
                Korisnik k = new Korisnik
                {
                    Ime = input.Ime,
                    Prezime = input.Prezime,
                    DatumRodjenja = input.DatumRodjenja,
                    Telefon = input.Telefon,
                    Email=input.Email,
                    Adresa=input.Adresa,
                    Grad_ID = input.GradID,
                    
                    
                };
                _context.Korisnik.Add(k);
                _context.SaveChanges();

                KorisnickiNalozi kn = new KorisnickiNalozi
                {
                    KorisnickoIme = input.korisnicko,
                    Lozinka = input.korisnikovalozinka
                };
                _context.KorisnickiNalozi.Add(kn);
                _context.SaveChanges();

                CitaocKorisnik pk = new CitaocKorisnik
                {
                    Korisnik_ID = k.ID,
                    KorisnickiNalog_ID = kn.KorisnickiNalog_ID
                };
                _context.CitaocKorisnik.Add(pk);
                _context.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        
    }



}
