using FYJ.Constant;
using FYJ.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYJ.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext db;

        public AccountController()
        {
            db = new ApplicationContext();
        }
        // GET: Account
        public ActionResult Index()
        {
            //TODO check session
            if (Session[SystemSession.USER_SESSION] == null)
            {
                return RedirectToAction("../Article/Index");
            }
            else
            {
                int userId = Convert.ToInt32(Session[SystemSession.USER_SESSION]);
                var userModel = db.User.Where(c => c.UserId == userId).FirstOrDefault();
                ViewBag.UserName = userModel.UserName;
                ViewBag.UserMail = userModel.Email;

                UserInfo userInfo = db.UserInfo.Where(c => c.UserId == userId).FirstOrDefault();
                return View(userInfo);
            }
        }

        [HttpPost]
        public ActionResult Index(UserInfo model)
        {   //TODO check session
            if (Session[SystemSession.USER_SESSION] == null)
            {
                return RedirectToAction("../Article/Index");
            }
            else
            {
                //TODO check model format
                int userId = Convert.ToInt32(Session[SystemSession.USER_SESSION]);
                UserInfo userInfo = db.UserInfo.Where(c => c.UserId == userId).FirstOrDefault();

                //modify
                if (userInfo != null)
                {
                    userInfo.City = model.City;
                    userInfo.Contact = model.Contact;
                    userInfo.Description = model.Description;
                    userInfo.Favorite = model.Favorite;
                    userInfo.NickName = model.NickName;
                    userInfo.Photo = model.Photo;

                    db.Entry(userInfo).State = EntityState.Modified;
                }
                //add
                else
                {
                    model.UserId = userId;
                    db.Entry(model).State = EntityState.Added;
                }

                db.SaveChanges();
                return View();
            }
        }
    }
}