using team_status_undefined_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace team_status_undefined_backend.Migrations;

public class BarberDbContext : DbContext
{
    public DbSet<Barber> Barber { get; set; }
    public BarberDbContext(DbContextOptions<BarberDbContext> options)
        : base(options)
    {
    }


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
        });

        modelBuilder.Entity<Barber>().HasData
        (
         new Barber 
         { 
            BarberId = 1,
            FirstName = "Austin",
            LastName = "Fogle",
            Address = "123 A Road",
            City = "Dayton",
            State = "Ohio",
            PhoneNumber = 0123456789,
            LicenseNumber = "C123456",
            ProfilePic = "https://images.unsplash.com/photo-1517832606299-7ae9b720a186?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=772&q=80",
            Description = "Hi everyone! I've got openings, who needs a trim?",
         }
        );
    }
}