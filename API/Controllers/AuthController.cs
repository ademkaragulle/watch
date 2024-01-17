using API.Data;
using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService authService;
        DataContext db = new DataContext();

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("/LoginUser")]
        [AllowAnonymous]
        public async Task<ActionResult<UserLoginResponse>> LoginUserAsync([FromBody] UserLoginRequest request)
        {
            var result = await authService.LoginUserAsync(request);
            return result;
        }


        [HttpPost("/registerUser")]
        [AllowAnonymous]
        public async Task<ActionResult<Boolean>> RegisterUserAsync([FromBody] RegisterUserRequest request)
        {
            User newUser = new User();
            var user = db.User.FirstOrDefault(x => x.Username == request.Username);
            if (user == null)
            {
                newUser.Username = request.Username;
                newUser.Password = request.Password;
                newUser.Email = request.Email;
                db.User.Add(newUser);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
