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
            //users
            context.User.AddOrUpdate(
                new User() { UserName = "admin", Password = "admin", Email = "admin@fyj.com", EmailCode = "", EmailConfirm = true, IsLock = false, IsDelete = false, LockDate = null, RoleId = 1, CrDate = DateTime.Now, CrUserId = 0 }
            );

            //user role
            context.Role.AddOrUpdate(
                new Role() { RoleName = "superadmin" },
                new Role() { RoleName = "admin" },
                new Role() { RoleName = "user" }
            );

            //Controller and action
            context.CA.AddOrUpdate(
                new CA() { CAId = 1, Name = "Article", Type = 0, RoleId = "" },
                new CA() { CAId = 2, Name = "Create", Type = 1, RoleId = "3" }
            );
        }
    }
}
