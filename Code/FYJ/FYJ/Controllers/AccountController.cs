using FYJ.BLL;
using FYJ.Constant;
using FYJ.Model;
using FYJ.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
                ViewBag.NickName = userInfo.NickName;
            }
            
            return View(userInfo);

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

            UserRepository userRep = new UserRepository();
            int userId = Convert.ToInt32(Session[SystemSession.USER_SESSION]);
            UserInfo userInfo = userRep.GetUserInfo(userId);
            if (userInfo != null)
            {
                ViewBag.NickName = userInfo.NickName;
            }
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
                //TODO 需要给错误类型分开，如果没登陆跳到登录页面
                UserRepository userRep = new UserRepository();
                if (!userRep.CheckOldPassword(Convert.ToInt32(Session[SystemSession.USER_SESSION]), model.Password))
                {
                    ModelState.AddModelError("", "原始密码错误");
                }
                else
                {
                    //update new password
                    if (userRep.ChangePassword(Convert.ToInt32(Session[SystemSession.USER_SESSION]), model.NewPassword))
                    {
                        //TODO success
                    }
                    else
                    {
                        ModelState.AddModelError("", "修改密码错误，请联系客服");
                    }
                }
                return View();
            }
        }

        [HttpGet]
        public ActionResult UploadImage()
        {
            UserRepository userRep = new UserRepository();
            int userId = Convert.ToInt32(Session[SystemSession.USER_SESSION]);
            UserInfo userInfo = userRep.GetUserInfo(userId);
            if (userInfo != null)
            {
                ViewBag.NickName = userInfo.NickName;
            }
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(string path)
        {
            if (Session[SystemSession.USER_SESSION] != null)
            {
                foreach (string item in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    if (FileOperator.CheckImageExtension(file.InputStream))
                    {
                        string imageFileName = FileOperator.GenerateImageFileName();
                        string fileName = imageFileName + Path.GetExtension(file.FileName);
                        try
                        {
                            file.SaveAs(Server.MapPath(FilePath.USER_PHOTO_FOLDER + "/" + fileName));
                            int userId = Convert.ToInt32(Session[SystemSession.USER_SESSION]);
                            var userModel = db.UserInfo.Where(c => c.UserId == userId).FirstOrDefault();
                            userModel.Photo = fileName;

                            //TODO delete old photo?
                            db.Entry(userModel).State = EntityState.Modified;
                            db.SaveChanges();

                            return RedirectToAction("../Article/Index");
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Upload user photo failed." + ex.ToString());
                        }
                    }
                    else
                    {

                    }
                }
            }
            return View();
        }
    }
}