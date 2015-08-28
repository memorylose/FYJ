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
            //Create database when model changed
            Database.SetInitializer<ApplicationContext>(new ApplicationDbInitializer());
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

    /// <summary>
    /// Rewrite method to seed
    /// </summary>
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            //context.Blog.Add(new Blog() { Title = "test2", Contents = "test2", UserName = "test2", CrDate = DateTime.Now.ToString() });
        }
    }

    [Table("FYJ_User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(200)]
        public string Password { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(20)]
        public string EmailCode { get; set; }
        public bool? EmailConfirm { get; set; }
        public bool? IsLock { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? LockDate { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CrDate { get; set; }
        public int? CrUserId { get; set; }
    }

    [Table("FYJ_Role")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(20)]
        public string RoleName { get; set; }
    }

    [Table("FYJ_UserInfo")]
    public class UserInfo
    {
        [Key]
        public int InfoId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(30)]
        public string Photo { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(20)]
        public string City { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(20)]
        public string Contact { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(20)]
        public string Favorite { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Description { get; set; }
    }

    [Table("FYJ_UserLogin")]
    public class UserLogin
    {
        [Key]
        public int LoginId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(10)]
        public string LoginRegion { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(10)]
        public string LoginIp { get; set; }
        public DateTime? LoginDate { get; set; }
        public int? LoginType { get; set; }
    }

    [Table("FYJ_Article")]
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength]
        public string Contents { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength]
        public string ArticleTypeId { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime CrDate { get; set; }
        public int CrUserId { get; set; }
        public DateTime? UpDate { get; set; }
        public int? UpUserId { get; set; }
    }

    [Table("FYJ_ArticleType")]
    public class ArticleType
    {
        [Key]
        public int TypeId { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(10)]
        public string TypeName { get; set; }
    }

    [Table("FYJ_ArticleFavorite")]
    public class ArticleFavorite
    {
        [Key]
        public int FId { get; set; }
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public DateTime CrDate { get; set; }
    }

    [Table("FYJ_ArticlePicture")]
    public class ArticlePicture
    {
        [Key]
        public int PictureId { get; set; }
        public int ArticleId { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]
        public string Path { get; set; }
        public bool? IsDelete { get; set; }
    }

    [Table("FYJ_ArticleComment")]
    public class ArticleComment
    {
        [Key]
        public int CommentId { get; set; }
        public int ArticleId { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(500)]
        public string Comments { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime CrDate { get; set; }
        public int CrUserId { get; set; }
    }
}
