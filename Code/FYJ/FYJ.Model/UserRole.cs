using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYJ.Model
{
    [Table("FYJ_Role")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(20)]
        public string RoleName { get; set; }
    }
}
