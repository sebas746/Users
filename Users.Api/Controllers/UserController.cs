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

        // GET: api/<UsersController>
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

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
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

        // POST api/<UsersController>
        [HttpPost]
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

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
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

        // DELETE api/<UsersController>/8
        [HttpDelete("{id}")]
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
