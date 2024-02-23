using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domestica.Budget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class transactionCategoriesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "category",
                table: "budgeted_transaction_category");

            migrationBuilder.AddColumn<Guid>(
                name: "category_id",
                table: "transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "category_id",
                table: "budgeted_transaction_category",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "transaction_categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    transaction_category_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_categories", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_transactions_category_id",
                table: "transactions",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_budgeted_transaction_category_category_id",
                table: "budgeted_transaction_category",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_budgeted_transaction_category_transaction_category_category_t",
                table: "budgeted_transaction_category",
                column: "category_id",
                principalTable: "transaction_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_categories_category_id1",
                table: "transactions",
                column: "category_id",
                principalTable: "transaction_categories",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_budgeted_transaction_category_transaction_category_category_t",
                table: "budgeted_transaction_category");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_categories_category_id1",
                table: "transactions");

            migrationBuilder.DropTable(
                name: "transaction_categories");

            migrationBuilder.DropIndex(
                name: "ix_transactions_category_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "ix_budgeted_transaction_category_category_id",
                table: "budgeted_transaction_category");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "budgeted_transaction_category");

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "budgeted_transaction_category",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
