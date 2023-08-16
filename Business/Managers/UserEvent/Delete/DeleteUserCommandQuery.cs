using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.UserEvent.Delete
{
    public partial class DeleteUserCommandQuery : IRequest<User>
    {
        public Guid UserID { get; set; }
    }
}