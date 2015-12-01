using FYJ.Constant;
using FYJ.IBLLStrategy;
using FYJ.Model;
using FYJ.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FYJ.BLL
{
    public class UserArticleRepository : IArticle
    {
        private ApplicationContext db = new ApplicationContext();

        public int AddArticle(Article viewModel, int userId)
        {
            Article articleModel = new Article();
            articleModel.ArticleTypeId = string.Empty;
            articleModel.Contents = viewModel.Contents;
            articleModel.CrDate = DateTime.Now;
            articleModel.CrUserId = userId;
            articleModel.IsDelete = false;
            articleModel.Title = "";

            db.Entry(articleModel).State = EntityState.Added;
            db.SaveChanges();

            return articleModel.ArticleId;
        }

        public void UploadArticleUserImage(HttpPostedFileBase file, ref string dbPath)
        {
            if (FileOperator.CheckImageExtension(file.InputStream))
            {
                //db path
                dbPath = string.Empty;

                //user images folder
                string userImageFolder = "~/" + FilePath.USER_ARTICLE_IMAGE_FOLDER;

                //full image path
                string imgPath = string.Empty;

                //image date folder
                string imgDatePath = string.Empty;

                //create image folder
                FileOperator.CheckImageFileFolder(userImageFolder, ref imgPath, ref imgDatePath);
                try
                {
                    string imageFileName = FileOperator.GenerateImageFileName();
                    string fullPath = imgPath + imageFileName + Path.GetExtension(Path.GetFileName(file.FileName));
                    file.SaveAs(fullPath);
                    dbPath = imgDatePath + imageFileName + Path.GetExtension(Path.GetFileName(file.FileName));
                }
                catch (Exception ex)
                {
                    Log.Error("Upload user image failed." + ex.ToString());
                }
            }
            else
            {
                Log.Error("Check image extension failed." + file.InputStream);
            }
        }

        public void AddImageInDb(int articleId, string path, bool isIndex)
        {
            ArticlePicture picModel = new ArticlePicture();
            picModel.ArticleId = articleId;
            picModel.IsDelete = false;
            picModel.Path = path;
            picModel.IsIndex = isIndex;

            db.Entry(picModel).State = EntityState.Added;
            db.SaveChanges();
        }
    }
}
