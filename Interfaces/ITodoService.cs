using System.Security.Claims;
using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllAsync(ClaimsPrincipal principal);
        Task<Todo> GetByIdAsync(Guid id, ClaimsPrincipal principal);
        Task CreateTodoAsync(CreateTodoRequest request, ClaimsPrincipal principal);
        Task UpdateTodoAsync(UpdateTodoRequest request, ClaimsPrincipal principal);
        Task DeleteTodoAsync(Guid id, ClaimsPrincipal principal);
    }
}