using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Repository;
using UserService.Repository.EfCore;
using UserService.Service;


[ApiController]
[Route("api")]
public class UserController : ControllerBase
{

    private readonly IUserService service;
    // private UserService.Service.UserService service;
    // private EfCoreUserRepository repository;
    private static ILogger<UserController> logger;

    public UserController(
        // EfCoreUserRepository repository, 
        IUserService service 
        ) { 
        this.service = service;
        // this.logger = logger;
        // this.repository = repository;
    }


    // GET /api/user/test
    [HttpGet("test")]
    public string Test() { 
        return "Hello World!";
    }

    [HttpGet("/users/{id}")]
    public async Task<IActionResult> GetUser(int id) {
        // logger.LogInformation("Received GetUser request");
        var user = await service.FindOne(id);

        return Ok(user);
    }
    [HttpGet("/users")]
    public async Task<IActionResult> GetAllUsers() {
        logger.LogInformation("Received GetAllUsers request");

        var users = await service.FindAll();

        if (!users.Any()) return NotFound(); 


        return Ok(users);
    }

    [HttpPost("/users")]
    public async Task<IActionResult> SaveUser(User user) {
        logger.LogInformation("Saving User to the Database");

        var newUser = await service.Save(user);
        
        return Ok(newUser);
    }
    [HttpPut("/users/{id}")]
    public async Task<IActionResult> PartialUpdateUser(User newUser, int id) {

        // logger.LogInformation("Received a PartialUpdate");

        // Check if the user exists in the Database         
        // if (await service.FindOne(id) == null) return NotFound("Missing Entity");

        // Check if the ID is the same
        if (newUser.Id != id) return BadRequest("Given Ids Do Not Match");

        var updated = await service.PartialUpdate(newUser, id);
        return Ok(updated);
    }
    [HttpDelete("/users/{id}")]
    public async Task<IActionResult> DeleteUser(int Id) {
        // logger.LogInformation("Received Delete Request to the User");
        if (await service.FindOne(Id) == null) return BadRequest("Given Ids Do Not Match");

        var isDeleted  = await service.DeleteOne(Id);

        return Ok(isDeleted);
    }
}