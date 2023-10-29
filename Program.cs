using Microsoft.EntityFrameworkCore;
using UserService.Repository;
using UserService.Repository.EfCore;
using UserService.Service;
using Domains

;
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddScoped<EfCoreUserRepository>();
services.AddScoped<IUserService, UserService.Service.UserService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var config = builder.Configuration;
string connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidProgramException("Missing Database Connection String!");

// Add services to the container.
builder.Services.AddDbContext<UserContext>(options=> {
    options.UseNpgsql(connectionString);

     }, ServiceLifetime.Transient);
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


