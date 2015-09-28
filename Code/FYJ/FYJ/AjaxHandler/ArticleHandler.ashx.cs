using FYJ.BLL;
using FYJ.IBLLStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYJ.AjaxHandler
{
    /// <summary>
    /// Summary description for ArticleHandler
    /// </summary>
    public class ArticleHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Charset = "utf-8";

            string AjaxMethod = context.Request.Params["Method"];
            switch (AjaxMethod)
            {
                case "AddArticle":
                    AddArticle(context);
                    break;
            }
        }

        public void AddArticle(HttpContext context)
        {
            //HttpPostedFile test = context.Request.
            //string test1 = test.FileName;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;



            //TODO NINJECT
            IArticle article = new UserArticleRepository();
            article.AddArticle();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}