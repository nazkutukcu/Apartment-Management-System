using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.BuildingMonthlyBills;
using SiteYonetimSistemi.API.Models.Enums;
using SiteYonetimSistemi.API.Models.MonthlyDues;
using SiteYonetimSistemi.API.Models.Payments;
using SiteYonetimSistemi.API.Models.UserApartments;
using SiteYonetimSistemi.API.Models.Users;

namespace SiteYonetimSistemi.API.Models
{
    public class AppDbContext : IdentityDbContext<User, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<MonthlyDue> MonthlyDues { get; set; }
        public DbSet<MonthlyBill> MonthlyBills { get; set; }
        public DbSet<UserApartment> UserApartments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().Property(p => p.Amount).HasPrecision(18, 2);
            modelBuilder.Entity<MonthlyDue>().Property(p => p.Amount).HasPrecision(18, 2);
            modelBuilder.Entity<MonthlyBill>().Property(p => p.Amount).HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
