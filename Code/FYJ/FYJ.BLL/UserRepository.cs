using FYJ.Model;
using System;
using System.Collections.Generic;
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
    }
}
