using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using Microsoft.Extensions.Logging;
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
        public async Task ShouldInsert_NewUser_QueryOnSuccess()
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
        public async Task GetById_ShouldReturnProduct_WhenProductExist() { 
            
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
    }
}