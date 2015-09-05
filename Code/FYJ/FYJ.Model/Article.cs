using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYJ.Model
{
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
