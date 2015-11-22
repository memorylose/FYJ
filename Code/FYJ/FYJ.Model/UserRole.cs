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

    [Table("FYJ_CARole")]
    public class CARole
    {
        public int CAId { get; set; }
        public string CAName { get; set; }
        public int CAType { get; set; }
        public string RoleId { get; set; }
    }
}
