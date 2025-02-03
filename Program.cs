using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using AutoDice.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string directoryPath = Path.Combine(homeDirectory, "AutoDice");

if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

string databasePath = Path.Combine(directoryPath, "database.db");

builder.Services.AddDbContext<AutoDiceDbContext>(options =>
    options.UseSqlite($"Data Source={databasePath};Version=3;"));

var app = builder.Build();

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
