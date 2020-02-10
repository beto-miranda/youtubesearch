using Microsoft.EntityFrameworkCore;
using YoutubeSearch.Domain;

namespace YoutubeSearch.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Channel> Channels { get; set; }
    }
}
