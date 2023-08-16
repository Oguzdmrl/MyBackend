using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.UserEvent.Select
{
    public partial class GetUserQuery : IRequest<ResponseDataResult<User>>
    {
        public bool WithDeleted { get; set; }
    }
    public partial class GetUserIDQuery : IRequest<ResponseDataResult<User>>
    {
        public bool WithDeleted { get; set; }
        public Guid UserID { get; set; }
    }
}