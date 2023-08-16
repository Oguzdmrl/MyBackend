using Core.Results;
using MediatR;

namespace Business.Managers.AuthEvent.Login
{
    public  partial class LoginQuery : IRequest<JWTResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}