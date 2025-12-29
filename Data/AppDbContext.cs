using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PandaList.Models;

namespace PandaList.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }
        
        public DbSet<Models.Film> Films { get; set; }
        public DbSet<Models.Series> Series { get; set; }
        public DbSet<Models.Book> Books { get; set; }   

    }
}
