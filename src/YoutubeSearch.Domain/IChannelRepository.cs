using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutubeSearch.Domain
{
    public interface IChannelRepository
    {
        Task AddRangeAsync(IEnumerable<Channel> videos);
        IEnumerable<Channel> SearchAsync(string searchTerm);
    }
}
