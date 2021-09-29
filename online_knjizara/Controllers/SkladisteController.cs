using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using online_knjizara.EF;
using online_knjizara.EntityModels;
using online_knjizara.Helpers;
using online_knjizara.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.Controllers
{
    [Autorizacija(zaposlenik: true, citaockorisnik: false)]
    public class SkladisteController : Controller
    {
        
        private OnlineKnjizaraDbContext _context;
        public SkladisteController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }
        public IActionResult Index(string option, string search)
        {
            List<SkladistePrikazVM> model = _context.Skladiste.Select(
                x => new SkladistePrikazVM
                {
                    ID = x.ID,
                    Naziv = x.Naziv,
                    Adresa=x.Adresa,
                    Kolicina=x.Kolicina,
                    Grad = x.Grad != null ? x.Grad.Naziv + " [" + x.Grad.Drzava.Naziv + "]" : " - "

                }).ToList();
            if (option == "Naziv")
            {
                return View(model.Where(x => search == null || x.Naziv.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Adresa")
            {
                return View(model.Where(x => search == null || x.Adresa.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Grad")
            {
                return View(model.Where(x => search == null || x.Grad.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Sve")
            {
                return View(model);
            }
            return View(model);
        }
        public IActionResult Dodaj()
        {
            SkladisteUrediVM model = new SkladisteUrediVM
            {
                Grad = _context.Grad.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv + " [" + a.Drzava.Naziv + "]"
                }).ToList()
            };

            return View(model);
        }
        public IActionResult Snimi(SkladisteUrediVM vm)
        {
            Validiraj(vm);
            if (!ModelState.IsValid)
            {
                vm.Grad = _context.Grad.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv + " [" + a.Drzava.Naziv + "]"
                }).ToList();
                return View("Dodaj", vm);

            }
            Skladiste skladiste;
            if (vm.ID == 0)
            {
                skladiste = new Skladiste();
                _context.Add(skladiste);
            }
            else
            {
                skladiste = _context.Skladiste.Find(vm.ID);
            }


            skladiste.ID = vm.ID;
            skladiste.Naziv = vm.Naziv;
            skladiste.Adresa = vm.Adresa;
            skladiste.Kolicina = vm.Kolicina;
            skladiste.Grad_ID = vm.Grad_ID;


            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private void Validiraj(SkladisteUrediVM vm)
        {
            if (vm.Grad_ID == 0)
            {
                ModelState.AddModelError("Grad", "Ne postoji dodan ni jedan grad u bazi!");
            }
            foreach (var item in _context.Skladiste)
            {
                if (item.Naziv == vm.Naziv&& item.Grad_ID == vm.Grad_ID&&item.Adresa==vm.Adresa&&item.Kolicina==vm.Kolicina)
                {
                    ModelState.AddModelError("Grad", "Skladiste već postoji!");

                }
            }
        }

        

        public IActionResult Uredi(int id)
        {

            Skladiste skladiste = _context.Skladiste.Find(id);
            if (skladiste == null)
            {
                return RedirectToAction(nameof(Index));
            }

            SkladisteUrediVM model = new SkladisteUrediVM
            {
                ID = skladiste.ID,
                Naziv = skladiste.Naziv,
                Adresa= skladiste.Adresa,
                Kolicina= skladiste.Kolicina,
                Grad = _context.Grad.OrderBy(x => x.Naziv).Select(o => new SelectListItem(o.Naziv + " [" + o.Drzava.Naziv + "]", o.ID.ToString())).ToList(),
                Grad_ID = skladiste.Grad_ID
        
            };

            return View("Dodaj", model);
        }

        public IActionResult Obrisi(int id)
        {
            var skladiste = _context.Skladiste.Find(id);
            _context.Skladiste.Remove(skladiste);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

