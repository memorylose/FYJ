using FYJ.BLL;
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

            if (userLogin.CheckUserLogin(model.Email, model.VerifyCode, ref message))
            {

            }
            else
            {

            }

            ViewBag.Message = message;
            return View();
        }
    }
}