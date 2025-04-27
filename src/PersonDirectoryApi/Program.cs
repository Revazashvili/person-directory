using System.Globalization;
using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonDirectoryApi;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence;
using PersonDirectoryApi.Persistence.Repositories;
using PersonDirectoryApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IStringLocalizer, DbStringLocalizer>();
builder.Services.AddSingleton<IMultimediaService, MultimediaService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IPersonService, PersonService>();

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

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapOpenApi();

app.Use((context, next) =>
{
    var language = context.Request.Headers.AcceptLanguage.ToString();
    if (!string.IsNullOrEmpty(language))
    {
        var cultures = language.Split(',').Select(lang => lang.Split(';')[0]);
        var firstCulture = cultures.FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(firstCulture))
        {
            var culture = new CultureInfo(firstCulture);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }

    return next();
});

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

// must not be used in production environment
try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<PersonContext>();

    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    context.Database.Migrate();
}
catch {}

app.Run();
