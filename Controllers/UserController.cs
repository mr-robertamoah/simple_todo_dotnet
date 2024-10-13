using Microsoft.AspNetCore.Mvc;
using TodoAPIDotNet.Interfaces;

namespace TodoAPIDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string? token = await _service.LoginAsync(request);

                return Ok( new { message = "User successfully logged in.", token } );
            }
            catch (Exception e)
            {
                return  StatusCode(500, e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string? token = await _service.RegisterAsync(request);
                
                return Ok( new { message = "User successfully Registered.", token } );
            }
            catch (Exception e)
            {
                return  StatusCode(500, e.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            try
            {
                var data = await _service.GetByIdAsync(id);

                if (data == null)
                    return NotFound(new { message = $"User with id: {id} was not found."});
                
                return Ok(new { message = $"Successfully retrieved the information of User with id: {id}.", data});
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountAsync(string id)
        {
            try
            {
                await _service.DeleteAccountAsync(id);

                return Ok(new { message = $"User account with id {id} has been successfully deleted." });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                await _service.LogoutAsync(User);

                return Ok(new { message = $"Successfully logged out." });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}