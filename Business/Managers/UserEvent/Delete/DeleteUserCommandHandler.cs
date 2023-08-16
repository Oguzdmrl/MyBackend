using Business.Repo;
using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.UserEvent.Delete
{
    public partial class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandQuery, User>
    {
        private readonly EFUserRepository _userService;
        public DeleteUserCommandHandler() => _userService ??= new();
        public async Task<User> Handle(DeleteUserCommandQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(await _userService.DeleteAsync(new User() { Id = request.UserID }));
        }
    }
}