using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PandaList.Data;
using DotNetEnv;

Env.Load();
Console.WriteLine("ENV loaded!");

// DB connection
var host = Environment.GetEnvironmentVariable("PGHOST");
var port = Environment.GetEnvironmentVariable("PORT") ?? "5432";
var database = Environment.GetEnvironmentVariable("PGDATABASE");
var user = Environment.GetEnvironmentVariable("PGUSER");
var password = Environment.GetEnvironmentVariable("PGPASSWORD");

var connectionString = $"Host={host};Port={port};Database={database};Username={user};Password={password};";

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<AppDbContext>();

// Razor / Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery();

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
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
