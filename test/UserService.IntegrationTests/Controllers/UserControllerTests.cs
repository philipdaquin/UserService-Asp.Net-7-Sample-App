using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using UserService.Repository;
using Xunit;
using Moq;
using Domains;
using UserService.Service;

/*
    Integration Test with External Client 
*/
namespace UserService.IntegrationTests.Controllers
{
    public class UserControllerTests : BaseIntegrationTest
    {
        public UserControllerTests(IntegrationTestRepository<Program, UserContext> _factory) : base(_factory) {}

        [Fact]
        public async Task GetOne_OnSuccess_ReturnStatusCode200()
        {

            string testUrl = "/api/test";

            var response = await webClient.GetAsync(testUrl);
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal(result, "Hello World!");
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        }

        [Fact]
        public async Task shouldGetUser_GivenUserDoesNotExists_ReturnsUser() {

            // Arrange 
            int userId = 1;
            string testUrl = $"api/users/1";
            
            User expectedUser = new User {
                Id = userId, 
                Name = "Test User",
                Email = "Test@Email.com"
            };

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.FindOne(userId))
                .ReturnsAsync(expectedUser);

            var response = await webClient.GetAsync(testUrl);
            // response.EnsureSuccessStatusCode();

            // var result = await response.ReadContentAsync<User>();

            // Assert.Equal(expectedUser.Name, result.Name);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    
        }

        [Fact]
        public void shouldGetAllUsers_GivenUsersDoesNotExist_ReturnsAListOfUsers() {}

        [Fact]
        public void shouldSaveUser_OnSuccess_ReturnsUser() {}

        [Fact]
        public void shouldPartiallyUpdateUser_GivenUserExists_ReturnsUpdatedUser() {}

        [Fact]
        public void shouldDeleteUser_GivenUserExists_ReturnsBoolean() {}
    }
}