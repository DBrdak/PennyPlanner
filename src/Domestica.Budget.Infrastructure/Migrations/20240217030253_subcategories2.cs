using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domestica.Budget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class subcategories2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transaction_subcategories_transaction_categories_category_id",
                table: "transaction_subcategories");

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_subcategories_transaction_categories_category_id",
                table: "transaction_subcategories",
                column: "category_id",
                principalTable: "transaction_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transaction_subcategories_transaction_categories_category_id",
                table: "transaction_subcategories");

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_subcategories_transaction_categories_category_id",
                table: "transaction_subcategories",
                column: "category_id",
                principalTable: "transaction_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
