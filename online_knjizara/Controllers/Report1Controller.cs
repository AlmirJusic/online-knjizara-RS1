using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using online_knjizara.EF;
using online_knjizara.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1;

namespace online_knjizara.Controllers
{
    [Autorizacija(zaposlenik: true, citaockorisnik: false)]
    public class Report1Controller : Controller
    {
        private OnlineKnjizaraDbContext _context;

        public Report1Controller(OnlineKnjizaraDbContext context)
        {
            _context = context;
        }
        public static List<Report1Row> getNarudzbe(OnlineKnjizaraDbContext _context)
        {
            List<Report1Row> podaci = _context.Narudzba.Select(x => new Report1Row
            {
                Knjiga = x.Knjiga.Naziv,
                Korisnik = x.Korisnik.Ime + " " + x.Korisnik.Prezime,
                DatumVrijeme = x.DatumVrijeme.ToShortDateString(),
                Kolicina = x.Kolicina,
                Cijena = x.Cijena,
                UkupnaCijena = x.Cijena * x.Kolicina


            }).ToList();

            return podaci;
        }

        public IActionResult Index()
        {
            LocalReport _localReport = new LocalReport("Reports/Report1.rdlc");
            List<Report1Row> podaci = getNarudzbe(_context);
            _localReport.AddDataSource("DataSet1", podaci);

            ReportResult result = _localReport.Execute(RenderType.Pdf);

            return File(result.MainStream, "application/pdf");
        }
    }
}
