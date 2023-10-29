using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using UserService.Repository.EfCore;

namespace UserService.Service;
public class UserService
{
    private readonly EfCoreUserRepository repository;
    private readonly ILogger<UserService> logger;

    public UserService(EfCoreUserRepository repository, ILogger<UserService> _logger) { 
        repository = repository;
        logger = _logger;
    }

    /// <summary>
    /// Get one User by Id
    /// </summary>
    /// <param name="Id">Id of the entity</param>
    /// 
    /// <returns>the entity: `User`</returns>
    public async Task<User?> FindOne(int Id) {
        return await repository.FindOneById(Id);
    }

    /// <summary>
    /// Returns all Users inside the entity
    /// </summary>
    /// <returns>the list of entities</returns>
    public async Task<List<User>> FindAll() {
        logger.LogInformation("Retrieving all entities ");

        return await repository.FindAll();
    }

    /// <summary>
    /// Save a User
    /// </summary>
    /// <param name="user">entity to save</param>
    /// <returns>persisted entity</returns>
    public async Task<User> Save(User user) {
        logger.LogInformation("Saving User {} into Database", user);

        await repository.Save(user);

        return user;
    }   

    
    /// <summary>
    /// Partially update a User
    /// </summary>
    /// <param name="newUser">the entity to update partially</param>
    /// <returns>the persisted entity</returns>
    public async Task<User?> PartialUpdate(User newUser) { 
        logger.LogInformation("Request to partial update User: {}", newUser);

        User? currentUser = await repository.FindOneById(newUser.Id);
        // Check if the item exists on the database 
        if (currentUser == null) return null;
        if (newUser.Name != null)  {currentUser.Name = newUser.Name;}
        if (newUser.Email != null) {currentUser.Email = newUser.Email;}

        await repository.Save(currentUser);

        return currentUser;
    }
    
    /// <summary>
    /// Deletes User Entity by Id
    /// </summary>
    /// <param name="Id">entity id</param>
    /// <returns>boolean</returns>
    public async Task<bool> DeleteOne(int Id) {    
        logger.LogInformation("Request to delete User");

        return await repository.DeleteById(Id);
    }
   
}
