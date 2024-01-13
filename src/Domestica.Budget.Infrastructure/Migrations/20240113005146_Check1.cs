using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domestica.Budget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Check1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_accounts_from_account_id1",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_accounts_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_accounts_to_account_id1",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entity_recipient_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entity_sender_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "ix_transactions_from_account_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "ix_transactions_recipient_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "ix_transactions_sender_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "ix_transactions_to_account_id",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_accounts_account_id",
                table: "transactions",
                column: "account_id",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_accounts_account_id",
                table: "transactions");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_from_account_id",
                table: "transactions",
                column: "from_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_recipient_id",
                table: "transactions",
                column: "recipient_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_sender_id",
                table: "transactions",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_to_account_id",
                table: "transactions",
                column: "to_account_id");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_accounts_from_account_id1",
                table: "transactions",
                column: "from_account_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_accounts_id",
                table: "transactions",
                column: "account_id",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_accounts_to_account_id1",
                table: "transactions",
                column: "to_account_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_entity_recipient_id",
                table: "transactions",
                column: "recipient_id",
                principalTable: "transaction_entities",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_entity_sender_id",
                table: "transactions",
                column: "sender_id",
                principalTable: "transaction_entities",
                principalColumn: "id");
        }
    }
}
