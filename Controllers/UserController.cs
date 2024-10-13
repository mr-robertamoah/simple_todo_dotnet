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

        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? token = await _service.LoginAsync(request);

            return Ok( new { message = "User successfully logged in.", token } );
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? token = await _service.RegisterAsync(request);

            return Ok( new { message = "User successfully Registered.", token } );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var todo = await _service.GetByIdAsync(id);

            if (todo == null)
                return NotFound(new { message = $"User with id: {id} was not found."});
            
            return Ok(new { message = $"Successfully retrieved the information of User with id: {id}.", data = todo});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountAsync(Guid id)
        {
            await _service.DeleteAccountAsync(id);

            return Ok(new { message = $"User account with id {id} has been successfully deleted." });
        }
    }
}