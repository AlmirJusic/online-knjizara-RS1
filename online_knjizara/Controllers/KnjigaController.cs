using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using online_knjizara.EF;
using online_knjizara.EntityModels;
using online_knjizara.ViewModels;
using online_knjizara.Helpers;
using Microsoft.AspNetCore.Http;


namespace online_knjizara.Controllers
{
    public class KnjigaController : Controller
    {
        private readonly OnlineKnjizaraDbContext _db;

        public KnjigaController(OnlineKnjizaraDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index(string option, string search)
        {

            List<KnjigaPrikazVM> model = _db.Knjiga
            .Select(
            x => new KnjigaPrikazVM
            {
                ID = x.ID,
                Autor=x.Autor.Ime+" "+x.Autor.Prezime,
                Cijena=x.Cijena,
                DatumIzdavanja=x.DatumIzdavanja,
                Izdavac=x.Izdavac.Naziv,
                Naziv=x.Naziv,
                NazivOdlomka=x.Odlomak.NazivOdlomka,
                SadrzajOdlomka=x.Odlomak.Sadrzaj,
                Skladiste=x.Skladiste.Naziv,
                Stanje=x.Stanje.TipStanja,
                Zanr=x.Zanr.Naziv,
                Slika= x.Slika


            }).ToList();
            
            if (option == "Naziv")
            {
                return View(model.Where(x => search == null || (x.Naziv.ToLower().Contains(search.ToLower()))).ToList());
            }
            else if (option == "Autor")
            {
                return View(model.Where(x => search == null || x.Autor.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Izdavac")
            {
                return View(model.Where(x => search == null || x.Izdavac.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Zanr")
            {
                return View(model.Where(x => search == null || x.Zanr.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Sve")
            {
                return View(model);
            }

            return View(model);

        }

        [Autorizacija(zaposlenik: true, citaockorisnik: false)]
        public IActionResult Dodaj()
        {
            KnjigaUrediVM model = new KnjigaUrediVM
            {
                Autor = _db.Autor.OrderBy(x => x.Ime + " " + x.Prezime).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Ime + " " + a.Prezime
                }).ToList(),
                Izdavac = _db.Izdavac.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv
                }).ToList(),
                Skladiste = _db.Skladiste.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv
                }).ToList(),
                Zanr = _db.Zanr.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv
                }).ToList(),
                Stanje = _db.Stanje.OrderBy(x => x.TipStanja).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.TipStanja
                }).ToList(),
                Odlomak = _db.Odlomak.OrderBy(x => x.Sadrzaj).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.NazivOdlomka
                }).ToList(),
                
                
            };

