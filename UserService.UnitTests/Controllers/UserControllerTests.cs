using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using UserService.Service;
using Xunit;


namespace UserService.UnitTests.Controllers
{
    public class UserControllerTests
    {

        private readonly IUserService service;

        [Fact]
        public async Task TestUrl_OnSuccess_ReturnsStatus200Code()
        {
            // Arrange
            var controller = new UserController(service);
        
            // Act
            var result = controller.Test();
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var testResult = Assert.IsType<string>(okResult.Value);
            // Assert
            Assert.Equal("Hello World", testResult);
        }

        [Fact]
        public async Task GetUser_OnSuccess_ReturnsStatusCode200Async()
        {
            
            // Arrange 
            int userId = 1;
            
            User expectedUser = new User { 
                Id = userId,
                Name = "Test User",
                Email = "Test@Email.com"
            };

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.FindOne(userId)).ReturnsAsync(expectedUser);

            // Act 
            var controller =new UserController(mockUserService.Object);
            var result = await controller.GetUser(userId);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var userResult = Assert.IsType<User>(okResult.Value);

            Assert.Equal(userId, userResult.Id);
            Assert.Equal(expectedUser.Name, userResult.Name);
            Assert.Equal(expectedUser.Email, userResult.Email);
        }
       
        [Fact]
        public async Task GetAllUsers_GivenOnSuccess_ReturnslistOfUsers()
        {
            // Arrange 
            int userId1 = 1;
            int userId2 = 2;
     
            List<User> expectedLists = new List<User> {
                new User { 
                    Id = userId1,
                    Name = "Test User",
                    Email = "Test@Email.com"
                },
                new User { 
                    Id = userId2,
                    Name = "Test User2",
                    Email = "Test2@Email.com"
                }

            };
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.FindAll()).ReturnsAsync(expectedLists);

            // Act 
            var controller =new UserController(mockUserService.Object);
            var result = await controller.GetAllUsers();

            // Assert 

            var okResult = Assert.IsType<OkObjectResult>(result);
            var typeResult = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(expectedLists, typeResult);
            Assert.Equal(expectedLists.Count, typeResult.Count);
            
            Assert.Equal(expectedLists[0].Name, typeResult[0].Name);
            Assert.Equal(expectedLists[0].Id, typeResult[0].Id);
            Assert.Equal(expectedLists[0].Email, typeResult[0].Email);

            Assert.Equal(expectedLists[1].Name, typeResult[1].Name);
            Assert.Equal(expectedLists[1].Id, typeResult[1].Id);
            Assert.Equal(expectedLists[1].Email, typeResult[1].Email);

        }


        [Fact]
        public async Task GetAllUsers_GivenNoUsersFound_ReturnsStatusCode404()
        {
            // Arrange 
            int userId = 1;
            List<User> expectedLists = new List<User>();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.FindAll()).ReturnsAsync(expectedLists);

            // Act 
            var controller =new UserController(mockUserService.Object);
            var result = await controller.GetAllUsers();

            // Assert 
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task SaveUser_OnSuccess_ReturnsUser() { 
            // Arrange 
            int userId = 1;
            
            User expectedUser = new User {
                Id = userId, 
                Name = "Test User",
                Email = "Test@Email.com"
            };

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Save(expectedUser)).ReturnsAsync(expectedUser);
            // Act
            var controller = new UserController(mockUserService.Object);
            var result = await controller.SaveUser(expectedUser);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var userResult = Assert.IsType<User>(okResult.Value);

            Assert.Equal(expectedUser.Id, userResult.Id);
            Assert.Equal(expectedUser.Name,userResult.Name);
            Assert.Equal(expectedUser.Email, userResult.Email);

        }

        [Fact]
        public async Task PartialUpdateUser_GivenOnSuccess_ReturnsUpdatedUser() { 
             // Arrange 
            int userId = 1;
            int missingId = 2;
            
            User expectedUser = new User {
                Id = userId, 
                Name = "Test User",
                Email = "Test@Email.com"
            };

            User updatedUser = new User{ 
                Id = userId, 
                Name = "John Adams",
                Email = "John@Adams.com"
            };


            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.PartialUpdate(expectedUser, userId)).ReturnsAsync(updatedUser);
            // Act
            var controller = new UserController(mockUserService.Object);
            var result = await controller.PartialUpdateUser(expectedUser, userId);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var userResult = Assert.IsType<User>(okResult.Value);

            Assert.Equal(updatedUser.Id, userResult.Id);
            Assert.Equal(updatedUser.Name,userResult.Name);
            Assert.Equal(updatedUser.Email, userResult.Email);

        }

        [Fact]
        public async Task PartialUpdateUser_GivenUserNotFound_ReturnsStatusCode404() {
            // Arrange 
            int userId = 1;
            int missingId = 2;
            User expectedUser = new User {
                Id = userId, 
                Name = "Test User",
                Email = "Test@Email.com"
            };

            User updatedUser = new User{ 
                Id = userId, 
                Name = "John Adams",
                Email = "John@Adams.com"
            };
            var mockUserService = new Mock<IUserService>();

            // mockUserService.Setup(service => service.FindOne(missingId)).ReturnsAsync(expectedUser);
            mockUserService.Setup(service => service.PartialUpdate(expectedUser, userId)).ReturnsAsync(updatedUser);

            // Act
            var controller = new UserController(mockUserService.Object);

            var result = await controller.PartialUpdateUser(expectedUser, missingId);

            // Assert 
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task DeleteUser_GivenOnSuccess_ReturnsTrue() {
            // Arrange 
            int userId = 1;
            User expectedUser = new User {
                Id = userId, 
                Name = "Test User",
                Email = "Test@Email.com"
            };
        
            var mockUserService = new Mock<IUserService>();

            // mockUserService.Setup(service => service.FindOne(missingId)).ReturnsAsync(expectedUser);
            mockUserService.Setup(service => service.FindOne(userId)).ReturnsAsync(expectedUser);
            mockUserService.Setup(service => service.DeleteOne(userId)).ReturnsAsync(true);

            // Act
            var controller = new UserController(mockUserService.Object);

            var result = await controller.DeleteUser(userId);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var userResult = Assert.IsType<bool>(okResult.Value);

            Assert.True(userResult);
        }
        
        [Fact]
        public async Task DeleteUser_GivenUserNotFound_ReturnsStatusCode404() {
            // Arrange 
            int userId = 1;
            // User expectedUser = new User {
            //     Id = userId, 
            //     Name = "Test User",
            //     Email = "Test@Email.com"
            // };
            var mockUserService = new Mock<IUserService>();
            // mockUserService.Setup(service => service.FindOne(userId)).ReturnsAsync(expectedUser);
            mockUserService.Setup(service => service.DeleteOne(userId)).ReturnsAsync(true);
            // Act
            var controller = new UserController(mockUserService.Object);
            var result = await controller.DeleteUser(userId);
            // Assert 
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}