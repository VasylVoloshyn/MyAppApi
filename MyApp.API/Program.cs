using MyApp.Application;
using MyApp.Infrastructure;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection")!);

// Application
builder.Services.AddMediatR(typeof(MyApp.Application.AssemblyReference).Assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
