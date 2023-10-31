using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using UserService.Repository;
using Xunit;

/*
    Integration Test with External Client 
*/
namespace UserService.IntegrationTests.Controllers
{
    public class UserControllerTests : BaseIntegrationTest
    {
        public UserControllerTests(IntegrationTestRepository<Program, UserContext> _factory) : base(_factory)
        {
        }

        [Fact]
        public async Task GetOne_OnSuccess_ReturnStatusCode200()
        {
            // Given
            var response = await webClient.GetAsync("/api/test");
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        }
        
     
    }
}