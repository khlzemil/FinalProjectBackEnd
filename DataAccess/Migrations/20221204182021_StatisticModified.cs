using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class StatisticModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorsCount",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "PatientsCount",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Statistics");

            migrationBuilder.AddColumn<string>(
                name: "Count",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Element",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Element",
                table: "Statistics");

            migrationBuilder.AddColumn<int>(
                name: "DoctorsCount",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientsCount",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quality",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
