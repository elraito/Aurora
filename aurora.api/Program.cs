using System.Data;
using Aurora.Api.Configuration;
using Aurora.Api.Middleware;
using Aurora.Api.Repositories;
using Aurora.Api.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbConnection>(_ =>
{
    var dbSettings = builder.Configuration.GetSection("DbSettings").Get<DbSettings>();
    if (dbSettings is null)
        throw new Exception("DbSettings not found");
    return new NpgsqlConnection($"Server={dbSettings.Server};Database={dbSettings.Database};User Id={dbSettings.UserId};Password={dbSettings.Password};");
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
