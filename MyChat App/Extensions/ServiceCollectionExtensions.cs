using Domain.Interfaces.Base;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MyChat_App.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime  
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    sqlOptions =>
                    {

                    });
            });

            return services;
        }

        // Add repositories base
        public static IServiceCollection AddRepositoriesBase(this IServiceCollection services)
        {
            return services
                    .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

        // Add unitOfWork
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return
                services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // Add instances of in-use services
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IWorkService, WorkService>();
        }

        // Add current user info
        public static void AddUserInfo(this IServiceCollection services)
        {
            services.AddScoped(serviceProvider =>
            {
                var httpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;

                return httpContext?.CurrentUser();
            });
        }
    }
}
