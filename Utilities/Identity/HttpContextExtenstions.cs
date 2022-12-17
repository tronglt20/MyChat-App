using Microsoft.AspNetCore.Http;
using Utilities.DTOs;
using Utilities.Interfaces;

namespace Utilities
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

        public static ICurrentUserInfo CurrentUser(this HttpContext context)
        {
            return new CurrentUserInfo(context.UserId()
                , context.UserName()
                , context.UserEmail());
        }
    }
}
