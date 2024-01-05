using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Budgetify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    balance_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    balance_currency = table.Column<string>(type: "text", nullable: false),
                    account_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "budget_plans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    budget_period_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    budget_period_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_budget_plans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transaction_entities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    entity_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_entities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "budgeted_transaction_category",
                columns: table => new
                {
                    budget_plan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category = table.Column<string>(type: "text", nullable: false),
                    budgeted_amount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    budgeted_amount_currency = table.Column<string>(type: "text", nullable: false),
                    actual_amount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    actual_amount_currency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_budgeted_transaction_category", x => new { x.budget_plan_id, x.id });
                    table.ForeignKey(
                        name: "fk_budgeted_transaction_category_budget_plan_budget_plan_temp_",
                        column: x => x.budget_plan_id,
                        principalTable: "budget_plans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_account_id = table.Column<Guid>(type: "uuid", nullable: true),
                    to_account_id = table.Column<Guid>(type: "uuid", nullable: true),
                    sender_id = table.Column<Guid>(type: "uuid", nullable: true),
                    recipient_id = table.Column<Guid>(type: "uuid", nullable: true),
                    transaction_amount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    transaction_amount_currency = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    transaction_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    transaction_entity_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_transactions_accounts_from_account_id1",
                        column: x => x.from_account_id,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_transactions_accounts_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_transactions_accounts_to_account_id1",
                        column: x => x.to_account_id,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_transactions_transaction_entity_recipient_id",
                        column: x => x.recipient_id,
                        principalTable: "transaction_entities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_transactions_transaction_entity_sender_id",
                        column: x => x.sender_id,
                        principalTable: "transaction_entities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_transactions_transaction_entity_transaction_entity_temp_id",
                        column: x => x.transaction_entity_id,
                        principalTable: "transaction_entities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_transactions_account_id",
                table: "transactions",
                column: "account_id");

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

            migrationBuilder.CreateIndex(
                name: "ix_transactions_transaction_entity_id",
                table: "transactions",
                column: "transaction_entity_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "budgeted_transaction_category");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "budget_plans");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "transaction_entities");
        }
    }
}
