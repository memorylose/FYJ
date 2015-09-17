using FYJ.BLL;
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

            //base.OnActionExecuting(filterContext);
        }
    }
}