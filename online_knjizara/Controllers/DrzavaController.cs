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
    public class DrzavaController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public DrzavaController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }

        public IActionResult Index(string option, string search)
        {
            List<DrzavaPrikazVM> model = _context.Drzava.Select(
                x => new DrzavaPrikazVM
                {
                    ID = x.ID,
                    Naziv=x.Naziv
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
            DrzavaUrediVM model = new DrzavaUrediVM();

            return View(model);
        }
        public IActionResult Snimi(DrzavaUrediVM vm)
        {
            Validiraj(vm);

            if (!ModelState.IsValid)
            {
                
                return View("Dodaj", vm);
            }
            
            Drzava drzava;
            if (vm.ID == 0)
            {
                
                drzava = new Drzava();
                _context.Add(drzava);
            }
            else
            {
                drzava = _context.Drzava.Find(vm.ID);
            }


            drzava.ID = vm.ID;
            drzava.Naziv = vm.Naziv;
            

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private void Validiraj(DrzavaUrediVM vm)
        {
            foreach (var item in _context.Drzava)
            {
                if (item.Naziv == vm.Naziv)
                {
                    ModelState.AddModelError("Naziv", "Naziv države već postoji!");

                }
            }
        }

        public IActionResult Uredi(int id)
        {

            Drzava drzava = _context.Drzava.Find(id);
            if (drzava == null)
            {
                return RedirectToAction(nameof(Index));
            }

            DrzavaUrediVM model = new DrzavaUrediVM
            {
                ID = drzava.ID,
                Naziv = drzava.Naziv
            };

            return View("Dodaj", model);
        }

        public IActionResult Obrisi(int id)
        {
            var drzava = _context.Drzava.Find(id);
            _context.Drzava.Remove(drzava);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
