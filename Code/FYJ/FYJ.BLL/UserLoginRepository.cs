using FYJ.Model;
using FYJ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.BLL
{
    public class UserLoginRepository
    {
        private ApplicationContext db = new ApplicationContext();

        public bool ValidateLogin(string email, string verifyCode, ref string message)
        {
            bool result = false;
            message = string.Empty;

            string code = System.Web.HttpContext.Current.Request.Cookies["CheckCode"].Value.ToLower();

            if (System.Web.HttpContext.Current.Request.Cookies["CheckCode"] == null)
            {
                message = "您浏览器的cookie已被禁止，登录失败";
            }
            else if (!string.Equals(System.Web.HttpContext.Current.Request.Cookies["CheckCode"].Value.ToLower(), verifyCode.ToLower()))
            {
                message = "验证码错误";
            }
            else if (!RegExp.IsEmail(email))
            {
                message = "用户名密码错误";
            }
            else
            {
                result = true;
            }
            return result;
        }

        public bool IsLogin(string email, string password)
        {
            bool result = false;
            var userModel = db.User.Where(c => c.Email == email).FirstOrDefault();
            if (userModel != null)
            {
                string pwd = Encryption.CreateSHA256HashString(password + userModel.Salt);
                if (string.Equals(pwd, userModel.Password))
                    result = true;
            }
            return result;
        }
    }
}
