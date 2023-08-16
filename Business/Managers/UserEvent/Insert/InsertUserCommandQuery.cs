using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.UserEvent.Insert
{
    public partial class InsertUserCommandQuery : IRequest<User>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}