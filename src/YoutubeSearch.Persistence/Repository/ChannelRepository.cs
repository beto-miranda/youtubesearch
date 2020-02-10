using YoutubeSearch.Persistence;
using YoutubeSearch.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YoutubeSearch.Persistence
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ChannelRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task AddRangeAsync(IEnumerable<Channel> channels)
        {
            var ids = channels.Select(v => v.Id);
            var existingChannels = dbContext.Channels.Where(v => ids.Contains(v.Id)).Select(v => v.Id);
            dbContext.Channels.AddRange(channels.Where(v => !existingChannels.Contains(v.Id)));
            return dbContext.SaveChangesAsync();
        }

        public IEnumerable<Channel> SearchAsync(string searchTerm)
        {
            return dbContext.Channels.Where(v => EF.Functions.Like(v.Title, $"%{searchTerm}%"));
        }
    }
}
