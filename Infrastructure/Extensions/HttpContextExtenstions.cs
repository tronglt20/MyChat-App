using Domain.Base.Interfaces;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Extensions
{
    public static class HttpContextExtenstions
    {
        public static string GetClaimValue(HttpContext context, string claimName)
        {
            if (context?.User?.Identity?.IsAuthenticated == true)
            {
                return context.User.Claims.FirstOrDefault(_ => _.Type == claimName)?.Value;
            }

            return default;
        }
        public static int UserId(this HttpContext context)
        {
            int.TryParse(GetClaimValue(context, AppClaimType.UserId), out var userId);

            return userId;
        }

        public static string UserName(this HttpContext context)
        {
            return GetClaimValue(context, AppClaimType.UserName);
        }

        public static string UserEmail(this HttpContext context)
        {
            return GetClaimValue(context, AppClaimType.UserEmail);
        }

        public static IUserInfo CurrentUser(this HttpContext context)
        {
            return new UserInfo(context.UserId()
                , context.UserName()
                , context.UserEmail());
        }
    }
}
