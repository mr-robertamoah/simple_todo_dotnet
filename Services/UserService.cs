using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TodoAPIDotNet.AppDataContext;
using TodoAPIDotNet.Interfaces;
using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<TodoService> _logger;
        private readonly TodoDbContext _context;
        private readonly IMapper _mapper;

        public UserService(
            IConfiguration configuration,
            TodoDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<TodoService> logger,
            IMapper mapper
        )
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task DeleteAccountAsync(string id)
        {
            try
            {
                User? user = await _context.FindAsync<User>(id);

                if (user == null)
                    throw new Exception($"User account with id {id} was not found");

                await _signInManager.SignOutAsync();

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                    throw new Exception("Something unfortunate happened while deleting account. Please try again shortly.");
            }
            catch (Exception e)
            {
                ThrowException(
                    "Deletion of user account failed. Please try again shortly.",
                    e
                );
            }
        }

        // public Task<IEnumerable<User>> GetAllAsync()
        // {
        //     throw new NotImplementedException();
        // }

        public async Task<UserDTO?> GetByIdAsync(string id)
        {
            try
            {
                User? user = await _context.FindAsync<User>(id);
                
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception e)
            {
                ThrowException(
                    "Something unfortunate happened while getting User information.",
                    e
                );

                return null;
            }
        }

        public async Task LogoutAsync(ClaimsPrincipal principal)
        {
            try
            {
                var result = _signInManager.IsSignedIn(principal);

                if (!result)
                    throw new Exception("You cannot log out when not logged in.");

                await _signInManager.SignOutAsync();
            }
            catch (Exception e)
            {
                ThrowException(
                    "Something unfortunate happened while logging out.",
                    e
                );
            }
        }

        public async Task<string?> LoginAsync(UserLoginRequest request)
        {
            try
            {
                User? user = null;

                if (request.Email != null)
                    user = await _userManager.FindByEmailAsync(
                        _userManager.NormalizeEmail(request.Email)
                    );
                else if (request.Username != null)
                    user = await _userManager.FindByNameAsync(
                        _userManager.NormalizeName(request.Username)
                    );

                if (user == null)
                {
                    throw new Exception("Invalid login credentials.");
                }

                var result = await _signInManager.PasswordSignInAsync(
                    userName: user.UserName,
                    password: request.Password,
                    false,
                    false
                );

                if (!result.Succeeded)
                {
                    throw new Exception("Invalid login credentials.");
                }

                return GenerateJwtToken(user);
            }
            catch (Exception e)
            {
                ThrowException(
                    "Something unfortunate happened while logging in. Please try again shortly.",
                    e
                );

                return null;
            }
        }

        public async Task<string?> RegisterAsync(UserRegisterRequest request)
        {
            try
            {
                User? user = await _userManager.FindByEmailAsync(
                    _userManager.NormalizeEmail(request.Email)
                );

                if (user != null)
                    throw new Exception("Credentials provided for your account already exists. Consider changing your Email");

                user = await _userManager.FindByNameAsync(
                    _userManager.NormalizeName(request.Username)
                );

                if (user != null)
                    throw new Exception("Credentials provided for your account already exists. Consider changing your Username");

                user = new User {
                    UserName = request.Username,
                    Email = request.Email,
                    NormalizedEmail = _userManager.NormalizeEmail(request.Email),
                    NormalizedUserName = _userManager.NormalizeName(request.Username),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    LockoutEnabled = false
                };

                var identityResult = await _userManager.CreateAsync(user, request.Password);

                // TODO: get password relative errors when result fails
                // TODO: make exceptions thrown from within try get sent to user
                // TODO: add transactions
                if (!identityResult.Succeeded)
                    throw new Exception("Failed to create User account.");

                if (_userManager.Options.SignIn.RequireConfirmedEmail)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
                    if (!confirmResult.Succeeded)
                    {
                        var errors = string.Join(", ", confirmResult.Errors.Select(e => e.Description));
                        throw new Exception($"Failed to confirm email: {errors}");
                    }
                }

                if (await _userManager.IsLockedOutAsync(user))
                    await _userManager.SetLockoutEndDateAsync(user, null);

                var signInResult = await _signInManager.PasswordSignInAsync(
                    _userManager.NormalizeName(user.UserName),
                    request.Password,
                    false,
                    false
                );

                if (!signInResult.Succeeded)
                    throw new Exception("Failed to sign in new account. Log into your account.");

                return GenerateJwtToken(user);
            }
            catch (Exception e)
            {
                ThrowException(
                    "Something unfortunate happened while registering. Please try again shortly.",
                    e
                );

                return null;
            }
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new []{
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id)
            };

            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key) || key.Length < 16)
            {
                throw new ArgumentException("JWT key is either missing or too short in configuration.");
            }

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), 
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void ThrowException(string message, Exception e)
        {
            _logger.LogError(message, e);
            throw new Exception(message);
        }

    }

}