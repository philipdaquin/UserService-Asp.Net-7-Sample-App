using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;

namespace UserService.Repository.EfCore
{
    public class EfCoreUserRepository : EfCoreRepository<int, User, UserContext>
    {
        public EfCoreUserRepository(UserContext context) : base(context)
        {
        }
    }
}