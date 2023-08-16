using Business.Repo;
using Entities;
using MediatR;

namespace Business.Managers.UserRoleEvent.Update
{
    public partial class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommandQuery, UserRole>
    {
        private readonly EFUserRoleRepository _userRoleService;

        public UpdateUserRoleCommandHandler() => _userRoleService ??= new();
        public async Task<UserRole> Handle(UpdateUserRoleCommandQuery request, CancellationToken cancellationToken)
        {
            var GetModel = await _userRoleService.GetAsnyc(x => x.Id == request.UserRoleID);
            if (GetModel.ModelCount > 0)
            {
                GetModel.ListResponseModel.FirstOrDefault().UserId = request.UserID;
                GetModel.ListResponseModel.FirstOrDefault().RoleId = request.RoleID;
            }
            return await Task.FromResult(await _userRoleService.UpdateAsync(GetModel.ListResponseModel.FirstOrDefault()));
        }
    }
}