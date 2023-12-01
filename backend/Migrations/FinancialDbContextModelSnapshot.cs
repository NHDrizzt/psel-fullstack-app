﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Models;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(FinancialDbContext))]
    partial class FinancialDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("backend.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AccountStatus")
                        .HasColumnType("bit");

                    b.Property<string>("CpfCnpj")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("CPF_CNPJ");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Account__3214EC07F2317990");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("backend.Models.Cashback", b =>
                {
                    b.Property<int>("IdCashback")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCashback"));

                    b.Property<int>("IdTransaction")
                        .HasColumnType("int");

                    b.Property<decimal>("TaxCashback")
                        .HasColumnType("decimal(5, 4)");

                    b.HasKey("IdCashback")
                        .HasName("PK__Cashback__C47DA0B0517A0B71");

                    b.HasIndex("IdTransaction");

                    b.ToTable("Cashback", (string)null);
                });

            modelBuilder.Entity("backend.Models.Transaction", b =>
                {
                    b.Property<int>("IdTransaction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTransaction"));

                    b.Property<decimal?>("Cashback")
                        .HasColumnType("decimal(5, 4)");

                    b.Property<int>("IdAccount")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionData")
                        .HasColumnType("datetime");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("IdTransaction")
                        .HasName("PK__Transact__D44F6292EA3C151D");

                    b.HasIndex("IdAccount");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("backend.Models.Cashback", b =>
                {
                    b.HasOne("backend.Models.Transaction", "IdTransactionNavigation")
                        .WithMany("Cashbacks")
                        .HasForeignKey("IdTransaction")
                        .IsRequired()
                        .HasConstraintName("FK__Cashback__IdTran__3C69FB99");

                    b.Navigation("IdTransactionNavigation");
                });

            modelBuilder.Entity("backend.Models.Transaction", b =>
                {
                    b.HasOne("backend.Models.Account", "IdAccountNavigation")
                        .WithMany("Transactions")
                        .HasForeignKey("IdAccount")
                        .IsRequired()
                        .HasConstraintName("FK__Transaction__IdCon__398D8EEE");

                    b.Navigation("IdAccountNavigation");
                });

            modelBuilder.Entity("backend.Models.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("backend.Models.Transaction", b =>
                {
                    b.Navigation("Cashbacks");
                });
#pragma warning restore 612, 618
        }
    }
}
