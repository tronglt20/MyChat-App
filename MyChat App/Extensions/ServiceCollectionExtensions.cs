using Domain.Base.Utilities;
using Domain.Interfaces.Base;
using Infrastructure;
using Infrastructure.Extensions;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using MyChat_App.Services.IAM;
using System.Reflection;

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

        public static IServiceCollection AddRepositoriesBase(this IServiceCollection services)
        {
            return services
                    .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
                    .AddImplementationInterfaces(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return
                services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IAMService>();
        }

        public static void AddUserInfo(this IServiceCollection services)
        {
            services.AddScoped(serviceProvider =>
            {
                var httpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;

                return httpContext?.CurrentUser();
            });
        }

        public static void AddTokenGenerator(this IServiceCollection services)
        {
            services.AddSingleton<ITokenGenerator, TokenGenerator>();
        }

        public static IServiceCollection AddImplementationInterfaces(this IServiceCollection services
            , Type interfaceType
            , Type implementAssemblyType)
        {
            var implementTypes = Assembly.GetAssembly(implementAssemblyType).GetTypes().Where(_ =>
                        _.IsClass
                        && !_.IsAbstract
                        && !_.IsInterface
                        && !_.IsGenericType
                        && _.GetInterface(interfaceType.Name) != null);

            foreach (var implementType in implementTypes)
            {
                var mainInterfaces = implementType
                    .GetInterfaces()
                    .Where(_ => _.GenericTypeArguments.Count() == 0);

                foreach (var mainInterface in mainInterfaces)
                {
                    services.AddScoped(mainInterface, implementType);
                }
            }

            return services;
        }

    }
}
