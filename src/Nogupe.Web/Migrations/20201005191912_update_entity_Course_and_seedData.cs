using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nogupe.Web.Migrations
{
    public partial class update_entity_Course_and_seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Matters",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50) CHARACTER SET utf8mb4",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Careers",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50) CHARACTER SET utf8mb4",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "5f7a86cd10b8f636c70b60d793c624ef3a537851", "ijhseVtfT5Gcote1rUaaVDftji13ZlCb0rnQ7Rqmg0g=" });

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Lunes 08hs-12hs");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Lunes 18hs-22hs");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Martes 08hs-12hs");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Martes 18hs-22hs");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Miércoles 08hs-12hs");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Miércoles 18hs-22hs");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Jueves 08hs-12hs");

            migrationBuilder.InsertData(
                table: "Weekdays",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 8, "Jueves 18hs-22hs" },
                    { 9, "Viernes 08hs-12hs" },
                    { 10, "Viernes 18hs-22hs" },
                    { 11, "Sábado 08hs-12hs" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Matters",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Courses",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Courses",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Careers",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "8ef51c2f8f21422aa55a17ea16be2772ac029623", "BscJeGxPM9gj+kTpdsodpItwqhMZdRDJaFyheqyoBzA=" });

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Lunes");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Martes");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Miercoles");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Jueves");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Viernes");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Sabado");

            migrationBuilder.UpdateData(
                table: "Weekdays",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Domingo");
        }
    }
}
