using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassStudentManagement.Models
{
    public class HocSinh
    {
        [Key]
        public string IDHocSinh { get; set; }
        public string TenHocSinh { get; set; }
        public string NgSinh { get; set; }

        [ForeignKey("LopHoc")]
        public string IDLop {  get; set; }
        public LopHoc LopHoc { get; set; } = null!;
    }
}
