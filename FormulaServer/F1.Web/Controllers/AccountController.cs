using System;
using System.Threading.Tasks;
using F1.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using F1Web.Security;
using F1Web.ViewModels;
using F1Web.Service.Inerfaces;
using F1Web.Service.Implementations;

namespace F1Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("Cors")]
    public class AccountController : ControllerBase
    {

        private readonly ITokenService _tokenService;
        private readonly IAuthenticationService<User> _authService;

        public AccountController(IAuthenticationService<User> authService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _authService = authService;
        }


        [HttpPost("login")]
        [AllowAnonymous]        
        public async Task<IActionResult> Login([FromBody] LoginDto loginObject)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.AttemptLoginAsync(loginObject);
                if (result.Result == AuthResults.Success)
                {
                    return Ok(CreateLoggedInUserResponse(result.User, _tokenService.GenerateToken(result.User.Id)));
                }

                // for security reasons, this is bad practice, but it shows how many ways the app can respond
                return Unauthorized($"Incorrect password for user {loginObject.Username.ToUpper()}");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.AttemptLogoutAsync();
            return Ok(result);
        }

        private UserDto CreateLoggedInUserResponse(User user, string token)
        {
            return new UserDto()
            {
                Id = user.Id,
                Name = user.UserName,
                Token = token
            };
        }
    }
}