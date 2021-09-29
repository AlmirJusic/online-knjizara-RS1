using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using online_knjizara.EF;
using online_knjizara.ViewModels;

namespace TuristickaAgencija.Controllers
{

    public class KlijentHomeController : Controller
    {
        private readonly OnlineKnjizaraDbContext _context;

        public KlijentHomeController(OnlineKnjizaraDbContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Uslovi()
        {
            return View();
        }
       
    }
}
