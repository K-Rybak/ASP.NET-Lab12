using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Lab12.Models;

namespace Lab12.Controllers
{
    public class HomeController : Controller
    {
       ChatContext db = new ChatContext();

       
       public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult Index(string username, string password)
        {
            User autification = db.Users.FirstOrDefault(x => x.Login == username && x.Password == password);

            if (autification != null)
            {
                Session["user"] = autification;
                return RedirectPermanent("/Chat/Index");
            }
            else
                return Redirect("/");
            
           
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}