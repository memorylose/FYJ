using FYJ.BLL;
using FYJ.Constant;
using FYJ.Model;
using FYJ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYJ.Controllers
{
    public class UserController : Controller
    {
        // GET: User register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(RegisterModel model)
        {
            UserLoginRepository userLogin = new UserLoginRepository();
            string message = string.Empty;

            if (userLogin.ValidateLogin(model.Email, model.VerifyCode, ref message))
            {
                if (userLogin.IsLogin(model.Email, model.Password))
                {
                    Session[SystemSession.USER_SESSION] = model.Email;
                    return RedirectToAction("../Article/Index");
                }
                else
                {
                    message = "用户名密码错误";
                }
            }
            ViewBag.Message = message;
            return View();
        }
    }
}