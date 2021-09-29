using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using online_knjizara.EF;
using online_knjizara.EntityModels;
using online_knjizara.ViewModels;

namespace online_knjizara.Controllers
{
    public class AutorController : Controller
    {
        private OnlineKnjizaraDbContext _context;
        public AutorController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }

        public IActionResult Index(string option,string search)
        {
            List<AutorPrikazVM> model = _context.Autor.Select(
                x => new AutorPrikazVM
                {
                    ID=x.ID,
                    Ime=x.Ime,
                    Prezime=x.Prezime,
                    Adresa=x.Adresa,
                    Email=x.Email,
                    Grad=x.Grad!=null?x.Grad.Naziv+" ["+x.Grad.Drzava.Naziv+"]":" - "
                }).ToList();
            if (option == "ImePrezime")
            {
                return View(model.Where(x => search == null || (x.Ime + " " + x.Prezime).ToLower().Contains(search.ToLower()) || (x.Prezime + " " + x.Ime).ToLower().Contains(search.ToLower())).ToList());
            }
            else if (option == "Adresa")
            {
                return View(model.Where(x => search == null || (x.Adresa.ToLower().Contains(search.ToLower()))).ToList());
            }
            else if (option == "Sve")
            {
                return View(model);
            }

            return View(model);
        }
        public IActionResult Dodaj()
        {
            AutorUrediVM model = new AutorUrediVM
            {
                Grad = _context.Grad.OrderBy(x=>x.Naziv).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv + " [" + a.Drzava.Naziv + "]"
                }).ToList()
            };
        
            return View(model);
        }
        public IActionResult Snimi(AutorUrediVM vm)
        {
            Validiraj(vm);
            if (!ModelState.IsValid)
            {
                vm.Grad = _context.Grad.OrderBy(x => x.Naziv).Select(a => new SelectListItem 
                {
                    Value = a.ID.ToString(),
                    Text = a.Naziv + " [" + a.Drzava.Naziv + "]"
                }).ToList();
                return View("Dodaj",vm);

            }
            Autor autor;
            if (vm.ID == 0)
            {
                autor = new Autor();
                _context.Add(autor);

            }
            else
            {
                autor = _context.Autor.Find(vm.ID);
            }

            
            autor.ID = vm.ID;
            autor.Ime = vm.Ime;
            autor.Prezime = vm.Prezime;
            autor.Adresa = vm.Adresa;
            autor.Email = vm.Email;
            autor.Grad_ID = vm.Grad_ID;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private void Validiraj(AutorUrediVM vm)
        {
            if (vm.Grad_ID == 0)
            {
                ModelState.AddModelError("Grad", "Ne postoji dodan ni jedan grad u bazi!");
            }
            foreach (var item in _context.Autor)
            {
                if (item.Ime==vm.Ime&&item.Prezime==vm.Prezime&&item.Adresa==vm.Adresa&&item.Email==vm.Email&&item.Grad_ID==vm.Grad_ID)
                {
                    ModelState.AddModelError("Grad", "Autor već postoji!");

                }
            }
        }

        public IActionResult Uredi(int id)
        {

            Autor autor = _context.Autor.Find(id);
            if (autor == null)
            {
                return RedirectToAction(nameof(Index));
            }

            AutorUrediVM model = new AutorUrediVM 
            { 
                ID = autor.ID,
                Ime = autor.Ime,
                Prezime = autor.Prezime,
                Adresa = autor.Adresa,
                Email = autor.Email,
                Grad = _context.Grad.OrderBy(x => x.Naziv).Select(o => new SelectListItem(o.Naziv + " [" + o.Drzava.Naziv + "]", o.ID.ToString())).ToList(),
                Grad_ID = autor.Grad_ID
            };
            
           

            return View("Dodaj",model);
        }

        public IActionResult Obrisi(int id)
        {
            var autor = _context.Autor.Find(id);
            _context.Autor.Remove(autor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
