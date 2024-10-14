using System.Security.Claims;
using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.Interfaces
{
    public interface IUserService
    {
        // Task<IEnumerable<User>> GetAllAsync();
        Task<UserDTO?> GetByTokenAsync(ClaimsPrincipal principal);
        Task LogoutAsync(ClaimsPrincipal principal);
        Task<string?> LoginAsync(UserLoginRequest request);
        Task<string?> RegisterAsync(UserRegisterRequest request);
        Task DeleteAccountAsync(ClaimsPrincipal principal);
    }
}