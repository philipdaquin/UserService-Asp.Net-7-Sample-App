using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using UserService.Repository;
using UserService.Repository.EfCore;
using Xunit;

/*
    Integration Test with Extern Database 
*/
namespace UserService.IntegrationTests.Repository
{
    public class PostgresIntegrationTests: BaseIntegrationTest
    {

        private static ILogger<PostgresIntegrationTests> logger;
        public PostgresIntegrationTests(IntegrationTestRepository<Program, UserContext> _factory) : base(_factory)
        {
            _factory.CreateClient();

        }

        [Fact]
        public async Task shouldInsert_NewUser_QueryOnSuccess()
        {
            User userA = new User { 
                Email = "userA@mail.com",
                Name = "userA"
            };

            User user = await userService.Save(userA);
            // User? userTest = await userService.FindOne(user.Id);

            User? test = await userContext.Users.FindAsync(user.Id);
            

            Assert.Equal(user.Name, test?.Name);
            Assert.Equal(user.Email, test?.Email);
            Assert.True(true);
        }



        [Fact]
        public async Task shouldGetById_ShouldReturnUser_WhenUserExist() { 
            
            User userA = new User { 
                Email = "userA@mail.com",
                Name = "userA"
            };
            User user = await userService.Save(userA);
            User? userTest = await userService.FindOne(user.Id);
            // User? test = await userContext.Users.FindAsync(user.Id);
            Assert.Equal(user.Name, userTest?.Name);
            Assert.Equal(user.Email, userTest?.Email);
            Assert.True(true);
        }

        [Fact]
        public void shouldPartiallyUpdate_GivenOnSuccess_ReturnsUpdatedUser() {
            User userA = new User { 
                Email = "userA@mail.com",
                Name = "userA"
            };
            User user = await userService.Save(userA);

            User updatedUser = new User { 
                Id = user.Id,
                Email = "userB@mail.com",
                Name = "userB"
            };
            User partialUpdateRes = await userService.PartialUpdate(updatedUser, user.Id);
            Assert.NotEqual(user.Name, updatedUser?.Name);
            Assert.NotEqual(user.Email, updatedUser?.Email);
            Assert.Equal(user.Id, updatedUser?.Id);
        }
        
        [Fact]
        public async void shouldDeleteUser_GivenUserExists_ReturnsTrue() {
            User userA = new User { 
                Email = "userA@mail.com",
                Name = "userA"
            };
            User user = await userService.Save(userA);

            Assert.NotNull(user);
            bool isDeleted = await userService.DeleteOne(userA.Id);
            Assert.True(isDeleted);
            Assert.Null(await userService.FindOne(user.Id));
        }
        
        [Fact]
        public async void shouldGetAllUsers_GivenDBisNotEmpty_ReturnsListOfUsers() {
            User userA = new User { 
                Email = "userA@mail.com",
                Name = "userA"
            };
            User savedA = await userService.Save(userA);

            User userB = new User { 
                Email = "userB@mail.com",
                Name = "userB"
            };
            User savedB = await userService.Save(userB);

            // 
            Assert.NotNull(savedA);
            Assert.NotNull(savedB);

            List<User> userList = await userService.FindAll();
            Assert.NotNull(userList);
            Assert.Equal(userList.Count, 2);
        }



    }
}