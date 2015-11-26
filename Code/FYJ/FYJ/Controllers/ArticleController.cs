using FYJ.BLL;
using FYJ.Constant;
using FYJ.IBLLStrategy;
using FYJ.Model;
using FYJ.Model.ViewModel;
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

            if (Session[SystemSession.USER_SESSION] != null)
            {
                int userId = Convert.ToInt32(Session[SystemSession.USER_SESSION]);
                ArticleUserModel userInfo = (from t in db.User
                                             join t0 in db.UserInfo on t.UserId equals t0.UserId into t0_join
                                             from t0 in t0_join.DefaultIfEmpty()
                                             where
                                               t.UserId == userId
                                             select new ArticleUserModel()
                                             {
                                                 UserId = t.UserId,
                                                 UserName = t.UserName,
                                                 Description = t0.Description,
                                                 NickName = t0.NickName,
                                                 Photo = t0.Photo
                                             }).FirstOrDefault();

                if (userInfo != null)
                {
                    //get nickname
                    if (string.IsNullOrEmpty(userInfo.NickName))
                        userInfo.NickName = userInfo.UserName;

                    //get photo
                    if (string.IsNullOrEmpty(userInfo.Photo))
                        userInfo.Photo = Server.MapPath("/Image/Ano.png");
                }
                ViewBag.ArticleUser = userInfo;
            }
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
            FYJ.Model.ArticleModel articlesD = (from c in db.Article
                                      select new FYJ.Model.ArticleModel()
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