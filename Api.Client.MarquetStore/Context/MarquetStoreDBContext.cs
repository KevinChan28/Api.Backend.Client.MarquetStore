using System;
using System.Collections.Generic;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Client.MarquetStore.Context;

public partial class MarquetstoreDbContext : DbContext
{
    public MarquetstoreDbContext()
    {
    }

    public MarquetstoreDbContext(DbContextOptions<MarquetstoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Concept> Concepts { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Pay> Pays { get; set; }

    public virtual DbSet<PaymentsMethod> PaymentsMethods { get; set; }

    public virtual DbSet<Personalization> Personalizations { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Exchange> Exchanges { get; set; }

    public virtual DbSet<View> Views { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Address");

            entity.HasIndex(e => e.UserId, "UserId_INDEX");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.InteriorNumber).HasMaxLength(10);
            entity.Property(e => e.Neighborhood).HasMaxLength(50);
            entity.Property(e => e.Neighborhood).HasMaxLength(50);
            entity.Property(e => e.OutdoorNumber).HasMaxLength(10);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnType("int(11)");
            entity.Property(e => e.ZipCode).HasMaxLength(5);
        });

        modelBuilder.Entity<Concept>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Concept");

            entity.HasIndex(e => e.ProductId, "ProductId_INDEX");

            entity.HasIndex(e => e.SaleId, "SaleId_INDEX");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Import).HasPrecision(10);
            entity.Property(e => e.ProductId).HasColumnType("int(11)");
            entity.Property(e => e.Quantity).HasColumnType("int(11)");
            entity.Property(e => e.SaleId).HasColumnType("int(11)");
            entity.Property(e => e.Price).HasPrecision(10);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IsAvailable).HasColumnType("bit(1)");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PathImage).HasMaxLength(900);
            entity.Property(e => e.Price).HasPrecision(10);
            entity.Property(e => e.Stock).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Pay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Pay");

            entity.HasIndex(e => e.PaymentsMethodId, "PaymentsMethod_INDEX");

            entity.HasIndex(e => e.SaleId, "SaleId_INDEX");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentsMethodId).HasColumnType("int(11)");
            entity.Property(e => e.SaleId).HasColumnType("int(11)");
        });

        modelBuilder.Entity<PaymentsMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("PaymentsMethod");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasMaxLength(400);
        });

        modelBuilder.Entity<Personalization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Personalization");

            entity.HasIndex(e => e.ConceptId, "ConceptId_INDEX");

            entity.HasIndex(e => e.IngredientId, "IngredientId_INDEX");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.ConceptId).HasColumnType("int(11)");
            entity.Property(e => e.IngredientId).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsAvailable).HasColumnType("bit(1)");
            entity.Property(e => e.Name).HasMaxLength(300);
            entity.Property(e => e.PathImage).HasMaxLength(600);
            entity.Property(e => e.Price).HasPrecision(10);
            entity.Property(e => e.Stock).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasMaxLength(800);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Sale");

            entity.HasIndex(e => e.UserId, "UserId_INDEX");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(200);
            entity.Property(e => e.Total).HasPrecision(10);
            entity.Property(e => e.UserId).HasColumnType("int(11)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.RolId, "RolId_INDEX");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(900);
            entity.Property(e => e.RolId).HasColumnType("int(11)");
            entity.Property(e => e.Telephone).HasMaxLength(20);
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Coupon");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Code).HasMaxLength(5);
            entity.Property(e => e.Description).HasMaxLength(800);
            entity.Property(e => e.Discount).HasPrecision(10);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Duration).HasColumnType("int(11)");
            entity.Property(e => e.Quantity).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Exchange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Exchange");

            entity.HasIndex(e => e.UserId, "UserId_INDEX");
            entity.HasIndex(e => e.CouponId, "CouponId_INDEX");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IsUsed).HasColumnType("bit(1)");
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnType("int(11)");
            entity.Property(e => e.CouponId).HasColumnType("int(11)");
            entity.Property(e => e.Count).HasColumnType("int(11)");
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("View");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasColumnType("longtext");
            entity.Property(e => e.Content).HasColumnType("longtext");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
