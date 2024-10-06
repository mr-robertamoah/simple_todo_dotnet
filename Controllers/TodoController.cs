using Microsoft.AspNetCore.Mvc;
using TodoAPIDotNet.Interfaces;

namespace TodoAPIDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoService todoService, ILogger<TodoController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var todos = await _todoService.GetAllAsync();

                if (todos == null || !todos.Any())
                {
                    return NotFound(new { message = "No Todo items found." });
                }

                return Ok(new { message = "Successfully retrieved all Todo items.", data = todos});
            }
            catch (System.Exception e)
            {
                string message = "An error occurred while fetching Todo items.";

                _logger.LogError(e, message);
                return StatusCode(500, new { message = message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _todoService.CreateTodoAsync(request);

                return Ok( new { message = "Todo successfully created."} );
            }
            catch (System.Exception e)
            {
                string message = "An error occurred while creating a Todo item.";

                _logger.LogError(e, message);
                return StatusCode(500, new { message = message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodoAsync([FromBody] UpdateTodoRequest request)
        {
            try
            {
                await _todoService.UpdateTodoAsync(request);

                return Ok();
            }
            catch (System.Exception e)
            {
                string message = "An error occurred while updating a Todo item.";

                _logger.LogError(e, message);
                return Problem(message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            try
            {
                await _todoService.DeleteTodoAsync(id);

                return Ok();
            }
            catch (System.Exception e)
            {
                string message = "An error occurred while deleting a Todo item.";

                _logger.LogError(e, message);
                return Problem(message);
            }
        }
    }
}