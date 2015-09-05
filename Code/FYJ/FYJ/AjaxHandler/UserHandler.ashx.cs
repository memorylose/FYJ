using FYJ.BLL;
using FYJ.IBLLStrategy;
using FYJ.Utility;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;

namespace FYJ.AjaxHandler
{
    public class UserHandler : IHttpHandler
    {
        UserRepository userRep = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Charset = "utf-8";

            string AjaxMethod = context.Request.Params["Method"];
            switch (AjaxMethod)
            {
                case "CheckUserMail":
                    CheckUserMailExist(context);
                    break;
                case "Register":
                    RegisterUser(context);
                    break;
            }
        }

        /// <summary>
        /// Check user mail exist
        /// </summary>
        /// <param name="context"></param>
        public void CheckUserMailExist(HttpContext context)
        {
            bool validResult = true;
            string userMail = context.Request.Params["userMail"] == null ? "" : context.Request.Params["userMail"].ToString();
            if (RegExp.IsEmail(userMail))
            {
                userRep = new UserRepository();
                if (userRep.CheckUserMailExists(userMail))
                    validResult = false;
            }
            var rtnJsonModel = new { valid = validResult };
            string json = JsonConvert.SerializeObject(rtnJsonModel);

            context.Response.Write(json);
        }

        public void RegisterUser(HttpContext context)
        {
            string mail = context.Request.Params["email"] == null ? "" : context.Request.Params["email"].ToString();
            string password = context.Request.Params["password"] == null ? "" : context.Request.Params["password"].ToString();
            string code = context.Request.Params["code"] == null ? "" : context.Request.Params["code"].ToString();

            string g_code = context.Request.Cookies["CheckCode"].Value;

            //IRegister register = new UserRegisterRepository();



            //register.Register();
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