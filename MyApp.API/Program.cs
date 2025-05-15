using MediatR;
using MyApp.Infrastructure;
using MyApp.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var isTesting = builder.Environment.EnvironmentName == "Testing";

if (isTesting)
{
    builder.Services.AddInfrastructure(useInMemory: true);
}
else
{
    builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection")!);
}
// Application
builder.Services.AddMediatR(typeof(MyApp.Application.AssemblyReference).Assembly);

var app = builder.Build();

if (!isTesting)
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await AppDbContextSeed.SeedAsync(dbContext);
    }
}
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

public partial class Program { }
