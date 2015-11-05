using FYJ.BLL;
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
                //get username and mail
                int userId = Convert.ToInt32(Session[SystemSession.USER_SESSION]);
                var userModel = db.User.Where(c => c.UserId == userId).FirstOrDefault();
                ViewBag.UserName = userModel.UserName;
                ViewBag.UserMail = userModel.Email;

                //get user info
                UserInfo userInfo = db.UserInfo.Where(c => c.UserId == userId).FirstOrDefault();
                if (userInfo != null)
                {
                    //set sex
                    if (userInfo.Sex == 1)
                        ViewBag.SexM = "selected=\"selected\"";
                    if (userInfo.Sex == 2)
                        ViewBag.SexF = "selected=\"selected\"";
                }
                return View(userInfo);
            }
        }

        [HttpPost]
        public ActionResult Index(UserInfo model, FormCollection collection)
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

                //set sex
                string sex = collection.GetValue("selSex").AttemptedValue;
                if (string.Equals(sex, Portal.SEX_MAIL) || string.Equals(sex, Portal.SEX_FEMAIL))
                {
                    model.Sex = Convert.ToInt32(sex);
                }
                else
                {
                    //TODO failed
                }

                //modify
                if (userInfo != null)
                {
                    userInfo.City = model.City;
                    userInfo.Contact = model.Contact;
                    userInfo.Description = model.Description;
                    userInfo.Favorite = model.Favorite;
                    userInfo.NickName = model.NickName;
                    userInfo.Photo = model.Photo;
                    userInfo.Sex = model.Sex;

                    db.Entry(userInfo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                //add
                else
                {
                    model.UserId = userId;
                    db.Entry(model).State = EntityState.Added;
                    db.SaveChanges();
                }
                return RedirectToAction("../Account/Index");
            }
        }

        // GET: ChangePassword
        public ActionResult ChangePassword()
        {
            //TODO check token
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword model)
        {
            //TODO check token
            if (!UserRepository.CheckValidateCode(model.VerifyCode))
            {
                ModelState.AddModelError("", "验证码错误");
                return View();
            }
            else if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                UserRepository userRep = new UserRepository();
                if (!userRep.CheckOldPassword(Convert.ToInt32(Session[SystemSession.USER_SESSION]), model.Password))
                {
                    ModelState.AddModelError("", "原始密码错误");
                }
                else
                {
                    //update new password
                    if (userRep.ChangePassword(Convert.ToInt32(Session[SystemSession.USER_SESSION]), model.Password))
                    {
                        //success
                    }
                    else
                    {
                        ModelState.AddModelError("", "修改密码错误，请联系客服");
                    }
                }
                return View();
            }
        }
    }
}