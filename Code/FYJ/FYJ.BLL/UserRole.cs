using FYJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.BLL
{
    public class UserRole
    {
        private ApplicationContext db = new ApplicationContext();

        /// <summary>
        /// Check user role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool CheckUserRole(int userId, int roleId)
        {
            User userModel = db.User.Where(c => c.UserId == userId).FirstOrDefault();
            if (userModel.RoleId != null && userModel.RoleId == roleId)
                return true;
            else
                return false;
        }
    }
}
