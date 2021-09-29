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
    public class IzdavacController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public IzdavacController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }
        [Autorizacija(zaposlenik: true, citaockorisnik: false)]
        public IActionResult Index(string option, string search)
        {
            List<IzdavacPrikazVM> model = _context.Izdavac.Select(
                x => new IzdavacPrikazVM
                {
                    ID = x.ID,
                    Naziv = x.Naziv,
                    Adresa=x.Adresa,
                    Email=x.Email,
                    Telefon=x.Telefon,
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
        [Autorizacija(zaposlenik: true, citaockorisnik: false)]
        public IActionResult Dodaj()
        {
            IzdavacUrediVM model = new IzdavacUrediVM
            {
                Grad = _context.Grad.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv + " [" + a.Drzava.Naziv + "]"
                }).ToList()
            };

            return View(model);
        }
        [Autorizacija(zaposlenik: true, citaockorisnik: false)]
        public IActionResult Snimi(IzdavacUrediVM vm)
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
            Izdavac izdavac;
            if (vm.ID == 0)
            {
                izdavac = new Izdavac();
                _context.Add(izdavac);
            }
            else
            {
                izdavac = _context.Izdavac.Find(vm.ID);
            }


            izdavac.ID = vm.ID;
            izdavac.Naziv = vm.Naziv;
            izdavac.Adresa = vm.Adresa;
            izdavac.Email = vm.Email;
            izdavac.Telefon = vm.Telefon;
            izdavac.Grad_ID = vm.Grad_ID;


            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private void Validiraj(IzdavacUrediVM vm)
        {
            if (vm.Grad_ID == 0)
            {
                ModelState.AddModelError("Grad", "Ne postoji dodan ni jedan grad u bazi!");
            }
            foreach (var item in _context.Izdavac)
            {
                if (item.Naziv == vm.Naziv&& item.Grad_ID == vm.Grad_ID&&item.Adresa==vm.Adresa&&item.Email==vm.Email&&item.Telefon==vm.Telefon)
                {
                    ModelState.AddModelError("Grad", "Izdavac već postoji!");

                }
            }
        }


        [Autorizacija(zaposlenik: true, citaockorisnik: false)]
        public IActionResult Uredi(int id)
        {

            Izdavac izdavac = _context.Izdavac.Find(id);
            if (izdavac == null)
            {
                return RedirectToAction(nameof(Index));
            }

            IzdavacUrediVM model = new IzdavacUrediVM
            {
                ID = izdavac.ID,
                Naziv = izdavac.Naziv,
                Adresa= izdavac.Adresa,
                Telefon=izdavac.Telefon,
                Email=izdavac.Email,
                Grad = _context.Grad.OrderBy(x => x.Naziv).Select(o => new SelectListItem(o.Naziv + " [" + o.Drzava.Naziv + "]", o.ID.ToString())).ToList(),
                Grad_ID = izdavac.Grad_ID
        
            };

            return View("Dodaj", model);
        }
        [Autorizacija(zaposlenik: true, citaockorisnik: false)]
        public IActionResult Obrisi(int id)
        {
            var izdavac = _context.Izdavac.Find(id);
            _context.Izdavac.Remove(izdavac);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

