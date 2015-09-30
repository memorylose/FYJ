using FYJ.Constant;
using FYJ.IBLLStrategy;
using FYJ.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FYJ.BLL
{
    public class UserArticleRepository : IArticle
    {
        public void AddArticle()
        {

        }

        public void UploadArticleUserImage(HttpPostedFileBase file)
        {
            if (FileOperator.CheckImageExtension(file.InputStream))
            {
                string userImageFolder = "~/" + FilePath.USER_ARTICLE_IMAGE_FOLDER;
                string imgPath = string.Empty;
                FileOperator.CheckFileFolder(userImageFolder, ref imgPath);
                try
                {
                    string ext = Path.GetExtension(Path.GetFileName(file.FileName));
                    file.SaveAs(imgPath + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Path.GetFileName(file.FileName)));
                }
                catch (Exception ex)
                {
                    //TODO log
                }
            }
            else
            {
                //TODO log
            }
        }
    }
}
