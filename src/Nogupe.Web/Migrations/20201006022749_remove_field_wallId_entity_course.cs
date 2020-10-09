using Microsoft.EntityFrameworkCore.Migrations;

namespace Nogupe.Web.Migrations
{
    public partial class remove_field_wallId_entity_course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Walls_WallId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_WallId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "WallId",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "f25d07e690df882c5ac2ba43910e8c070120db55", "FMlOJJArrPB5GHatdjzrgSGHXb0JwB/KgebjMkZnhr0=" });

            migrationBuilder.CreateIndex(
                name: "IX_Walls_CourseId",
                table: "Walls",
                column: "CourseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Walls_Courses_CourseId",
                table: "Walls",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walls_Courses_CourseId",
                table: "Walls");

            migrationBuilder.DropIndex(
                name: "IX_Walls_CourseId",
                table: "Walls");

            migrationBuilder.AddColumn<int>(
                name: "WallId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "5f7a86cd10b8f636c70b60d793c624ef3a537851", "ijhseVtfT5Gcote1rUaaVDftji13ZlCb0rnQ7Rqmg0g=" });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_WallId",
                table: "Courses",
                column: "WallId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Walls_WallId",
                table: "Courses",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
