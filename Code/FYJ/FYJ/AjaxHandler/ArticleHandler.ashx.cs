﻿using FYJ.BLL;
using FYJ.IBLLStrategy;
using FYJ.Model;
using FYJ.Model.ViewModel;
using Newtonsoft.Json;
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
        private ApplicationContext db = new ApplicationContext();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Charset = "utf-8";

            string Method = context.Request.Params["Method"];
            string Count = context.Request.Params["Count"];

            switch (Method)
            {
                case "GetArticle":
                    GetArticle(context, Count);
                    break;
            }
        }

        private void GetArticle(HttpContext context, string count)
        {
            List<ArticleModel> blogList = (from c in db.Article
                                           join p in db.ArticlePicture on c.ArticleId equals p.ArticleId
                                           join t0 in db.UserInfo on new { CrUserId = c.CrUserId } equals new { CrUserId = t0.UserId } into t0_join
                                           from t0 in t0_join.DefaultIfEmpty()
                                           select new ArticleModel()
                                           {
                                               ArticleId = c.ArticleId,
                                               Contents = c.Contents,
                                               Image = p.Path,
                                               IsArticleDel = c.IsDelete,
                                               IsPicDel = p.IsDelete,
                                               IsIndex = p.IsIndex,
                                               UserPhoto = t0.Photo
                                           }).Where(p => p.IsArticleDel == false && p.IsPicDel == false && p.IsIndex == true).OrderByDescending(c => c.ArticleId).Skip(Convert.ToInt32(count)).Take(10).ToList();
            string json = JsonConvert.SerializeObject(blogList);
            context.Response.Write(json);
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