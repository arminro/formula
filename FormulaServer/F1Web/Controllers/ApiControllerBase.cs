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
        protected readonly IRepository<TEntity> _repository;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;

        public ApiControllerBase(IRepository<TEntity> repository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Gets a list of entities.
        /// </summary>
        /// <returns>A collection of entities or an empty collection.</returns>
        // GET api/[entities]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            try
            {
                var result = await _repository.GetElementsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Could not retrieve list of entity");
            }
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
            try
            {
                var entity = await _repository.GetElementAsync(elementId);
                if (entity == null)
                {
                    return NotFound("The requested element is not available.");
                }
                else
                {
                    return Ok(entity);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Could not retrieve the element");
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
            try
            {
                if (IsUserLoggedIn())
                {
                    await _repository.CreateAsync(value);
                    return Accepted(); // usually, Created() is used, but my repo does not return anything 
                }

                return Unauthorized();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Could not create element ${value.GetType().Name}");
            }
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
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (ModelState.IsValid)
                    {
                        await _repository.UpdateAsync(value);
                        return Accepted();
                    }
                    return BadRequest(ModelState); 
                }

                return Unauthorized();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Could not update element ${value.GetType().Name}");
            }
        }

        // DELETE api/[entities]/5555-5555-5555
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var entity = await _repository.GetElementAsync(id);
                    if (entity == null)
                    {
                        return NotFound("The user to be deleted is not found in the databse");
                    }
                    else
                    {
                        await _repository.DeleteAsync(entity);
                        return Ok(entity);
                    } 
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Could not delete element");
            }
        }

        private Guid GetUserIdFromJWT()
        {
            return Guid.Parse(User.Claims.FirstOrDefault(e => e.Type == "aud").Value);
        }
    }
}
