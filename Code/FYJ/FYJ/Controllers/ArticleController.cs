using FYJ.BLL;
using FYJ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYJ.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string test222)
        {




            //add user images
            //TODO SHOW THE PICTURE(@Url.Content)
            foreach (string item in Request.Files)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    UserArticleRepository userRep = new UserArticleRepository();
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    userRep.UploadArticleUserImage(file);
                }
            }
            
            return View();
        }
    }
}