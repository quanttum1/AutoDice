using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using AutoDice.Interfaces;
using AutoDice.Repositories;
using AutoDice.Models;
using AutoDice.Services;

// TODO: Create a separate class out of this
void ConfigureDirectories(WebApplicationBuilder builder)
{
    string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    string directoryPath = Path.Combine(homeDirectory, "AutoDice");

    if (!Directory.Exists(directoryPath))
    {
        Directory.CreateDirectory(directoryPath);
    }

    string databasePath = Path.Combine(directoryPath, "database.db");

    builder.Services.AddDbContext<AutoDiceDbContext>(options =>
        options.UseSqlite($"Data Source={databasePath}"));
}

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddMemoryCache(); // For AuthCodeService
    builder.Services.AddSingleton<IAuthCodeService<Player>, AuthCodeService>();

    builder.Services.AddTransient<IRepository<TgToken>, TgTokenRepository>();
    builder.Services.AddTransient<IRepository<TgUser>, TgUserRepository>();
    builder.Services.AddTransient<IRepository<Player>, PlayerRepository>();

    builder.Services.AddSingleton<ITgBot, TgBotBackgroundService>();
    builder.Services.AddHostedService<TgBotBackgroundService>();
}

var builder = WebApplication.CreateBuilder(args);

ConfigureDirectories(builder);
ConfigureServices(builder);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AutoDiceDbContext>();
    db.Database.Migrate(); // Ensure database is created migrations are applied
    // TODO: Apply migrations only at the first run

    scope.ServiceProvider.GetRequiredService<ITgBot>().TryStart(); // Try to start tg bot at start
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
