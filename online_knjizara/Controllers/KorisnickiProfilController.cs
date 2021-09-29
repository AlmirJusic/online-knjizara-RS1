using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using online_knjizara.EF;
using online_knjizara.ViewModels;
using online_knjizara.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using online_knjizara.EntityModels;

namespace online_knjizara.Controllers
{
    public class KorisnickiProfilController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public KorisnickiProfilController(OnlineKnjizaraDbContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            KorisnickiProfilPrikazVM model = new KorisnickiProfilPrikazVM();
            var logiranikorisnik = HttpContext.GetLogiraniKorisnik();
            var zaposlenik = _context.Zaposlenici.FirstOrDefault(s => s.KorisnickiNalog_ID == logiranikorisnik.KorisnickiNalog_ID);
            var citaoc = _context.CitaocKorisnik.FirstOrDefault(s => s.KorisnickiNalog_ID == logiranikorisnik.KorisnickiNalog_ID);

            

            if(zaposlenik!=null)
            {
                var korisnik = _context.Korisnik.FirstOrDefault(x => x.ID == zaposlenik.Korisnik_ID);
                model.Ime = korisnik.Ime;
                model.Prezime = korisnik.Prezime;
                model.Telefon = korisnik.Telefon;
                model.DatumRodjenja = korisnik.DatumRodjenja;
                model.KorisnickoIme = zaposlenik.KorisnickiNalog.KorisnickoIme;
                model.Password = zaposlenik.KorisnickiNalog.Lozinka;
                model.Email = korisnik.Email;
                model.Grad_ID = korisnik.Grad_ID;
                model.Grad = _context.Grad.FirstOrDefault(x => x.ID == korisnik.Grad_ID).Naziv;
                model.Adresa = korisnik.Adresa;
                model.JelZaposlenik = true;
                
            }
            else if(citaoc!=null)
            {
                var korisnik = _context.Korisnik.FirstOrDefault(x => x.ID == citaoc.Korisnik_ID);
                model.Ime = korisnik.Ime;
                model.Prezime = korisnik.Prezime;
                model.Telefon = korisnik.Telefon;
                model.DatumRodjenja = korisnik.DatumRodjenja;
                model.KorisnickoIme = citaoc.KorisnickiNalog.KorisnickoIme;
                model.Password = citaoc.Korisnik.Prezime;
                model.Email = korisnik.Email;
                model.Grad_ID = korisnik.Grad_ID;
                model.Grad = _context.Grad.FirstOrDefault(x => x.ID == korisnik.Grad_ID).Naziv;
                model.Adresa = korisnik.Adresa;
                model.JelZaposlenik = false;
            }
           
            return View("Index",model);
        }

        public IActionResult Uredi(int id)
        {
            KorisnickiProfilUrediVM model = new KorisnickiProfilUrediVM();
            var logiranikorisnik = HttpContext.GetLogiraniKorisnik();
            var zaposlenik = _context.Zaposlenici.FirstOrDefault(s => s.KorisnickiNalog_ID == logiranikorisnik.KorisnickiNalog_ID);
            var citaoc = _context.CitaocKorisnik.FirstOrDefault(s => s.KorisnickiNalog_ID == logiranikorisnik.KorisnickiNalog_ID);


            if (zaposlenik != null)
            {
                var korisnik = _context.Korisnik.FirstOrDefault(x => x.ID == zaposlenik.Korisnik_ID);
                model.Ime = korisnik.Ime;
                model.Prezime = korisnik.Prezime;
                model.Email = korisnik.Email;
                model.Telefon = korisnik.Telefon;
                model.DatumRodjenja = korisnik.DatumRodjenja;
                model.KorisnickoIme = zaposlenik.KorisnickiNalog.KorisnickoIme;
                model.Password = zaposlenik.KorisnickiNalog.Lozinka;
                model.Adresa = korisnik.Adresa;
                model.Grad = _context.Grad.OrderBy(x => x.Naziv).Select(o => new SelectListItem(o.Naziv + " [" + o.Drzava.Naziv + "]", o.ID.ToString())).ToList();
                model.Grad_ID = korisnik.Grad_ID;
                model.JelZaposlenik = true;
            }
            else
            {
                var korisnik = _context.Korisnik.FirstOrDefault(x => x.ID == citaoc.Korisnik_ID);
                model.Ime = korisnik.Ime;
                model.Prezime = korisnik.Prezime;
                model.Email = korisnik.Email;
                model.Telefon = korisnik.Telefon;
                model.DatumRodjenja = korisnik.DatumRodjenja;
                model.KorisnickoIme = citaoc.KorisnickiNalog.KorisnickoIme;
                model.Password = citaoc.KorisnickiNalog.Lozinka;
                model.Grad = _context.Grad.OrderBy(x => x.Naziv).Select(o => new SelectListItem(o.Naziv + " [" + o.Drzava.Naziv + "]", o.ID.ToString())).ToList();
                model.Adresa = korisnik.Adresa;
                model.Grad_ID = korisnik.Grad_ID;
                model.JelZaposlenik = false;
            }
            return View("Uredi", model);
        }

        public IActionResult Snimi(KorisnickiProfilUrediVM model)
        {
            var logiranikorisnik = HttpContext.GetLogiraniKorisnik();
            var zaposlenik = _context.Zaposlenici.FirstOrDefault(s => s.KorisnickiNalog_ID == logiranikorisnik.KorisnickiNalog_ID);
            var citaoc = _context.CitaocKorisnik.FirstOrDefault(s => s.KorisnickiNalog_ID == logiranikorisnik.KorisnickiNalog_ID);

            if (zaposlenik != null)
            {
                var korisnik = _context.Korisnik.FirstOrDefault(x => x.ID == zaposlenik.Korisnik_ID);

                korisnik.Telefon = model.Telefon;
                zaposlenik.KorisnickiNalog.KorisnickoIme = model.KorisnickoIme;
                zaposlenik.KorisnickiNalog.Lozinka = model.Password;
                korisnik.Grad_ID = model.Grad_ID;
                korisnik.Adresa = model.Adresa;

            }
            else
            {
                var korisnik = _context.Korisnik.FirstOrDefault(x => x.ID == citaoc.Korisnik_ID);

                korisnik.Telefon = model.Telefon;
                citaoc.KorisnickiNalog.KorisnickoIme = model.KorisnickoIme;
                citaoc.KorisnickiNalog.Lozinka = model.Password;
                korisnik.Grad_ID = model.Grad_ID;
                korisnik.Adresa = model.Adresa;
            }

            _context.SaveChanges();
            return Redirect("Index");
        }
    }
}