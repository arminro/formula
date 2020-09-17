using F1.Data.Models;
using F1Web.Service.Inerfaces;
using F1Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1Web.Service.Implementations
{
    /// <summary>
    /// Generic class for authentication based on MS Identity.
    /// </summary>
    /// <typeparam name="T">The type of the user.</typeparam>
    /// <typeparam name="TKey">Teh type of the key of the user.</typeparam>
    public class IdentityAuthenticationService<T, TKey> : IAuthenticationService<T>
        where T: IdentityUser<TKey>
        where TKey: IEquatable<TKey>
    {
        private readonly UserManager<T> _userManager;
        private readonly SignInManager<T> _signInManager;

        public IdentityAuthenticationService(UserManager<T> userManager, SignInManager<T> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthResult<T>> AttemptLoginAsync(LoginDto login)
        {
            var userInDb = await _userManager.FindByNameAsync(login.Username);
            if (userInDb == null)
            {
                return new AuthResult<T>()
                {
                    Result = AuthResults.Failure
                };
            }

            var identitySigninResult = await _signInManager
                .PasswordSignInAsync(login.Username, login.Password, false, false);
            
            return new AuthResult<T>()
            {
                Result = identitySigninResult.Succeeded ? AuthResults.Success : AuthResults.Failure,
                User = userInDb
            };

        }

        public async Task<AuthResult<T>> AttemptLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return new AuthResult<T>()
            {
                Result = AuthResults.Success
            };
        }
    }
}
