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
    public class StanjeController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public StanjeController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }

        public IActionResult Index(string option, string search)
        {
            List<StanjePrikazVM> model = _context.Stanje.Select(
                x => new StanjePrikazVM
                {
                    ID = x.ID,
                    TipStanja = x.TipStanja,
                    
                    
                }).ToList();
            if (option == "TipStanja")
            {
                return View(model.Where(x => search == null || x.TipStanja.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Sve")
            {
                return View(model);
            }
            return View(model);
        }
        public IActionResult Dodaj()
        {
            StanjeUrediVM model = new StanjeUrediVM();

            return View(model);
        }
        public IActionResult Snimi(StanjeUrediVM vm)
        {
            Validiraj(vm);
            if (!ModelState.IsValid)
            {
                return View("Dodaj", vm);
            }
            Stanje stanje;
            if (vm.ID == 0)
            {
                stanje = new Stanje();
                _context.Add(stanje);
            }
            else
            {
                stanje = _context.Stanje.Find(vm.ID);
            }


            stanje.ID = vm.ID;
            stanje.TipStanja = vm.TipStanja;
            


            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private void Validiraj(StanjeUrediVM vm)
        {
            foreach (var item in _context.Stanje)
            {
                if(item.TipStanja==vm.TipStanja)
                {
                    ModelState.AddModelError("TipStanja", "Tip stanja vec postoji!");
                }
            }
        }

        public IActionResult Uredi(int id)
        {

            Stanje stanje = _context.Stanje.Find(id);
            if (stanje == null)
            {
                return RedirectToAction(nameof(Index));
            }

            StanjeUrediVM model = new StanjeUrediVM
            {
                ID = stanje.ID,
                TipStanja = stanje.TipStanja,
                
            };

            return View("Dodaj", model);
        }

        public IActionResult Obrisi(int id)
        {
            var stanje = _context.Stanje.Find(id);
            _context.Stanje.Remove(stanje);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
