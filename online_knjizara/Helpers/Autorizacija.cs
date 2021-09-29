using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_knjizara.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using online_knjizara.Helpers;
using online_knjizara.EF;

namespace online_knjizara.Helpers
{

    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool zaposlenik, bool citaockorisnik)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { zaposlenik, citaockorisnik };
        }
    }


    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool zaposlenik, bool citaocikorisnici)
        {
            _zaposlenik = zaposlenik;
            _citaocikorisnici = citaocikorisnici;
        }
        private readonly bool _zaposlenik;
        private readonly bool _citaocikorisnici;
        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            KorisnickiNalozi k = filterContext.HttpContext.GetLogiraniKorisnik();

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }

                filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { area = "" });
                return;
            }

            //Preuzimamo DbContext preko app services
            OnlineKnjizaraDbContext db = filterContext.HttpContext.RequestServices.GetService<OnlineKnjizaraDbContext>();

            //zaposlenici mogu pristupiti 
            if (_zaposlenik && db.Zaposlenici.Any(s => s.KorisnickiNalog_ID == k.KorisnickiNalog_ID))
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            //citaocikorisnici mogu pristupiti studenti

            if (_citaocikorisnici && db.CitaocKorisnik.Any(s => s.KorisnickiNalog_ID == k.KorisnickiNalog_ID))
            {
                await next();//ok - ima pravo pristupa
                return;
            }
            if (db.CitaocKorisnik.All(x => x.KorisnickiNalog_ID != k.KorisnickiNalog_ID) && db.Zaposlenici.Any(x => x.KorisnickiNalog_ID != k.KorisnickiNalog_ID))
            {
                filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { area = "" });
                return;
            }
            if (filterContext.Controller is Controller c1)
            {
                c1.TempData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { @area = "" });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}