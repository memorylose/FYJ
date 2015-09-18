﻿using FYJ.BLL;
using FYJ.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYJ.AttributeHanlder
{
    public class RoleAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserRole userRole = new UserRole();

            //TODO DONT KNOW CAN GET SESSION?
            if (System.Web.HttpContext.Current.Session[SystemSession.USER_SESSION] != null && userRole.CheckUserRole(System.Web.HttpContext.Current.Session[SystemSession.USER_SESSION].ToString(), 3))
            {
                filterContext.Result = new RedirectResult("/Article/Index");
            }
            else
            {
                string urlreffer = filterContext.HttpContext.Request.UrlReferrer == null ? string.Empty
                                           : filterContext.HttpContext.Request.UrlReferrer.AbsoluteUri;
                if (string.IsNullOrEmpty(urlreffer))
                    urlreffer = filterContext.HttpContext.Request.Url == null ? string.Empty : filterContext.HttpContext.Request.Url.AbsoluteUri;

                string locationUrl = string.Empty;
                //locationUrl = TsingDa.Common.WebConfig.GetWebConfig("website_url", "") + "/Home/Login?ReturnUrl=" +
                //                            filterContext.HttpContext.Server.UrlEncode(urlreffer);
                RedirectResult loginUrl = new RedirectResult("/user/login");
                filterContext.Result = loginUrl;
            }
            base.OnActionExecuting(filterContext);

            ////TODO
            //if (!userRole.CheckUserRole(0, 0))
            //{
            //    string urlreffer = filterContext.HttpContext.Request.UrlReferrer == null ? string.Empty
            //                               : filterContext.HttpContext.Request.UrlReferrer.AbsoluteUri;
            //    if (string.IsNullOrEmpty(urlreffer))
            //        urlreffer = filterContext.HttpContext.Request.Url == null ? string.Empty : filterContext.HttpContext.Request.Url.AbsoluteUri;

            //    string locationUrl = string.Empty;
            //    locationUrl = TsingDa.Common.WebConfig.GetWebConfig("website_url", "") + "/Home/Login?ReturnUrl=" +
            //                                filterContext.HttpContext.Server.UrlEncode(urlreffer);
            //    RedirectResult loginUrl = new RedirectResult(locationUrl);
            //    filterContext.Result = loginUrl;
            //}
            //else
            //{
            //    //---------【验证用户的控制器的访问权限（利用位运算灵活的解决多角色问题）】---------
            //    if (((int)_role & (int)memberLoginRole) <= 0 && ((int)_role != memberLoginRole))
            //    {
            //        filterContext.Result = new RedirectResult("/Home/RoleError");
            //    }
            //}


        }
    }
}