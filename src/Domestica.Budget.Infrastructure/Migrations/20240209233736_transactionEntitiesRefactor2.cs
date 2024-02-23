using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domestica.Budget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class transactionEntitiesRefactor2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entities_recipient_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entities_sender_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entities_transaction_entity_id1",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entities_transaction_entity_id2",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "ix_transactions_transaction_entity_id1",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "ix_transactions_transaction_entity_id2",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "transaction_entity_id1",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "transaction_entity_id2",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_entities_transaction_entity_id1",
                table: "transactions",
                column: "recipient_id",
                principalTable: "transaction_entities",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_entities_transaction_entity_id11",
                table: "transactions",
                column: "sender_id",
                principalTable: "transaction_entities",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entities_transaction_entity_id1",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entities_transaction_entity_id11",
                table: "transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "transaction_entity_id1",
                table: "transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "transaction_entity_id2",
                table: "transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_transactions_transaction_entity_id1",
                table: "transactions",
                column: "transaction_entity_id1");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_transaction_entity_id2",
                table: "transactions",
                column: "transaction_entity_id2");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_entities_recipient_id",
                table: "transactions",
                column: "recipient_id",
                principalTable: "transaction_entities",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_entities_sender_id",
                table: "transactions",
                column: "sender_id",
                principalTable: "transaction_entities",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_entities_transaction_entity_id1",
                table: "transactions",
                column: "transaction_entity_id1",
                principalTable: "transaction_entities",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_entities_transaction_entity_id2",
                table: "transactions",
                column: "transaction_entity_id2",
                principalTable: "transaction_entities",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
