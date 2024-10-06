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

        public Task DeleteTodoAsync(Guid id)
        {
            throw new NotImplementedException();
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

        public Task<Todo> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTodoAsync(UpdateTodoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}