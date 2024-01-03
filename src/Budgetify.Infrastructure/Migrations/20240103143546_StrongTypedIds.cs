using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgetify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StrongTypedIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_budgeted_transaction_category_budget_plan_budget_plan_id",
                table: "budgeted_transaction_category");

            migrationBuilder.DropForeignKey(
                name: "fk_transaction_accounts_destination_account_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transaction_accounts_internal_destination_account_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transaction_accounts_internal_source_account_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transaction_accounts_source_account_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_accounts_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entities_recipient_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entities_sender_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entity_transaction_entity_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "ix_transaction_destination_account_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "ix_transaction_source_account_id",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "destination_account_id",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "source_account_id",
                table: "transactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "sender_id",
                table: "transactions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "recipient_id",
                table: "transactions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "internal_source_account_id",
                table: "transactions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "internal_destination_account_id",
                table: "transactions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "transactions",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "account_id",
                table: "transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "transaction_entity_id",
                table: "transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "transaction_entities",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "budget_plan_id",
                table: "budgeted_transaction_category",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "budget_plans",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "accounts",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_account_id",
                table: "transactions",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_transaction_entity_id",
                table: "transactions",
                column: "transaction_entity_id");

            migrationBuilder.AddForeignKey(
                name: "fk_budgeted_transaction_category_budget_plan_budget_plan_temp_",
                table: "budgeted_transaction_category",
                column: "budget_plan_id",
                principalTable: "budget_plans",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_accounts_internal_destination_account_id1",
                table: "transactions",
                column: "internal_destination_account_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_accounts_internal_source_account_id1",
                table: "transactions",
                column: "internal_source_account_id",
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

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transaction_entity_transaction_entity_temp_id",
                table: "transactions",
                column: "transaction_entity_id",
                principalTable: "transaction_entities",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_budgeted_transaction_category_budget_plan_budget_plan_temp_",
                table: "budgeted_transaction_category");

            migrationBuilder.DropForeignKey(
                name: "fk_transaction_accounts_internal_destination_account_id1",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transaction_accounts_internal_source_account_id1",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_accounts_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entity_recipient_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entity_sender_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transaction_entity_transaction_entity_temp_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "ix_transactions_account_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "ix_transactions_transaction_entity_id",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "account_id",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "transaction_entity_id",
                table: "transactions");

            migrationBuilder.AlterColumn<string>(
                name: "sender_id",
                table: "transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "recipient_id",
                table: "transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "internal_source_account_id",
                table: "transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "internal_destination_account_id",
                table: "transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "transactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "destination_account_id",
                table: "transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "source_account_id",
                table: "transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "transaction_entities",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "budget_plan_id",
                table: "budgeted_transaction_category",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "budget_plans",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "accounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_destination_account_id",
                table: "transactions",
                column: "destination_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_source_account_id",
                table: "transactions",
                column: "source_account_id");

            migrationBuilder.AddForeignKey(
                name: "fk_budgeted_transaction_category_budget_plan_budget_plan_id",
                table: "budgeted_transaction_category",
                column: "budget_plan_id",
                principalTable: "budget_plans",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_accounts_destination_account_id",
                table: "transactions",
                column: "destination_account_id",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_accounts_internal_destination_account_id",
                table: "transactions",
                column: "internal_destination_account_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_accounts_internal_source_account_id",
                table: "transactions",
                column: "internal_source_account_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_accounts_source_account_id",
                table: "transactions",
                column: "source_account_id",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_accounts_id",
                table: "transactions",
                column: "id",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
                name: "fk_transactions_transaction_entity_transaction_entity_id",
                table: "transactions",
                column: "id",
                principalTable: "transaction_entities",
                principalColumn: "id");
        }
    }
}
