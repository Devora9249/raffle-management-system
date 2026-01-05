using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    // public DbSet<DonorModel> Donors { get; set; }
    public DbSet<GiftModel> Gifts { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<PurchaseModel> Purchases { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<WinningModel> Winnings { get; set; }
    // public DbSet<RaffleModel> Raffles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // DonorModel configuration
        // modelBuilder.Entity<DonorModel>(entity =>
        // {
        //     entity.HasKey(e => e.Id);
        //     entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        //     entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
        //     entity.Property(e => e.Phone).HasMaxLength(20);
        //     entity.HasMany(e => e.Gifts)
        //         .WithOne(e => e.Donor)
        //         .HasForeignKey(e => e.DonorId)
        //         .OnDelete(DeleteBehavior.Restrict);
        // });

        // CategoryModel configuration
        modelBuilder.Entity<CategoryModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasMany(e => e.Gifts)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // GiftModel configuration
        modelBuilder.Entity<GiftModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Price).HasPrecision(18, 2).IsRequired();

            entity.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Gift_Price_Positive",
                    "[Price] > 0"
                );
            });

            entity.HasOne(e => e.Category)
                   .WithMany(c => c.Gifts)
                   .HasForeignKey(e => e.CategoryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
                   entity.HasOne(g => g.Donor)
      .WithMany() // אין לנו אוסף Gifts בתוך UserModel כרגע
      .HasForeignKey(g => g.DonorId)
      .IsRequired()
      .OnDelete(DeleteBehavior.Restrict);
        //     entity.HasOne(e => e.Donor)
        //            .WithMany(d => d.Gifts)
        //            .HasForeignKey(e => e.DonorId)
        //            .IsRequired()
        //            .OnDelete(DeleteBehavior.Restrict);
        entity.Property(e => e.ImageUrl).HasMaxLength(1000);
         });

        // UserModel configuration
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Role)
                  .HasConversion<int>()
                  .IsRequired();
            entity.HasMany(e => e.Cart)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // PurchaseModel configuration
        modelBuilder.Entity<PurchaseModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Qty).IsRequired();
            entity.HasIndex(p => new { p.UserId, p.Status });


            entity.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Purchase_Qty_Positive",
                    "[Qty] > 0"
                );
            });

            entity.Property(e => e.Status)
                .HasConversion<int>()
                .IsRequired()
                .HasDefaultValue(Status.Draft);

            entity.Property(e => e.PurchaseDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(e => e.User)
                   .WithMany(u => u.Cart)
                   .HasForeignKey(e => e.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Gift)
                   .WithMany()
                   .HasForeignKey(e => e.GiftId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        });


        // WinningModel configuration
        modelBuilder.Entity<WinningModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.User)
                     .WithMany()
                     .HasForeignKey(e => e.WinnerId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Gift)
                   .WithMany()
                   .HasForeignKey(e => e.GiftId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        });

        // // RaffleModel configuration
        // modelBuilder.Entity<RaffleModel>(entity =>
        // {
        //     entity.HasKey(e => e.Id);

        //     entity.Property(e => e.RaffleDate)
        //         .IsRequired()
        //         .HasDefaultValueSql("GETUTCDATE()");

        //     entity.Property(e => e.Description)
        //         .IsRequired()
        //         .HasMaxLength(500);

        //     entity.Property(e => e.IsActive)
        //         .IsRequired()
        //         .HasDefaultValue(true);

        //     entity.HasMany(e => e.Gifts)
        //           .WithOne()
        //           .HasForeignKey("RaffleId")
        //           .OnDelete(DeleteBehavior.Restrict);

        //     entity.HasMany(e => e.Winnings)
        //           .WithOne(e => e.Raffle)
        //           .HasForeignKey(e => e.RaffleId)
        //           .OnDelete(DeleteBehavior.Cascade);
        // });
    }
}