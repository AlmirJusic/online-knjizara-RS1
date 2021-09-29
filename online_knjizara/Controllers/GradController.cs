using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using online_knjizara.EF;
using online_knjizara.EntityModels;
using online_knjizara.Helpers;
using online_knjizara.ViewModels;

namespace online_knjizara.Controllers
{
    [Autorizacija(zaposlenik: true, citaockorisnik: false)]
    public class GradController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public GradController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }

        public IActionResult Index(string option, string search)
        {
            List<GradPrikazVM> model = _context.Grad.Select(
                x => new GradPrikazVM
                {
                    ID = x.ID,
                    Naziv = x.Naziv,
                    Drzava=x.Drzava.Naziv
                }).ToList();
            if (option == "Naziv")
            {
                return View(model.Where(x => search == null || x.Naziv.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Drzava")
            {
                return View(model.Where(x => search == null || x.Drzava.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Sve")
            {
                return View(model);
            }
            return View(model);
        }
        public IActionResult Dodaj()
        {
            GradUrediVM model = new GradUrediVM
            {
                Drzava = _context.Drzava.OrderBy(x=>x.Naziv).Select(x => new SelectListItem
                {
                    Value=x.ID.ToString(),
                    Text=x.Naziv
                }).ToList()
            };

            return View(model);
        }
        public IActionResult Snimi(GradUrediVM vm)
        {
            Validiraj(vm);

            if (!ModelState.IsValid)
            {
                vm.Drzava = _context.Drzava.OrderBy(x => x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv
                }).ToList();
                return View("Dodaj", vm);
            }
            Grad grad;
            if (vm.ID == 0)
            {
                grad = new Grad();
                _context.Add(grad);
            }
            else
            {
                grad = _context.Grad.Find(vm.ID);
            }


            grad.ID = vm.ID;
            grad.Naziv = vm.Naziv;
            grad.Drzava_ID = vm.Drzava_ID;


            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private void Validiraj(GradUrediVM vm)
        {
            if(vm.Drzava_ID==0)
            {
                ModelState.AddModelError("Drzava", "Ne postoji dodana ni jedna država u bazi!");
            }
            foreach (var item in _context.Grad)
            {
                if (item.Naziv == vm.Naziv && item.Drzava_ID == vm.Drzava_ID)
                {
                    ModelState.AddModelError("Naziv", "Naziv grada u državi već postoji!");

                }
            }
        }

        public IActionResult Uredi(int id)
        {

            Grad grad = _context.Grad.Find(id);
            if (grad == null)
            {
                return RedirectToAction(nameof(Index));
            }

            GradUrediVM model = new GradUrediVM
            {
                ID = grad.ID,
                Naziv = grad.Naziv,
                Drzava = _context.Drzava.OrderBy(x => x.Naziv).Select(o => new SelectListItem(o.Naziv, o.ID.ToString())).ToList(),
                Drzava_ID=grad.Drzava_ID
            };

            return View("Dodaj", model);
        }

        public IActionResult Obrisi(int id)
        {
            var grad = _context.Grad.Find(id);
            _context.Grad.Remove(grad);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
