using Microsoft.EntityFrameworkCore.Migrations;

namespace Nogupe.Web.Migrations
{
    public partial class Update_Entity_User_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdressNumber",
                table: "Users",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CellPhone",
                table: "Users",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "Users",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TokenRecovery",
                table: "Users",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "b57074ba7ded9de3e5eb4e561cd81a7364590f9d", "SNEseu2qnzyWu2bEPKaPQMdNvG7SSLBrp09Toneyix8=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AdressNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CellPhone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenRecovery",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "1d68ec1f9a786d9c971c12279c10ed62549841a0", "os5FJ9vHXg2lhvSwaik3U7/qkOaqqYBbmECREYmwy4c=" });
        }
    }
}
