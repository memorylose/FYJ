using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYJ.Model
{
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
        [MaxLength(200)]
        public string Salt { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(30)]
        public string EmailCode { get; set; }
        public bool? EmailConfirm { get; set; }
        public bool? IsLock { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? LockDate { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CrDate { get; set; }
        public int? CrUserId { get; set; }
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
        [Column(TypeName = "nvarchar")]
        [MaxLength(30)]
        public string NickName { get; set; }
        public int Sex { get; set; }
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
}
