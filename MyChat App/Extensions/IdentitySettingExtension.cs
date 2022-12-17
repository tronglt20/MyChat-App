using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Utilities;
using Utilities.DTOs;
using Utilities.Interfaces;

namespace MyChat_App.Extensions
{
    public static class IdentitySettingExtension
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.GetSection("JwtTokenSettings").Get<JwtTokenSettings>();

            services.AddScoped<IJwtGenerator, JwtGenerator>();

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.SaveToken = true;
                cfg.RequireHttpsMetadata = false;
                cfg.Events = GetTokenValidationEvents();
                cfg.TokenValidationParameters = GetTokenValidationParameters(configuration);
            });

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }

        private static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtTokenSettings.Issuer,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenSettings.SecretKey)),

                ValidateAudience = true,
                ValidAudience = JwtTokenSettings.Audience,

                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
        }

        private static JwtBearerEvents GetTokenValidationEvents()
        {
            return new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        }
    }
}
