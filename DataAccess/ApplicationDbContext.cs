using System.Reflection;
using DataAccess.Configurations;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DataAccess;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, Role, Guid>(options)
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<ApartmentImage> ApartmentImages { get; set; }
    public DbSet<ApartmentDiscount> ApartmentDiscounts { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Suspension> Suspensions { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<PaymentLog> PaymentLogs { get; set; }
    public DbSet<UserFollows> Followings { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.CustomizeIdentityTablesNames();
        SeedRoles(modelBuilder);
        SeedPalestineCities(modelBuilder);
        //SeedUsers(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<Role>().HasData(
            new Role
            {
                Id = new Guid("99CF7D7D-1D6E-435D-A94C-8F359AE200C3"),
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new Role
            {
                Id = new Guid("EFE3F972-2DB9-47D2-857B-CC08FC6CB1AC"),
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            },
            new Role
            {
                Id = new Guid("5D72467F-B037-4886-A3A7-F047FFB4AD52"),
                Name = "Owner",
                NormalizedName = "OWNER"
            }
        );
    }
    private static void SeedPalestineCities(ModelBuilder builder)
    {
        builder.Entity<City>().HasData(
            new City
            {
                Id = new Guid("FB460870-C643-4CBC-92FC-28D86BBF6BDE"),
                Name = "Ramallah",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new City
            {
                Id = new Guid("0F77FEAC-28E6-4741-96A7-F954AB70D80B"),
                Name = "Nablus",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new City
            {
                Id = new Guid("57ED738B-9A40-4B4C-A23C-EC24870D7F58"),
                Name = "Hebron",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new City
            {
                Id = new Guid("701AE20F-76EA-4316-854F-6616ACA7C6A7"),
                Name = "Jerusalem",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new City
            {
                Id = new Guid("A4225617-0AEB-4FAF-A748-EF6A6A31D94E"),
                Name = "Gaza",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new City
            {
                Id = new Guid("8D90E7A2-198E-426B-ABBB-7B53B751EC2C"),
                Name = "Jenin",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new City
            {
                Id = new Guid("2BFE8B81-9C44-4C0C-AA1E-B9AA68D15CE8"),
                Name = "Tulkarm",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new City
            {
                Id = new Guid("F2E3BD05-84FB-42BD-A744-A4050316A90B"),
                Name = "Qalqilya",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new City
            {
                Id = new Guid("D49A1339-07E3-4EA2-94E1-237BC37BC511"),
                Name = "Bethlehem",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new City
            {
                Id = new Guid("22DD888C-AE72-451E-9C58-1AC2F0547C2F"),
                Name = "Tubas",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new City
            {
                Id = new Guid("F0B8F861-AF7E-48BE-A91A-8F4F8CBC6C62"),
                Name = "Salfit",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new City
            {
                Id = new Guid("2AAFF023-58B8-4C89-A898-171573A15739"),
                Name = "Jericho",
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            });
    }
}