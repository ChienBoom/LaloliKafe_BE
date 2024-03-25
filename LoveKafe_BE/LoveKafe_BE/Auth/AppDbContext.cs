using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LoveKafe_BE.Models;

namespace LoveKafe_BE.Auth
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //cau hinh DbSet
        public DbSet<Area> Area { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<UserDetail> UserDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Area>()
                .HasMany(o => o.Tables)
                .WithOne(o => o.Area)
                .HasForeignKey(o => o.AreaId);
            builder.Entity<Category>()
                .HasMany(o => o.Products)
                .WithOne(o => o.Category)
                .HasForeignKey(o => o.CategoryId);
            builder.Entity<Table>()
                .HasMany(o => o.Orders)
                .WithOne(o => o.Table)
                .HasForeignKey(O => O.TableId);
            builder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId);
            builder.Entity<Product>()
                .HasMany(o => o.OrderDetails)
                .WithOne(o => o.Product)
                .HasForeignKey(o => o.ProductId);
            builder.Entity<UserDetail>()
                .HasOne(o => o.AppUser)
                .WithOne(o => o.UserDetail)
                .HasForeignKey<AppUser>(o => o.UserDetailId);
        }
    }
}
