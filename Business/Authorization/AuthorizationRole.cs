using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Business.Authorization;

public class AuthorizationRole : ActionFilterAttribute
{
    private string[] _roles;
    private readonly HttpContextAccessor _httpContextAccessor;
    public AuthorizationRole(string roles)
    {
        _httpContextAccessor ??= new();
        _roles = roles.ToString().Split(',');
    }
    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        List<string> roleClaims = await _httpContextAccessor.HttpContext.User.ClaimRoles();
        foreach (var role in _roles)
        {
            if (roleClaims.Contains(role))
                return;
        }
        throw new Exception("YETKİSİZ KULLANICI!");
    }
}