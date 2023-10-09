using HMS.Business.Repositories;
using HMS.Domain.Entities;
using HMS.Domain.Interfaces.Repositories;
using HMS.Infrastructure.Repositories;
using HMS.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace HMS.Infrastructure
{
    public static class DependencyContainer
    {
        /// <summary>
        /// Connects our interfaces and their implementations from multiple projects 
        /// into single point of reference. That is the purpose of IoC layer.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(IServiceCollection services)
        {
            // Register Repo
            services.AddScoped<ISiteUserRepository, SiteUserRepository>();
            services.AddScoped<ISiteRoleRepository, SiteRoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IExternalLoginRepository, ExternalLoginRepository>();
            services.AddScoped<IHallrepository, HallReository>();
            services.AddScoped<ICollageRepository, CollageRepository>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IDashBordRepository, DashBoredRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();

            services.AddTransient<IUserStore<SiteUser>, UserStore>();
            services.AddTransient<IRoleStore<SiteRole>, RoleStore>();

            services.AddIdentity<SiteUser, SiteRole>()
                    .AddDefaultTokenProviders();

            // Add application services.
            // services.AddTransient<IEmailService, EmailService>();
        }
    }
}
