using Business.Repo;
using Core.Helper;
using Core.Results;
using DataAccess.UnitOfWorks;
using Entities;
using Entities.JWTEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Business.Managers.AuthEvent.Login;

public partial class LoginHandler : IRequestHandler<LoginQuery, JWTResponse>
{
    private readonly EFUserRepository _userService;
    private readonly EFUserRoleRepository _userRoleService;
    private readonly IConfiguration configuration;
    public LoginHandler(IConfiguration configuration)
    {
        _userService = new();
        _userRoleService = new();
        this.configuration = configuration;
    }

    public async Task<JWTResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        JwtSettings jwtSettings = new()
        {
            Audience = configuration.GetSection("JwtSettings")["Audience"],
            Issuer = configuration.GetSection("JwtSettings")["Issuer"],
            SigningKey = configuration.GetSection("JwtSettings")["SigningKey"],
        };
        List<string> roles = new();

        var Model = await _userService.GetListAsync(
            predicate: x => x.Username == request.Username && x.Password == request.Password,
            cancellationToken: cancellationToken
            );

        if (Model.ModelCount == 0)
            return new JWTResponse() { ErrorMessage = "Kullancı Adı Veya Şifre Hatalı", Status = false };
        if (Model.ListResponseModel.FirstOrDefault().WithDeleted == true)
            return new JWTResponse() { ErrorMessage = "Kullancı Bulunamadı.", Status = false };
        var test = Model.ListResponseModel.FirstOrDefault().Id;

        var userRoleList = await _userRoleService.GetListAsync(
            x => x.UserId == Model.ListResponseModel.FirstOrDefault().Id,
            include: r => r.Include(r => r.Role)
            );

        if (userRoleList.Status == true)
            roles = userRoleList.ListResponseModel.Select(x => x.Role.Name).ToList();

        List<Claim> claims = new()
                {
                    new(ClaimTypes.Name, Model.ListResponseModel.FirstOrDefault().Username),
                    new(ClaimTypes.GivenName, Model.ListResponseModel.FirstOrDefault().Name),
                    new(ClaimTypes.Surname, Model.ListResponseModel.FirstOrDefault().Surname),
                    new(ClaimTypes.Email, Model.ListResponseModel.FirstOrDefault().Email),
                    new(ClaimTypes.NameIdentifier, Model.ListResponseModel.FirstOrDefault().Id.ToString()),
                    new(ClaimTypes.Authentication, Model.ListResponseModel.FirstOrDefault().Id.ToString()),
                };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        TokenModel token = JwtHelper.GetJwtToken(jwtSettings, claims);
        return new JWTResponse() { TokenResponse = token, Status = true };
    }
}