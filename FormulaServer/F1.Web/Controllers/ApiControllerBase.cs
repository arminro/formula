using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using F1.Data.Interfaces;
using F1Web.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using F1.Data.Models;
using F1Web.Service.Inerfaces;

namespace F1Web.Controllers
{
    /// <summary>
    /// Generic parent for controllers
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("Cors")]
    public class ApiControllerBase<TEntity> : ControllerBase
        where TEntity : class, IDbEntry
    {
        protected readonly IFormulaService<TEntity> _service;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;

        public ApiControllerBase(IFormulaService<TEntity> service, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Gets a list of entities.
        /// </summary>
        /// <returns>A collection of entities or an empty collection.</returns>
        // GET api/[entities]
        [HttpGet]
        [AllowAnonymous]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            var result = await _service.GetElementsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets a single entity.
        /// </summary>
        /// <param name="elementId"></param>
        /// <returns>The entity if found, null otherwise.</returns>
        // GET api/[entities]/5555-5555-5555-5555
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(Guid elementId)
        {
            var entity = await _service.GetElementAsync(elementId);
            if (entity == null)
            {
                return NotFound("The requested element is not available.");
            }
            else
            {
                return Ok(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST api/[entities]
        [HttpPost]
        public virtual async Task<ActionResult> Post([FromBody] TEntity value)
        {
            if (IsUserLoggedIn())
            {
                await _service.CreateAsync(value);
                return Accepted(); // usually, Created() is used, but my repo does not return anything 
            }

            return Unauthorized();
        }

        private bool IsUserLoggedIn()
        {
            // this works both for identity and jwt if authorize is configured properly
            return User.Identity.IsAuthenticated;

            // this works only if identity is configured to be the auth provider
            //return _signInManager.IsSignedIn(HttpContext.User);
        }

        // PUT api/[entities]
        [HttpPut]
        public virtual async Task<ActionResult> Put([FromBody] TEntity value)
        {
            if (IsUserLoggedIn())
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdateAsync(value);
                    return Accepted();
                }
                return BadRequest(ModelState);
            }

            return Unauthorized();
        }

        // DELETE api/[entities]/5555-5555-5555
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            if (IsUserLoggedIn())
            {
                var entity = await _service.GetElementAsync(id);
                if (entity == null)
                {
                    return NotFound("The user to be deleted is not found in the databse");
                }
                else
                {
                    await _service.DeleteAsync(entity);
                    return Ok(entity);
                }
            }

            return Unauthorized();
        }
    }
}
