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
    }
}
