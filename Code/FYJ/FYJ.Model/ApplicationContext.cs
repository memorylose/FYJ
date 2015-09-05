using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.Model
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("FYJConnection")
        {
        }

        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new CreateDatabaseIfNotExists<ApplicationContext>());
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleType> ArticleType { get; set; }
        public DbSet<ArticleFavorite> ArticleFavorite { get; set; }
        public DbSet<ArticlePicture> ArticlePicture { get; set; }
        public DbSet<ArticleComment> ArticleComment { get; set; }
    }
}
