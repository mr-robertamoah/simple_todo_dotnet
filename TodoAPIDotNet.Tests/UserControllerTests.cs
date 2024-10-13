using Moq;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using TodoAPIDotNet.Models;
using TodoAPIDotNet.Interfaces;
using TodoAPIDotNet.Controllers;

namespace TodoAPIDotNet.Tests
{

    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserService =  new Mock<IUserService>();
            _controller = new UserController(_mockUserService.Object);
        }
        
        [Fact]
        public async Task LoginAsync_ReturnsOkResult_WithToken_UsingEmail()
        {
            var request = new UserLoginRequest{
                Email = "mr_robertamoah@yahoo.com",
                Password = "password",
            };

            _mockUserService.Setup(service => service.LoginAsync(request))
                .ReturnsAsync("fake-jwt-token");

            var result = await _controller.LoginAsync(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value;

            Assert.Equal("User successfully logged in.", response?.GetType().GetProperty("message")?.GetValue(response, null));
            Assert.Equal("fake-jwt-token", response?.GetType().GetProperty("token")?.GetValue(response, null));
        }
        
        [Fact]
        public async Task LoginAsync_ReturnsOkResult_WithToken_UsingUsername()
        {
            var request = new UserLoginRequest{
                Username = "mr_robertamoah",
                Password = "password",
            };

            _mockUserService.Setup(service => service.LoginAsync(request))
                .ReturnsAsync("fake-jwt-token");

            var result = await _controller.LoginAsync(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value;

            Assert.Equal("User successfully logged in.", response?.GetType().GetProperty("message")?.GetValue(response, null));
            Assert.Equal("fake-jwt-token", response?.GetType().GetProperty("token")?.GetValue(response, null));
        }
        
        [Fact]
        public async Task LoginAsync_ReturnsError_WhenNotLoggedIn()
        {
            var request = new UserLoginRequest{
                Username = "mr_robertamoah",
                Password = "password",
            };

            string message = "Invalid login credentials.";
            _mockUserService.Setup(service => service.LoginAsync(request))
                .Throws(new Exception(message));

            var result = await _controller.LoginAsync(request);

            var errResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errResult.StatusCode);
            Assert.Equal(message, errResult.Value);
        }
        
        [Fact]
        public async Task RegisterAsync_ReturnsOkResult_WithToken_UsingNecessaryData()
        {
            var request = new UserRegisterRequest{
                Username = "mr_robertamoah",
                Email = "mr_robertamoah@yahoo.com",
                Password = "password",
            };

            _mockUserService.Setup(service => service.RegisterAsync(request))
                .ReturnsAsync("fake-jwt-token");

            var result = await _controller.RegisterAsync(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value;

            Assert.Equal("User successfully Registered.", response?.GetType().GetProperty("message")?.GetValue(response, null));
            Assert.Equal("fake-jwt-token", response?.GetType().GetProperty("token")?.GetValue(response, null));
        }
        
        [Fact]
        public async Task RegisterAsync_ReturnsStatus500_WithoutNecessaryData()
        {
            var request = new UserRegisterRequest{
                Username = "mr_robertamoah",
                Email = "mr_robertamoah@yahoo.com",
                Password = "password",
            };

            string message = "Credentials provided for your account already exists. Consider changing your Email";
            _mockUserService.Setup(service => service.RegisterAsync(request))
                .Throws(new Exception(message));

            var result = await _controller.RegisterAsync(request);

            var errResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errResult.StatusCode);
            Assert.Equal(message, errResult.Value);
        }
    }

}