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
        private ApplicationContext db = new ApplicationContext();

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
            if (user != null && user.Password == Encryption.CreateSHA256HashString(user.Password + user.Salt))
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
                user.Password = Encryption.CreateSHA256HashString(password + salt);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}
