using Business.Repo;
using Core.Results;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Managers.UserRoleEvent.Select
{
    public partial class GetUserRoleHandler : IRequestHandler<GetUserRoleQuery, ResponseDataResult<UserRole>>
    {
        private readonly EFUserRoleRepository _userRoleService;

        public GetUserRoleHandler() => _userRoleService ??= new();
        public async Task<ResponseDataResult<UserRole>> Handle(GetUserRoleQuery request, CancellationToken cancellation)
        {
            return await Task.FromResult(await _userRoleService.GetListAsync(
                include: x => x.Include(x => x.User).Include(x => x.Role)
                ));
        }
    }
    public partial class GetUserRoleIDHandler : IRequestHandler<GetUserRoleIDQuery, ResponseDataResult<UserRole>>
    {
        private readonly EFUserRoleRepository _userRoleService;

        public GetUserRoleIDHandler() => _userRoleService ??= new();
        public async Task<ResponseDataResult<UserRole>> Handle(GetUserRoleIDQuery request, CancellationToken cancellation)
        {
            return await Task.FromResult(await _userRoleService.GetAsnyc(x => x.Id == request.UserRoleID));
        }
    }
}