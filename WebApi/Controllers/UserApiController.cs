using Business.Authorization;
using Business.Managers.UserEvent.Delete;
using Business.Managers.UserEvent.Insert;
using Business.Managers.UserEvent.Select;
using Business.Managers.UserEvent.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserApiController : BaseController
    {
        [HttpGet, AuthorizationRole("Admin")]
        public async Task<IActionResult> GetAll([FromQuery] GetUserQuery param) => Ok(await Mediator.Send(param));
        [HttpGet, AuthorizationRole("Admin")]
        public async Task<IActionResult> GetIDUser([FromQuery] GetUserIDQuery param) => Ok(await Mediator.Send(param));

        [HttpPost, AuthorizationRole("Admin,Insert")]
        public async Task<IActionResult> InsertUser([FromBody] InsertUserCommandQuery param) => Ok(await Mediator.Send(param));

        [HttpPut, AuthorizationRole("Admin,Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandQuery param) => Ok(await Mediator.Send(param));
        [HttpDelete, AuthorizationRole("Admin,Delete")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommandQuery param) => Ok(await Mediator.Send(param));
    }
}