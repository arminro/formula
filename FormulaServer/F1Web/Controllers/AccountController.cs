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

namespace F1Web.Controllers
{
    [ApiController]
    [Authorize]
    [EnableCors("Cors")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Route("api/[controller]/login")]
        [AllowAnonymous]        
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInDb = await _userManager.FindByNameAsync(viewModel.Username);
                    if (userInDb == null)
                    {
                        return NotFound($"The user with the username {viewModel.Username.ToUpper()} is not registered");
                    }

                    var signInResult = await _signInManager
                        .PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
                   
                    if (signInResult.Succeeded)
                    {
                        return Ok(CreateLoggedInUserResponse(userInDb, _tokenService.GenerateToken(userInDb.Id)));
                    }

                    // for security reasons, this is bad practice, but it shows how many ways the app can respond
                    return Unauthorized($"Incorrect password for user {viewModel.Username.ToUpper()}");
                }
                return BadRequest(ModelState);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Unexpected error during login of user {viewModel.Username}");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Unexpected error during logging out");
            }
        }

        private UserViewModel CreateLoggedInUserResponse(User user, string token)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                Name = user.UserName,
                Token = token
            };
        }
    }
}