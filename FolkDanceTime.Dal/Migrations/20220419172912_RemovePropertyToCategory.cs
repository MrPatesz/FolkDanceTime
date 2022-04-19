using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FolkDanceTime.Dal.Migrations
{
    public partial class RemovePropertyToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_OwnerUserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSetTransactions_ItemsSets_ItemSetId",
                table: "ItemSetTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTransactions_Items_ItemId",
                table: "ItemTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_Items_ItemId",
                table: "PropertyValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_Properties_PropertyId",
                table: "PropertyValues");

            migrationBuilder.DropTable(
                name: "PropertyToCategories");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "AdminRoleId", "41d51771-a1b8-428f-9e00-6d17a7f5122a" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41d51771-a1b8-428f-9e00-6d17a7f5122a");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "aae82bcb-467e-4cb5-8e84-d54b12297b11");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DancerRoleId",
                column: "ConcurrencyStamp",
                value: "6af44782-7781-4b4f-8347-972c0c68520f");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "df4a5778-1888-409b-a3eb-9426426e8d57", 0, "aa2f72ed-0fa9-4905-8031-3c554f32d29c", "admin@folkdancetime.com", true, true, null, "ADMIN@FOLKDANCETIME.COM", "ADMIN@FOLKDANCETIME.COM", "AQAAAAEAACcQAAAAEMq6Qyk8HReJj/jThT2Gzun3s8zYD/2Qb9JQsPoDg5OctyTu8dlNsOxlXaBaqNl+7Q==", null, false, "169f8bc3-de82-4a6b-a349-2099cf602241", false, "admin@folkdancetime.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "AdminRoleId", "df4a5778-1888-409b-a3eb-9426426e8d57" });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_OwnerUserId",
                table: "Items",
                column: "OwnerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSetTransactions_ItemsSets_ItemSetId",
                table: "ItemSetTransactions",
                column: "ItemSetId",
                principalTable: "ItemsSets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTransactions_Items_ItemId",
                table: "ItemTransactions",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_Items_ItemId",
                table: "PropertyValues",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_Properties_PropertyId",
                table: "PropertyValues",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_OwnerUserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSetTransactions_ItemsSets_ItemSetId",
                table: "ItemSetTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTransactions_Items_ItemId",
                table: "ItemTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_Items_ItemId",
                table: "PropertyValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_Properties_PropertyId",
                table: "PropertyValues");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "AdminRoleId", "df4a5778-1888-409b-a3eb-9426426e8d57" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df4a5778-1888-409b-a3eb-9426426e8d57");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Properties");

            migrationBuilder.CreateTable(
                name: "PropertyToCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyToCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyToCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyToCategories_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_PropertyToCategories_CategoryId",
                table: "PropertyToCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyToCategories_PropertyId",
                table: "PropertyToCategories",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_OwnerUserId",
                table: "Items",
                column: "OwnerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSetTransactions_ItemsSets_ItemSetId",
                table: "ItemSetTransactions",
                column: "ItemSetId",
                principalTable: "ItemsSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTransactions_Items_ItemId",
                table: "ItemTransactions",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_Items_ItemId",
                table: "PropertyValues",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_Properties_PropertyId",
                table: "PropertyValues",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
