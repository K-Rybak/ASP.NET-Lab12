using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab12.Models;
using System.Data.Entity;

namespace Lab12.Controllers
{
    public class RegistrationController : Controller
    {
        ChatContext db = new ChatContext();

        public ActionResult Reg()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult Reg(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectPermanent("/Home/Index");
        }
    }
}