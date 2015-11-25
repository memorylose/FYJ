using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYJ.Model
{
    [Table("FYJ_CA")]
    public class CA
    {
        [Key]
        public int CAId { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(20)]
        public string Name { get; set; }
        public int Type { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(30)]
        public string RoleId { get; set; }
    }
}
