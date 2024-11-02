using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ClassStudentManagement.Models;

namespace ClassStudentManagement.Data
{
    internal class ClassDbContext : DbContext
    {
        public DbSet<LopHoc> DSLopHoc { get; set; }
        public DbSet<HocSinh> DSHocSinh { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var DbPath = Preferences.Get("DB_PATH", "");
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}