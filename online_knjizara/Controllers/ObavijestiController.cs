using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using online_knjizara.EF;
using online_knjizara.EntityModels;
using online_knjizara.Helpers;
using online_knjizara.ViewModels;
using cloudscribe.Pagination.Models;

namespace online_knjizara.Controllers
{
    public class ObavijestiController : Controller
    {
        private OnlineKnjizaraDbContext _context;


        public ObavijestiController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;

        }
        


        [Autorizacija(zaposlenik: true, citaockorisnik: false)]

        public IActionResult Index(int trenutnaStr = 1, int velicinaStr = 1)
        {
            List<ObavijestiPrikazVM> model = _context.Obavijesti.Select(
                k => new ObavijestiPrikazVM
                {
                    ID = k.ID,
                    Naziv = k.Naziv,
                    Sadrzaj = k.Sadrzaj,
                    Datum = k.Datum,
                    Slika = k.Slika
                }).ToList();

            var items = _context.Obavijesti.OrderBy(x => x.Naziv).Skip((trenutnaStr - 1) * velicinaStr).
          Take(velicinaStr).ToList();
            

            var result = new PagedResult<Obavijesti>
            {
                Data = items.ToList(),
                TotalItems=_context.Obavijesti.Count(),
                PageNumber=trenutnaStr,
                PageSize=velicinaStr
            };
            return View(result);

        }
        

        public IActionResult IndexUser(int trenutnaStr = 1, int velicinaStr = 1)
        {
            List<ObavijestiPrikazVM> model = _context.Obavijesti.Select(
                k => new ObavijestiPrikazVM
                {
                    ID = k.ID,
                    Naziv = k.Naziv,
                    Sadrzaj = k.Sadrzaj,
                    Datum = k.Datum,
                    Slika = k.Slika
                }).ToList();

            var items = _context.Obavijesti.OrderBy(x => x.Naziv).Skip((trenutnaStr - 1) * velicinaStr).
          Take(velicinaStr).ToList();


            var result = new PagedResult<Obavijesti>
            {
                Data = items.ToList(),
                TotalItems = _context.Obavijesti.Count(),
                PageNumber = trenutnaStr,
                PageSize = velicinaStr
            };
            return View(result);

        }
        [Autorizacija(zaposlenik: true, citaockorisnik: false)]

        public IActionResult Dodaj()
        {
            ObavijestiUrediVM model = new ObavijestiUrediVM();
            return View(model);

        }
        [Autorizacija(zaposlenik: true, citaockorisnik: false)]

        public IActionResult Snimi(ObavijestiUrediVM input)
        {
            if (!ModelState.IsValid)
            {
                return View("Dodaj", input);

            }
            Obavijesti o;
            if (input.ID == 0)
            {
                o = new Obavijesti();
                _context.Add(o);

            }
            else
            {
                o = _context.Obavijesti.Find(input.ID);

            }
            if (input.Slika != null)
            {
                var memoryStream = new MemoryStream();


                input.Slika.CopyTo(memoryStream);
                var j = memoryStream.ToArray();
                o.Slika = j;

            }


            o.Naziv = input.Naziv;
            o.Sadrzaj = input.Sadrzaj;
            o.Datum =DateTime.Now;

            _context.SaveChanges();
          

            
             return RedirectToAction("Index");


        }
        [Autorizacija(zaposlenik: true, citaockorisnik: false)]

        public IActionResult Uredi(int ObavijestID)
        {

            Obavijesti o = _context.Obavijesti.Find(ObavijestID);
            if (o == null)
            {
               
                return RedirectToAction(nameof(Index));
            }

            ObavijestiUrediVM model = new ObavijestiUrediVM();
            model.ID = o.ID;
            model.Naziv = o.Naziv;
            model.Sadrzaj = o.Sadrzaj;
            model.Datum = o.Datum;



            return View(model);
        }
        [Autorizacija(zaposlenik: true, citaockorisnik: false)]

        public IActionResult Obrisi(int ObavijestID)
        {
            Obavijesti o = _context.Obavijesti.Find(ObavijestID);
            if (o == null)
            {


            }
            else
            {
                _context.Remove(o);

                _context.SaveChanges();


            }
            return RedirectToAction("Index");
        }

    }
}