using FYJ.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FYJ.BLL
{
    public class SessionRepository
    {
        public static void CreateUserSession(string userId)
        {
            if (System.Web.HttpContext.Current.Session[Session.USER_SESSION] == null)
            {
                System.Web.HttpContext.Current.Session[Session.USER_SESSION] = userId;
            }
        }
    }
}
