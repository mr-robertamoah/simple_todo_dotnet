using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoAPIDotNet.AppDataContext;
using TodoAPIDotNet.Interfaces;
using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _todoDbContext;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<TodoService> _logger;
        private readonly IMapper _mapper;

        public TodoService(
            TodoDbContext todoDbContext, 
            ILogger<TodoService> logger, 
            IMapper mapper,
            UserManager<User> userManager
        )
        {
            _todoDbContext = todoDbContext;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task CreateTodoAsync(CreateTodoRequest request, ClaimsPrincipal principal)
        {
            try
            {
                User user = await GetUserFromPrincipal(principal);
                // TODO: validate due date
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.UtcNow;
                todo.UserId = user.Id;
                _todoDbContext.Add(todo);

                await _todoDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ThrowException(
                    $"An error occurred while creating a Todo item.",
                    e
                );
            }
        }

        public async Task DeleteTodoAsync(Guid id, ClaimsPrincipal principal)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null)
                throw new Exception($"Todo item with id {id} not found.");

            User user = await GetUserFromPrincipal(principal);

            UserMustOwnTodo(user, todo);
            
            _todoDbContext.Todos.Remove(todo);
            await _todoDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoDTO>> GetAllAsync(ClaimsPrincipal principal)
        {
            User user = await GetUserFromPrincipal(principal);
            
            var todos = await _todoDbContext.Todos
                .Where(todo => todo.UserId == user.Id)
                .Select(todo => new TodoDTO {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    IsComplete = todo.IsComplete,
                    Priority = todo.Priority,
                    CreatedAt = todo.CreatedAt,
                })
                .ToListAsync();

            if (todos == null)
            {
                throw new Exception("No Todo items found.");
            }

            return todos;
        }

        public async Task<Todo> GetByIdAsync(Guid id, ClaimsPrincipal principal)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);
            if (todo == null)
                throw new KeyNotFoundException($"Todo item with id {id} not found.");
            
            User user = await GetUserFromPrincipal(principal);

            UserMustOwnTodo(user, todo);
            
            return todo;
        }

        public async Task UpdateTodoAsync(UpdateTodoRequest request, ClaimsPrincipal principal)
        {
            try
            {
                var todo = await _todoDbContext.Todos.FindAsync(request.Id);

                if (todo == null)
                    throw new KeyNotFoundException($"Todo item with id {request.Id} not found.");

                User user = await GetUserFromPrincipal(principal);

                if (user.Id != todo.UserId)
                    throw new Exception("You do not own this Todo item.");
                
                if (!string.IsNullOrEmpty(request.Title))
                    todo.Title = request.Title;

                if (!string.IsNullOrEmpty(request.Description))
                    todo.Description = request.Description;

                if (request.IsComplete != null)
                    todo.IsComplete = request.IsComplete.Value;

                if (request.DueDate != null)
                    todo.DueDate = request.DueDate.Value;

                if (request.Priority != null)
                    todo.Priority = request.Priority.Value;

                todo.UpdatedAt = DateTime.UtcNow;

                await _todoDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ThrowException(
                    $"An error occurred while updating a Todo item with id: {request.Id}.",
                    e
                );
            }
        }

        private void ThrowException(string message, Exception e)
        {
            _logger.LogError(e, message);
            throw new Exception(message);
        }

        private async Task<User> GetUserFromPrincipal(ClaimsPrincipal principal)
        {
            User? user = await _userManager.GetUserAsync(principal);
            if (user == null)
                throw new Exception("User is not logged in.");

            return user;
        }

        private void UserMustOwnTodo(User user, Todo todo)
        {
            if (user.Id != todo.UserId)
                throw new Exception("You do not own this Todo item.");
        }

    }
}