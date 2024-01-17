using API.Data;
using API.Models;
using API.Services.Interfaces;
using API.Services.Models;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        readonly ITokenService tokenService;
        DataContext db = new DataContext();

        public AuthService(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            UserLoginResponse response = new();

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentNullException(nameof(request));
            }
            var user = db.User.FirstOrDefault(x => x.Username == request.Username && request.Password == x.Password);
            if (user != null)
            {
                var generatedTokenInformation = await tokenService.GenerateToken(new GenerateTokenRequest { Username = request.Username });
                response.AuthenticateResult = true;
                response.AuthToken = generatedTokenInformation.Token;
                response.AccessTokenExpireDate = generatedTokenInformation.TokenExpireDate;
                response.Username = user.Username;
                response.Password=user.Password;
                response.Email = user.Email;
            }
            return response;
        }
    }
}
