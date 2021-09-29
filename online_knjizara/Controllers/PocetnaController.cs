using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using online_knjizara.EF;
using online_knjizara.EntityModels;
using online_knjizara.ViewModels;

namespace online_knjizara.Controllers
{
    public class PocetnaController : Controller
    {
        private readonly OnlineKnjizaraDbContext _db;

        public PocetnaController(OnlineKnjizaraDbContext db)
        {
            _db = db;
        }

        public IActionResult Knjige()
        {
            List<PocetnaKnjigeVM.Knjiga> knjige = _db.Knjiga
                .Select(k => new PocetnaKnjigeVM.Knjiga
                {
                    ID = k.ID,
                    Naziv = k.Naziv,
                    Autor = k.Autor.Ime + " " + k.Autor.Prezime,
                    Slika = k.Slika
                }).ToList();

            PocetnaKnjigeVM item = new PocetnaKnjigeVM();
            item.zanrovi = _db.Zanr.Select(z => new PocetnaKnjigeVM.Zanr
            {
                ID = z.ID,
                Naziv = z.Naziv
            }).ToList();

            item.knjige = knjige;

            return View(item);
        }

    }
}
