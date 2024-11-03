using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ClassStudentManagement.Models;


namespace ClassStudentManagement.Data
{
    internal class ClassDbContext : DbContext
    {
        public DbSet<LopHoc> LopHoc { get; set; }
        public DbSet<HocSinh> HocSinh { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var DbPath = Preferences.Get("DB_PATH", "");
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HocSinh>().Property(hs => hs.IDHocSinh).HasColumnName("ID");
            modelBuilder.Entity<HocSinh>().Property(hs => hs.TenHocSinh).HasColumnName("NAME");
            modelBuilder.Entity<HocSinh>().Property(hs => hs.NgSinh).HasColumnName("DOB");
            modelBuilder.Entity<HocSinh>().Property(hs => hs.IDLop).HasColumnName("IDLOP");

            modelBuilder.Entity<LopHoc>().Property(lop => lop.IDLop).HasColumnName("ID");
            modelBuilder.Entity<LopHoc>().Property(lop => lop.TenLop).HasColumnName("NAME");
        }
    }
}