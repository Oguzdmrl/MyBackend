using Business.Repo;
using Core.Results;
using Entities;
using MediatR;

namespace Business.Managers.UserEvent.Select
{
    public partial class GetUserHandler : IRequestHandler<GetUserQuery, ResponseDataResult<User>>
    {
        private readonly EFUserRepository _userService;

        public GetUserHandler() => _userService ??= new();

        public async Task<ResponseDataResult<User>> Handle(GetUserQuery request, CancellationToken cancellation)
        {
            return await Task.FromResult(await _userService.GetListAsync(withDeleted: request.WithDeleted));
        }
    }
    public partial class GetUserIDHandler : IRequestHandler<GetUserIDQuery, ResponseDataResult<User>>
    {
        private readonly EFUserRepository _userService;

        public GetUserIDHandler() => _userService ??= new();
        public async Task<ResponseDataResult<User>> Handle(GetUserIDQuery request, CancellationToken cancellation)
        {
            return await Task.FromResult(await _userService.GetAsnyc(x => x.Id == request.UserID, withDeleted: request.WithDeleted));
        }
    }
}