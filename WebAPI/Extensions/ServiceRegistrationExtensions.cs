using BusinessLayer.Services.AuthService;
using BusinessLayer.Services.CountryService;
using BusinessLayer.Services.DashboardService;
using BusinessLayer.Services.LogService;
using BusinessLayer.Services.RoleService;
using BusinessLayer.Services.UserService;
using BusinessLayer.Services.WareHouseService;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Repositories;
using DomainLayer.Utilities;

namespace WebAPI.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddScoped<IWareHouseRepository, WareHouseRepository>();
            services.AddScoped<IWareHouseItemsRepository, WareHouseItemsRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILogsRepository, LogsRepository>();

            

            services.AddScoped<WareHouseService>();
            services.AddScoped<WareHouseItemsService>();
            services.AddScoped<CountryService>();
            services.AddScoped<RoleService>();
            services.AddScoped<UserService>();
            services.AddScoped<PasswordHasher>();
            services.AddScoped<AuthService>();
            services.AddScoped<LogsService>();
            services.AddScoped<DashboardService>();

        }
    }
}
