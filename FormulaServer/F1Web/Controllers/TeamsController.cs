using F1.Data.Models;
using Microsoft.AspNetCore.Mvc;
using F1Web.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using F1Web.Service.Inerfaces;
using System;

namespace F1Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ApiControllerBase<FormulaTeam>
    {
        public TeamsController(IFormulaService<FormulaTeam> service, UserManager<User> userManager, SignInManager<User> signInManager)
            :base(service, userManager, signInManager)
        {
        }
    }
}
