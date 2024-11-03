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
    }
}