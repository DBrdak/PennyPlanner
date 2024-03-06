﻿// <auto-generated />
using System;
using Domestica.Budget.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domestica.Budget.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240306004231_users02")]
    partial class users02
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domestica.Budget.Domain.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("currency");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("account_type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("account_type");

                    b.HasKey("Id")
                        .HasName("pk_accounts");

                    b.ToTable("accounts", (string)null);

                    b.HasDiscriminator<string>("account_type").HasValue("Account");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domestica.Budget.Domain.BudgetPlans.BudgetPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.HasKey("Id")
                        .HasName("pk_budget_plans");

                    b.ToTable("budget_plans", (string)null);
                });

            modelBuilder.Entity("Domestica.Budget.Domain.TransactionCategories.TransactionCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.Property<string>("transaction_category_type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("transaction_category_type");

                    b.HasKey("Id")
                        .HasName("pk_transaction_categories");

                    b.ToTable("transaction_categories", (string)null);

                    b.HasDiscriminator<string>("transaction_category_type").HasValue("TransactionCategory");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domestica.Budget.Domain.TransactionEntities.TransactionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("entity_type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("entity_type");

                    b.HasKey("Id")
                        .HasName("pk_transaction_entities");

                    b.ToTable("transaction_entities", (string)null);

                    b.HasDiscriminator<string>("entity_type").HasValue("TransactionEntity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domestica.Budget.Domain.TransactionSubcategories.TransactionSubcategory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_transaction_subcategories");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_transaction_subcategories_category_id");

                    b.ToTable("transaction_subcategories", (string)null);
                });

            modelBuilder.Entity("Domestica.Budget.Domain.Transactions.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("account_id");

                    b.Property<Guid?>("BudgetPlanId")
                        .HasColumnType("uuid")
                        .HasColumnName("budget_plan_id");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<Guid?>("FromAccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("from_account_id");

                    b.Property<Guid?>("RecipientId")
                        .HasColumnType("uuid")
                        .HasColumnName("recipient_id");

                    b.Property<Guid?>("SenderId")
                        .HasColumnType("uuid")
                        .HasColumnName("sender_id");

                    b.Property<Guid?>("SubcategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("subcategory_id");

                    b.Property<Guid?>("ToAccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("to_account_id");

                    b.Property<DateTime>("TransactionDateUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("transaction_date_utc");

                    b.Property<Guid?>("TransactionEntityId")
                        .HasColumnType("uuid")
                        .HasColumnName("transaction_entity_id");

                    b.HasKey("Id")
                        .HasName("pk_transactions");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("ix_transactions_account_id");

                    b.HasIndex("BudgetPlanId")
                        .HasDatabaseName("ix_transactions_budget_plan_id");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_transactions_category_id");

                    b.HasIndex("FromAccountId")
                        .HasDatabaseName("ix_transactions_from_account_id");

                    b.HasIndex("RecipientId")
                        .HasDatabaseName("ix_transactions_recipient_id");

                    b.HasIndex("SenderId")
                        .HasDatabaseName("ix_transactions_sender_id");

                    b.HasIndex("SubcategoryId")
                        .HasDatabaseName("ix_transactions_subcategory_id");

                    b.HasIndex("ToAccountId")
                        .HasDatabaseName("ix_transactions_to_account_id");

                    b.HasIndex("TransactionEntityId")
                        .HasDatabaseName("ix_transactions_transaction_entity_id");

                    b.ToTable("transactions", (string)null);
                });

            modelBuilder.Entity("Domestica.Budget.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("currency");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)")
                        .HasColumnName("email");

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("identity_id");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.HasIndex("IdentityId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_identity_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Domestica.Budget.Domain.Accounts.SavingsAccounts.SavingsAccount", b =>
                {
                    b.HasBaseType("Domestica.Budget.Domain.Accounts.Account");

                    b.HasDiscriminator().HasValue("SavingsAccount");
                });

            modelBuilder.Entity("Domestica.Budget.Domain.Accounts.TransactionalAccounts.TransactionalAccount", b =>
                {
                    b.HasBaseType("Domestica.Budget.Domain.Accounts.Account");

                    b.HasDiscriminator().HasValue("TransactionalAccount");
                });

            modelBuilder.Entity("Domestica.Budget.Domain.TransactionCategories.IncomeTransactionCategory", b =>
                {
                    b.HasBaseType("Domestica.Budget.Domain.TransactionCategories.TransactionCategory");

                    b.HasDiscriminator().HasValue("IncomeTransactionCategory");
                });

            modelBuilder.Entity("Domestica.Budget.Domain.TransactionCategories.OutcomeTransactionCategory", b =>
                {
                    b.HasBaseType("Domestica.Budget.Domain.TransactionCategories.TransactionCategory");

                    b.HasDiscriminator().HasValue("OutcomeTransactionCategory");
                });

            modelBuilder.Entity("Domestica.Budget.Domain.TransactionEntities.TransactionRecipients.TransactionRecipient", b =>
                {
                    b.HasBaseType("Domestica.Budget.Domain.TransactionEntities.TransactionEntity");

                    b.HasDiscriminator().HasValue("TransactionRecipient");
                });

            modelBuilder.Entity("Domestica.Budget.Domain.TransactionEntities.TransactionSenders.TransactionSender", b =>
                {
                    b.HasBaseType("Domestica.Budget.Domain.TransactionEntities.TransactionEntity");

                    b.HasDiscriminator().HasValue("TransactionSender");
                });

            modelBuilder.Entity("Domestica.Budget.Domain.BudgetPlans.BudgetPlan", b =>
                {
                    b.OwnsOne("DateKit.DB.DateTimeRange", "BudgetPeriod", b1 =>
                        {
                            b1.Property<Guid>("BudgetPlanId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateTime>("End")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("budget_period_end");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("budget_period_start");

                            b1.HasKey("BudgetPlanId");

                            b1.ToTable("budget_plans");

                            b1.WithOwner()
                                .HasForeignKey("BudgetPlanId")
                                .HasConstraintName("fk_budget_plans_budget_plans_id");
                        });

                    b.OwnsMany("Domestica.Budget.Domain.BudgetPlans.BudgetedTransactionCategory", "BudgetedTransactionCategories", b1 =>
                        {
                            b1.Property<Guid>("BudgetPlanId")
                                .HasColumnType("uuid")
                                .HasColumnName("budget_plan_id");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("CategoryId")
                                .HasColumnType("uuid")
                                .HasColumnName("category_id");

                            b1.HasKey("BudgetPlanId", "Id")
                                .HasName("pk_budgeted_transaction_category");

                            b1.HasIndex("CategoryId")
                                .HasDatabaseName("ix_budgeted_transaction_category_category_id");

                            b1.ToTable("budgeted_transaction_category", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BudgetPlanId")
                                .HasConstraintName("fk_budgeted_transaction_category_budget_plan_budget_plan_temp_");

                            b1.HasOne("Domestica.Budget.Domain.TransactionCategories.TransactionCategory", "Category")
                                .WithMany()
                                .HasForeignKey("CategoryId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired()
                                .HasConstraintName("fk_budgeted_transaction_category_transaction_category_category_t");

                            b1.OwnsOne("Money.DB.Money", "ActualAmount", b2 =>
                                {
                                    b2.Property<Guid>("BudgetedTransactionCategoryBudgetPlanId")
                                        .HasColumnType("uuid")
                                        .HasColumnName("budget_plan_id");

                                    b2.Property<int>("BudgetedTransactionCategoryId")
                                        .HasColumnType("integer")
                                        .HasColumnName("id");

                                    b2.Property<decimal>("Amount")
                                        .HasColumnType("numeric")
                                        .HasColumnName("actual_amount_amount");

                                    b2.Property<string>("Currency")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("actual_amount_currency");

                                    b2.HasKey("BudgetedTransactionCategoryBudgetPlanId", "BudgetedTransactionCategoryId");

                                    b2.ToTable("budgeted_transaction_category");

                                    b2.WithOwner()
                                        .HasForeignKey("BudgetedTransactionCategoryBudgetPlanId", "BudgetedTransactionCategoryId")
                                        .HasConstraintName("fk_budgeted_transaction_category_budgeted_transaction_category");
                                });

                            b1.OwnsOne("Money.DB.Money", "BudgetedAmount", b2 =>
                                {
                                    b2.Property<Guid>("BudgetedTransactionCategoryBudgetPlanId")
                                        .HasColumnType("uuid")
                                        .HasColumnName("budget_plan_id");

                                    b2.Property<int>("BudgetedTransactionCategoryId")
                                        .HasColumnType("integer")
                                        .HasColumnName("id");

                                    b2.Property<decimal>("Amount")
                                        .HasColumnType("numeric")
                                        .HasColumnName("budgeted_amount_amount");

                                    b2.Property<string>("Currency")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("budgeted_amount_currency");

                                    b2.HasKey("BudgetedTransactionCategoryBudgetPlanId", "BudgetedTransactionCategoryId");

                                    b2.ToTable("budgeted_transaction_category");

                                    b2.WithOwner()
                                        .HasForeignKey("BudgetedTransactionCategoryBudgetPlanId", "BudgetedTransactionCategoryId")
                                        .HasConstraintName("fk_budgeted_transaction_category_budgeted_transaction_category");
                                });

                            b1.Navigation("ActualAmount")
                                .IsRequired();

                            b1.Navigation("BudgetedAmount")
                                .IsRequired();

                            b1.Navigation("Category");
                        });

                    b.Navigation("BudgetPeriod")
                        .IsRequired();

                    b.Navigation("BudgetedTransactionCategories");
                });

            modelBuilder.Entity("Domestica.Budget.Domain.TransactionSubcategories.TransactionSubcategory", b =>
                {
                    b.HasOne("Domestica.Budget.Domain.TransactionCategories.TransactionCategory", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_transaction_subcategories_transaction_categories_category_id");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domestica.Budget.Domain.Transactions.Transaction", b =>
                {
                    b.HasOne("Domestica.Budget.Domain.Accounts.Account", null)
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_transactions_accounts_account_id");

                    b.HasOne("Domestica.Budget.Domain.BudgetPlans.BudgetPlan", null)
                        .WithMany("Transactions")
                        .HasForeignKey("BudgetPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_transactions_budget_plans_budget_plan_id");

                    b.HasOne("Domestica.Budget.Domain.TransactionCategories.TransactionCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_transactions_transaction_categories_category_id1");

                    b.HasOne("Domestica.Budget.Domain.Accounts.Account", null)
                        .WithMany()
                        .HasForeignKey("FromAccountId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_transactions_accounts_from_account_id");

                    b.HasOne("Domestica.Budget.Domain.TransactionEntities.TransactionEntity", null)
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_transactions_transaction_entities_transaction_entity_id1");

                    b.HasOne("Domestica.Budget.Domain.TransactionEntities.TransactionEntity", null)
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_transactions_transaction_entities_transaction_entity_id11");

                    b.HasOne("Domestica.Budget.Domain.TransactionSubcategories.TransactionSubcategory", "Subcategory")
                        .WithMany()
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_transactions_transaction_subcategory_subcategory_temp_id");

                    b.HasOne("Domestica.Budget.Domain.Accounts.Account", null)
                        .WithMany()
                        .HasForeignKey("ToAccountId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_transactions_accounts_to_account_id");

                    b.HasOne("Domestica.Budget.Domain.TransactionEntities.TransactionEntity", null)
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionEntityId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_transactions_transaction_entity_transaction_entity_temp_id");

                    b.OwnsOne("Money.DB.Money", "TransactionAmount", b1 =>
                        {
                            b1.Property<Guid>("TransactionId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("transaction_amount_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("transaction_amount_currency");

                            b1.HasKey("TransactionId");

                            b1.ToTable("transactions");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId")
                                .HasConstraintName("fk_transactions_transactions_id");
                        });

                    b.Navigation("Category");

                    b.Navigation("Subcategory");

                    b.Navigation("TransactionAmount")
                        .IsRequired();
                });

            modelBuilder.Entity("Domestica.Budget.Domain.Accounts.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domestica.Budget.Domain.BudgetPlans.BudgetPlan", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domestica.Budget.Domain.TransactionCategories.TransactionCategory", b =>
                {
                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("Domestica.Budget.Domain.TransactionEntities.TransactionEntity", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
