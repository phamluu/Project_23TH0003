using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLHocPhan_23TH0003.Data.Migrations.QLHocPhan
{
    /// <inheritdoc />
    public partial class CreateQLHocPhanSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HocKy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocKy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenHocKy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThuTu = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocKy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DangKyHocPhan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSinhVien = table.Column<int>(type: "int", nullable: false),
                    IdLopHocPhan = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKyHocPhan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiemChuyenCan = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiemGiuaKy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiemThucHanh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiemCuoiKy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IdDangKyHocPhan = table.Column<int>(type: "int", nullable: false),
                    DiemTrungBinh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    XepLoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diem_DangKyHocPhan_IdDangKyHocPhan",
                        column: x => x.IdDangKyHocPhan,
                        principalTable: "DangKyHocPhan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiangVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGiangVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdKhoa = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangVien", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenKhoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTruongKhoa = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Khoa_GiangVien_IdTruongKhoa",
                        column: x => x.IdTruongKhoa,
                        principalTable: "GiangVien",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenLop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdKhoa = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lop_Khoa_IdKhoa",
                        column: x => x.IdKhoa,
                        principalTable: "Khoa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MonHoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaMonHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenMonHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdKhoa = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonHoc_Khoa_IdKhoa",
                        column: x => x.IdKhoa,
                        principalTable: "Khoa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSinhVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdLop = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinhVien_Lop_IdLop",
                        column: x => x.IdLop,
                        principalTable: "Lop",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HocPhan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocPhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenHocPhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdHocKy = table.Column<int>(type: "int", nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: true),
                    IdMonHoc = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HocPhan_HocKy_IdHocKy",
                        column: x => x.IdHocKy,
                        principalTable: "HocKy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HocPhan_MonHoc_IdMonHoc",
                        column: x => x.IdMonHoc,
                        principalTable: "MonHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LopHocPhan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLopHocPhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenLopHocPhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiSoToiDa = table.Column<int>(type: "int", nullable: true),
                    ThoiGianHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaDiemHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeSoChuyenCan = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HeSoGiuaKy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HeSoThucHanh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HeSoCuoiKy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IdHocPhan = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocPhan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LopHocPhan_HocPhan_IdHocPhan",
                        column: x => x.IdHocPhan,
                        principalTable: "HocPhan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhanCongGiangDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLopHocPhan = table.Column<int>(type: "int", nullable: false),
                    IdGiangVien = table.Column<int>(type: "int", nullable: false),
                    NgayPhanCong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VaiTro = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCongGiangDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhanCongGiangDay_GiangVien_IdGiangVien",
                        column: x => x.IdGiangVien,
                        principalTable: "GiangVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanCongGiangDay_LopHocPhan_IdLopHocPhan",
                        column: x => x.IdLopHocPhan,
                        principalTable: "LopHocPhan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DangKyHocPhan_IdLopHocPhan",
                table: "DangKyHocPhan",
                column: "IdLopHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyHocPhan_IdSinhVien",
                table: "DangKyHocPhan",
                column: "IdSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_Diem_IdDangKyHocPhan",
                table: "Diem",
                column: "IdDangKyHocPhan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiangVien_IdKhoa",
                table: "GiangVien",
                column: "IdKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_HocPhan_IdHocKy",
                table: "HocPhan",
                column: "IdHocKy");

            migrationBuilder.CreateIndex(
                name: "IX_HocPhan_IdMonHoc",
                table: "HocPhan",
                column: "IdMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_Khoa_IdTruongKhoa",
                table: "Khoa",
                column: "IdTruongKhoa",
                unique: true,
                filter: "[IdTruongKhoa] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Lop_IdKhoa",
                table: "Lop",
                column: "IdKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhan_IdHocPhan",
                table: "LopHocPhan",
                column: "IdHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_MonHoc_IdKhoa",
                table: "MonHoc",
                column: "IdKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongGiangDay_IdGiangVien",
                table: "PhanCongGiangDay",
                column: "IdGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongGiangDay_IdLopHocPhan",
                table: "PhanCongGiangDay",
                column: "IdLopHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_IdLop",
                table: "SinhVien",
                column: "IdLop");

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHocPhan_LopHocPhan_IdLopHocPhan",
                table: "DangKyHocPhan",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHocPhan_SinhVien_IdSinhVien",
                table: "DangKyHocPhan",
                column: "IdSinhVien",
                principalTable: "SinhVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiangVien_Khoa_IdKhoa",
                table: "GiangVien",
                column: "IdKhoa",
                principalTable: "Khoa",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiangVien_Khoa_IdKhoa",
                table: "GiangVien");

            migrationBuilder.DropTable(
                name: "Diem");

            migrationBuilder.DropTable(
                name: "PhanCongGiangDay");

            migrationBuilder.DropTable(
                name: "DangKyHocPhan");

            migrationBuilder.DropTable(
                name: "LopHocPhan");

            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "HocPhan");

            migrationBuilder.DropTable(
                name: "Lop");

            migrationBuilder.DropTable(
                name: "HocKy");

            migrationBuilder.DropTable(
                name: "MonHoc");

            migrationBuilder.DropTable(
                name: "Khoa");

            migrationBuilder.DropTable(
                name: "GiangVien");
        }
    }
}
