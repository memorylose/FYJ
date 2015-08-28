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

        ////POST: User register
        //[HttpPost]
        //public ActionResult Register()
        //{
        //    return View();
        //}
    }
}