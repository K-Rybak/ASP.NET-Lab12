using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab12.Models;

namespace Lab12.Controllers
{
    public class ChatController : Controller
    {
        ChatContext db = new ChatContext();

        public ActionResult Index()
        {
            if (Session["user"] == null)
                return Redirect("/");

            var currentUser = (User)Session["user"];

                ViewBag.currentUser = db.Users.Where(x => x.Id == currentUser.Id)
                    .Select(x => x.Surname + " " + x.Name + " " + x.Patronymic).First();

                var query = from pl in db.Users
                                  join fromWho in db.Messages on pl.Id equals fromWho.SenderId
                                  where fromWho.ReciverId == currentUser.Id
                                  select new MessageData
                                  {
                                      Title = fromWho.TitleMessage,
                                      Login = pl.Login,
                                      Date = fromWho.CreateAt
                                  };

            ViewBag.messageData = query;
            return View(query);
        }

        public ActionResult MessageView(string title, string login)
        {
            if (Session["user"] == null)
                return Redirect("/");

            var currentUser = (User)Session["user"];
            
            var query = db.Users.Where(x => x.Login.Equals(login)).Select(x => x.Id).First();

            var result = db.Messages.Where(x => x.ReciverId == currentUser.Id && x.SenderId == query 
            && x.TitleMessage.Equals(title)).Select(x => x.TextMessage).FirstOrDefault();
            ViewBag.Message = result;
            return View();
        }


        public ActionResult MessageCreate()
        {
            if (Session["user"] == null)
                return Redirect("/");

            var currentUser = (User)Session["user"];
    
            return View();         
        }


        [HttpPost]
        public ActionResult SendTheNewMessage(string User, Message model)
        {
            var currentUser = (User)Session["user"];

            if (string.IsNullOrEmpty(User))
            {
                ModelState.AddModelError("Person", "Некорректный адресант");
            }
            if (string.IsNullOrEmpty(model.TitleMessage))
            {
                ModelState.AddModelError("header", "Некорректный заголовок");
            }
            if (string.IsNullOrEmpty(model.TextMessage))
            {
                ModelState.AddModelError("text", "Некорректный текс");
            }

            if (ModelState.IsValid)
            {
                model.SenderId = currentUser.Id;
                model.ReciverId = db.Users.Where(x => x.Login.Equals(User)).Select(x => x.Id).FirstOrDefault();
                model.CreateAt = DateTime.Now;
                db.Messages.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Не верный ввод";
                return RedirectToAction("MessageCreate");
            }
        }

    }
}