using Business.Repo;
using Entities;
using MediatR;

namespace Business.Managers.RoleEvent.Delete
{
    public partial class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandQuery, Role>
    {
        private readonly EFRoleRepository _roleService;

        public DeleteRoleCommandHandler() => _roleService ??= new();
        public async Task<Role> Handle(DeleteRoleCommandQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(await _roleService.DeleteAsync(new Role() { Id = request.RoleID }));
        }
    }
}