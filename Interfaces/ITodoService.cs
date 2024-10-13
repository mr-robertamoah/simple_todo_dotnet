using System.Security.Claims;
using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(Guid id);
        Task CreateTodoAsync(CreateTodoRequest request, ClaimsPrincipal principal);
        Task UpdateTodoAsync(UpdateTodoRequest request, ClaimsPrincipal principal);
        Task DeleteTodoAsync(Guid id);
    }
}