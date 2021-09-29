using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using online_knjizara.EF;
using online_knjizara.EntityModels;
using online_knjizara.Helpers;
using online_knjizara.ViewModels;

namespace online_knjizara.Controllers
{
    [Autorizacija(zaposlenik: true, citaockorisnik: false)]
    public class OdlomakController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public OdlomakController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }

        public IActionResult Index(string option, string search)
        {
            List<OdlomakPrikazVM> model = _context.Odlomak.Select(
                x => new OdlomakPrikazVM
                {
                    ID = x.ID,
                    NazivOdlomka=x.NazivOdlomka,
                    Sadrzaj=x.Sadrzaj
                    
                }).ToList();
            if (option == "Sadrzaj")
            {
                return View(model.Where(x => search == null || x.Sadrzaj.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "NazivOdlomka")
            {
                return View(model.Where(x => search == null || x.NazivOdlomka.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Sve")
            {
                return View(model);
            }
            return View(model);
        }
        public IActionResult Dodaj()
        {
            OdlomakUrediVM model = new OdlomakUrediVM();

            return View(model);
        }
        public IActionResult Snimi(OdlomakUrediVM vm)
        {
            Validiraj(vm);
            if (!ModelState.IsValid)
            {
                return View("Dodaj", vm);
            }
            Odlomak odlomak;
            if (vm.ID == 0)
            {
                odlomak = new Odlomak();
                _context.Add(odlomak);
            }
            else
            {
                odlomak = _context.Odlomak.Find(vm.ID);
            }


            odlomak.ID = vm.ID;
            odlomak.Sadrzaj = vm.Sadrzaj;
            odlomak.NazivOdlomka = vm.NazivOdlomka;
            


            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private void Validiraj(OdlomakUrediVM vm)
        {
            foreach (var item in _context.Odlomak)
            {
                if(item.Sadrzaj==vm.Sadrzaj&&item.NazivOdlomka==vm.NazivOdlomka)
                {
                    ModelState.AddModelError("Sadrzaj", "Sadrzaj odlomka vec postoji!");
                }
            }
        }

        public IActionResult Uredi(int id)
        {

            Odlomak odlomak = _context.Odlomak.Find(id);
            if (odlomak == null)
            {
                return RedirectToAction(nameof(Index));
            }

            OdlomakUrediVM model = new OdlomakUrediVM
            {
                ID = odlomak.ID,
                Sadrzaj = odlomak.Sadrzaj,
                NazivOdlomka=odlomak.NazivOdlomka
                
            };

            return View("Dodaj", model);
        }

        public IActionResult Obrisi(int id)
        {
            var odlomak = _context.Odlomak.Find(id);
            _context.Odlomak.Remove(odlomak);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
