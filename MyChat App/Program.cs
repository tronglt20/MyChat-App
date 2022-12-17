using MediatR;
using MyChat_App.Extensions;
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
services.AddSwaggerGen();

services
    .AddDatabase(configuration)
    .AddServices()
    .AddRepositoriesBase()
    .AddUnitOfWork();

services.AddCurrentUserInfo();
services.AddEmailSender();
services.AddTokenGenerator();

// Config MediatR
services.AddMediatR(Assembly.GetExecutingAssembly());

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
