using Business.Repo;
using Entities;
using MediatR;

namespace Business.Managers.UserEvent.Insert
{
    public partial class InsertUserCommandHandler : IRequestHandler<InsertUserCommandQuery, User>
    {
        private readonly EFUserRepository _userService;

        public InsertUserCommandHandler() => _userService ??= new();

        public async Task<User> Handle(InsertUserCommandQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(await _userService.AddAsync(new User()
            {
                Name = request.Name,
                Surname = request.Surname,
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            }));
        }
    }
}