using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FolkDanceTime.Dal.Migrations
{
    public partial class CategoryNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "AdminRoleId", "e0209efe-750d-423f-994f-95fcddfd4bbe" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e0209efe-750d-423f-994f-95fcddfd4bbe");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "9df4f790-0e25-4919-a245-8319a7b5d419");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DancerRoleId",
                column: "ConcurrencyStamp",
                value: "821fcdca-c050-4f9d-85c1-f9f0b954dd5b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2b39e28f-e306-4704-95a5-b4e048dda5ec", 0, "290c5cde-b8fe-4257-b1d5-2bc659eb45cd", "admin@folkdancetime.com", true, true, null, "ADMIN@FOLKDANCETIME.COM", "ADMIN@FOLKDANCETIME.COM", "AQAAAAEAACcQAAAAEKCKHMkyxQsnosMFof4Ps48b30uN2YR7q+2gh0PdS0jUsdTfjxR/Xru2NranedneAw==", null, false, "01cdb74f-0c9d-452c-abaa-2c13a3bccc40", false, "admin@folkdancetime.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "2b39e28f-e306-4704-95a5-b4e048dda5ec");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "AdminRoleId", "2b39e28f-e306-4704-95a5-b4e048dda5ec" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "AdminRoleId", "2b39e28f-e306-4704-95a5-b4e048dda5ec" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b39e28f-e306-4704-95a5-b4e048dda5ec");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "93a99e42-a568-47f4-889e-875f741a11f1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DancerRoleId",
                column: "ConcurrencyStamp",
                value: "fa493f62-cdfc-4d8f-9a87-9edc2a807e21");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e0209efe-750d-423f-994f-95fcddfd4bbe", 0, "e749fe2a-6ba2-4239-a727-ba80aacf911d", "admin@folkdancetime.com", true, true, null, "ADMIN@FOLKDANCETIME.COM", "ADMIN@FOLKDANCETIME.COM", "AQAAAAEAACcQAAAAENmMl1BqDYxQc0vUSp8GMndXsNeVPv7Z1hQ3d5fZD+E9d6HS14iE9OHW921P0S5O9Q==", null, false, "4b70cfbd-da2f-4386-bd6a-dbf733a33625", false, "admin@folkdancetime.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "e0209efe-750d-423f-994f-95fcddfd4bbe");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "AdminRoleId", "e0209efe-750d-423f-994f-95fcddfd4bbe" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
