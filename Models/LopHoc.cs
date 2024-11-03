using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassStudentManagement.Models
{
    [Table("LOPHOC")]
    public class LopHoc
    {
        [Key]
        public string IDLop { get; set; }
        [Required]
        public string TenLop { get; set; }
        public ICollection<HocSinh> DSHStrongLop { get; } = new List<HocSinh>();
    }
}
