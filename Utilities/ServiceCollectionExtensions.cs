﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Utilities.Interfaces;

namespace Utilities
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTokenGenerator(this IServiceCollection services)
        {
            return services.AddSingleton<ITokenGenerator, TokenGenerator>();
        }

        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            return services.AddSingleton<IEmailSender, EmailSender>();
        }

        public static IServiceCollection AddCurrentUserInfo(this IServiceCollection services)
        {
            services.AddScoped(serviceProvider =>
            {
                var httpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;

                return httpContext?.CurrentUser();
            });

            return services;
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
