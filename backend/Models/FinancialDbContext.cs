using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public partial class FinancialDbContext : DbContext
{
    public FinancialDbContext()
    {
    }

    public FinancialDbContext(DbContextOptions<FinancialDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cashback> Cashbacks { get; set; }

    public virtual DbSet<Account> Account { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cashback>(entity =>
        {
            entity.HasKey(e => e.IdCashback).HasName("PK__Cashback__C47DA0B0517A0B71");

            entity.ToTable("Cashback");

            entity.Property(e => e.TaxCashback).HasColumnType("decimal(5, 4)");

            entity.HasOne(d => d.IdTransactionNavigation).WithMany(p => p.Cashbacks)
                .HasForeignKey(d => d.IdTransaction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cashback__IdTran__3C69FB99");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC07F2317990");

            entity.Property(e => e.CpfCnpj)
                .HasMaxLength(20)
                .HasColumnName("CPF_CNPJ");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.IdTransaction).HasName("PK__Transact__D44F6292EA3C151D");

            entity.ToTable("Transaction");

            entity.Property(e => e.Cashback).HasColumnType("decimal(5, 4)");
            entity.Property(e => e.TransactionData).HasColumnType("datetime");
            entity.Property(e => e.Value).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transaction__IdCon__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
