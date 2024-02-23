using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domestica.Budget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class categoriesrefactor12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_categories_category_id",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_categories_category_id",
                table: "transactions",
                column: "category_id",
                principalTable: "transaction_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_categories_category_id",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_categories_category_id",
                table: "transactions",
                column: "category_id",
                principalTable: "transaction_categories",
                principalColumn: "id");
        }
    }
}
