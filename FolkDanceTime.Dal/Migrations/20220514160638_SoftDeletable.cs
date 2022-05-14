using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FolkDanceTime.Dal.Migrations
{
    public partial class SoftDeletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "AdminRoleId", "7e86ed82-cf6a-4e9d-aa69-b54c13d0893b" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e86ed82-cf6a-4e9d-aa69-b54c13d0893b");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ItemSets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "AdminRoleId", "e0209efe-750d-423f-994f-95fcddfd4bbe" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e0209efe-750d-423f-994f-95fcddfd4bbe");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ItemSets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Items");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "4a00037a-ca00-4de7-8f8c-31d5ab5bf4cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DancerRoleId",
                column: "ConcurrencyStamp",
                value: "86c1b978-2a53-47d1-989f-2d2f0ffb7cb6");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7e86ed82-cf6a-4e9d-aa69-b54c13d0893b", 0, "6781c1b6-eab8-4480-848c-22ce5e943935", "admin@folkdancetime.com", true, true, null, "ADMIN@FOLKDANCETIME.COM", "ADMIN@FOLKDANCETIME.COM", "AQAAAAEAACcQAAAAEDF9p2WiWJOPB3scn34evqa9beR9sgQl3/7eM122xUveKExbnBN/YFo+o9zd/565cA==", null, false, "7e04cbc5-3221-489b-a511-ebc14cc5fa4f", false, "admin@folkdancetime.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "7e86ed82-cf6a-4e9d-aa69-b54c13d0893b");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "AdminRoleId", "7e86ed82-cf6a-4e9d-aa69-b54c13d0893b" });
        }
    }
}
