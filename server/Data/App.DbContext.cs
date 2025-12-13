using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class AppDbContext : DbContext
{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<PurchaseModel> Purchases { get; set; }
        public DbSet<WinningModel> Winnings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Cart)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseModel>()
                .HasOne(p => p.Gift)
                .WithMany(g => g.Purchases)
                .HasForeignKey(p => p.GiftId);

            modelBuilder.Entity<WinningModel>()
                .HasOne(w => w.Gift)
                .WithMany()
                .HasForeignKey(w => w.GiftId);

            modelBuilder.Entity<WinningModel>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.WinnerId);

        }
    }

//     public DbSet<DonorModel> Donors { get; set; }
//     public DbSet<GiftModel> Gifts { get; set; }
