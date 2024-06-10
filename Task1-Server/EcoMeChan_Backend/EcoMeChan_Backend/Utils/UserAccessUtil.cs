// UserAccessUtil.cs
using EcoMeChan.Enums;
using Microsoft.AspNetCore.Http;

namespace EcoMeChan.Utils
{
    public static class UserAccessUtil
    {
        public static Role? GetCurrentUserRole(IHttpContextAccessor httpContextAccessor)
        {
            var roleCookie = httpContextAccessor.HttpContext.Request.Cookies["role"];

            if (string.IsNullOrEmpty(roleCookie))
            {
                return null;
            }

            if (Enum.TryParse(roleCookie, out Role role))
            {
                return role;
            }

            return null;
        }
    }
}