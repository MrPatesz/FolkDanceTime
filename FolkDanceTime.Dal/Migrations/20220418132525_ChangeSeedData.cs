using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FolkDanceTime.Dal.Migrations
{
    public partial class ChangeSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Dancer");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "Admin", "AdminId" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Admin");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "AdminId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "AdminRoleId", "d5742994-15bf-41da-8583-e21a3a611881", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "DancerRoleId", "12a5c121-d9b2-4bd7-bd5c-3085d0c9d27d", "Dancer", "DANCER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00000000-0000-0000-0000-000000000000", 0, "23e5f5bd-09af-48ff-96cf-fcaf567618f8", "admin@folkdancetime.com", true, true, null, "ADMIN@FOLKDANCETIME.COM", "ADMIN@FOLKDANCETIME.COM", "AQAAAAEAACcQAAAAEC22jPI9DWj3rrjWRKDQ6TfmtM9Su2atcYYtq2RL0UJecTTrRc1AbjqgdLgV04oJUQ==", null, false, "70bccd14-42c1-4280-8b4a-332e3e347800", false, "admin@folkdancetime.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "AdminRoleId", "00000000-0000-0000-0000-000000000000" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DancerRoleId");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "AdminRoleId", "00000000-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "Admin", "c8053393-35e8-46f9-8852-54de04f7d9f8", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "Dancer", "f4e2ff97-783a-4207-8435-de8b219ffec5", "Dancer", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "AdminId", 0, "40fdc9ef-5a11-4ea4-ab89-4d75a42c499b", "admin@folkdancetime.com", false, false, null, null, null, "AQAAAAEAACcQAAAAELgY/pgSH0Ry//YMd5IzW8KQTEppmqynU7wMIXoIT/jc5VnG9ya4dH0ObWAPSPXT0A==", null, false, "19b41aee-fc84-43a0-ab00-64af38c374bc", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "Admin", "AdminId" });
        }
    }
}
