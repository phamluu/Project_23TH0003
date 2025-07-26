using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLHocPhan_23TH0003.Data.Migrations.QLHocPhan
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaiHoc_LopHocPhan_IdLopHocPhan",
                table: "BaiHoc");

            migrationBuilder.DropForeignKey(
                name: "FK_DangKyHocPhan_LopHocPhan_IdLopHocPhan",
                table: "DangKyHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_DangKyHocPhan_SinhVien_IdSinhVien",
                table: "DangKyHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_Diem_DangKyHocPhan_IdDangKyHocPhan",
                table: "Diem");

            migrationBuilder.DropForeignKey(
                name: "FK_GiangVien_Khoa_IdKhoa",
                table: "GiangVien");

            migrationBuilder.DropForeignKey(
                name: "FK_HocPhan_HocKy_IdHocKy",
                table: "HocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_HocPhan_MonHoc_IdMonHoc",
                table: "HocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_Lop_Khoa_IdKhoa",
                table: "Lop");

            migrationBuilder.DropForeignKey(
                name: "FK_LopHocPhan_HocPhan_IdHocPhan",
                table: "LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_MonHoc_Khoa_IdKhoa",
                table: "MonHoc");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongGiangDay_GiangVien_IdGiangVien",
                table: "PhanCongGiangDay");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongGiangDay_LopHocPhan_IdLopHocPhan",
                table: "PhanCongGiangDay");

            migrationBuilder.DropForeignKey(
                name: "FK_SinhVien_Lop_IdLop",
                table: "SinhVien");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhToanHocPhi_HocKy_IdHocKy",
                table: "ThanhToanHocPhi");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhToanHocPhi_SinhVien_IdSinhVien",
                table: "ThanhToanHocPhi");

            migrationBuilder.AddForeignKey(
                name: "FK_BaiHoc_LopHocPhan_IdLopHocPhan",
                table: "BaiHoc",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHocPhan_LopHocPhan_IdLopHocPhan",
                table: "DangKyHocPhan",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHocPhan_SinhVien_IdSinhVien",
                table: "DangKyHocPhan",
                column: "IdSinhVien",
                principalTable: "SinhVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Diem_DangKyHocPhan_IdDangKyHocPhan",
                table: "Diem",
                column: "IdDangKyHocPhan",
                principalTable: "DangKyHocPhan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GiangVien_Khoa_IdKhoa",
                table: "GiangVien",
                column: "IdKhoa",
                principalTable: "Khoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhan_HocKy_IdHocKy",
                table: "HocPhan",
                column: "IdHocKy",
                principalTable: "HocKy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhan_MonHoc_IdMonHoc",
                table: "HocPhan",
                column: "IdMonHoc",
                principalTable: "MonHoc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lop_Khoa_IdKhoa",
                table: "Lop",
                column: "IdKhoa",
                principalTable: "Khoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocPhan_HocPhan_IdHocPhan",
                table: "LopHocPhan",
                column: "IdHocPhan",
                principalTable: "HocPhan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MonHoc_Khoa_IdKhoa",
                table: "MonHoc",
                column: "IdKhoa",
                principalTable: "Khoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongGiangDay_GiangVien_IdGiangVien",
                table: "PhanCongGiangDay",
                column: "IdGiangVien",
                principalTable: "GiangVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongGiangDay_LopHocPhan_IdLopHocPhan",
                table: "PhanCongGiangDay",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SinhVien_Lop_IdLop",
                table: "SinhVien",
                column: "IdLop",
                principalTable: "Lop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhToanHocPhi_HocKy_IdHocKy",
                table: "ThanhToanHocPhi",
                column: "IdHocKy",
                principalTable: "HocKy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhToanHocPhi_SinhVien_IdSinhVien",
                table: "ThanhToanHocPhi",
                column: "IdSinhVien",
                principalTable: "SinhVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaiHoc_LopHocPhan_IdLopHocPhan",
                table: "BaiHoc");

            migrationBuilder.DropForeignKey(
                name: "FK_DangKyHocPhan_LopHocPhan_IdLopHocPhan",
                table: "DangKyHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_DangKyHocPhan_SinhVien_IdSinhVien",
                table: "DangKyHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_Diem_DangKyHocPhan_IdDangKyHocPhan",
                table: "Diem");

            migrationBuilder.DropForeignKey(
                name: "FK_GiangVien_Khoa_IdKhoa",
                table: "GiangVien");

            migrationBuilder.DropForeignKey(
                name: "FK_HocPhan_HocKy_IdHocKy",
                table: "HocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_HocPhan_MonHoc_IdMonHoc",
                table: "HocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_Lop_Khoa_IdKhoa",
                table: "Lop");

            migrationBuilder.DropForeignKey(
                name: "FK_LopHocPhan_HocPhan_IdHocPhan",
                table: "LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_MonHoc_Khoa_IdKhoa",
                table: "MonHoc");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongGiangDay_GiangVien_IdGiangVien",
                table: "PhanCongGiangDay");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongGiangDay_LopHocPhan_IdLopHocPhan",
                table: "PhanCongGiangDay");

            migrationBuilder.DropForeignKey(
                name: "FK_SinhVien_Lop_IdLop",
                table: "SinhVien");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhToanHocPhi_HocKy_IdHocKy",
                table: "ThanhToanHocPhi");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhToanHocPhi_SinhVien_IdSinhVien",
                table: "ThanhToanHocPhi");

            migrationBuilder.AddForeignKey(
                name: "FK_BaiHoc_LopHocPhan_IdLopHocPhan",
                table: "BaiHoc",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "Id");

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
                name: "FK_Diem_DangKyHocPhan_IdDangKyHocPhan",
                table: "Diem",
                column: "IdDangKyHocPhan",
                principalTable: "DangKyHocPhan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiangVien_Khoa_IdKhoa",
                table: "GiangVien",
                column: "IdKhoa",
                principalTable: "Khoa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhan_HocKy_IdHocKy",
                table: "HocPhan",
                column: "IdHocKy",
                principalTable: "HocKy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhan_MonHoc_IdMonHoc",
                table: "HocPhan",
                column: "IdMonHoc",
                principalTable: "MonHoc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lop_Khoa_IdKhoa",
                table: "Lop",
                column: "IdKhoa",
                principalTable: "Khoa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocPhan_HocPhan_IdHocPhan",
                table: "LopHocPhan",
                column: "IdHocPhan",
                principalTable: "HocPhan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonHoc_Khoa_IdKhoa",
                table: "MonHoc",
                column: "IdKhoa",
                principalTable: "Khoa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongGiangDay_GiangVien_IdGiangVien",
                table: "PhanCongGiangDay",
                column: "IdGiangVien",
                principalTable: "GiangVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongGiangDay_LopHocPhan_IdLopHocPhan",
                table: "PhanCongGiangDay",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SinhVien_Lop_IdLop",
                table: "SinhVien",
                column: "IdLop",
                principalTable: "Lop",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhToanHocPhi_HocKy_IdHocKy",
                table: "ThanhToanHocPhi",
                column: "IdHocKy",
                principalTable: "HocKy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhToanHocPhi_SinhVien_IdSinhVien",
                table: "ThanhToanHocPhi",
                column: "IdSinhVien",
                principalTable: "SinhVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
