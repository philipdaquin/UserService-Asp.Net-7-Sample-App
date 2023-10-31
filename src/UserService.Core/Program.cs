using Microsoft.EntityFrameworkCore;
using UserService.Repository;
using UserService.Repository.EfCore;
using UserService.Service;
using Domains;

using System.Text.Json.Serialization;





var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// ConfigureServices(services);
    services.AddScoped<EfCoreUserRepository>();
    services.AddScoped<IUserService, UserService.Service.UserService>();

builder.Services.AddControllers()
    .AddJsonOptions(policy => { 
        policy.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        policy.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var config = builder.Configuration;
string connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidProgramException("Missing Database Connection String!");

// Add services to the container.
builder.Services.AddDbContext<UserContext>(options=> {
    options.UseNpgsql(connectionString).UseUpperSnakeCaseNamingConvention();
    // options.UseSqlServer(connectionString).UseUpperSnakeCaseNamingConvention();
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
app.UseCors(
    policy => policy    
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
);
app.UseAuthorization();

app.MapControllers();

app.Run();




void ConfigureServices(IServiceCollection services) { 
    services.AddScoped<EfCoreUserRepository>();
    services.AddScoped<IUserService, UserService.Service.UserService>();
}


public partial class Program {}