using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

namespace PandaList.Data;

public class DataProtectionKeyContext : DbContext, IDataProtectionKeyContext
{
    public DataProtectionKeyContext(DbContextOptions<DataProtectionKeyContext> options)
        : base(options) { }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = default!;
}
