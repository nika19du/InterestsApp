using InYourInterest.Common;
using System.Security.Claims;

namespace InYourInterest.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).ToString();
        }

        public static bool IsAdministrator(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.IsInRole(GlobalConstants.AdministratorRoleName);
    }
}
