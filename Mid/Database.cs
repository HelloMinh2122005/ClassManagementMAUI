using Microsoft.EntityFrameworkCore;
using ClassStudentManagement.Data;
using ClassStudentManagement.Models;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;

namespace ClassStudentManagement.Mid
{
    internal class Database
    {
        public Database() { }
        public static async Task<string> PickDatabasePathIfNotExist()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Chọn file cơ sở dữ liệu (.sqlite3)",
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new[] { ".sqlite3" } } 
            })
                });

                if (result != null)
                {
                    return result.FullPath; 
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", $"Không thể chọn file: {ex.Message}", "OK");
            }

            return string.Empty; 
        }

        public static async Task<List<LopHoc>> GetLopHocListAsync()
        {
            using (var context = new ClassDbContext()) 
                return await context.DSLopHoc.ToListAsync();
        }

        public static async Task<List<HocSinh>> GetHocSinhListAsync(string idlop)
        {
            using (var context = new ClassDbContext())
                return await context.DSHocSinh.Where(LopHoc => LopHoc.IDLop == idlop).ToListAsync();
        }
        public static void ThemLopHoc(LopHoc lop)
        {
            using (var context = new ClassDbContext())
            {
                if (context.DSLopHoc.Any(LopHoc => LopHoc.IDLop == lop.IDLop))
                {
                    App.Current.MainPage.DisplayAlert("Thông báo", "Mã lớp đã tồn tại", "OK");
                    return;
                }
                context.DSLopHoc.Add(lop);
                context.SaveChanges();
            }
        }
        public static void ThemHocSinh(HocSinh hocsinh)
        {
            using (var context = new ClassDbContext())
            {
                if (context.DSHocSinh.Any(HocSinh => HocSinh.IDHocSinh == hocsinh.IDHocSinh))
                {
                    App.Current.MainPage.DisplayAlert("Thông báo", "Mã học sinh đã tồn tại", "OK");
                    return;
                }
                context.DSHocSinh.Add(hocsinh);
                context.SaveChanges();
            }
        }
        public static void XoaLopHoc(ObservableCollection<LopHoc> dsChon)
        {
            if (dsChon.Count() == 0)
            {
                App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn lớp để xóa", "OK");
                return;
            }
            foreach (var Lop in dsChon)
            {
                using (var context = new ClassDbContext())
                {
                    context.DSLopHoc.Remove(Lop);
                    context.SaveChanges();
                }
            }
        }
        public static void XoaHocSinh(ObservableCollection<HocSinh> dsChon)
        {
            if (dsChon.Count() == 0)
            {
                App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn học sinh để xóa", "OK");
                return;
            }
            foreach (var hocsinh in dsChon)
            {
                using (var context = new ClassDbContext())
                {
                    context.DSHocSinh.Remove(hocsinh);
                    context.SaveChanges();
                }
            }
        }
        public static void CapNhatLopHoc(LopHoc newlop)
        {
            using (var context = new ClassDbContext())
            {
                var lophoc = context.DSLopHoc.Single(LopHoc => LopHoc.IDLop == newlop.IDLop);
                lophoc.TenLop = newlop.TenLop;
                context.SaveChanges();
            }
        }
        public static void CapNhatHocSinh(HocSinh newhocsinh)
        {
            using (var context = new ClassDbContext())
            {
                var hocsinh = context.DSHocSinh.Single(HocSinh => HocSinh.IDHocSinh == newhocsinh.IDHocSinh);
                hocsinh.TenHocSinh = newhocsinh.TenHocSinh;
                hocsinh.NgSinh = newhocsinh.NgSinh;
                context.SaveChanges();
            }
        }
    }
}