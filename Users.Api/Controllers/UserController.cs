using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Users.Api.Filters;
using Users.Core.Dto;
using Users.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>A list of the users or error if the system fails.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "An error occurred while getting all users");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>One user of error if the system fails.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            try
            {
                var users = await _userService.GetByIdAsync(id);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting user by id");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>OK if the user was created or error if the system fails.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [AgeValidationFilter(18)]
        public async Task<ActionResult> Post([FromBody] UserDto user)
        {
            try
            {
                await _userService.AddAsync(user);
                return Ok("User created");
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                _logger.LogWarning($"A user with the Email {user.Email} already exists.");
                return Conflict($"A user with the Email {user.Email} already exists.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while creating a user");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="user">User updating information.</param>
        /// <returns>OK if the user was updated or error if the system fails.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put(int id, [FromBody] UserDto user)
        {
            try
            {
                await _userService.UpdateAsync(user);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while updating a user");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>OK if the user was deleted or an error if the system fails.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while deleting a user");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
