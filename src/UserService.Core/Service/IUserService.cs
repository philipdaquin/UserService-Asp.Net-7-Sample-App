using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;

namespace UserService.Service
{public interface IUserService
    {
        Task<User?> FindOne(int Id);
        Task<List<User>> FindAll();
        Task<User> Save(User user);
        Task<User?> PartialUpdate(User newUser, int userId);
        Task<bool> DeleteOne(int Id);
    }
}