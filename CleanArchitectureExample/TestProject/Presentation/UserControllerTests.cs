using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Application.Dtos;
using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.WebAPI.Controllers;
using CleanArchitectureExample.WebAPI.Models;

using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject.Presentation
{
    public class UserControllerTests
    {
        [Fact]
        public async Task RegisterUserAsync_ReturnsCreatedResult_WhenRegistrationSucceeds()
        {
            // Arrange
            var mockRegistrationService = new Mock<IUserRegistrationService>();
            var mockFetchService = new Mock<IUserFetchService>();
            mockRegistrationService.Setup(service => service.RegisterUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            var controller = new UsersController(mockRegistrationService.Object, mockFetchService.Object);
            // Act
            var result = await controller.RegisterUserAsync(new UserRegistrationRequest
            {
                Name = "John Doe",
                Email = "john@doe.com"
            });

            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task RegisterUserAsync_ReturnsBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var mockRegistrationService = new Mock<IUserRegistrationService>();
            var mockFetchService = new Mock<IUserFetchService>();
            mockRegistrationService.Setup(service => service.RegisterUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            var controller = new UsersController(mockRegistrationService.Object, mockFetchService.Object);

            // Act
            var result = await controller.RegisterUserAsync(new UserRegistrationRequest
            {
                Name = "John Doe",
                Email = "john"
            });

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task FetchUserByEmailAsync_ValidEmail()
        {
            // Arrange
            var mockRegistrationService = new Mock<IUserRegistrationService>();
            var mockFetchService = new Mock<IUserFetchService>();
            var userDto = new UserDto { Id = Guid.NewGuid(), Name = "valid", Email = "valid@valid.com" };
            mockFetchService.Setup(service => service.FetchUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(userDto);

            var controller = new UsersController(mockRegistrationService.Object, mockFetchService.Object);

            // Act
            var result = await controller.FetchUserByEmailAsync(new UserFetchRequest
            {
                Email = "valid@valid.com"
            });

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task FetchUserByEmailAsync_InvalidEmail()
        {
            // Arrange
            var mockRegistrationService = new Mock<IUserRegistrationService>();
            var mockFetchService = new Mock<IUserFetchService>();
            UserDto userDto = null;
            mockFetchService.Setup(service => service.FetchUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(userDto);

            var controller = new UsersController(mockRegistrationService.Object, mockFetchService.Object);

            // Act
            var result = await controller.FetchUserByEmailAsync(new UserFetchRequest
            {
                Email = "invalid@invalid.com"
            });

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
