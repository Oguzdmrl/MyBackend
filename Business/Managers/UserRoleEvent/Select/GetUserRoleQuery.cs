using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.UserRoleEvent.Select
{
    public partial class GetUserRoleQuery : IRequest<ResponseDataResult<UserRole>>
    {
    }
    public partial class GetUserRoleIDQuery : IRequest<ResponseDataResult<UserRole>>
    {
        public Guid UserRoleID { get; set; }
    }
}