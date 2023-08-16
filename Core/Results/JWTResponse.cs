
using Entities.JWTEntity;

namespace Core.Results;

public sealed partial class JWTResponse
{
    public TokenModel TokenResponse { get; set; } = null;
    public string ErrorMessage { get; set; } = null;
    public bool Status { get; set; }

    public JWTResponse()
    {
    }
    public JWTResponse(TokenModel tokenResponse, bool status)
    {
        TokenResponse = tokenResponse;
        Status = status;
    }
    public JWTResponse(string errorMessage, bool status)
    {
        ErrorMessage = errorMessage;
        Status = status;
    }
}