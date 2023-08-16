using System.Security.Claims;

namespace Business.Authorization;

public static class ClaimsPrincipalExtensions
{
    public static async Task<List<string>> Claims(this ClaimsPrincipal claimsPrincipal, string claimType) => await Task.FromResult(claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList());
    public static async Task<List<string>> ClaimRoles(this ClaimsPrincipal claimsPrincipal) => await Task.FromResult(await claimsPrincipal?.Claims(ClaimTypes.Role));
}