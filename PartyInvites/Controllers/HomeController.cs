using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        DataClasses1DataContext db = new DataClasses1DataContext();
        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good morning" : "Good afternoon";
            return View();
            
        }
        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }
        [HttpPost]
        public ViewResult RsvpForm(CHIRA guestResponse)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.CHIRAs.InsertOnSubmit(guestResponse);
                    db.SubmitChanges();
                   // db.SPNIKALO(guestResponse.Name, guestResponse.Email, guestResponse.Phone, guestResponse.WillAttend,"Insert");
                    return View("Thanks", guestResponse);
                }
                catch (Exception )
                {
                   // return( "Something Bad happened, Please contact Administrator!!!!");  
                    return View("Sorry",guestResponse);
                }

                
            }
            else
            {
                // there is a validation error - redisplay the form
                return View();
            }
        }
        [HttpGet]
        public ViewResult show()
        {
            return View();
        }
        [HttpPost]
        public ViewResult show(string email)
        {
            var data = db.CHIRAs.Where(x=>x.Email==email).ToList();


            return View("Attendes", data);
        }
        [HttpGet]
        public ViewResult DataC()
        {
            return View();
        }
        [HttpPost]
        public ViewResult DataC(GuestResponse guestResponse,string mail)
        {
            db.DataChange(guestResponse.Name, guestResponse.Email, guestResponse.Phone, guestResponse.WillAttend, mail);
            return View("UPDATE_DATA",guestResponse);
        }
        [HttpGet]
        public ViewResult DataRem()
        {
            return View();
        }
        [HttpPost]
        public ViewResult DataRem(string email)
        {
            db.DataRemove(email);
            return View("DATA_DELETE");
        }

    }
}
