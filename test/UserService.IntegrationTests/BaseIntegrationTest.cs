using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UserService.Repository;
using UserService.Repository.EfCore;
using UserService.Service;

namespace UserService.IntegrationTests
{
    public abstract class BaseIntegrationTest: IClassFixture<IntegrationTestRepository<Program, UserContext>>
    {
        private readonly IServiceScope scope;
        protected readonly IUserService userService;

        protected readonly UserContext userContext;

        protected readonly HttpClient webClient;


        // protected EfCoreUserRepository repository; 

        protected BaseIntegrationTest(IntegrationTestRepository<Program, UserContext> _factory) { 
            scope = _factory.Services.CreateScope();

            userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            // repository = scope.ServiceProvider.GetRequiredService<EfCoreUserRepository>();

            userContext = scope.ServiceProvider.GetRequiredService<UserContext>();

            // Web API
            webClient = _factory.CreateClient();
        }   
    }
}