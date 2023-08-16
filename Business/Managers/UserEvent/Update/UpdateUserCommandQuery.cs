using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.UserEvent.Update
{
    public partial class UpdateUserCommandQuery : IRequest<User>
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}