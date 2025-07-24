using Xunit;
using Microsoft.EntityFrameworkCore;
using InternshipProject.Data;
using InternshipProject.Services.concretes;
using InternshipProject.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace InternshipProject.Tests
{
    public class AuthServiceTests
    {
        private readonly InternPortalContext _context;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            var options = new DbContextOptionsBuilder<InternPortalContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString()) // new db before every test case
                .Options;
            _context = new InternPortalContext(options);
            _authService = new AuthService(_context);

            
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        #region Login Tests

        [Fact]
        public async Task Login_ShouldReturnNull_WhenUserNotFound()
        {
            //  null user

            
            var result = await _authService.Login("nonexistent@example.com", "password");

            
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenCredentialsAreCorrect()
        {
            
            var user = new User 
            { 
                Email = "test@example.com", 
                PasswordHash = "correctpassword",
                FirstName = "Test",
                LastName = "User",
                Role = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            
            var result = await _authService.Login("test@example.com", "correctpassword");

            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenPasswordIsIncorrect()
        {
            
            var user = new User 
            { 
                Email = "test@example.com", 
                PasswordHash = "correctpassword",
                FirstName = "Test",
                LastName = "User",
                Role = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            
            var result = await _authService.Login("test@example.com", "wrongpassword");

            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_ReturnsToken_WithCorrectEmailInPayload()
        {
            
            var user = new User 
            { 
                Email = "payload@example.com", 
                PasswordHash = "password",
                FirstName = "Test",
                LastName = "User",
                Role = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            
            var token = await _authService.Login("payload@example.com", "password");

           
            Assert.NotNull(token);
            var decoded = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
            Assert.Contains("payload@example.com", decoded);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenUserExistsButPasswordIsIncorrectCase()
        {
            
            var user = new User 
            {
                Email = "case@example.com",
                PasswordHash = "CorrectPassword",
                FirstName = "Test",
                LastName = "User",
                Role = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            
            var result = await _authService.Login("case@example.com", "correctpassword"); // lowercase/uppercase

            
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenUserExistsButEmailIsIncorrectCase()
        {
            
            var user = new User 
            {
                Email = "Case@Example.com", // upperCase email
                PasswordHash = "password",
                FirstName = "Test",
                LastName = "User",
                Role = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            
            var result = await _authService.Login("case@example.com", "password"); // lowercase email

            
            Assert.Null(result);
        }

        #endregion

        #region Register Tests

        [Fact]
        public async Task Register_ShouldCreateUserAndReturnToken_WhenDataIsValid()
        {
            
            var model = new RegisterRequestModel
            {
                FirstName = "New",
                LastName = "User",
                Email = "newuser@example.com",
                Password = "newpassword",
                Role = "User"
            };

            
            var result = await _authService.Register(model);
            var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == "newuser@example.com");

            
            Assert.NotNull(result); 
            Assert.NotNull(createdUser); 
            Assert.Equal("New", createdUser.FirstName);
            Assert.Equal("newpassword", createdUser.PasswordHash); 
        }

        [Fact]
        public async Task Register_ShouldStillCreateUser_WhenEmailAlreadyExists()
        {
            
            var existingUser = new User 
            { 
                Email = "existing@example.com", 
                PasswordHash = "password123",
                FirstName = "Existing",
                LastName = "User",
                Role = "User"
            };
            _context.Users.Add(existingUser);
            await _context.SaveChangesAsync();


            var model = new RegisterRequestModel
            {
                FirstName = "Another",
                LastName = "Person",
                Email = "existing@example.com", // same email
                Password = "anotherpassword",
                Role = "Admin"
            };

            
            var result = await _authService.Register(model);
            var userCount = await _context.Users.CountAsync(u => u.Email == "existing@example.com");

            
            Assert.NotNull(result); 
            
        }

        [Fact]
        public async Task RegisterAsync_NullInput_ThrowsArgumentNullException()
        {
            
            RegisterRequestModel model = null;

            
            await Assert.ThrowsAsync<ArgumentNullException>(() => _authService.Register(model));
        }

        [Fact]
        public async Task GenerateToken_ShouldContainCorrectExpiry()
        {
            var user = new User { Email = "expiry@example.com", PasswordHash = "pass", FirstName = "a", LastName = "b", Role = "c" };

            var token = await _authService.Login(user.Email, user.PasswordHash);

            Assert.NotNull(token);
            var decoded = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
            Assert.Contains(DateTime.UtcNow.Ticks.ToString().Substring(0, 5), decoded);
        }

        #endregion
    }
}