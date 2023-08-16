using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.UserRoleEvent.Delete
{
    public partial class DeleteUserRoleCommandQuery : IRequest<UserRole>
    {
        public Guid UserRoleID { get; set; }
    }
}