﻿using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Infrastructure.Data;
using CleanArchitectureExample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace TestProject.Infrastructure
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task AddAsync_ShouldAddUser_WhenUserIsValid()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                // Arrange
                var userRepository = new UserRepository(context);
                var user = new User
                {
                    Name = "Test User",
                    Email = "test@ex.com"
                };

                // Act
                await userRepository.AddAsync(user);

                //Assert
                var userInDb = await context.Users.FirstOrDefaultAsync(u => u.Email == "test@ex.com");
                Assert.NotNull(userInDb);
                Assert.Equal("Test User", userInDb.Name);
            }
        }
        [Fact]
        public async Task EmailExistsAsync_ShouldReturnTrue_WhenEmailExists()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                // Arrange
                context.Users.Add(new User
                {
                    Name = "Existing User",
                    Email = "existing@ex.com"
                });

                context.SaveChanges();

                var userRepository = new UserRepository(context);

                //Act
                var exists = await userRepository.EmailExistsAsync("existing@ex.com");

                //Assert
                Assert.True(exists);
            }
        }
    }
}
