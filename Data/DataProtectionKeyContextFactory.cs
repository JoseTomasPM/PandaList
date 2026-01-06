using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PandaList.Data;

public class DataProtectionKeyContextFactory
    : IDesignTimeDbContextFactory<DataProtectionKeyContext>
{
    public DataProtectionKeyContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString =
            config.GetConnectionString("DefaultConnection")
            ?? $"Host={System.Environment.GetEnvironmentVariable("PGHOST")};" +
               $"Port=5432;" +
               $"Database={System.Environment.GetEnvironmentVariable("PGDATABASE")};" +
               $"Username={System.Environment.GetEnvironmentVariable("PGUSER")};" +
               $"Password={System.Environment.GetEnvironmentVariable("PGPASSWORD")};";

        var options = new DbContextOptionsBuilder<DataProtectionKeyContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new DataProtectionKeyContext(options);
    }
}
