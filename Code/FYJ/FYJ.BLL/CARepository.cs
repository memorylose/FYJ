using FYJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.BLL
{
    public class CARepository
    {
        private ApplicationContext db;

        public CARepository()
        {
            db = new ApplicationContext();
        }

        public string GetActionRoleId(string controllerName, string actionName)
        {
            string role = string.Empty;
            CA controlRole = db.CA.Where(c => c.Name == controllerName && c.Type == 0).FirstOrDefault();
            if (controlRole != null)
            {
                string actionRole = db.CA.Where(c => c.Name == actionName && c.Type == controlRole.CAId).Select(c => c.RoleId).FirstOrDefault();
                if (string.IsNullOrEmpty(controlRole.RoleId) && !string.IsNullOrEmpty(actionRole))
                {
                    role = actionRole;
                }
                else if (!string.IsNullOrEmpty(controlRole.RoleId) && string.IsNullOrEmpty(actionRole))
                {
                    role = controlRole.RoleId;
                }
                else if (!string.IsNullOrEmpty(controlRole.RoleId) && !string.IsNullOrEmpty(actionRole))
                {
                    role = actionRole;
                }
            }
            return role;
        }
    }
}
