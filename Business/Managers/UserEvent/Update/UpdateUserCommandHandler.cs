using Business.Repo;
using Entities;
using MediatR;

namespace Business.Managers.UserEvent.Update
{
    public partial class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandQuery, User>
    {
        private readonly EFUserRepository _userService;

        public UpdateUserCommandHandler() => _userService ??= new();
        public async Task<User> Handle(UpdateUserCommandQuery request, CancellationToken cancellationToken)
        {
            var GetModel = await _userService.GetListAsync(x => x.Id == request.UserID);
            if (GetModel.ModelCount > 0)
            {
                GetModel.ListResponseModel.FirstOrDefault().Name = request.Name;
                GetModel.ListResponseModel.FirstOrDefault().Surname = request.Surname;
                GetModel.ListResponseModel.FirstOrDefault().Username = request.Username;
                GetModel.ListResponseModel.FirstOrDefault().Password = request.Password;
                GetModel.ListResponseModel.FirstOrDefault().Email = request.Email;
            }
            return await Task.FromResult(await _userService.UpdateAsync(GetModel.ListResponseModel.FirstOrDefault()));
        }
    }
}