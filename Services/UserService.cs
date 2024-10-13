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
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public UserService(
            IConfiguration configuration,
            UserDbContext context,
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

        public async Task DeleteAccountAsync(Guid id)
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

        public async Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<string?> LoginAsync(UserLoginRequest request)
        {
            try
            {
                User? user = null;

                if (request.Email != null)
                    user = await _userManager.FindByEmailAsync(request.Email);
                else if (request.Username != null)
                    user = await _userManager.FindByNameAsync(request.Username);

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
                User? user = await _userManager.FindByEmailAsync(request.Email);

                if (user != null)
                    throw new Exception("Credentials provided for your account already exists. Consider changing your Email");

                user = await _userManager.FindByNameAsync(request.Username);

                if (user != null)
                    throw new Exception("Credentials provided for your account already exists. Consider changing your Username");


                user = new User {
                    UserName = request.Username,
                    Email = request.Email
                };

                var identityResult = await _userManager.CreateAsync(
                    user: user,
                    password: request.Password
                );

                if (!identityResult.Succeeded)
                    throw new Exception("Failed to create User account.");

                var signInResult = await _signInManager.PasswordSignInAsync(
                    user.UserName,
                    user.Password,
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

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), 
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
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