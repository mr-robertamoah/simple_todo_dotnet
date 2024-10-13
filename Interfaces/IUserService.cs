using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.Interfaces
{
    public interface IUserService
    {
        // Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<string?> LoginAsync(UserLoginRequest request);
        Task<string?> RegisterAsync(UserRegisterRequest request);
        Task DeleteAccountAsync(Guid id);
    }
}