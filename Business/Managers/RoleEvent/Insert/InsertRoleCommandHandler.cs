using Business.Repo;
using Entities;
using MediatR;

namespace Business.Managers.RoleEvent.Insert
{
    public partial class InsertRoleCommandHandler : IRequestHandler<InsertRoleCommandQuery, Role>
    {
        private readonly EFRoleRepository _roleService;

        public InsertRoleCommandHandler() => _roleService ??= new();

        public async Task<Role> Handle(InsertRoleCommandQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(await _roleService.AddAsync(new Role() { Name = request.Name, Description = request.Description }));
        }
    }
}