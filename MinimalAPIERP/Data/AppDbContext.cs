using Microsoft.EntityFrameworkCore;

namespace ERP.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Raincheck> Rainchecks { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK_dbo.CartItems");

            entity.Property(e => e.CartItemGuid).HasDefaultValueSql("(newsequentialid())");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_dbo.CartItems_dbo.Products_ProductId");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_dbo.Categories");

            entity.Property(e => e.CategoryGuid).HasDefaultValueSql("(newsequentialid())");
        });


        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_dbo.Orders");

            entity.Property(e => e.OrderGuid).HasDefaultValueSql("(newsequentialid())");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK_dbo.OrderDetails");
                       
            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasConstraintName("FK_dbo.OrderDetails_dbo.Orders_OrderId");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails).HasConstraintName("FK_dbo.OrderDetails_dbo.Products_ProductId");

            entity.Property(e => e.OrderDetailGuid).HasDefaultValueSql("(newsequentialid())");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_dbo.Products");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("FK_dbo.Products_dbo.Categories_CategoryId");

            entity.Property(e => e.ProductGuid).HasDefaultValueSql("(newsequentialid())");
        });

        modelBuilder.Entity<Raincheck>(entity =>
        {
            entity.HasKey(e => e.RaincheckId).HasName("PK_dbo.Rainchecks");

            entity.HasOne(d => d.Product).WithMany(p => p.Rainchecks).HasConstraintName("FK_dbo.Rainchecks_dbo.Products_ProductId");

            entity.HasOne(d => d.Store).WithMany(p => p.Rainchecks).HasConstraintName("FK_dbo.Rainchecks_dbo.Stores_StoreId");

            entity.Property(e => e.RaincheckGuid).HasDefaultValueSql("(newsequentialid())");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK_dbo.Stores");

            entity.Property(e => e.StoreGuid).HasDefaultValueSql("(newsequentialid())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}