using Business.Authorization;
using Business.Managers.RoleEvent.Delete;
using Business.Managers.RoleEvent.Insert;
using Business.Managers.RoleEvent.Select;
using Business.Managers.RoleEvent.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoleApiController : BaseController
    {
        
        [HttpGet, AuthorizationRole("Admin")]
        public async Task<IActionResult> GetAll() => Ok(await Mediator.Send(new GetRoleQuery()));
        [HttpGet, AuthorizationRole("Admin")]
        public async Task<IActionResult> GetIDRole([FromBody] GetRoleIDQuery param) => Ok(await Mediator.Send(param));

        [HttpPost, AuthorizationRole("Admin,Insert")]
        public async Task<IActionResult> InsertRole([FromBody] InsertRoleCommandQuery param) => Ok(await Mediator.Send(param));

        [HttpPut, AuthorizationRole("Admin,Update")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommandQuery param) => Ok(await Mediator.Send(param));
        [HttpDelete, AuthorizationRole("Admin,Delete")]
        public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleCommandQuery param) => Ok(await Mediator.Send(param));
    }
}