using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoAPIDotNet.AppDataContext;
using TodoAPIDotNet.Interfaces;
using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _todoDbContext;
        private readonly ILogger<TodoService> _logger;
        private readonly IMapper _mapper;

        public TodoService(TodoDbContext todoDbContext, ILogger<TodoService> logger, IMapper mapper)
        {
            _todoDbContext = todoDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.UtcNow;
                _todoDbContext.Add(todo);

                await _todoDbContext.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                string message = $"An error occurred while creating a Todo item.";

                _logger.LogError(e, message);
                throw new Exception(message);
            }
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null)
                throw new Exception($"Todo item with id {id} not found.");

            _todoDbContext.Todos.Remove(todo);
            await _todoDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            var todos = await _todoDbContext.Todos.ToListAsync();

            if (todos == null)
            {
                throw new Exception("No Todo items found.");
            }

            return todos;
        }

        public async Task<Todo> GetByIdAsync(Guid id)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                throw new KeyNotFoundException($"Todo item with id {id} not found.");
            }

            return todo;
        }

        public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            try
            {
                var todo = await _todoDbContext.Todos.FindAsync(id);

                if (todo == null)
                {
                    throw new KeyNotFoundException($"Todo item with id {id} not found.");
                }

                if (!string.IsNullOrEmpty(request.Title))
                {
                    todo.Title = request.Title;
                }

                if (!string.IsNullOrEmpty(request.Description))
                {
                    todo.Description = request.Description;
                }

                if (request.IsComplete != null)
                {
                    todo.IsComplete = request.IsComplete.Value;
                }

                if (request.DueDate != null)
                {
                    todo.DueDate = request.DueDate.Value;
                }

                if (request.Priority != null)
                {
                    todo.Priority = request.Priority.Value;
                }

                todo.UpdatedAt = DateTime.UtcNow;

                await _todoDbContext.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                string message = $"An error occurred while updating a Todo item with id: {id}.";

                _logger.LogError(e, message);
                throw new Exception(message);
            }
        }
    }
}