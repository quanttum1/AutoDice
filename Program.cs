using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using AutoDice.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache(); // For AuthCodeService
builder.Services.AddSingleton<AuthCodeService>();

string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string directoryPath = Path.Combine(homeDirectory, "AutoDice");

if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

string databasePath = Path.Combine(directoryPath, "database.db");

builder.Services.AddDbContext<AutoDiceDbContext>(options =>
    options.UseSqlite($"Data Source={databasePath}"));

var app = builder.Build();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AutoDiceDbContext>();
    db.Database.Migrate(); // Apply migrations automatically
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
