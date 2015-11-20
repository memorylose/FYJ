using FYJ.Constant;
using FYJ.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.Model
{
    internal sealed class Configuration : DbMigrationsConfiguration<FYJ.Model.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "FYJ.Model.ApplicationContext";
        }

        protected override void Seed(ApplicationContext context)
        {
            string salt = Encryption.GetRandomSalt(Security.SALT_BYTE_NUMBER); 

            //users
            context.User.AddOrUpdate(
                new User() { UserName = "admin@fyj.com", Password = Encryption.CreateSHA256HashString("admin" + salt), Salt = salt, Email = "admin@fyj.com", EmailCode = "", EmailConfirm = true, IsLock = false, IsDelete = false, LockDate = null, RoleId = 1, CrDate = DateTime.Now, CrUserId = 0 }
            );

            //user role
            context.Role.AddOrUpdate(
                new Role() { RoleName = "superadmin" },
                new Role() { RoleName = "admin" },
                new Role() { RoleName = "user" }
            );
        }
    }
}
