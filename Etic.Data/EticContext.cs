using Etic.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Data
{
    public class EticContext :DbContext
    {

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=EticDB;Integrated Security=True;TrustServerCertificate=True");
            optionsBuilder.EnableSensitiveDataLogging(true);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ============================================
            // COMPOSITE KEYS (Çoklu Primary Key)
            // ============================================
            
            // ProductCategory: ProductId + CategoryId birlikte Primary Key
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.CategoryId, pc.ProductId });

            // ============================================
            // RELATIONSHIPS (İlişkiler)
            // ============================================
            
            // User -> Addresses (1-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade); // User silinince adresleri de sil

            // User -> Orders (1-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); // User silinince siparişler kalmalı

            // User -> Basket (1-to-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Basket)
                .WithOne(b => b.User)
                .HasForeignKey<Basket>(b => b.UserId);

            // Product -> Images (1-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Ürün silinince görselleri de sil

            // Product -> ProductCategories (Many-to-Many via ProductCategory)
            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            // Category -> SubCategories (Self-Referencing)
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict); // Ana kategori silinince alt kategoriler kalmalı

            // Order -> OrderProducts (1-to-Many)
            modelBuilder.Entity<Orders>()
                .HasMany(o => o.OrderProducts)
                .WithOne(op => op.Order)
                .HasForeignKey(op => op.OrderId);

            // Order -> DeliveryAddress (Many-to-1, opsiyonel)
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.DeliveryAddress)
                .WithMany() // UserAddress tarafından Orders collection'ı yok, o yüzden WithMany() boş
                .HasForeignKey(o => o.DeliveryAddressId)
                .OnDelete(DeleteBehavior.SetNull); // Adres silinince Order'daki adres NULL olsun

            // Basket -> BasketProducts (1-to-Many)
            modelBuilder.Entity<Basket>()
                .HasMany(b => b.BasketProducts)
                .WithOne(bp => bp.Basket)
                .HasForeignKey(bp => bp.BasketId);

            // ============================================
            // INDEXES (Performans için)
            // ============================================
            
            // User Email unique olmalı (bir email ile bir kullanıcı)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Product Name index (arama için)
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);

            // Product SeoLink unique olmalı
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SeoLink)
                .IsUnique();

            // Category SeoLink unique olmalı
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.SeoLink)
                .IsUnique();

            // Order UserId index (kullanıcının siparişlerini hızlı bulmak için)
            modelBuilder.Entity<Orders>()
                .HasIndex(o => o.UserId);

            // Product IsActive index (aktif ürünleri hızlı bulmak için)
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.IsActive);

            // Category Sort index (sıralı kategoriler için)
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Sort);

            // Slider IsActive index (aktif sliderları hızlı bulmak için)
            modelBuilder.Entity<Slider>()
                .HasIndex(s => s.IsActive);

            // User IsDeleted index (soft delete sorguları için)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.IsDeleted);

            // Product IsDeleted index (soft delete sorguları için)
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.IsDeleted);

            // ============================================
            // VIEWS (SQL View'ları tanımla)
            // ============================================
            
            // View'lar Id içerdiği için HasNoKey kullanmıyoruz
            modelBuilder.Entity<VwCategoryProduct>()
                .ToView("VwCategoryProducts"); // SQL View adı

            modelBuilder.Entity<VwBasketProductList>()
                .ToView("VwBasketProductList"); // SQL View adı

            // ============================================
            // DEFAULT VALUES (Varsayılan değerler)
            // ============================================
            
            // Product IsActive varsayılan true
            modelBuilder.Entity<Product>()
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            // Product Stock varsayılan 0
            modelBuilder.Entity<Product>()
                .Property(p => p.Stock)
                .HasDefaultValue(0);

            // Slider IsActive varsayılan true
            modelBuilder.Entity<Slider>()
                .Property(s => s.IsActive)
                .HasDefaultValue(true);

            // User IsAdmin varsayılan false
            modelBuilder.Entity<User>()
                .Property(u => u.IsAdmin)
                .HasDefaultValue(false);

            // User RegisterDate varsayılan şimdiki zaman
            modelBuilder.Entity<User>()
                .Property(u => u.RegisterDate)
                .HasDefaultValueSql("GETDATE()");

            // BaseEntity CreatedDate varsayılan şimdiki zaman (Tüm BaseEntity kullanan entity'ler için)
            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Category>()
                .Property(c => c.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Slider>()
                .Property(s => s.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ProductImage>()
                .Property(pi => pi.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Orders>()
                .Property(o => o.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<BasketProduct>()
                .Property(bp => bp.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<OrderProducts>()
                .Property(op => op.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<UserAddress>()
                .Property(ua => ua.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            // IsDeleted varsayılan false (Tüm BaseEntity kullanan entity'ler için)
            modelBuilder.Entity<Product>()
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                .Property(u => u.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Category>()
                .Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Slider>()
                .Property(s => s.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<ProductImage>()
                .Property(pi => pi.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Orders>()
                .Property(o => o.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<BasketProduct>()
                .Property(bp => bp.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<OrderProducts>()
                .Property(op => op.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<UserAddress>()
                .Property(ua => ua.IsDeleted)
                .HasDefaultValue(false);

            // ============================================
            // PRECISION (Decimal hassasiyet)
            // ============================================
            
            // Product Price: 18 digit, 2 decimal
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // OrderProducts Price
            modelBuilder.Entity<OrderProducts>()
                .Property(op => op.Price)
                .HasPrecision(18, 2);

            // Orders TotalAmount
            modelBuilder.Entity<Orders>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            // View: VwCategoryProduct Price
            modelBuilder.Entity<VwCategoryProduct>()
                .Property(v => v.Price)
                .HasPrecision(18, 2);

            // View: VwBasketProductList Price
            modelBuilder.Entity<VwBasketProductList>()
                .Property(v => v.Price)
                .HasPrecision(18, 2);
        }

        // ============================================
        // DB SETS (Veritabanı Tabloları)
        // ============================================
        
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Slider> Sliders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductImage> ProductImages { get; set; } = null!;
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<Basket> Baskets { get; set; } = null!;
        public DbSet<BasketProduct> BasketProducts { get; set; } = null!;
        public DbSet<Setting> Settings { get; set; } = null!;
        public DbSet<Orders> Orders { get; set; } = null!;
        public DbSet<OrderProducts> OrderProducts { get; set; } = null!;
        public DbSet<UserAddress> UserAddresses { get; set; } = null!; // ✅ Çoğul
        
        // ============================================
        // DB SETS (SQL View'ları)
        // ============================================
        
        public DbSet<VwCategoryProduct> VwCategoryProducts { get; set; } = null!;
        public DbSet<VwBasketProductList> VwBasketProductList { get; set; } = null!;


    }
}
