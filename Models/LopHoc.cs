using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassStudentManagement.Models
{
    public class LopHoc
    {
        public string IDLop { get; set; }
        public string TenLop { get; set; }
        public ICollection<HocSinh> DSHStrongLop { get; } = new List<HocSinh>();
    }
}
