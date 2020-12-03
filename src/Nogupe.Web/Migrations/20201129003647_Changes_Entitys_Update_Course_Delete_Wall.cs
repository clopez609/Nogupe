using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nogupe.Web.Migrations
{
    public partial class Changes_Entitys_Update_Course_Delete_Wall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Files_FileId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Walls_WallId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Walls");

            migrationBuilder.DropIndex(
                name: "IX_Comments_FileId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_WallId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "WallId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "UIdFileName",
                table: "Files",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(260) CHARACTER SET utf8mb4",
                oldMaxLength: 260);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Files",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Commentary",
                table: "Comments",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Today",
                table: "Assistances",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "1f929befd4fb5f4c131174552f578eeed0d49521", "MsY/PxAzzyyfGCYAvuxkuYoOHkbcoHLJTTJYeWcBPVM=" });

            migrationBuilder.CreateIndex(
                name: "IX_Files_CourseId",
                table: "Files",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UIdFileName",
                table: "Files",
                column: "UIdFileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CourseId",
                table: "Comments",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Courses_CourseId",
                table: "Comments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Courses_CourseId",
                table: "Files",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Courses_CourseId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Courses_CourseId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_CourseId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_UIdFileName",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CourseId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Today",
                table: "Assistances");

            migrationBuilder.AlterColumn<string>(
                name: "UIdFileName",
                table: "Files",
                type: "varchar(260) CHARACTER SET utf8mb4",
                maxLength: 260,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Commentary",
                table: "Comments",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comments",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WallId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Walls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Walls_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    WallId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Walls_WallId",
                        column: x => x.WallId,
                        principalTable: "Walls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "b57074ba7ded9de3e5eb4e561cd81a7364590f9d", "SNEseu2qnzyWu2bEPKaPQMdNvG7SSLBrp09Toneyix8=" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FileId",
                table: "Comments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_WallId",
                table: "Comments",
                column: "WallId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_FileId",
                table: "Documents",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_WallId",
                table: "Documents",
                column: "WallId");

            migrationBuilder.CreateIndex(
                name: "IX_Walls_CourseId",
                table: "Walls",
                column: "CourseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Files_FileId",
                table: "Comments",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Walls_WallId",
                table: "Comments",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
