using Business.Authorization;
using Business.Managers.UserRoleEvent.Delete;
using Business.Managers.UserRoleEvent.Insert;
using Business.Managers.UserRoleEvent.Select;
using Business.Managers.UserRoleEvent.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserRoleApiController : BaseController
    {
        [HttpGet, AuthorizationRole("Admin")]
        public async Task<IActionResult> GetAll() => Ok(await Mediator.Send(new GetUserRoleQuery()));
        [HttpGet, AuthorizationRole("Admin")]
        public async Task<IActionResult> GetIDUserRole([FromBody] GetUserRoleIDQuery param) => Ok(await Mediator.Send(param));

        [HttpPost, AuthorizationRole("Admin,Insert")]
        public async Task<IActionResult> InsertUserRole([FromBody] InsertUserRoleCommandQuery param) => Ok(await Mediator.Send(param));

        [HttpPut, AuthorizationRole("Admin,Update")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleCommandQuery param) => Ok(await Mediator.Send(param));
        [HttpDelete, AuthorizationRole("Admin,Delete")]
        public async Task<IActionResult> DeleteUserRole([FromBody] DeleteUserRoleCommandQuery param) => Ok(await Mediator.Send(param));
    }
}