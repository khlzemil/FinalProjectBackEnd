using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AboutUsPhotosModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutUsPhotos_AboutUs_AboutUsId",
                table: "AboutUsPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "AboutUsId",
                table: "AboutUsPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUsPhotos_AboutUs_AboutUsId",
                table: "AboutUsPhotos",
                column: "AboutUsId",
                principalTable: "AboutUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutUsPhotos_AboutUs_AboutUsId",
                table: "AboutUsPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "AboutUsId",
                table: "AboutUsPhotos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUsPhotos_AboutUs_AboutUsId",
                table: "AboutUsPhotos",
                column: "AboutUsId",
                principalTable: "AboutUs",
                principalColumn: "Id");
        }
    }
}
