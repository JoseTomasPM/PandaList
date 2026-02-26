using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PandaList.Data;
using DotNetEnv;
using PandaList.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

Env.Load();
Console.WriteLine("ENV loaded!");

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString =
    builder.Configuration.GetConnectionString("AppDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

// DbContexts
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<DataProtectionKeyContext>(options =>
    options.UseNpgsql(connectionString));

// IDENTITY 
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Razor + Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Auth for Blazor
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

// Data Protection
builder.Services
    .AddDataProtection()
    .PersistKeysToDbContext<DataProtectionKeyContext>()
    .SetApplicationName("PandaList");

// Cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

// Logout 15min

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    options.SlidingExpiration = true;
});


var app = builder.Build();

// DB test
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.OpenConnection();
    db.Database.CloseConnection();
    Console.WriteLine("😸 Connected to DB");
}

// Pipeline
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

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
