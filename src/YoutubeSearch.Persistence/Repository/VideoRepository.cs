using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YoutubeSearch.Persistence;
using YoutubeSearch.Domain;

namespace YoutubeSearch.Persistence
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext dbContext;

        public VideoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task AddRangeAsync(IEnumerable<Video> videos)
        {
            var ids = videos.Select(v => v.Id);
            var existingVideos = dbContext.Videos.Where(v => ids.Contains(v.Id)).Select(v => v.Id);
            dbContext.Videos.AddRange(videos.Where(v => !existingVideos.Contains(v.Id)));
            return dbContext.SaveChangesAsync();
        }

        public IEnumerable<Video> SearchAsync(string searchTerm)
        {
            return dbContext.Videos.Where(v => EF.Functions.Like(v.Title, $"%{searchTerm}%"));
        }
    }
}