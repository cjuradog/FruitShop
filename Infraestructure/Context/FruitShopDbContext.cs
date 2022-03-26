using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Models
{
    public class FruitShopDbContext : DbContext
    {
        public FruitShopDbContext()
        {
        }

        public FruitShopDbContext(DbContextOptions<FruitShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleType> ArticleType { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=FruitShop;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.ArticleType)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.ArticleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Article_ArticleType");
            });

            modelBuilder.Entity<ArticleType>(entity =>
            {
                entity.HasIndex(e => e.ArticleTypeId)
                    .HasName("UQ__ArticleT__50BEBDB2E51F2D88")
                    .IsUnique();

                entity.Property(e => e.ArticleTypeId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Dni)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(7, 2)");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Article");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Customer");
            });
        }
    }
}
