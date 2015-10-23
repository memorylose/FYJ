using FYJ.BLL;
using FYJ.Constant;
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
        private ApplicationContext db;

        public ArticleController()
        {
            db = new ApplicationContext();
        }

        // GET: Article
        public ActionResult Index()
        {
            //TODO CHECK TOKEN

            string nickName = string.Empty;

            if (Session[SystemSession.USER_SESSION] != null)
            {
                int userId = Convert.ToInt32(Session[SystemSession.USER_SESSION]);
                UserInfo userInfo = db.UserInfo.Where(c => c.UserId == userId).FirstOrDefault();
                nickName = userInfo.NickName;
            }

            ViewBag.NickName = nickName;
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
        public ActionResult Detail(int id)
        {
            //TODO check format
            ArticleModel articlesD = (from c in db.Article
                                      select new ArticleModel()
                                      {
                                          ArticleId = c.ArticleId,
                                          Contents = c.Contents,
                                          CrDate = c.CrDate,
                                      }).Where(c => c.ArticleId == id).FirstOrDefault();

            //get content picture
            List<ArticlePicture> imageList = (from c in db.ArticlePicture select c).Where(c => c.ArticleId == id && c.IsDelete == false).ToList();
            ViewBag.ImageList = imageList;

            return View(articlesD);
        }
    }
}