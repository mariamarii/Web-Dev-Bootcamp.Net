using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class Seeding_Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    { Guid.NewGuid().ToString(), "Admin", "ADMIN", Guid.NewGuid().ToString() },
                    { Guid.NewGuid().ToString(), "ProductCreator", "PRODUCTCREATOR", Guid.NewGuid().ToString() },
                    { Guid.NewGuid().ToString(), "User", "USER", Guid.NewGuid().ToString() }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Name",
                keyValues: new object[] { "Admin", "ProductCreator", "User" });
        }
    }
}
