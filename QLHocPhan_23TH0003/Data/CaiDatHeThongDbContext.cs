using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.Data
{
    public class CaiDatHeThongDbContext : DbContext
    {
        public CaiDatHeThongDbContext(DbContextOptions<CaiDatHeThongDbContext> options) : base(options)
        {
        }
        public DbSet<CauHinhHeThong> CauHinhHeThong { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CauHinhHeThong>(entity =>
            {
                entity.ToTable("CauHinhHeThong");
                entity.HasKey(e => e.Id);
            });
        }
    }
}
