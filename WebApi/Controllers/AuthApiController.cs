using Business.Managers.AuthEvent.Login;
using Business.Managers.AuthEvent.Register;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthApiController : BaseController
    {
        [HttpPost] public async Task<IActionResult> Login([FromBody] LoginQuery param) => Ok(await Mediator.Send(param));
        [HttpPost] public async Task<IActionResult> Register([FromBody] RegisterQuery param) => Ok(await Mediator.Send(param));
    }
}