using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class IconNameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Signature",
                table: "AboutUs",
                newName: "IconName");

            migrationBuilder.AddColumn<int>(
                name: "AboutUsId",
                table: "AboutUsPhotos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AboutUsPhotos_AboutUsId",
                table: "AboutUsPhotos",
                column: "AboutUsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUsPhotos_AboutUs_AboutUsId",
                table: "AboutUsPhotos",
                column: "AboutUsId",
                principalTable: "AboutUs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutUsPhotos_AboutUs_AboutUsId",
                table: "AboutUsPhotos");

            migrationBuilder.DropIndex(
                name: "IX_AboutUsPhotos_AboutUsId",
                table: "AboutUsPhotos");

            migrationBuilder.DropColumn(
                name: "AboutUsId",
                table: "AboutUsPhotos");

            migrationBuilder.RenameColumn(
                name: "IconName",
                table: "AboutUs",
                newName: "Signature");
        }
    }
}
