using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListing.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23b39120-a3a3-44d8-8bad-e1db7d97a232", "2918898f-af65-4173-a7ba-813ebc89a83a", "User", "USER" },
                    { "573fafc5-2107-43f7-888e-3a94d6de181e", "6ca3796f-3f39-4454-ad53-7290eaceb244", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23b39120-a3a3-44d8-8bad-e1db7d97a232");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "573fafc5-2107-43f7-888e-3a94d6de181e");
        }
    }
}
