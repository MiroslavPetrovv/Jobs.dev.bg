using System.Security.Claims;

namespace JobApplications.Extensions
{
    public static class  ClaimsPrincipalExtensions
    {
        public static string? GetId(this ClaimsPrincipal user)
                        => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

       // public static bool IsAdmin(this ClaimsPrincipal user)
           //=> user.IsInRole(AdminRoleName);
    }
}
