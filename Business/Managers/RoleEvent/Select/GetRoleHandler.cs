using Business.Repo;
using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.RoleEvent.Select
{
    public partial class GetRoleHandler : IRequestHandler<GetRoleQuery, ResponseDataResult<Role>>
    {
        private readonly EFRoleRepository _roleService;

        public GetRoleHandler() => _roleService ??= new();
        public async Task<ResponseDataResult<Role>> Handle(GetRoleQuery request, CancellationToken cancellation)
        {
            return await Task.FromResult(await _roleService.GetListAsync());
        }
    }
    public partial class GetRoleIDHandler : IRequestHandler<GetRoleIDQuery, ResponseDataResult<Role>>
    {
        private readonly EFRoleRepository _roleService;

        public GetRoleIDHandler() => _roleService ??= new();
        public async Task<ResponseDataResult<Role>> Handle(GetRoleIDQuery request, CancellationToken cancellation)
        {
            return await Task.FromResult(await _roleService.GetAsnyc(x => x.Id == request.RoleID));
        }
    }
}