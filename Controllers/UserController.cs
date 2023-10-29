using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
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
    // private ILogger<UserController> logger;

    public UserController(
        // ILogger<UserController> logger,
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
    public async Task<ActionResult<User>> GetUser(int id) {
        var user = await service.FindOne(id);

        return Ok(user);
    }
    [HttpGet("/users")]
    public async Task<ActionResult<List<User>>> GetAllUsers() {
        var users = await service.FindAll();
        return Ok(users);
    }

    [HttpPost("/users")]
    public async Task<ActionResult<User>> SaveUser(User user) {
        var newUser = await service.Save(user);

        return Ok(newUser);
    }
    [HttpPut("/users/{id}")]
    public async Task<ActionResult<User?>> PartialUpdateUser(User newUser, int id) {
        // if (await repository.ExistsById(newUser.Id)) return BadRequest("Missing Entity");

        var updated = await service.PartialUpdate(newUser);
        return Ok(updated);
    }
    [HttpDelete("/users/{id}")]
    public async Task<ActionResult<Boolean>> DeleteUser(int Id) {
        var isDeleted  = await service.DeleteOne(Id);

        return Ok(isDeleted);
    }
}