﻿using FYJ.BLL;
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
        private ApplicationContext db = new ApplicationContext();

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

            int index = 0;
            bool indexFlag = false;
            //add user images
            //TODO SHOW THE PICTURE(@Url.Content)
            foreach (string item in Request.Files)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    index++;
                    string dbPath = string.Empty;
                    UserArticleRepository userRep = new UserArticleRepository();
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    userRep.UploadArticleUserImage(file, ref dbPath);
                    if (index == 1)
                        indexFlag = true;
                    userRep.AddImageInDb(articleId, dbPath, indexFlag);
                    indexFlag = false;
                }
            }
            return RedirectToAction("Index", "Article");
        }

        // GET: Article Detial
        public ActionResult Detail(int articleId)
        {
            List<Article> details = (from c in db.Article select c).Where(c => c.ArticleId == articleId).ToList();

            return View(details);
        }
    }
}