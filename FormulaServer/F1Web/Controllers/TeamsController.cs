using F1.Data.Models;
using Microsoft.AspNetCore.Mvc;
using F1Web.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace F1Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ApiControllerBase<FormulaTeam>
    {
        public TeamsController(IRepository<FormulaTeam> repository, UserManager<User> userManager, SignInManager<User> signInManager)
            :base(repository, userManager, signInManager)
        {
        }
    }
}
