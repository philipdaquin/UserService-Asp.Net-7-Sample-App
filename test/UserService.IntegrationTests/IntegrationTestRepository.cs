using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.PostgreSql;
using UserService.Repository;
using UserService.Service;

namespace UserService.IntegrationTests;

public class IntegrationTestRepository<TProgram, TDBContext>: WebApplicationFactory<Program>, IAsyncLifetime
    where TProgram: class where TDBContext: DbContext
{   
    private readonly PostgreSqlContainer postgres;

    public IntegrationTestRepository() { 
        postgres = PostgresContainerFixture.getDefaultContainer();
    }
    public new async Task DisposeAsync()
    {
        await postgres.DisposeAsync();
    }
    public async Task InitializeAsync()
    {
        await postgres.StartAsync();
    }
    public string GetConnectionString() { 
        return postgres.GetConnectionString();
    }

    // Gives a fixture an opportunity to configure the application before it gets built.
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {   

            // Remove AppDbContext
            services.RemoveDbContext<UserContext>();
            
            // Add DB context pointing to test container
            services.AddDbContext<UserContext>(options => { options.UseNpgsql(postgres.GetConnectionString()); });
            
            // Ensure schema gets created
            services.EnsureDbCreated<UserContext>();
            
            // Add Service
            services.AddScoped<IUserService, UserService.Service.UserService>();
        });
    }

}