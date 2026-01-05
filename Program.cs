using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PandaList.Data;
using DotNetEnv;
using PandaList.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.DataProtection;


Env.Load();
Console.WriteLine("ENV loaded!");

// DB connection
var host = Environment.GetEnvironmentVariable("PGHOST");

var database = Environment.GetEnvironmentVariable("PGDATABASE");
var user = Environment.GetEnvironmentVariable("PGUSER");
var password = Environment.GetEnvironmentVariable("PGPASSWORD");

var connectionString = $"Host={host};Port=5432;Database={database};Username={user};Password={password};";

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Identity
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/access-denied";
});


// Razor / Blazor
builder.Services.AddRazorPages(options =>
{
    options.RootDirectory = "/Components/Pages";
});
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery();
builder.Services.AddAuthorizationCore();

//Data protection 
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/var/data/dataprotection"))
    .SetApplicationName("PandaList");

var app = builder.Build();

// Test DB connection
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.OpenConnection();
        dbContext.Database.CloseConnection();
        Console.WriteLine("😸 Connected to DB");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"😿 Error connecting DB: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");



app.Run();
