using FYJ.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYJ.AttributeHanlder
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public new string[] Roles { get; set; }

        /// <summary>
        /// 当加载页面的时候，获取当前Action的权限
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            CARepository caRep = new CARepository();
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string roles = caRep.GetActionRoleId(controllerName, actionName);
            this.Roles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 对比当前页面的权限和当前用户的权限，如果通过则返回True
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (Roles == null || Roles.Length == 0)
            {
                return true;
            }
            else
            {
                UserRepository userRep = new UserRepository();
                string roleId = userRep.GetUserRole();
                if (string.IsNullOrEmpty(roleId))
                {
                    return false;
                }
                else
                {
                    if (Roles.Count(c => c == roleId) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 如果AuthorizeCore为False，会走到这个方法，并且返回401，如果重写了这个方法，那么会走到这个方法里，如果没有则会走到webconfig中定义的return url
        /// </summary>
        /// <param name="context"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            else
            {
                string path = context.HttpContext.Request.Path;
                string strUrl = "/User/Login";
                context.HttpContext.Response.Redirect(string.Format(strUrl, HttpUtility.UrlEncode(path)), true);
            }
        }
    }
}