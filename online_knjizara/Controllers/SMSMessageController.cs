using Microsoft.AspNetCore.Mvc;
using Nexmo.Api;
using online_knjizara.Helpers;
using online_knjizara.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.Controllers
{
    [Autorizacija(zaposlenik: true, citaockorisnik: false)]
    public class SMSMessageController : Controller
    {
        // GET: SMSMessage  
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(Message message)
        {
            var results = SMS.Send(new SMS.SMSRequest
            {
                from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
                to = message.To,
                text = message.ContentMsg
            });


            return View();
        }
    }
}
