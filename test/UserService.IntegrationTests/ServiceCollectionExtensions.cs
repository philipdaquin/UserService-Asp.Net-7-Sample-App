using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserService.Repository;

namespace UserService.IntegrationTests
{
    public static class ServiceCollectionExtensions
    {
        public static void RemoveDbContext<T>(this IServiceCollection services) where T: DbContext { 

            // Remove AppDbContext
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UserContext>));
            if (descriptor != null) services.Remove(descriptor);
        }
        public static void EnsureDbCreated<T>(this IServiceCollection services) where T: DbContext { 
            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<UserContext>();
            context.Database.EnsureCreated();
        }
    }
}