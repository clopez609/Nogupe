using Microsoft.EntityFrameworkCore.Migrations;

namespace Nogupe.Web.Migrations
{
    public partial class add_field_username_entity_comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comments",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "1d68ec1f9a786d9c971c12279c10ed62549841a0", "os5FJ9vHXg2lhvSwaik3U7/qkOaqqYBbmECREYmwy4c=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "619a10aa6057582cb4bb97beed7abb55868bdc58", "sjQi30l8482mrl0qfEv3ia//atF4tQLL2YY4496Uy1w=" });
        }
    }
}
