using KebabCore.DomainModels.Menu;
using KebabCore.DomainModels.Orders;
using KebabCore.Views;
using Microsoft.EntityFrameworkCore;

namespace KebabInfrastructure.Context
{
    public class KebabMenuDbContext : DbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Item> Items { get; set; }
        //public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<MenuView> MenuView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(
                @"Data Source=DESKTOP-AH54VTK;Initial Catalog=KebabDB;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuView>().HasNoKey();

            modelBuilder.Entity<MenuItem>().HasKey(mi => new { mi.MenuId, mi.ItemId });
            modelBuilder.Entity<MenuItem>().HasOne<Menu>().WithMany(m => m.MenuItems).HasForeignKey(e => e.MenuId).IsRequired();
            modelBuilder.Entity<MenuItem>().HasOne<Item>().WithMany(m => m.MenuItems).HasForeignKey(e => e.MenuItemId).IsRequired();
            //modelBuilder
            //    .HasOne<Item>().WithMany(i => i.MenuItems);
            modelBuilder
                .Entity<Item>()
                .Property(e => e.Category)
                .HasConversion<int>();

            modelBuilder
                .Entity<MenuView>()
                .Property(v => v.ItemCategory)
                .HasConversion<string>();

        }
    }
}
