using System.Globalization;
using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonDirectoryApi;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddSingleton<IStringLocalizer, StringLocalizer>();

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

app.Use((context, next) =>
{
    var language = context.Request.Headers.AcceptLanguage.ToString();

    var culture = CultureInfo.GetCultureInfo(language);
    
    CultureInfo.CurrentCulture = culture;
    CultureInfo.CurrentUICulture = culture;
    return next();
});

app.MapControllers();

app.Run();
