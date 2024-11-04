using Microsoft.EntityFrameworkCore;
using ClassStudentManagement.Data;
using ClassStudentManagement.Models;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ClassStudentManagement.Mid
{
    internal class Database
    {
        private static Lazy<ClassDbContext> _context = new Lazy<ClassDbContext>(() => new ClassDbContext());
        private static ClassDbContext Context => _context.IsValueCreated ? _context.Value : new ClassDbContext();
        public static async Task pickDatabasePathIfNotExist()
        {
            if (!string.IsNullOrEmpty(Preferences.Get("DB_PATH", ""))) return;

            var fileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new[] { ".db", ".sqlite", ".sqlite3" } }
            });

            var dbFile = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Chọn file cơ sở dữ liệu SQLite",
                FileTypes = fileType
            });

            while (dbFile == null)
            {
                if (App.Current?.MainPage == null) return;
                await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn file cơ sở dữ liệu SQLite", "OK");

                dbFile = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Chọn file cơ sở dữ liệu SQLite",
                    FileTypes = fileType
                });
            }

            Preferences.Set("DB_PATH", dbFile.FullPath);
        }

        public static async Task<List<LopHoc>> GetLopHocListAsync()
        {
            using var context = Context;
            return await context.LopHoc
                .Select(l => new LopHoc
                {
                    IDLop = l.IDLop,
                    TenLop = l.TenLop ?? string.Empty 
                })
                .ToListAsync();
        }

        public static async Task<List<HocSinh>> GetHocSinhListAsync(string idlop)
        {
            using var context = Context;
            return await context.HocSinh.Where(LopHoc => LopHoc.IDLop == idlop).ToListAsync();
        }
        public static void ThemLopHoc(LopHoc lop)
        {
            using var context = Context;
            if (context.LopHoc.Any(LopHoc => LopHoc.IDLop == lop.IDLop))
            {
                if (App.Current != null && App.Current.MainPage != null)
                    App.Current.MainPage.DisplayAlert("Thông báo", "Mã lớp đã tồn tại", "OK");
                return;
            }
            context.LopHoc.Add(lop);
            context.SaveChanges();
        }
        public static void ThemHocSinh(HocSinh hocsinh)
        {
            using var context = Context;
            if (context.HocSinh.Any(HocSinh => HocSinh.IDHocSinh == hocsinh.IDHocSinh))
            {
                if (App.Current != null && App.Current.MainPage != null)
                    App.Current.MainPage.DisplayAlert("Thông báo", "Mã học sinh đã tồn tại", "OK");
                return;
            }
            context.HocSinh.Add(hocsinh);
            context.SaveChanges();
        }
        public static void XoaLopHoc(LopHoc lopChon)
        {
            using var context = Context;
            if (lopChon == null)
            {
                if (App.Current != null && App.Current.MainPage != null)
                    App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn lớp học để xóa", "OK");
                return;
            }
            context.LopHoc.Remove(lopChon);
            context.SaveChanges();
        }
        public static void XoaHocSinh(HocSinh hsChon)
        {
            using var context = Context;
            if (hsChon == null)
            {
                if (App.Current != null && App.Current.MainPage != null)
                    App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn học sinh để xóa", "OK");
                return;
            }
            context.HocSinh.Remove(hsChon);
            context.SaveChanges();
        }
        public static void CapNhatLopHoc(LopHoc newlop)
        {
            using var context = Context;
            var lophoc = context.LopHoc.SingleOrDefault(LopHoc => LopHoc.IDLop == newlop.IDLop);

            if (lophoc == null)
            {
                if (App.Current != null && App.Current.MainPage != null)
                    App.Current.MainPage.DisplayAlert("Thông báo", "Lớp học không tồn tại", "OK");
                return;
            }

            if (newlop.TenLop != null)
            {
                lophoc.TenLop = newlop.TenLop;
            }

            context.SaveChanges();
        }

        public static void CapNhatHocSinh(HocSinh newhocsinh)
        {
            using var context = Context;
            var hocsinh = context.HocSinh.SingleOrDefault(HocSinh => HocSinh.IDHocSinh == newhocsinh.IDHocSinh);

            if (hocsinh == null)
            {
                if (App.Current != null && App.Current.MainPage != null)
                    App.Current.MainPage.DisplayAlert("Thông báo", "Học sinh không tồn tại", "OK");
                return;
            }

            if (newhocsinh.TenHocSinh != null)
            {
                hocsinh.TenHocSinh = newhocsinh.TenHocSinh;
            }

            if (newhocsinh.NgSinh != null)
            {
                hocsinh.NgSinh = newhocsinh.NgSinh;
            }

            context.SaveChanges();
        }

    }
}