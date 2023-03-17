using team_status_undefined_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace team_status_undefined_backend.Migrations;

public class BarberDbContext : DbContext
{
    public DbSet<Barber> Barber { get; set; }
    public DbSet<BarberImageLink> BarberImageLinks { get; set; }

    public BarberDbContext(DbContextOptions<BarberDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Barber>(entity =>
        {
            entity.HasKey(e => e.BarberId);
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.Address).IsRequired();
            entity.Property(e => e.City).IsRequired();
            entity.Property(e => e.State).IsRequired();
            entity.Property(e => e.PhoneNumber).IsRequired();
            entity.Property(e => e.LicenseNumber).IsRequired();
            entity.Property(e => e.ProfilePic).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(e => e.Password).IsRequired();

            entity.HasMany<BarberImageLink>(b => b.ImageLinks).WithOne(i => i.Barber);
        });
        modelBuilder.Entity<BarberImageLink>(entity =>
        {
            entity.HasKey(e => e.BarberImageLinkId);
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.BarberId).IsRequired();
            entity.Property(e => e.ImageUrl).IsRequired();
        });
    }
}
