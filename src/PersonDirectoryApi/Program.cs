using Microsoft.EntityFrameworkCore;
using PersonDirectoryApi.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString(nameof(PersonContext));
builder.Services.AddDbContext<PersonContext>(builder =>
{
    builder.UseNpgsql(connectionString, optionsBuilder =>
    {
        optionsBuilder.MigrationsAssembly(typeof(PersonContext).Assembly.FullName);
    });
    builder.UseSnakeCaseNamingConvention();
});

var app = builder.Build();

app.MapOpenApi();

app.Run();
