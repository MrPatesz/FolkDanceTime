using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FolkDanceTime.Dal.Migrations
{
    public partial class ChangeSeedDataAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "AdminRoleId", "00000000-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "394c20fb-bec0-4893-abc2-c3ef24013f12");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DancerRoleId",
                column: "ConcurrencyStamp",
                value: "8c77b26d-b10a-4aa8-923e-097186f8d5b0");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "41d51771-a1b8-428f-9e00-6d17a7f5122a", 0, "34a161ce-b191-446f-be55-25ca1b1ae821", "admin@folkdancetime.com", true, true, null, "ADMIN@FOLKDANCETIME.COM", "ADMIN@FOLKDANCETIME.COM", "AQAAAAEAACcQAAAAEFn0Ts0ixRCQxzk9fbFWpA4on2EMculf3WcZKepwVTOFLxFYEPoQa8jqebJEiGYDNg==", null, false, "cc982949-22a1-4df9-a2a5-f8b1cf700d02", false, "admin@folkdancetime.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "AdminRoleId", "41d51771-a1b8-428f-9e00-6d17a7f5122a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "AdminRoleId", "41d51771-a1b8-428f-9e00-6d17a7f5122a" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41d51771-a1b8-428f-9e00-6d17a7f5122a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "d5742994-15bf-41da-8583-e21a3a611881");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DancerRoleId",
                column: "ConcurrencyStamp",
                value: "12a5c121-d9b2-4bd7-bd5c-3085d0c9d27d");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00000000-0000-0000-0000-000000000000", 0, "23e5f5bd-09af-48ff-96cf-fcaf567618f8", "admin@folkdancetime.com", true, true, null, "ADMIN@FOLKDANCETIME.COM", "ADMIN@FOLKDANCETIME.COM", "AQAAAAEAACcQAAAAEC22jPI9DWj3rrjWRKDQ6TfmtM9Su2atcYYtq2RL0UJecTTrRc1AbjqgdLgV04oJUQ==", null, false, "70bccd14-42c1-4280-8b4a-332e3e347800", false, "admin@folkdancetime.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "AdminRoleId", "00000000-0000-0000-0000-000000000000" });
        }
    }
}
