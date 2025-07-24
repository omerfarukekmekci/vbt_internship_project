using Xunit;
using System.Net.Http.Json;
using System.Net;
using System.Threading.Tasks;
using InternshipProject.Models;
using InternshipProject.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore; 

namespace InternshipProject.Tests.Controllers
{
    public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        private const string longString = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        public AuthControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        private void ClearDatabase()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<InternPortalContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        #region Register Endpoint Tests

        [Fact]
        public async Task Register_WithValidModel_ReturnsOkAndToken()
        {
            
            ClearDatabase();
            var model = new RegisterRequestModel
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test.user@example.com",
                Password = "Password123",
                Role = "User"
            };

            
            var response = await _client.PostAsJsonAsync("/auth/register", model);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Token));
        }

        [Fact]
        public async Task Register_WithMissingEmailOrPassword_ShouldReturnBadRequest()
        {
            
            ClearDatabase();
            // missing email
            var modelMissingEmail = new { FirstName = "Test", LastName = "User", Password = "Password123", Role = "User" };
            // missing pw
            var modelMissingPassword = new { FirstName = "Test", LastName = "User", Email = "test@example.com", Role = "User" };

            
            var responseEmail = await _client.PostAsJsonAsync("/auth/register", modelMissingEmail);
            var responsePassword = await _client.PostAsJsonAsync("/auth/register", modelMissingPassword);

           
            Assert.Equal(HttpStatusCode.BadRequest, responseEmail.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, responsePassword.StatusCode);
        }

        [Fact]
        public async Task Register_WithDuplicateEmail_ShouldReturnOkAndCreateSecondUser()
        {
            
            ClearDatabase();
            var model = new RegisterRequestModel
            {
                FirstName = "First",
                LastName = "User",
                Email = "duplicate@example.com",
                Password = "password",
                Role = "User"
            };
            // first register
            await _client.PostAsJsonAsync("/auth/register", model);

            
            var secondModel = new RegisterRequestModel
            {
                FirstName = "Second",
                LastName = "User",
                Email = "duplicate@example.com", // same email
                Password = "anotherpassword",
                Role = "Admin"
            };

           
            var response = await _client.PostAsJsonAsync("/auth/register", secondModel);

            
            // this test must be fail Conflict/BadRequest 
            response.EnsureSuccessStatusCode(); 
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<InternPortalContext>();
                var userCount = await context.Users.CountAsync(u => u.Email == "duplicate@example.com");
                Assert.Equal(2, userCount); 
            }
        }

        [Fact]
        public async Task Register_WithInvalidEmailFormat_ShouldReturnOkAndCreateUser()
        {
            
            ClearDatabase();
            var model = new RegisterRequestModel
            {
                FirstName = "Invalid",
                LastName = "Email",
                Email = "invalid-email", 
                Password = "password",
                Role = "User"
            };

           
            var response = await _client.PostAsJsonAsync("/auth/register", model);

            
            response.EnsureSuccessStatusCode();
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<InternPortalContext>();
                var createdUser = await context.Users.FirstOrDefaultAsync(u => u.Email == "invalid-email");
                Assert.NotNull(createdUser);
            }
        }

        [Fact]
        public async Task Register_WithShortPassword_ShouldReturnOkAndCreateUser()
        {
            
            ClearDatabase();
            var model = new RegisterRequestModel
            {
                FirstName = "Short",
                LastName = "Pass",
                Email = "shortpass@example.com",
                Password = "123", 
                Role = "User"
            };

            
            var response = await _client.PostAsJsonAsync("/auth/register", model);

            
            response.EnsureSuccessStatusCode();
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<InternPortalContext>();
                var createdUser = await context.Users.FirstOrDefaultAsync(u => u.Email == "shortpass@example.com");
                Assert.NotNull(createdUser);
            }
        }

        [Fact]
        public async Task Register_InvalidModel_ShouldReturnBadRequestForMissingFields()
        {
            
            ClearDatabase();
            var model = new { }; // null model

            
            var response = await _client.PostAsJsonAsync("/auth/register", model);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_WithLongStrings_ShouldReturnOkAndCreateUser()
        {
            
            ClearDatabase();
            
            var model = new RegisterRequestModel
            {
                FirstName = longString,
                LastName = longString,
                Email = "longstring@example.com",
                Password = longString,
                Role = "User"
            };

            
            var response = await _client.PostAsJsonAsync("/auth/register", model);

            //this code should have been failed bu it passes  because of db limits
            response.EnsureSuccessStatusCode();
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<InternPortalContext>();
                var createdUser = await context.Users.FirstOrDefaultAsync(u => u.Email == "longstring@example.com");
                Assert.NotNull(createdUser);
            }
        }

        #endregion

        #region Login Endpoint Tests

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsOkAndToken()
        {
            
            ClearDatabase();
            
            var registerModel = new RegisterRequestModel { Email = "login@test.com", Password = "password", FirstName = "a", LastName = "b", Role = "c" };
            await _client.PostAsJsonAsync("/auth/register", registerModel);

            
            var response = await _client.PostAsync($"/auth/login?email=login@test.com&password=password", null);

            
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Token));
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            
            ClearDatabase();

            
            var response = await _client.PostAsync($"/auth/login?email=wrong@test.com&password=wrong", null);

            
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Login_WithEmptyCredentials_ReturnsUnauthorized()
        {
            
            ClearDatabase();

            
            var response = await _client.PostAsync($"/auth/login?email=&password=", null);

            
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Login_WithNonExistentUser_ReturnsUnauthorized()
        {
            
            ClearDatabase();

            var response = await _client.PostAsync($"/auth/login?email=nonexistent@example.com&password=anypassword", null);

            
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Login_WithIncorrectCaseEmail_ReturnsUnauthorized()
        {
            
            ClearDatabase();
            var registerModel = new RegisterRequestModel { Email = "CaseSensitive@example.com", Password = "password", FirstName = "a", LastName = "b", Role = "c" };
            await _client.PostAsJsonAsync("/auth/register", registerModel);

            
            var response = await _client.PostAsync($"/auth/login?email=casesensitive@example.com&password=password", null);

            
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Login_WithIncorrectCasePassword_ReturnsUnauthorized()
        {
            
            ClearDatabase();
            var registerModel = new RegisterRequestModel { Email = "passwordcase@example.com", Password = "CorrectPassword", FirstName = "a", LastName = "b", Role = "c" };
            await _client.PostAsJsonAsync("/auth/register", registerModel);

            
            var response = await _client.PostAsync($"/auth/login?email=passwordcase@example.com&password=correctpassword", null);

            
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Login_WithVeryLongCredentials_ReturnsUnauthorized()
        {
            
            ClearDatabase();
            

            
            var response = await _client.PostAsync($"/auth/login?email={longString}@example.com&password={longString}", null);

            
            
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        #endregion
    }

    
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}