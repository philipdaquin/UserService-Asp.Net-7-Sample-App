using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserService.Repository.EfCore;

namespace UserService.Repository;
public class UserRepository
{       
//     private readonly EfCoreUserRepository context;
//     private readonly ILogger<UserRepository> logger; 

//     public UserRepository(EfCoreUserRepository _context, ILogger<UserRepository> _logger) { 
//         context = _context;
//         logger = _logger;
//     }

//     /// <summary>
//     /// Get one User by Id
//     /// </summary>
//     /// <param name="Id">Id of the entity</param>
//     /// 
//     /// <returns>the entity: `User`</returns>
//     public async Task<User?> FindOne(int Id) {
//         return await context.FindOneById(Id);
//     }

//     /// <summary>
//     /// Returns all Users inside the entity
//     /// </summary>
//     /// <returns>the list of entities</returns>
//     public async Task<List<User>> FindAll() {
//         logger.LogInformation("Retrieving all entities ");

//         return await context.Set<User>().ToListAsync();
//     }

//     /// <summary>
//     /// Save a User
//     /// </summary>
//     /// <param name="user">entity to save</param>
//     /// <returns>persisted entity</returns>
//     public async Task<User> Save(User user) {
//         logger.LogInformation("Saving User {} into Database", user);

//         await context.Users.AddAsync(user);
//         await context.SaveChangesAsync();

//         return user;
//     }   

    
//     /// <summary>
//     /// Partially update a User
//     /// </summary>
//     /// <param name="newUser">the entity to update partially</param>
//     /// <returns>the persisted entity</returns>
//     public async Task<User?> PartialUpdate(User newUser) { 
//         logger.LogInformation("Request to partial update User: {}", newUser);

//         User? currentUser = await context.Users.FindAsync(newUser.Id);
//         // Check if the item exists on the database 
//         if (currentUser == null) return null;
//         if (newUser.Name != null)  {currentUser.Name = newUser.Name;}
//         if (newUser.Email != null) {currentUser.Email = newUser.Email;}

//         await context.SaveChangesAsync();
//         return currentUser;
//     }
    
//     /// <summary>
//     /// Deletes User Entity by Id
//     /// </summary>
//     /// <param name="Id">entity id</param>
//     /// <returns>boolean</returns>
//     public async Task<bool> Delete(int Id) {    
//         logger.LogInformation("Request to delete User");
//         User? user = await FindOne(Id);

//         if (user == null) return false;

//         context.Users.Remove(user);

//         await context.SaveChangesAsync();

//         return true;
//     }

}