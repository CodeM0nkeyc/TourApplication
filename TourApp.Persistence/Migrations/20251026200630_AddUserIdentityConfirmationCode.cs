using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourApp.Persistence.Migrations
{
    public partial class AddUserIdentityConfirmationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Users",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.DropForeignKey("FK_Users_AppUserRoles_RoleId", "Users");
            migrationBuilder.DropPrimaryKey("PK_AppUserRoles", "AppUserRoles");
            migrationBuilder.DropColumn("Id", "AppUserRoles");
            migrationBuilder.AddColumn<int>(name: "Id", table: "AppUserRoles", type: "int", nullable: false);
            migrationBuilder.AddPrimaryKey("PK_AppUserRoles", "AppUserRoles", "Id");
            migrationBuilder.AddForeignKey("FK_Users_AppUserRoles_RoleId", "Users",
                column: "RoleId",
                principalTable: "AppUserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddColumn<int>(
                name: "ConfirmationCode_Code",
                table: "AppUserIdentities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmationCode_ExpireAt",
                table: "AppUserIdentities",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "AppUserIdentities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationCode_Code",
                table: "AppUserIdentities");

            migrationBuilder.DropColumn(
                name: "ConfirmationCode_ExpireAt",
                table: "AppUserIdentities");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "AppUserIdentities");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Users",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AppUserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
