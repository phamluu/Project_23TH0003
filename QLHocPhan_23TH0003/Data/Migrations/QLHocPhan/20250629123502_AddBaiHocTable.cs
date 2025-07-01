using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLHocPhan_23TH0003.Data.Migrations.QLHocPhan
{
    /// <inheritdoc />
    public partial class AddBaiHocTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaiHoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBaiHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaiLieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLopHocPhan = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiHoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaiHoc_LopHocPhan_IdLopHocPhan",
                        column: x => x.IdLopHocPhan,
                        principalTable: "LopHocPhan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaiHoc_IdLopHocPhan",
                table: "BaiHoc",
                column: "IdLopHocPhan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaiHoc");
        }
    }
}
