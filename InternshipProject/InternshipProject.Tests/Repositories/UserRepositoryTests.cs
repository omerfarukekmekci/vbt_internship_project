using Xunit;
using Microsoft.EntityFrameworkCore;
using InternshipProject.Data;
using InternshipProject.Repositories.concretes;
using InternshipProject.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace InternshipProject.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly InternPortalContext _context;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<InternPortalContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString()) // new db for every test case
                .Options;
            _context = new InternPortalContext(options);
            _userRepository = new UserRepository(_context);

            
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        #region GetAllAsync Tests

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
           
            _context.Users.Add(new User { Id = 1, FirstName = "Test1", LastName = "User1", Email = "test1@example.com", PasswordHash = "pass1", Role = "User" });
            _context.Users.Add(new User { Id = 2, FirstName = "Test2", LastName = "User2", Email = "test2@example.com", PasswordHash = "pass2", Role = "Admin" });
            await _context.SaveChangesAsync();

            
            var result = await _userRepository.GetAllAsync();

            
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            
            var result = await _userRepository.GetAllAsync();

            
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        #endregion

        #region GetByIdAsync Tests

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            
            var user = new User { Id = 1, FirstName = "Test", LastName = "User", Email = "test@example.com", PasswordHash = "pass", Role = "User" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            
            var result = await _userRepository.GetByIdAsync(1);

            
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            //null user

            
            var result = await _userRepository.GetByIdAsync(999);

            
            Assert.Null(result);
        }

        #endregion

        #region AddAsync Tests

        [Fact]
        public async Task AddAsync_ShouldAddUserSuccessfully()
        {
            
            var user = new User { Id = 1, FirstName = "New", LastName = "User", Email = "new@example.com", PasswordHash = "newpass", Role = "User" };

            
            await _userRepository.AddAsync(user);
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == 1);

            
            Assert.NotNull(result);
            Assert.Equal("new@example.com", result.Email);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenAddingUserWithExistingId()
        {
            
            _context.Users.Add(new User { Id = 1, FirstName = "Existing", LastName = "User", Email = "existing@example.com", PasswordHash = "pass", Role = "User" });
            await _context.SaveChangesAsync();

            var newUser = new User { Id = 1, FirstName = "Duplicate", LastName = "User", Email = "duplicate@example.com", PasswordHash = "pass", Role = "User" };

            
            await Assert.ThrowsAsync<System.InvalidOperationException>(() => _userRepository.AddAsync(newUser));
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingUser()
        {
            
            var user = new User { Id = 1, FirstName = "Original", LastName = "User", Email = "original@example.com", PasswordHash = "pass", Role = "User" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user.FirstName = "Updated";
            user.Email = "updated@example.com";

            
            await _userRepository.UpdateAsync(user);
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == 1);

            
            Assert.NotNull(result);
            Assert.Equal("Updated", result.FirstName);
            Assert.Equal("updated@example.com", result.Email);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowDbUpdateConcurrencyException_WhenUpdatingNonExistingUser()
        {
            
            var nonExistingUser = new User { Id = 999, FirstName = "NonExisting", LastName = "User", Email = "nonexisting@example.com", PasswordHash = "pass", Role = "User" };

            
            
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => _userRepository.UpdateAsync(nonExistingUser));
        }

        #endregion

        #region DeleteAsync Tests

        [Fact]
        public async Task DeleteAsync_ShouldDeleteExistingUser()
        {
           
            var user = new User { Id = 1, FirstName = "Test", LastName = "User", Email = "test@example.com", PasswordHash = "pass", Role = "User" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            
            await _userRepository.DeleteAsync(1);
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == 1);

            
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldNotThrowException_WhenDeletingNonExistingUser()
        {
            
            await _userRepository.DeleteAsync(999);
            var userCount = await _context.Users.CountAsync();
            Assert.Equal(0, userCount);
        }

        #endregion
    }
}