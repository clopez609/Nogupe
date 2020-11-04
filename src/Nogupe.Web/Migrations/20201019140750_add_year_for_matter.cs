using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nogupe.Web.Migrations
{
    public partial class add_year_for_matter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Matters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Careers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tecnicatura Universitaria en Desarrollo de Software" },
                    { 11, "Tecnicatura Universitaria en Comercio Internacional y Despacho Aduana" },
                    { 9, "Tecnicatura Universitaria en Guía de Turismo" },
                    { 8, "Tecnicatura Universitaria en Logística" },
                    { 7, "Tecnicatura Universitaria en Higiene y Seguridad" },
                    { 10, "Tecnicatura Universitaria en Hotelería y Turismo" },
                    { 5, "Licenciatura en Comercio Internacional" },
                    { 4, "Licenciatura en Gestión Aeroportuaria" },
                    { 3, "Licenciatura en Logística" },
                    { 2, "Licenciatura en Higiene y Seguridad" },
                    { 6, "Licenciatura en Turismo" }
                });

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Profesor");

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Alumno");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "cdae6aa1a67ec99a75be54ea610bbf7786a476a3", "zkc1peuZceoX1kBsKGuA9Zdngf36j9q3Y88oaEWmF9I=" });

            migrationBuilder.InsertData(
                table: "Years",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Primer Año" },
                    { 2, "Segundo Año" },
                    { 3, "Tercer Año" },
                    { 4, "Cuarto Año" },
                    { 5, "Quinto Año" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matters_YearId",
                table: "Matters",
                column: "YearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matters_Years_YearId",
                table: "Matters",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matters_Years_YearId",
                table: "Matters");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.DropIndex(
                name: "IX_Matters_YearId",
                table: "Matters");

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Matters");

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "profesor");

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "alumno");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "babb5681275ccbf4d7423c712e9b4fe3d861d6fe", "3NHCv8Ptx5840Xk8TH0PTN8fttnoL+TXpDOfVinAC58=" });
        }
    }
}
