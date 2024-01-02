﻿// <auto-generated />
using System;
using Budgetify.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Budgetify.Infrastructure.Migrations
{
    [DbContext(typeof(BudgetifyContext))]
    partial class BudgetifyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Budgetify.Domain.Accounts.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

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

            modelBuilder.Entity("Budgetify.Domain.BudgetPlans.BudgetPlan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.HasKey("Id")
                        .HasName("pk_budget_plans");

                    b.ToTable("budget_plans", (string)null);
                });

            modelBuilder.Entity("Budgetify.Domain.TransactionEntities.TransactionEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
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

            modelBuilder.Entity("Budgetify.Domain.Transactions.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<DateTime>("TransactionDateUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("transaction_date_utc");

                    b.Property<string>("transaction_type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("transaction_type");

                    b.HasKey("Id")
                        .HasName("pk_transactions");

                    b.ToTable("transactions", (string)null);

                    b.HasDiscriminator<string>("transaction_type").HasValue("Transaction");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Budgetify.Domain.Accounts.SavingsAccounts.SavingsAccount", b =>
                {
                    b.HasBaseType("Budgetify.Domain.Accounts.Account");

                    b.HasDiscriminator().HasValue("SavingsAccount");
                });

            modelBuilder.Entity("Budgetify.Domain.Accounts.TransactionalAccounts.TransactionalAccount", b =>
                {
                    b.HasBaseType("Budgetify.Domain.Accounts.Account");

                    b.HasDiscriminator().HasValue("TransactionalAccount");
                });

            modelBuilder.Entity("Budgetify.Domain.TransactionEntities.TransactionRecipients.TransactionRecipient", b =>
                {
                    b.HasBaseType("Budgetify.Domain.TransactionEntities.TransactionEntity");

                    b.HasDiscriminator().HasValue("TransactionRecipient");
                });

            modelBuilder.Entity("Budgetify.Domain.TransactionEntities.TransactionSenders.TransactionSender", b =>
                {
                    b.HasBaseType("Budgetify.Domain.TransactionEntities.TransactionEntity");

                    b.HasDiscriminator().HasValue("TransactionSender");
                });

            modelBuilder.Entity("Budgetify.Domain.Transactions.IncomingTransactions.IncomingTransaction", b =>
                {
                    b.HasBaseType("Budgetify.Domain.Transactions.Transaction");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("category");

                    b.Property<string>("DestinationAccountId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("destination_account_id");

                    b.Property<string>("InternalSourceAccountId")
                        .HasColumnType("text")
                        .HasColumnName("internal_source_account_id");

                    b.Property<string>("SenderId")
                        .HasColumnType("text")
                        .HasColumnName("sender_id");

                    b.HasIndex("DestinationAccountId")
                        .HasDatabaseName("ix_transaction_destination_account_id");

                    b.HasIndex("InternalSourceAccountId")
                        .HasDatabaseName("ix_transaction_internal_source_account_id");

                    b.HasIndex("SenderId")
                        .HasDatabaseName("ix_transaction_sender_id");

                    b.ToTable("transactions", t =>
                        {
                            t.Property("Category")
                                .HasColumnName("incoming_transaction_category");
                        });

                    b.HasDiscriminator().HasValue("IncomingTransaction");
                });

            modelBuilder.Entity("Budgetify.Domain.Transactions.OugoingTransactions.OutgoingTransaction", b =>
                {
                    b.HasBaseType("Budgetify.Domain.Transactions.Transaction");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("category");

                    b.Property<string>("InternalDestinationAccountId")
                        .HasColumnType("text")
                        .HasColumnName("internal_destination_account_id");

                    b.Property<string>("RecipientId")
                        .HasColumnType("text")
                        .HasColumnName("recipient_id");

                    b.Property<string>("SourceAccountId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("source_account_id");

                    b.HasIndex("InternalDestinationAccountId")
                        .HasDatabaseName("ix_transaction_internal_destination_account_id");

                    b.HasIndex("RecipientId")
                        .HasDatabaseName("ix_transaction_recipient_id");

                    b.HasIndex("SourceAccountId")
                        .HasDatabaseName("ix_transaction_source_account_id");

                    b.HasDiscriminator().HasValue("OutgoingTransaction");
                });

            modelBuilder.Entity("Budgetify.Domain.Accounts.Account", b =>
                {
                    b.OwnsOne("Money.DB.Money", "Balance", b1 =>
                        {
                            b1.Property<string>("AccountId")
                                .HasColumnType("text")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("balance_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("balance_currency");

                            b1.HasKey("AccountId");

                            b1.ToTable("accounts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId")
                                .HasConstraintName("fk_accounts_accounts_id");
                        });

                    b.Navigation("Balance")
                        .IsRequired();
                });

            modelBuilder.Entity("Budgetify.Domain.BudgetPlans.BudgetPlan", b =>
                {
                    b.OwnsMany("Budgetify.Domain.Shared.TransactionCategories.BudgetedTransactionCategory", "BudgetedTransactionCategories", b1 =>
                        {
                            b1.Property<string>("BudgetPlanId")
                                .HasColumnType("text")
                                .HasColumnName("budget_plan_id");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Category")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("category");

                            b1.HasKey("BudgetPlanId", "Id")
                                .HasName("pk_budgeted_transaction_category");

                            b1.ToTable("budgeted_transaction_category", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BudgetPlanId")
                                .HasConstraintName("fk_budgeted_transaction_category_budget_plan_budget_plan_id");

                            b1.OwnsOne("Money.DB.Money", "ActualAmount", b2 =>
                                {
                                    b2.Property<string>("BudgetedTransactionCategoryBudgetPlanId")
                                        .HasColumnType("text")
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
                                    b2.Property<string>("BudgetedTransactionCategoryBudgetPlanId")
                                        .HasColumnType("text")
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
                        });

                    b.OwnsOne("DateKit.DB.DateTimeRange", "BudgetPeriod", b1 =>
                        {
                            b1.Property<string>("BudgetPlanId")
                                .HasColumnType("text")
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

                    b.Navigation("BudgetPeriod")
                        .IsRequired();

                    b.Navigation("BudgetedTransactionCategories");
                });

            modelBuilder.Entity("Budgetify.Domain.Transactions.Transaction", b =>
                {
                    b.HasOne("Budgetify.Domain.Accounts.Account", null)
                        .WithMany("Transactions")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_transactions_accounts_id");

                    b.HasOne("Budgetify.Domain.TransactionEntities.TransactionEntity", null)
                        .WithMany("Transactions")
                        .HasForeignKey("Id")
                        .HasConstraintName("fk_transactions_transaction_entity_transaction_entity_id");

                    b.OwnsOne("Money.DB.Money", "TransactionAmount", b1 =>
                        {
                            b1.Property<string>("TransactionId")
                                .HasColumnType("text")
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

                    b.Navigation("TransactionAmount")
                        .IsRequired();
                });

            modelBuilder.Entity("Budgetify.Domain.Transactions.IncomingTransactions.IncomingTransaction", b =>
                {
                    b.HasOne("Budgetify.Domain.Accounts.Account", "DestinationAccount")
                        .WithMany()
                        .HasForeignKey("DestinationAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_transaction_accounts_destination_account_id");

                    b.HasOne("Budgetify.Domain.Accounts.Account", "InternalSourceAccount")
                        .WithMany()
                        .HasForeignKey("InternalSourceAccountId")
                        .HasConstraintName("fk_transaction_accounts_internal_source_account_id");

                    b.HasOne("Budgetify.Domain.TransactionEntities.TransactionSenders.TransactionSender", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .HasConstraintName("fk_transactions_transaction_entities_sender_id");

                    b.Navigation("DestinationAccount");

                    b.Navigation("InternalSourceAccount");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Budgetify.Domain.Transactions.OugoingTransactions.OutgoingTransaction", b =>
                {
                    b.HasOne("Budgetify.Domain.Accounts.Account", "InternalDestinationAccount")
                        .WithMany()
                        .HasForeignKey("InternalDestinationAccountId")
                        .HasConstraintName("fk_transaction_accounts_internal_destination_account_id");

                    b.HasOne("Budgetify.Domain.TransactionEntities.TransactionRecipients.TransactionRecipient", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .HasConstraintName("fk_transactions_transaction_entities_recipient_id");

                    b.HasOne("Budgetify.Domain.Accounts.Account", "SourceAccount")
                        .WithMany()
                        .HasForeignKey("SourceAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_transaction_accounts_source_account_id");

                    b.Navigation("InternalDestinationAccount");

                    b.Navigation("Recipient");

                    b.Navigation("SourceAccount");
                });

            modelBuilder.Entity("Budgetify.Domain.Accounts.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Budgetify.Domain.TransactionEntities.TransactionEntity", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
