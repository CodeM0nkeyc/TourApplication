using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourApp.Persistence.Migrations
{
    public partial class AddUserIdentityDialCodeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DialCode",
                table: "UserIdentities",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "DialCode",
                table: "UserIdentities");
        }
    }
}
