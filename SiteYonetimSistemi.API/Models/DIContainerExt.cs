using SiteYonetimSistemi.API.Models.Admin;
using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.BuildingMonthlyBills;
using SiteYonetimSistemi.API.Models.MonthlyDues;
using SiteYonetimSistemi.API.Models.Payments;
using SiteYonetimSistemi.API.Models.UnitOfWorks;
using SiteYonetimSistemi.API.Models.UserApartments;
using SiteYonetimSistemi.API.Models.Users;

namespace SiteYonetimSistemi.API.Models
{
    public static class DIContainerExt
    {
        public static void AddDIContainer(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApartmentService, ApartmentService>();
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IMonthlyDueRepository, MonthlyDueRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IMonthlyBillRepository, MonthlyBillRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserApartmentService, UserApartmentService>();
            services.AddScoped<IUserApartmentRepository, UserApartmentRepository>();
        }
    }
}
