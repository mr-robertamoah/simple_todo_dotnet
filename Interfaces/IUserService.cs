using System.Security.Claims;
using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.Interfaces
{
    public interface IUserService
    {
        // Task<IEnumerable<User>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(string id);
        Task LogoutAsync(ClaimsPrincipal principal);
        Task<string?> LoginAsync(UserLoginRequest request);
        Task<string?> RegisterAsync(UserRegisterRequest request);
        Task DeleteAccountAsync(string id);
    }
}