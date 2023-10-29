using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using Microsoft.EntityFrameworkCore;

namespace UserService.Repository;
public class UserContext: DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) {}
    public DbSet<User> Users {get; set;}
}