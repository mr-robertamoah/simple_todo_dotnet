using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPIDotNet.Interfaces;

namespace TodoAPIDotNet.Controllers
{
    [Authorize]
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

// TODO: must provide token and return todo for that user
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var todos = await _todoService.GetAllAsync(User);

                if (todos == null || !todos.Any())
                {
                    return NotFound(new { message = "No Todo items found." });
                }

                return Ok(new { message = "Successfully retrieved all Todo items.", todos });
            }
            catch (Exception e)
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
                await _todoService.CreateTodoAsync(request, User);

                return Ok( new { message = "Todo successfully created."} );
            }
            catch (Exception e)
            {
                string message = "An error occurred while creating a Todo item.";

                _logger.LogError(e, message);
                return StatusCode(500, new { message = message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                request.Id = id;
                await _todoService.UpdateTodoAsync(request, User);

                return Ok(new { message = $"Todo item with id {request.Id} successfully updated." });
            }
            catch (Exception e)
            {
                string message = "An error occurred while updating a Todo item.";

                _logger.LogError(e, message);
                return Problem(message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var todo = await _todoService.GetByIdAsync(id, User);

                if (todo == null)
                    return NotFound(new { message = $"Todo item with id: {id} was not found."});
                
                return Ok(new { message = $"Successfully retrieved Todo with id: {id}.", data = todo});
            }
            catch (Exception e)
            {
                string message = $"An error occurred while retrieving a Todo item with id {id}.";

                return StatusCode(500, new { message = message, error = e.Message});
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            try
            {
                await _todoService.DeleteTodoAsync(id, User);

                return Ok(new { message = $"Todo item with id {id} successfully deleted." });
            }
            catch (Exception)
            {
                string message = "An error occurred while deleting a Todo item.";

                return Problem(message);
            }
        }
    }
}