            return View(model);
        }
        [Autorizacija(zaposlenik: true, citaockorisnik: false)]
        public IActionResult Snimi(KnjigaUrediVM vm)
        {
            Validiraj(vm);
            if (!ModelState.IsValid)
            {
                vm.Autor = _db.Autor.OrderBy(x => x.Ime + " " + x.Prezime).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Ime + " " + a.Prezime
                }).ToList();
                vm.Izdavac = _db.Izdavac.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv
                }).ToList();
                vm.Skladiste = _db.Skladiste.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv
                }).ToList();
                vm.Zanr = _db.Zanr.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv
                }).ToList();
                vm.Stanje = _db.Stanje.OrderBy(x => x.TipStanja).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.TipStanja
                }).ToList();
                vm.Odlomak = _db.Odlomak.OrderBy(x => x.Sadrzaj).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.NazivOdlomka
                }).ToList();

                return View("Dodaj", vm);

            }
            Knjiga knjiga;
            if (vm.ID == 0)
            {
                knjiga = new Knjiga();
                _db.Add(knjiga);

            }
            else
            {
                knjiga = _db.Knjiga.Find(vm.ID);
            }

            if (vm.Slika != null)
            {
                var memoryStream = new MemoryStream();


                vm.Slika.CopyTo(memoryStream);
                var j = memoryStream.ToArray();
                knjiga.Slika = j;

            }


            knjiga.ID = vm.ID;
            knjiga.Autor_ID = vm.Autor_ID;
            knjiga.Izdavac_ID = vm.Izdavac_ID;
            knjiga.Skladiste_ID= vm.Skladiste_ID;
            knjiga.Zanr_ID = vm.Zanr_ID;
            knjiga.Stanje_ID = vm.Stanje_ID;
            knjiga.Odlomak_ID = vm.Odlomak_ID;
            knjiga.Naziv = vm.Naziv;
            knjiga.Cijena = vm.Cijena;
            knjiga.DatumIzdavanja = vm.DatumIzdavanja;
            
            
            


            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        private void Validiraj(KnjigaUrediVM vm)
        {
            if (vm.Autor_ID == 0)
            {
                ModelState.AddModelError("Autor", "Ne postoji dodan ni jedan autor u bazi!");
            }
            foreach (var item in _db.Knjiga)
            {
                if (item.Naziv == vm.Naziv && vm.ID == 0)
                {
                    ModelState.AddModelError("Naziv", "Već postoji knjiga sa ovim imenom!");
                }
                
            }

        }
        [Autorizacija(zaposlenik: true, citaockorisnik: false)]
        public IActionResult Uredi(int id)
        {

            Knjiga knjiga = _db.Knjiga.Find(id);
            if (knjiga == null)
            {
                return RedirectToAction(nameof(Index));
            }

            KnjigaUrediVM model = new KnjigaUrediVM
            {
                ID = knjiga.ID,
                Naziv=knjiga.Naziv,
                Autor= _db.Autor.OrderBy(x => x.Ime+" "+x.Prezime).Select(o => new SelectListItem(o.Ime + " " + o.Prezime, o.ID.ToString())).ToList(),
                Izdavac = _db.Izdavac.OrderBy(x => x.Naziv).Select(o => new SelectListItem(o.Naziv, o.ID.ToString())).ToList(),
                Skladiste = _db.Skladiste.OrderBy(x => x.Naziv).Select(o => new SelectListItem(o.Naziv, o.ID.ToString())).ToList(),
                Odlomak = _db.Odlomak.OrderBy(x => x.NazivOdlomka).Select(o => new SelectListItem(o.NazivOdlomka, o.ID.ToString())).ToList(),
                Stanje = _db.Stanje.OrderBy(x => x.TipStanja).Select(o => new SelectListItem(o.TipStanja, o.ID.ToString())).ToList(),
                Zanr = _db.Zanr.OrderBy(x => x.Naziv).Select(o => new SelectListItem(o.Naziv, o.ID.ToString())).ToList(),
                Cijena=knjiga.Cijena,
                DatumIzdavanja=knjiga.DatumIzdavanja

            };

            return View("Dodaj", model);
        }
        [Autorizacija(zaposlenik: true, citaockorisnik: false)]
        public IActionResult Obrisi(int id)
        {
            var knjiga = _db.Knjiga.Find(id);
            _db.Knjiga.Remove(knjiga);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //#######################################################
        public IActionResult Detalji(int KnjigaID)
        {
            KnjigaDetaljiVM detalji = _db.Knjiga
                .Where(k => k.ID == KnjigaID)
                .Select(k => new KnjigaDetaljiVM
                {
                    ID=k.ID,
                    Naziv=k.Naziv,
                    Autor=k.Autor.Ime+" "+k.Autor.Prezime,
                    Cijena=k.Cijena,
                    Zanr=k.Zanr.Naziv,
                    Stanje=k.Stanje.TipStanja,
                    Izdavac=k.Izdavac.Naziv,
                    Slika=k.Slika,
                    Sadrzaj=k.Odlomak.Sadrzaj
                }).Single();

            return View(detalji);
        }

        public IActionResult Komentari(int KnjigaID)
        {
            List<KnjigaKomentariVM.Komentari> komentari = _db.KomentarOcjena
                .Where(k => k.Knjiga_ID == KnjigaID)
                .Select(k => new KnjigaKomentariVM.Komentari
                {
                    User = k.Korisnik.Ime + " " + k.Korisnik.Prezime,
                    Komentar = k.Komentar,
                    Ocjena = k.Ocjena
                }).ToList();
            KnjigaKomentariVM komentar = new KnjigaKomentariVM();
            komentar.KnjigaID = KnjigaID;
            komentar.komentari = komentari;
            return View(komentar);
        }

        public IActionResult KomentarDodaj(int KnjigaID)
        {
            TempData["KnjigaID"] = KnjigaID;
            return View();
        }

        public IActionResult KomentarSnimi(KnjigaKomentarDodajVM kom)
        {
            int KnjigaID = (int)TempData["KnjigaID"];
            //Korisnik k = _db.Korisnik
            //    .Where(k => k.Username==kom.Username && k.Password == kom.Password)
            //    .Single();

            KomentarOcjena ko = new KomentarOcjena
            {
                Knjiga_ID = KnjigaID,
                //Korisnik_ID = k.ID,
                Komentar = kom.Komentar,
                Ocjena = kom.Ocjena
            };
            _db.KomentarOcjena.Add(ko);
            _db.SaveChanges();

            return Redirect("/Knjiga/Komentari?KnjigaID=" + KnjigaID);
        }
    }
}
