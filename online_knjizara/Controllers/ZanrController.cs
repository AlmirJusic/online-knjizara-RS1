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
    public class ZanrController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public ZanrController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }

        public IActionResult Index(string option, string search)
        {
            List<ZanrPrikazVM> model = _context.Zanr.Select(
                x => new ZanrPrikazVM
                {
                    ID = x.ID,
                    Naziv = x.Naziv,
                    Opis=x.Opis
                    
                }).ToList();
            if (option == "Naziv")
            {
                return View(model.Where(x => search == null || x.Naziv.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Sve")
            {
                return View(model);
            }
            return View(model);
        }
        public IActionResult Dodaj()
        {
            ZanrUrediVM model = new ZanrUrediVM();

            return View(model);
        }
        public IActionResult Snimi(ZanrUrediVM vm)
        {
            Validiraj(vm);
            if (!ModelState.IsValid)
            {
                return View("Dodaj", vm);
            }
            Zanr zanr;
            if (vm.ID == 0)
            {
                zanr = new Zanr();
                _context.Add(zanr);
            }
            else
            {
                zanr = _context.Zanr.Find(vm.ID);
            }


            zanr.ID = vm.ID;
            zanr.Naziv = vm.Naziv;
            zanr.Opis = vm.Opis;


            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private void Validiraj(ZanrUrediVM vm)
        {
            foreach (var item in _context.Zanr)
            {
                if(item.Naziv==vm.Naziv)
                {
                    ModelState.AddModelError("Naziv", "Naziv žanra vec postoji!");
                }
            }
        }

        public IActionResult Uredi(int id)
        {

            Zanr zanr = _context.Zanr.Find(id);
            if (zanr == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ZanrUrediVM model = new ZanrUrediVM
            {
                ID = zanr.ID,
                Naziv = zanr.Naziv,
                Opis= zanr.Opis
            };

            return View("Dodaj", model);
        }

        public IActionResult Obrisi(int id)
        {
            var zanr = _context.Zanr.Find(id);
            _context.Zanr.Remove(zanr);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
