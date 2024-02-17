using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domestica.Budget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class subcategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "subcategory_id",
                table: "transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "transaction_subcategories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_subcategories", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_subcategories_transaction_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "transaction_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_transactions_subcategory_id",
                table: "transactions",
                column: "subcategory_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_subcategories_category_id",
                table: "transaction_subcategories",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_subcategory_subcategory_temp_id",
                table: "transactions",
                column: "subcategory_id",
                principalTable: "transaction_subcategories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_subcategory_subcategory_temp_id",
                table: "transactions");

            migrationBuilder.DropTable(
                name: "transaction_subcategories");

            migrationBuilder.DropIndex(
                name: "ix_transactions_subcategory_id",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "subcategory_id",
                table: "transactions");
        }
    }
}
