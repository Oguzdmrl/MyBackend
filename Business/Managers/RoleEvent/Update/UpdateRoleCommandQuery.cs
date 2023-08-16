using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.RoleEvent.Update
{
    public partial class UpdateRoleCommandQuery : IRequest<Role>
    {
        public Guid RoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}