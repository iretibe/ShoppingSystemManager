using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoppingSystem.Discount.Grpc.Migrations
{
    /// <inheritdoc />
    public partial class DiscountContextMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Amount", "Description", "ProductName" },
                values: new object[,]
                {
                    { 1, 950, "This is a rice product", "Rice K-75 50KG" },
                    { 2, 300, "This is a spaghetti product", "Pasta Aroma Spaghetti" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
