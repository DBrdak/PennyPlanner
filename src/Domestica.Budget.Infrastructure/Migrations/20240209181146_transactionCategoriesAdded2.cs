using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domestica.Budget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class transactionCategoriesAdded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_categories_category_id1",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_categories_category_id",
                table: "transactions",
                column: "category_id",
                principalTable: "transaction_categories",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_categories_category_id",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_categories_category_id1",
                table: "transactions",
                column: "category_id",
                principalTable: "transaction_categories",
                principalColumn: "id");
        }
    }
}
