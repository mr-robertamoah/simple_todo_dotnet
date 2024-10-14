using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetByTokenAsync()
        {
            try
            {
                var data = await _service.GetByTokenAsync(User);

                if (data == null)
                    return NotFound(new { message = $"Your User information could not be retrieved. You are either not logged in or registered."});
                
                return Ok(new { message = $"Successfully retrieved the information of User with id: {id}.", data});
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccountAsync()
        {
            try
            {
                await _service.DeleteAccountAsync(User);

                return Ok(new { message = $"Your User account has been successfully deleted." });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
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