using MediatR;
using Microsoft.OpenApi.Models;
using MyChat_App.Extensions;
using MyChat_App.Services;
using System.Reflection;
using Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var configuration = builder.Configuration;
var environment = builder.Environment;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddHttpContextAccessor();
services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        Scheme = "Bearer",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

services
    .AddDatabase(configuration)
    .AddServices()
    .AddRepositoriesBase()
    .AddUnitOfWork();

// Utilities
services.AddCurrentUserInfo()
    .AddEmailSender()
    .AddTokenGenerator();

// Config MediatR
services.AddMediatR(Assembly.GetExecutingAssembly());

// Add SignalR
services.AddSignalR();

// Add Authentication
services
    .AddJwt(configuration)
    .AddAuthentication(configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapHub<ChatHubService>("/ChatHubService");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
