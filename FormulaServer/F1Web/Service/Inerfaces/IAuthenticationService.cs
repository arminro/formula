using F1Web.Service.Implementations;
using F1Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1Web.Service.Inerfaces
{
    public interface IAuthenticationService<TUser>
    {
        Task<AuthResult<TUser>> AttemptLoginAsync(LoginDto login);

        Task<AuthResult<TUser>> AttemptLogoutAsync();
    }
}
