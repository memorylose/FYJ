using FYJ.Constant;
using FYJ.Model;
using FYJ.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.BLL
{
    public class UserRepository
    {
        private ApplicationContext db;

        public UserRepository()
        {
            db = new ApplicationContext();
        }

        /// <summary>
        /// Check user mail exists
        /// </summary>
        /// <param name="userMail"></param>
        /// <returns></returns>
        public bool CheckUserMailExists(string userMail)
        {
            var userModel = db.User.Where(c => c.Email == userMail).FirstOrDefault();
            if (userModel != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Get userid by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int GetUserIdByEmail(string email)
        {
            var userModel = db.User.Where(c => c.Email == email).FirstOrDefault();
            if (userModel != null)
                return userModel.UserId;
            else
                return 0;
        }

        /// <summary>
        /// check verify code
        /// </summary>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public static bool CheckValidateCode(string verifyCode)
        {
            bool result = false;
            string userCode = verifyCode == null ? "" : verifyCode.ToLower();
            string systemCode = System.Web.HttpContext.Current.Request.Cookies["CheckCode"].Value.ToLower();

            if (System.Web.HttpContext.Current.Request.Cookies["CheckCode"] != null && string.Equals(systemCode, userCode))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Check user`s password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckOldPassword(int userId, string password)
        {
            bool result = false;
            User user = db.User.Where(c => c.UserId == userId).FirstOrDefault();
            if (user != null && user.Password == Encryption.CreateSHA256HashString(password + user.Salt))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Change user password
        /// </summary>
        public bool ChangePassword(int userId, string password)
        {
            bool result = false;
            User user = db.User.Where(c => c.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                string salt = Encryption.GetRandomSalt(Security.SALT_BYTE_NUMBER);
                user.Salt = salt;
                user.Password = Encryption.CreateSHA256HashString(password + salt);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUser(int userId)
        {
            User user = db.User.Where(c => c.UserId == userId).FirstOrDefault();
            if (user != null)
                return user;
            else
                return null;
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(int userId)
        {
            UserInfo userInfo = db.UserInfo.Where(c => c.UserId == userId).FirstOrDefault();
            if (userInfo != null)
                return userInfo;
            else
                return null;
        }

        /// <summary>
        /// Get current user role id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserRole()
        {
            string roleId = string.Empty;
            if (System.Web.HttpContext.Current.Session[SystemSession.USER_SESSION] != null)
            {
                int userId = Convert.ToInt32(System.Web.HttpContext.Current.Session[SystemSession.USER_SESSION]);
                roleId = db.User.Where(c => c.UserId == userId).Select(p => p.RoleId).FirstOrDefault();
            }
            return roleId;
        }
    }
}
