using Business.Repo;
using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.UserRoleEvent.Delete
{
    public partial class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommandQuery, UserRole>
    {
        private readonly EFUserRoleRepository _userRoleService;

        public DeleteUserRoleCommandHandler() => _userRoleService ??= new();
        public async Task<UserRole> Handle(DeleteUserRoleCommandQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(await _userRoleService.DeleteAsync(new UserRole() { Id = request.UserRoleID }));
        }
    }
}