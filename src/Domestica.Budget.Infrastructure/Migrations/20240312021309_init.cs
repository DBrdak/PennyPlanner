using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domestica.Budget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    balance_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    balance_currency = table.Column<string>(type: "text", nullable: false),
                    account_type = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_accounts_users_user_id1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "budget_plans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    budget_period_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    budget_period_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_budget_plans", x => x.id);
                    table.ForeignKey(
                        name: "fk_budget_plans_users_user_id1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transaction_categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    transaction_category_type = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_categories_users_user_id1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transaction_entities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    entity_type = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_entities", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_entities_users_user_id1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "budgeted_transaction_category",
                columns: table => new
                {
                    budget_plan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.ForeignKey(
                        name: "fk_budgeted_transaction_category_transaction_category_category_t",
                        column: x => x.category_id,
                        principalTable: "transaction_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transaction_subcategories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_subcategories", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_subcategories_transaction_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "transaction_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_transaction_subcategories_users_user_id1",
                        column: x => x.user_id,
                        principalTable: "users",
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
                    category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    subcategory_id = table.Column<Guid>(type: "uuid", nullable: true),
                    transaction_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    budget_plan_id = table.Column<Guid>(type: "uuid", nullable: true),
                    transaction_entity_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_transactions_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_transactions_accounts_from_account_id",
                        column: x => x.from_account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_transactions_accounts_to_account_id",
                        column: x => x.to_account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_transactions_budget_plans_budget_plan_id",
                        column: x => x.budget_plan_id,
                        principalTable: "budget_plans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_transactions_transaction_categories_category_id1",
                        column: x => x.category_id,
                        principalTable: "transaction_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_transactions_transaction_entities_transaction_entity_id1",
                        column: x => x.recipient_id,
                        principalTable: "transaction_entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_transactions_transaction_entities_transaction_entity_id11",
                        column: x => x.sender_id,
                        principalTable: "transaction_entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_transactions_transaction_entity_transaction_entity_temp_id",
                        column: x => x.transaction_entity_id,
                        principalTable: "transaction_entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_transactions_transaction_subcategory_subcategory_temp_id",
                        column: x => x.subcategory_id,
                        principalTable: "transaction_subcategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_transactions_users_user_id1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_accounts_user_id",
                table: "accounts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_budget_plans_user_id",
                table: "budget_plans",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_budgeted_transaction_category_category_id",
                table: "budgeted_transaction_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_categories_user_id",
                table: "transaction_categories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_entities_user_id",
                table: "transaction_entities",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_subcategories_category_id",
                table: "transaction_subcategories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_subcategories_user_id",
                table: "transaction_subcategories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_account_id",
                table: "transactions",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_budget_plan_id",
                table: "transactions",
                column: "budget_plan_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_category_id",
                table: "transactions",
                column: "category_id");

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
                name: "ix_transactions_subcategory_id",
                table: "transactions",
                column: "subcategory_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_to_account_id",
                table: "transactions",
                column: "to_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_transaction_entity_id",
                table: "transactions",
                column: "transaction_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_user_id",
                table: "transactions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "budgeted_transaction_category");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "budget_plans");

            migrationBuilder.DropTable(
                name: "transaction_entities");

            migrationBuilder.DropTable(
                name: "transaction_subcategories");

            migrationBuilder.DropTable(
                name: "transaction_categories");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
