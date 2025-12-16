using Microsoft.EntityFrameworkCore;
using UrlShorterer.Models;


namespace UrlShorterer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<UrlModel> Urls { get; set; }
    }
}
