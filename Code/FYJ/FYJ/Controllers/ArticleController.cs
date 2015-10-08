using FYJ.BLL;
using FYJ.IBLLStrategy;
using FYJ.Model;
using FYJ.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult Create(Article viewModel)
        {
            //TODO add tran

            IArticle article = new UserArticleRepository();
            int articleId = article.AddArticle(viewModel);

            //add user images
            //TODO SHOW THE PICTURE(@Url.Content)
            foreach (string item in Request.Files)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string dbPath = string.Empty;
                    UserArticleRepository userRep = new UserArticleRepository();
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    userRep.UploadArticleUserImage(file, ref dbPath);
                    userRep.AddImageInDb(articleId, dbPath);
                }
            }
            return View();
        }
    }
}