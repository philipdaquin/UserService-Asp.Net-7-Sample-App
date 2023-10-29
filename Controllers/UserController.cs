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
        return await service.FindOne(id);
    }
    [HttpGet("/users")]
    public async Task<ActionResult<List<User>>> GetAllUsers() {
        return await service.FindAll();
    }

    [HttpPost("/users")]
    public async Task<ActionResult<User>> SaveUser(User user) {
        return await service.Save(user);
    }
    [HttpPut("/users/{id}")]
    public async Task<ActionResult<User?>> PartialUpdateUser(User newUser, int id) {
        // if (await repository.ExistsById(newUser.Id)) return BadRequest("Missing Entity");

        return await service.PartialUpdate(newUser);
    }
    [HttpDelete("/users/{id}")]
    public async Task<ActionResult<Boolean>> DeleteUser(int Id) {
        return await service.DeleteOne(Id);
    }
}