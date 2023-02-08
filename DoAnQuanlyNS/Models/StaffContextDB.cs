using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoAnQuanlyNS.Models
{
    public partial class StaffContextDB : DbContext
    {
        public StaffContextDB()
            : base("name=StaffContextDB")
        {
        }

        public virtual DbSet<BaoHiem> BaoHiems { get; set; }
        public virtual DbSet<ChamCong> ChamCongs { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<Luong> Luongs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }
        public virtual DbSet<ThuongPhat> ThuongPhats { get; set; }
        public virtual DbSet<Userr> Userrs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaoHiem>()
                .Property(e => e.MaBaoHiem)
                .IsUnicode(false);

            modelBuilder.Entity<BaoHiem>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<ChamCong>()
                .Property(e => e.MaChamCong)
                .IsUnicode(false);

            modelBuilder.Entity<ChamCong>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<ChamCong>()
                .HasMany(e => e.Luongs)
                .WithRequired(e => e.ChamCong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChucVu>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<ChucVu>()
                .Property(e => e.PhuCap)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChucVu>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.ChucVu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Luong>()
                .Property(e => e.MaLuong)
                .IsUnicode(false);

            modelBuilder.Entity<Luong>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<Luong>()
                .Property(e => e.MaChamCong)
                .IsUnicode(false);

            modelBuilder.Entity<Luong>()
                .Property(e => e.MaQuyetDinh)
                .IsUnicode(false);

            modelBuilder.Entity<Luong>()
                .Property(e => e.LuongCB)
                .HasPrecision(18, 0);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaPB)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.CMND)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.BaoHiems)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.ChamCongs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.Luongs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.ThuongPhats)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.Userrs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhongBan>()
                .Property(e => e.MaPB)
                .IsUnicode(false);

            modelBuilder.Entity<PhongBan>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.PhongBan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThuongPhat>()
                .Property(e => e.MaQuyetDinh)
                .IsUnicode(false);

            modelBuilder.Entity<ThuongPhat>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<ThuongPhat>()
                .Property(e => e.SoTienThuong)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ThuongPhat>()
                .Property(e => e.SoTienPhat)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ThuongPhat>()
                .HasMany(e => e.Luongs)
                .WithRequired(e => e.ThuongPhat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Userr>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Userr>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<Userr>()
                .Property(e => e.MaNV)
                .IsUnicode(false);
        }
    }
}
