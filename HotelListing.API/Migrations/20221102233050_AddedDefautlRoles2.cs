using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.API.Migrations
{
    public partial class AddedDefautlRoles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60e8bcb8-90c7-43e6-9c96-53afccdf6988");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3df0852-2d3b-4764-9b93-bdafb2d100d9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2061bab3-846e-46a0-ad4f-5d8994958da4", "2631ab98-5e95-4e7f-ac32-db92e517df1d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "24e8e1dc-d1f0-48bc-95bf-7857d0b2e1d1", "73816076-0bc6-49af-b857-e40b58010fb7", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2061bab3-846e-46a0-ad4f-5d8994958da4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24e8e1dc-d1f0-48bc-95bf-7857d0b2e1d1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60e8bcb8-90c7-43e6-9c96-53afccdf6988", "f403e94f-7cce-47db-85f7-2cdf573a893d", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3df0852-2d3b-4764-9b93-bdafb2d100d9", "3e42126c-992f-4eec-b90e-9ce2a77d0f89", "Administrator", "ADMINISTRATOR" });
        }
    }
}
