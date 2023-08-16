using Business.Repo;
using Entities;
using MediatR;

namespace Business.Managers.AuthEvent.Register
{
    public partial class InsertRegisterHandler : IRequestHandler<RegisterQuery, User>
    {
        private readonly EFUserRepository _userService;

        public InsertRegisterHandler() => _userService ??= new();

        public async Task<User> Handle(RegisterQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(await _userService.AddAsync(new User()
            {
                Name = request.Name,
                Surname = request.Surname,
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                CreatedDate = DateTime.UtcNow,
            }));
        }
    }
}