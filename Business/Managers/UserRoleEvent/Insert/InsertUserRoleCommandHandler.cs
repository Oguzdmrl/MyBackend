using Business.Repo;
using Entities;
using MediatR;

namespace Business.Managers.UserRoleEvent.Insert
{
    public partial class InsertUserRoleCommandHandler : IRequestHandler<InsertUserRoleCommandQuery, UserRole>
    {
        private readonly EFUserRoleRepository _userRoleService;

        public InsertUserRoleCommandHandler() => _userRoleService ??= new();

        public async Task<UserRole> Handle(InsertUserRoleCommandQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(await _userRoleService.AddAsync(new UserRole() { UserId = request.UserId, RoleId = request.RoleId }));
        }
    }
}