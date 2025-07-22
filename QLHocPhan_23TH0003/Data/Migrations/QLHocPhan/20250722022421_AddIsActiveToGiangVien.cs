using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLHocPhan_23TH0003.Data.Migrations.QLHocPhan
{
    /// <inheritdoc />
    public partial class AddIsActiveToGiangVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "GiangVien",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "GiangVien");
        }
    }
}
