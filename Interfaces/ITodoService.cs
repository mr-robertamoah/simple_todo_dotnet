using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(Guid id);
        Task CreateTodoAsync(CreateTodoRequest request);
        Task UpdateTodoAsync(UpdateTodoRequest request);
        Task DeleteTodoAsync(Guid id);
    }
}