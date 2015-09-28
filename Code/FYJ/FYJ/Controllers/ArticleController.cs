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
        public ActionResult Create(IEnumerable<HttpPostedFileBase> files)
        {
            //TODO SHOW THE PICTURE(@Url.Content)
            foreach (var file1 in files)
            {
                string test = file1.FileName;
                file1.SaveAs(Server.MapPath("/UserImage/") + DateTime.Now.ToString("yyyyMMdd") + "-" + test);
            }
            return View();
        }
    }
}