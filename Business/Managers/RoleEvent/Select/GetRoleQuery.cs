using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.RoleEvent.Select
{
    public partial class GetRoleQuery : IRequest<ResponseDataResult<Role>>
    {
    }
    public partial class GetRoleIDQuery : IRequest<ResponseDataResult<Role>>
    {
        public Guid RoleID { get; set; }
    }
}