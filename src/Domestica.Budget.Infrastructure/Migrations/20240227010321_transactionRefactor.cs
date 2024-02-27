using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domestica.Budget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class transactionRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_budget_plans_budget_plan_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_categories_category_id",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_budget_plans_budget_plan_id",
                table: "transactions",
                column: "budget_plan_id",
                principalTable: "budget_plans",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_categories_category_id1",
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
                name: "fk_transactions_budget_plans_budget_plan_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_categories_category_id1",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_budget_plans_budget_plan_id",
                table: "transactions",
                column: "budget_plan_id",
                principalTable: "budget_plans",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_categories_category_id",
                table: "transactions",
                column: "category_id",
                principalTable: "transaction_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
