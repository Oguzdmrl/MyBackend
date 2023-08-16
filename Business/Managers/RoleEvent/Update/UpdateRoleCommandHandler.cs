using Business.Repo;
using Entities;
using MediatR;

namespace Business.Managers.RoleEvent.Update
{
    public partial class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandQuery, Role>
    {
        private readonly EFRoleRepository _roleService;

        public UpdateRoleCommandHandler() => _roleService ??= new();
        public async Task<Role> Handle(UpdateRoleCommandQuery request, CancellationToken cancellationToken)
        {
            var GetModel = await _roleService.GetListAsync(x => x.Id == request.RoleID);
            if (GetModel.ModelCount > 0)
            {
                GetModel.ListResponseModel.FirstOrDefault().Name = request.Name;
                GetModel.ListResponseModel.FirstOrDefault().Description = request.Description;
            }
            return await Task.FromResult(await _roleService.UpdateAsync(GetModel.ListResponseModel.FirstOrDefault()));
        }
    }
}