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
    public class CitaocKorisnikController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public CitaocKorisnikController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }
        public IActionResult Index(string option, string search)
        {

            List<CitaocKorisnikPrikazVM> model = _context.CitaocKorisnik.Select(
                x => new CitaocKorisnikPrikazVM
                {
                    CitaocKorisnik_ID = x.CitaocKorisnik_ID,
                    ImeIPrezime = x.Korisnik.Ime + " " + x.Korisnik.Prezime,
                    KorisnickoIme = x.KorisnickiNalog.KorisnickoIme,
                  

                }).ToList();

            
            if (option == "ImePrezime")
            {
                return View(model.Where(x => search == null || (x.ImeIPrezime ).ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "KorisnickoIme")
            {
                return View(model.Where(x => search == null || (x.KorisnickoIme).ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Sve")
            {
                return View(model);
            }
            return View(model);
        }
        

        public IActionResult Obrisi(int id)
        {
            var citaocKorisnik = _context.CitaocKorisnik.Find(id);

            var nalog = _context.KorisnickiNalozi.Where(x => x.KorisnickiNalog_ID == citaocKorisnik.KorisnickiNalog_ID);
            var korisnik = _context.Korisnik.Where(x => x.ID == citaocKorisnik.Korisnik_ID);
            
            
            if (citaocKorisnik == null)
            {


            }
            else
            {
                foreach (var item in _context.Zaposlenici)
                {
                    if(item.KorisnickiNalog_ID==citaocKorisnik.KorisnickiNalog_ID)
                        _context.Zaposlenici.Remove(item);
                }
                
                _context.KorisnickiNalozi.Remove(nalog.Single());
                _context.Korisnik.Remove(korisnik.Single());
                
                _context.Remove(citaocKorisnik);
                
                _context.SaveChanges();


            }
            
            return RedirectToAction("Index");
        }
    }
}

