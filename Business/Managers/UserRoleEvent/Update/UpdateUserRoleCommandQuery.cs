using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.UserRoleEvent.Update
{
    public partial class UpdateUserRoleCommandQuery : IRequest<UserRole>
    {
        public Guid UserRoleID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
    }
}