using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.Data
{
    public class MainDbContext: IdentityDbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
        public DbSet<HocKy> HocKy { get; set; }
        public DbSet<Khoa> Khoa { get; set; }
        public DbSet<Lop> Lop { get; set; }
        public DbSet<MonHoc> MonHoc { get; set; }
        public DbSet<SinhVien> SinhVien { get; set; }
        public DbSet<GiangVien> GiangVien { get; set; }
        public DbSet<HocPhan> HocPhan { get; set; }
        public DbSet<Diem> Diem { get; set; }
        public DbSet<DangKyHocPhan> DangKyHocPhan { get; set; }
        public DbSet<PhanCongGiangDay> PhanCongGiangDay { get; set; }
        public DbSet<LopHocPhan> LopHocPhan { get; set; }
        public DbSet<BaiHoc> BaiHoc { get; set; }
        public DbSet<CauHinhHeThong> CauHinhHeThong { get; set; }
        public DbSet<ThanhToanHocPhi> ThanhToanHocPhi { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HocKy>(entity =>
            {
                entity.ToTable("HocKy");
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.HocPhans).WithOne(e => e.HocKy).HasForeignKey(e => e.IdHocKy);
            });
            modelBuilder.Entity<Khoa>(entity =>
            {
                entity.ToTable("Khoa");
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Lops).WithOne(e => e.Khoa).HasForeignKey(e => e.IdKhoa);
                entity.HasMany(e => e.GiangViens).WithOne(e => e.Khoa).HasForeignKey(e => e.IdKhoa);
                entity.HasMany(e => e.MonHocs).WithOne(e => e.Khoa).HasForeignKey(e => e.IdKhoa);
                entity.HasOne(e => e.TruongKhoa).WithOne() // không cần navigation ngược
                                                .HasForeignKey<Khoa>(e => e.IdTruongKhoa);
            });
            modelBuilder.Entity<GiangVien>(entity =>
            {
                entity.ToTable("GiangVien");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Khoa).WithMany(e => e.GiangViens).HasForeignKey(e => e.IdKhoa).OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.PhanCongGiangDays).WithOne(e => e.GiangVien).HasForeignKey(e => e.IdGiangVien);
            });

            modelBuilder.Entity<Lop>(entity =>
            {
                entity.ToTable("Lop");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Khoa).WithMany(e => e.Lops).HasForeignKey(e => e.IdKhoa);
                entity.HasMany(e => e.SinhViens).WithOne(e => e.Lop).HasForeignKey(e => e.IdLop);
            });
            modelBuilder.Entity<MonHoc>(entity =>
            {
                entity.ToTable("MonHoc");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Khoa).WithMany(e => e.MonHocs).HasForeignKey(e => e.IdKhoa);
                entity.HasMany(e => e.HocPhans).WithOne(e => e.MonHoc).HasForeignKey(e => e.IdMonHoc);
            });
            modelBuilder.Entity<HocPhan>(entity =>
            {
                entity.ToTable("HocPhan");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.MonHoc).WithMany(e => e.HocPhans).HasForeignKey(e => e.IdMonHoc).OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.LopHocPhans).WithOne(e => e.HocPhan).HasForeignKey(e => e.IdHocPhan);
                entity.HasOne(e => e.HocKy).WithMany(e => e.HocPhans).HasForeignKey(e => e.IdHocKy).OnDelete(DeleteBehavior.Restrict);

            });
            modelBuilder.Entity<LopHocPhan>(entity =>
            {
                entity.ToTable("LopHocPhan");
                entity.HasKey(e => e.Id);
                entity.Property(l => l.HeSoChuyenCan).HasColumnType("decimal(18,2)");
                entity.Property(l => l.HeSoGiuaKy).HasColumnType("decimal(18,2)");
                entity.Property(l => l.HeSoThucHanh).HasColumnType("decimal(18,2)");
                entity.Property(l => l.HeSoCuoiKy).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.HocPhan).WithMany(e => e.LopHocPhans).HasForeignKey(e => e.IdHocPhan).OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.DangKyHocPhans).WithOne(e => e.LopHocPhan).HasForeignKey(e => e.IdLopHocPhan);
                entity.HasMany(e => e.PhanCongGiangDays).WithOne(e => e.LopHocPhan).HasForeignKey(e => e.IdLopHocPhan);
            });
            modelBuilder.Entity<DangKyHocPhan>(entity =>
            {
                entity.ToTable("DangKyHocPhan");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.SinhVien).WithMany(e => e.DangKyHocPhans).HasForeignKey(e => e.IdSinhVien).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.LopHocPhan).WithMany(e => e.DangKyHocPhans).HasForeignKey(e => e.IdLopHocPhan).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<PhanCongGiangDay>(entity =>
            {
                entity.ToTable("PhanCongGiangDay");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.GiangVien).WithMany(e => e.PhanCongGiangDays).HasForeignKey(e => e.IdGiangVien).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.LopHocPhan).WithMany(e => e.PhanCongGiangDays).HasForeignKey(e => e.IdLopHocPhan).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Diem>(entity =>
            {
                entity.ToTable("Diem");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.DangKyHocPhan).WithOne(e => e.Diem).HasForeignKey<Diem>(e => e.IdDangKyHocPhan);
                entity.Property(d => d.DiemChuyenCan).HasColumnType("decimal(18,2)");
                entity.Property(d => d.DiemGiuaKy).HasColumnType("decimal(18,2)");
                entity.Property(d => d.DiemThucHanh).HasColumnType("decimal(18,2)");
                entity.Property(d => d.DiemCuoiKy).HasColumnType("decimal(18,2)");
                entity.Property(d => d.DiemTrungBinh).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.ToTable("SinhVien");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Lop).WithMany(e => e.SinhViens).HasForeignKey(e => e.IdLop).OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.DangKyHocPhans).WithOne(e => e.SinhVien).HasForeignKey(e => e.IdSinhVien);
                entity.HasMany(e => e.ThanhToanHocPhis).WithOne(e => e.SinhVien).HasForeignKey(e => e.IdSinhVien);
            });
            modelBuilder.Entity<ThanhToanHocPhi>(entity =>
            {
                entity.ToTable("ThanhToanHocPhi");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.SinhVien).WithMany(e => e.ThanhToanHocPhis).HasForeignKey(e => e.IdSinhVien).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.HocKy).WithMany(e => e.ThanhToanHocPhis).HasForeignKey(e => e.IdHocKy).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<BaiHoc>(entity =>
            {
                entity.ToTable("BaiHoc");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.LopHocPhan).WithMany(e => e.BaiHocs).HasForeignKey(e => e.IdLopHocPhan).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<CauHinhHeThong>(entity =>
            {
                entity.ToTable("CauHinhHeThong");
                entity.HasKey(e => e.Id);
            });
        }
    }
}
