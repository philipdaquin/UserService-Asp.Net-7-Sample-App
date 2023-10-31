using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace UserService.IntegrationTests
{
    public class PostgresContainerFixture
    {   

        private static string IMAGE_NAME = "postgres:latest"; 

        public static PostgreSqlContainer getDefaultContainer() {
            return new PostgreSqlBuilder()
                    .WithImage(IMAGE_NAME)
                    .WithCleanUp(true)
                    .Build();
        }
    }
}