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
    public class TrenutnaSesijaController : Controller
    {
        private readonly OnlineKnjizaraDbContext _context;

        public TrenutnaSesijaController(OnlineKnjizaraDbContext _db)
        {
            _context = _db;
        }


        public IActionResult Index()
        {
            TrenutnaSesijaIndexVM model = new TrenutnaSesijaIndexVM();
            model.Rows = _context.AutorizacijskiToken.Select(s => new TrenutnaSesijaIndexVM.Row
            {
                VrijemeLogiranja = s.VrijemeEvidentiranja,
                token = s.Vrijednost,
                IpAdresa = s.IpAdresa
            }).ToList();
            model.TrenutniToken = HttpContext.GetTrenutniToken();
            return View(model);
        }
        public IActionResult Obrisi(string token)
        {
            AutorizacijskiToken obrisati = _context.AutorizacijskiToken.FirstOrDefault(x => x.Vrijednost == token);
            if (obrisati != null)
            {
                _context.AutorizacijskiToken.Remove(obrisati);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}